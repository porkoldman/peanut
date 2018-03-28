using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CharacterMenuController : MonoBehaviour {

	// a pool to generate character slot game object.
	public SimpleObjectPool characterSlotPool;

	// play group game object, that contain all play slots.
	public Transform playCharacterSlotParent;

	// rest group game object, that contain all rest slots.
	public Transform restCharacterSlotParent;

	// the data load from cache file, use it to generate all slots on play or rest group.
	public CharacterMenuData characterMenuData;

	// record which deck is deisplayed now
	private int currentDeckIndex = 0;

	// mark play slot group as 0
	private int playSlotType = 0;

	// mark rest slot group as 1
	private int restSlotType = 1;

	// record which play slot is selected now
	private CharacterSlot currentSelectedPlayCharacterSlot;

	// record which rest slot is selected now
	private CharacterSlot currentSelectedRestCharacterSlot;

	// init when scene start.
	void Start() {
		LoadCharacterMenuData ();
		InitCharacterMenu ();
	}

	// change the deck to display on character menu.
	public void SwitchDeckData(int deckIndex) {
		if (deckIndex == currentDeckIndex) {
			return;
		}
		CleanDeckDataOnScene ();
		InitDeckData (deckIndex);
		currentDeckIndex = deckIndex;
	}

	// record current selected slot on play or rest group.
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

	// cancel the "select" status from current selected slot
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

	public CharacterSlot GetCurrentSelectedPlaySlot() {
		if (currentSelectedPlayCharacterSlot.IsSelected () == true) {
			return currentSelectedPlayCharacterSlot;
		}
		return null;
	}

	public CharacterSlot GetLastNonEmptyPlaySlot() {
		int index = playCharacterSlotParent.childCount - 1;
		for (int i = index; i >= 0; i-- ) {
			CharacterSlot slot = playCharacterSlotParent.GetChild (i).GetComponent<CharacterSlot> ();

			if (slot.GetCharacterSlotData().isEmpty != true) {
				return slot;
			}
		}
		return null;
	}

	// load charcter menu data from th cache file.
	private void LoadCharacterMenuData() {
		string filePath = Path.Combine (Application.streamingAssetsPath, "data.json");
		if (File.Exists (filePath)) {
			string dataAsJson = File.ReadAllText (filePath);
			characterMenuData = JsonUtility.FromJson<CharacterMenuData> (dataAsJson);
		} else {
			Debug.LogError ("Cannot load character menu data!");
		}
	}

	// save character menu data to cache file
	private void SaveCharacterMenuData() {
		string filePath = Path.Combine (Application.streamingAssetsPath, "data.json");
		if (File.Exists (filePath)) {
			string dataAsJson = JsonUtility.ToJson(characterMenuData);
			File.WriteAllText (filePath, dataAsJson);
		} else {
			Debug.LogError ("Cannot load character menu data!");
		}

	}

	// init character menu scene when start.
	private void InitCharacterMenu() {
		InitDeckData (0);
	}

	// init character menu scene with specific deck index.
	private void InitDeckData(int deckIndex) {
		InitPlaySlotData (deckIndex);
		InitRestSlotData (deckIndex);
	}

	// init play slot group with specific deck index. 
	private void InitPlaySlotData(int deckIndex) {
		CharacterDeckData deck = characterMenuData.allDeckData[deckIndex];
		CharacterSlotData[] playSlots = deck.allPlaySlotData;
		for (int i = 0; i < playSlots.Length; i++) {
			GameObject characterSlotObject = characterSlotPool.GetObject();
			characterSlotObject.transform.SetParent (playCharacterSlotParent);

			CharacterSlot characterSlot = characterSlotObject.GetComponent<CharacterSlot> ();
			characterSlot.SetCharacterSlotData (playSlots[i]);
			characterSlot.MarkAsPlayGroup ();

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

	// init rest slot group with specific deck index. 
	private void InitRestSlotData(int deckIndex) {
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
			characterSlot.MarkAsRestGroup ();

			characterSlot.FillSlot ();
			characterSlot.SetLvl (restSlots[i].lvl);
			characterSlot.SetWeight (restSlots[i].weight);
			characterSlot.SetExp (restSlots[i].exp);
			characterSlot.SetCharacterImage (restSlots[i].imagePath);
		}
	}

	// delete all slots from play and rest group, it just remove from the scene, not character menu data. 
	private void CleanDeckDataOnScene() {
		//playCharacterSlotParent.DetachChildren ();
		//restCharacterSlotParent.DetachChildren ();

		foreach (Transform child in playCharacterSlotParent) {
			GameObject.Destroy(child.gameObject);
		}
		foreach (Transform child in restCharacterSlotParent) {
			GameObject.Destroy(child.gameObject);
		}
	}

	// reload character menu data to display character menu scene on current deck
	private void RefreshCurrentDeckData() {
		CleanDeckDataOnScene ();
		InitDeckData (currentDeckIndex);
	}

	// check whether slots selected on rest and play group, if so, switch two selected slots.
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

	// exchange group with two selected slots.
	private void SwitchCurrentRestAndPlaySlot() {
		// process flow: 
		// step1: change and save the characterMenuData. 
		// step2: update scene by switching object from its parents. 

		// step1
		// get all play and rest slot data from characterMenuData
		CharacterSlotData[] playSlots = characterMenuData.allDeckData [currentDeckIndex].allPlaySlotData;
		CharacterSlotData[] restSlots = characterMenuData.allDeckData [currentDeckIndex].allRestSlotData;

		// get current selected slot data
		CharacterSlotData currentPlaySlotData = currentSelectedPlayCharacterSlot.GetCharacterSlotData ();
		CharacterSlotData currentRestSlotData = currentSelectedRestCharacterSlot.GetCharacterSlotData ();

		// get selected play slot's index in all play slot data.
		int keyPlayIndex = Array.FindIndex(playSlots, s => s.IsEqual(currentPlaySlotData));

		// get selected rest slot's index in all rest slot data.
		int keyRestIndex = Array.FindIndex(restSlots, s => s.IsEqual(currentRestSlotData));

		// use index to switch the data between play and rest 
		CharacterSlotData temp = playSlots[keyPlayIndex];
		playSlots[keyPlayIndex] = restSlots[keyRestIndex];
		restSlots [keyRestIndex] = temp;

		// remove the empty slot from rest slots
		RemoveEmptySlotFromRestSlots ();

		// save changed data to the cache file
		SaveCharacterMenuData ();

		// step2
		// get sibling index in parent
		int currentPlaySiblingIndex = currentSelectedPlayCharacterSlot.gameObject.transform.GetSiblingIndex ();
		int currentRestSiblingIndex = currentSelectedRestCharacterSlot.gameObject.transform.GetSiblingIndex ();

		// remove slot object from the parent.
		currentSelectedPlayCharacterSlot.gameObject.transform.SetParent(null);
		currentSelectedRestCharacterSlot.gameObject.transform.SetParent(null);

		// check play slot is empty or not.
		if (currentSelectedPlayCharacterSlot.GetCharacterSlotData ().isEmpty == false) {
			// set current play slot to rest group
			currentSelectedPlayCharacterSlot.gameObject.transform.SetParent (restCharacterSlotParent);
			currentSelectedPlayCharacterSlot.gameObject.transform.SetSiblingIndex (currentRestSiblingIndex);
		} else {
			// remove current play slot
			GameObject.Destroy (currentSelectedPlayCharacterSlot.gameObject);
			currentSelectedPlayCharacterSlot = null;
		}

		// set current rest slot to play group
		currentSelectedRestCharacterSlot.gameObject.transform.SetParent (playCharacterSlotParent);
		currentSelectedRestCharacterSlot.gameObject.transform.SetSiblingIndex (currentPlaySiblingIndex);

		// switch current selected object
		CharacterSlot tempSlot = currentSelectedPlayCharacterSlot;
		currentSelectedPlayCharacterSlot = currentSelectedRestCharacterSlot;
		currentSelectedRestCharacterSlot = tempSlot;

		// remark group to current slot
		if (currentSelectedPlayCharacterSlot != null) {
			currentSelectedPlayCharacterSlot.MarkAsPlayGroup ();
		}
		if (currentSelectedRestCharacterSlot != null) {
			currentSelectedRestCharacterSlot.MarkAsRestGroup ();
		}

		// cancel selected status to current slot
		CancelCurrentSelectedCharacterSlot (0);
		CancelCurrentSelectedCharacterSlot (1);

		//RefreshCurrentDeckData ();
	}

	// delete the empty slot from rest slot group.
	private void RemoveEmptySlotFromRestSlots() {
		CharacterSlotData[] restSlots = characterMenuData.allDeckData [currentDeckIndex].allRestSlotData;
		restSlots = Array.FindAll(restSlots, val => val.isEmpty == false);
		characterMenuData.allDeckData [currentDeckIndex].allRestSlotData = restSlots;

		// test comment.
		/**
		int[] a = { 1, 2, 3, 4, 5 };
		int[] b = a;

		a = Array.FindAll(b, val => val < 6);

		b [0] = 9;
		Debug.Log (a[0].ToString());
		*/
	}


}
