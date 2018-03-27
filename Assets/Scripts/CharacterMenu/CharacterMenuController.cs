using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CharacterMenuController : MonoBehaviour {

	public SimpleObjectPool characterSlotPool;
	public Transform playCharacterSlotParent;
	public Transform restCharacterSlotParent;

	public CharacterMenuData characterMenuData;

	private int currentDeckIndex = 0;

	private int playSlotType = 0;
	private int restSlotType = 1;

	private CharacterSlot currentSelectedPlayCharacterSlot;
	private CharacterSlot currentSelectedRestCharacterSlot;

	void Start() {
		LoadCharacterMenuData ();
		InitCharacterMenu ();
	}

	public void SwitchDeckData(int deckIndex) {
		if (deckIndex == currentDeckIndex) {
			return;
		}
		ClearDeckData ();
		InitDeckData (deckIndex);
		currentDeckIndex = deckIndex;
	}

	public void SetCurrentSelectedCharacterSlot(CharacterSlot obj, int slotTyep) {
		if (slotTyep == playSlotType) {
			currentSelectedPlayCharacterSlot = obj;
		} else if (slotTyep == restSlotType) {
			currentSelectedRestCharacterSlot = obj;
		}

		if (CheckSwitchSlot() == true) {
			SwitchCurrentRestAndPlaySlot ();
		}
	}

	public void CancelCurrentSelectedCharacterSlot(int slotTyep) {
		CharacterSlot targetCharacterSlot;
		if (slotTyep == playSlotType) {
			targetCharacterSlot = currentSelectedPlayCharacterSlot;
		} else if (slotTyep == restSlotType) {
			targetCharacterSlot = currentSelectedRestCharacterSlot;
		} else {
			return;
		}
		if (targetCharacterSlot == null) {
			return;
		}
		if (targetCharacterSlot.IsSelected () == true) {
			targetCharacterSlot.ClickTrigger ();
		}
	}

	private void LoadCharacterMenuData() {
		string filePath = Path.Combine (Application.streamingAssetsPath, "data.json");
		if (File.Exists (filePath)) {
			string dataAsJson = File.ReadAllText (filePath);
			characterMenuData = JsonUtility.FromJson<CharacterMenuData> (dataAsJson);
		} else {
			Debug.LogError ("Cannot load character menu data!");
		}
	}

	private void SaveCharacterMenuData() {
		string filePath = Path.Combine (Application.streamingAssetsPath, "data.json");
		if (File.Exists (filePath)) {
			string dataAsJson = JsonUtility.ToJson(characterMenuData);
			File.WriteAllText (filePath, dataAsJson);
		} else {
			Debug.LogError ("Cannot load character menu data!");
		}

	}

	private void InitCharacterMenu() {
		InitDeckData (0);
	}

	private void InitDeckData(int deckIndex) {
		InitPlaySlotData (deckIndex);
		InitRestSlotData (deckIndex);
	}

	private void InitPlaySlotData(int deckIndex) {
		CharacterDeckData deck = characterMenuData.allDeckData[deckIndex];
		CharacterSlotData[] playSlots = deck.allPlaySlotData;
		for (int i = 0; i < playSlots.Length; i++) {
			GameObject characterSlotObject = characterSlotPool.GetObject();
			characterSlotObject.transform.SetParent (playCharacterSlotParent);

			CharacterSlot characterSlot = characterSlotObject.GetComponent<CharacterSlot> ();
			characterSlot.SetCharacterSlotData (playSlots[i]);
			characterSlot.LetItOnPlay ();

			if (playSlots[i].isEmpty == true) {
				characterSlot.ClearSlot ();
				continue;
			}
			characterSlot.FillSlot ();
			characterSlot.SetLvl (playSlots[i].lvl);
			characterSlot.SetWeight (playSlots[i].weight);
			characterSlot.SetExp (playSlots[i].exp);
			characterSlot.SetCharacterImage (playSlots[i].imagePath);
		}
	}

	private void InitRestSlotData(int deckIndex) {
		RemoveEmptySlotFromRestSlots ();
		CharacterDeckData deck = characterMenuData.allDeckData[deckIndex];
		CharacterSlotData[] restSlots = deck.allRestSlotData;
		for (int i = 0; i < restSlots.Length; i++) {
			if (restSlots[i].isEmpty == true) {
				continue;
			}

			GameObject characterSlotObject = characterSlotPool.GetObject();
			characterSlotObject.transform.SetParent (restCharacterSlotParent);

			CharacterSlot characterSlot = characterSlotObject.GetComponent<CharacterSlot> ();
			characterSlot.SetCharacterSlotData (restSlots[i]);
			characterSlot.LetItOnRest ();

			characterSlot.FillSlot ();
			characterSlot.SetLvl (restSlots[i].lvl);
			characterSlot.SetWeight (restSlots[i].weight);
			characterSlot.SetExp (restSlots[i].exp);
			characterSlot.SetCharacterImage (restSlots[i].imagePath);
		}
	}

	private void ClearDeckData() {
		//playCharacterSlotParent.DetachChildren ();
		//restCharacterSlotParent.DetachChildren ();

		foreach (Transform child in playCharacterSlotParent.transform) {
			GameObject.Destroy(child.gameObject);
		}
		foreach (Transform child in restCharacterSlotParent.transform) {
			GameObject.Destroy(child.gameObject);
		}
	}

	private void RefreshCurrentDeckData() {
		ClearDeckData ();
		InitDeckData (currentDeckIndex);
	}

	private bool CheckSwitchSlot() {
		if (currentSelectedPlayCharacterSlot == null || currentSelectedRestCharacterSlot == null) {
			return false;
		}
		if (currentSelectedPlayCharacterSlot.IsSelected() == false || 
			currentSelectedRestCharacterSlot.IsSelected() == false) {
			return false;
		}
		return true;
	}

	private void SwitchCurrentRestAndPlaySlot() {
		CharacterSlotData[] playSlots = characterMenuData.allDeckData [currentDeckIndex].allPlaySlotData;
		CharacterSlotData[] restSlots = characterMenuData.allDeckData [currentDeckIndex].allRestSlotData;

		CharacterSlotData currentPlaySlotData = currentSelectedPlayCharacterSlot.GetCharacterSlotData ();
		CharacterSlotData currentRestSlotData = currentSelectedRestCharacterSlot.GetCharacterSlotData ();

		int keyPlayIndex = Array.FindIndex(playSlots, s => s.IsEqual(currentPlaySlotData));
		int keyRestIndex = Array.FindIndex(restSlots, s => s.IsEqual(currentRestSlotData));

		CharacterSlotData temp = playSlots[keyPlayIndex];
		playSlots[keyPlayIndex] = restSlots[keyRestIndex];
		restSlots [keyRestIndex] = temp;

		CancelCurrentSelectedCharacterSlot (0);
		CancelCurrentSelectedCharacterSlot (1);
		RefreshCurrentDeckData ();
		SaveCharacterMenuData ();
	}

	private void RemoveEmptySlotFromRestSlots() {
		CharacterSlotData[] restSlots = characterMenuData.allDeckData [currentDeckIndex].allRestSlotData;
		restSlots = Array.FindAll(restSlots, val => val.isEmpty == false);
		/**
		int[] a = { 1, 2, 3, 4, 5 };
		int[] b = a;

		a = Array.FindAll(b, val => val < 6);

		b [0] = 9;
		Debug.Log (a[0].ToString());
		*/
		characterMenuData.allDeckData [currentDeckIndex].allRestSlotData = restSlots;
	}
}
