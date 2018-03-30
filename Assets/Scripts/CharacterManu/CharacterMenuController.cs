using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;


public class CharacterMenuController : MonoBehaviour
{
	// a pool to generate character slot object dynamitic.
	public SimpleObjectPool characterSlotPool;

	// a grid layout panel to contain all play-slots
	public GameObject playCharacterSlotLayoutPanel;

	// a grid layout panel to contain all rest-slots
	public GameObject restCharacterSlotLayoutPanel;

	// all alert object. it will display when play slot is over weight
	public GameObject alertOverWeightGroup;

	// weight text
	public GameObject weightGroup;

	// record which deck is displayed.
	private int currentDeckIndex = 0;

	// record which play-slot is selected. -1 means no slot selected.
	private int selectedPlaySlotIndex = -1;

	//record which rest-slot is selected. -1 means no slot selected.
	private int selectedRestSlotIndex = -1;

	// the max deck in the game.
	private const int MaxDeckNumber = 5;

	// the max play-slot in one deck.
	private const int MaxPlaySlotNumber = 5;

	// the max weight number that can be use in battle.
	private const int MaxWeight = 15;

	// a list to record all play slots in all decks, with structure: list[deckIndex][playSlotIndex] 
	private List<List<GameObject>> allPlaySlotObjectInAllDeck;

	// a list to record all rest slots in all decks, with structure: list[deckIndex][restSlotIndex] 
	private List<List<GameObject>> allRestSlotObjectInAllDeck;

	// Use this for initialization
	void Start () {
		// display the default deck
		currentDeckIndex = 0;
		InitDataFromFile ();
		DisplayDeck (0);
	}

	// record selected play-slot's index , it will unselected other play-slot.
	public void SetSelectedPlaySlotIndex(int newSelectedSlotIndex) {
		if (newSelectedSlotIndex == selectedPlaySlotIndex) {
			return;
		}
		// record previous selected slot index, if previous index = -1. means no previous selected slot.
		int previousIndex = selectedPlaySlotIndex;
		selectedPlaySlotIndex = newSelectedSlotIndex;

		// if previous selected slot is exist.
		if (previousIndex != -1) {
			// unselected previous selected slot.
			GameObject previousSelectedSlotObject = allPlaySlotObjectInAllDeck [currentDeckIndex] [previousIndex];
			CharacterSlotDataController previousSlotCtl = previousSelectedSlotObject.GetComponent<CharacterSlotDataController> ();
			if (previousSlotCtl.isSelected == true) {
				previousSlotCtl.SetUnSelected ();
			}
		}

		// is has a slot be selected.
		if (selectedPlaySlotIndex != -1) {
			// check slot is selected, if not => select it.
			GameObject selectedSlotObject = allPlaySlotObjectInAllDeck [currentDeckIndex] [selectedPlaySlotIndex];
			CharacterSlotDataController selectedSlotCtl = selectedSlotObject.GetComponent<CharacterSlotDataController> ();
			if (selectedSlotCtl.isSelected == false) {
				selectedSlotCtl.SetSelected ();
			}
		}

		// if rest and play slot both selected, => switch them.
		if (selectedPlaySlotIndex != -1 && selectedRestSlotIndex != -1) {
			SwitchSelectedRestAndPlaySlot ();
		}
	}

	// record selected rest-slot's index , it will unselected other rest-slot.
	public void SetSelectedRestSlotIndex(int newSelectedSlotIndex) {
		if (newSelectedSlotIndex == selectedRestSlotIndex) {
			return;
		}
		// record previous selected slot index, if previous index = -1. means no previous selected slot.
		int previousIndex = selectedRestSlotIndex;
		selectedRestSlotIndex = newSelectedSlotIndex;

		// if previous selected slot is exist.
		if (previousIndex != -1) {
			// unselected previous selected slot.
			GameObject previousSelectedSlotObject = allRestSlotObjectInAllDeck [currentDeckIndex] [previousIndex];
			CharacterSlotDataController previousSlotCtl = previousSelectedSlotObject.GetComponent<CharacterSlotDataController> ();
			if (previousSlotCtl.isSelected == true) {
				previousSlotCtl.SetUnSelected ();
			}
		}

		// is has a slot be selected.
		if (selectedRestSlotIndex != -1) {
			// check slot is selected, if not => select it.
			GameObject selectedSlotObject = allRestSlotObjectInAllDeck [currentDeckIndex] [selectedRestSlotIndex];
			CharacterSlotDataController selectedSlotCtl = selectedSlotObject.GetComponent<CharacterSlotDataController> ();
			if (selectedSlotCtl.isSelected == false) {
				selectedSlotCtl.SetSelected ();
			}
		}

		// if rest and play slot both selected, => switch them.
		if (selectedPlaySlotIndex != -1 && selectedRestSlotIndex != -1) {
			SwitchSelectedRestAndPlaySlot ();
		}
	}

	// retrun object's index in play group's list, if not found return -1;
	public int CheckPlaySlotObjectIndex(GameObject playSlot) {
		int result = allPlaySlotObjectInAllDeck [currentDeckIndex].IndexOf(playSlot);
		return result;
	}

	// retrun object's rest in play group's list, if not found return -1;
	public int CheckRestSlotObjectIndex(GameObject restSlot) {
		int result = allRestSlotObjectInAllDeck [currentDeckIndex].IndexOf(restSlot);
		return result;
	}

	// change other deck's data to display
	public void SwitchDeck(int deckIndex) {
		SetSelectedPlaySlotIndex (-1);
		SetSelectedRestSlotIndex (-1);

		ClearAllSlotFromTheScene ();
		currentDeckIndex = deckIndex;
		DisplayDeck (deckIndex);
	}

	// switch selected rest and play slot
	public void SwitchSelectedRestAndPlaySlot() {
		if (selectedPlaySlotIndex == -1 || selectedRestSlotIndex == -1) {
			return;
		}

		// record slot's index, and set both slot not selected
		int playSlotIndex = selectedPlaySlotIndex;
		int restSlotIndex = selectedRestSlotIndex;
		SetSelectedPlaySlotIndex (-1);
		SetSelectedRestSlotIndex (-1);

		// get the slot object from list
		List<GameObject> playListInDeck = allPlaySlotObjectInAllDeck [currentDeckIndex];
		List<GameObject> restListInDeck = allRestSlotObjectInAllDeck [currentDeckIndex];
		GameObject playSlot = playListInDeck [playSlotIndex];
		GameObject restSlot = restListInDeck [restSlotIndex];

		// remove th slot from list
		playListInDeck.RemoveAt (playSlotIndex);
		restListInDeck.RemoveAt (restSlotIndex);

		// check whether play slot is empty
		bool isPlaySlotEmplty = playSlot.GetComponent<CharacterSlotDataController> ().characterSlotData.isEmpty;

		// insert rests slot to play list
		playListInDeck.Insert (playSlotIndex, restSlot);

		// if play slot is not empty => insert it to rest list.
		if (isPlaySlotEmplty != true) {
			restListInDeck.Insert (restSlotIndex, playSlot);
		}

		// get sibling index in layout panel.
		int playSiblingIndex = playSlot.transform.GetSiblingIndex ();
		int restSiblingIndex = restSlot.transform.GetSiblingIndex ();

		// set rest slot to play layout panel with specific sibling index.
		restSlot.transform.SetParent (playCharacterSlotLayoutPanel.transform);
		restSlot.transform.SetSiblingIndex (playSiblingIndex);

		// if play slot is not empty => set it to rest layout panel with specific sibling index.
		if (isPlaySlotEmplty != true) {
			playSlot.transform.SetParent (restCharacterSlotLayoutPanel.transform);
			playSlot.transform.SetSiblingIndex (restSiblingIndex);
		}

		// if play slot is empty => destroy it from the game.
		if (isPlaySlotEmplty == true) {
			GameObject.Destroy (playSlot);
		}

		CheckOverWeight ();

		// save all changed to cache file.
		SaveAllDeckDataToFile ();
	}

	// clear a filled play slot from play group, move it to rest group, and add an empty slot to play group.
	public void ClearFilledPlaySlot() {
		// find which slot, we need to move to rest group
		// if there is a non empty selected play slot => move it
		// if not, => mvoe the last non empty play slot.
		int targetIndex = -1;
		if (selectedPlaySlotIndex != -1) {
			targetIndex = selectedPlaySlotIndex;
		} else {
			targetIndex = GetLastNonEmptyPlaySlotIndex ();
		}
		if (targetIndex == -1) {
			return;
		}

		// init slot's list
		List<GameObject> playListInDeck = allPlaySlotObjectInAllDeck [currentDeckIndex];
		List<GameObject> restListInDeck = allRestSlotObjectInAllDeck [currentDeckIndex];
		GameObject playSlot = playListInDeck [targetIndex];

		// check whether the target slot is empty
		bool isEmpty = playSlot.GetComponent<CharacterSlotDataController> ().characterSlotData.isEmpty;
		if (isEmpty == true) {
			return;
		}

		// unselect the target slot
		SetSelectedPlaySlotIndex (-1);
		SetSelectedRestSlotIndex (-1);

		// create a new empty slot, 
		GameObject newEmptySlotObject = CreateNewEmptySlot();
		// get the target slot object from list
		GameObject targetSlot = playListInDeck [targetIndex];

		// remove target slot from play list
		playListInDeck.RemoveAt (targetIndex);

		// add empty slot to the play list
		playListInDeck.Insert (targetIndex, newEmptySlotObject);

		// add target slot to the rest list
		restListInDeck.Insert (0, targetSlot);

		// get target slot's sibling index in grid layout panel
		int targetSiblingIndex = targetSlot.transform.GetSiblingIndex ();

		// change target slot's parent to rest grid layout panel
		targetSlot.transform.SetParent (restCharacterSlotLayoutPanel.transform);
		targetSlot.transform.SetAsFirstSibling ();

		// set empty slot;s parent to play grid layout panel
		newEmptySlotObject.transform.SetParent (playCharacterSlotLayoutPanel.transform);
		newEmptySlotObject.transform.SetSiblingIndex (targetSiblingIndex);

		CheckOverWeight ();

		// save all changed to cache file.
		SaveAllDeckDataToFile ();
	}

	// whether play slot weight is over max limit. if yes => show alert.  if not => close alert
	private void CheckOverWeight() {
		int totalWeight = CaculateTotalWeight ();
		SetWeightValueText (totalWeight.ToString());
		if (totalWeight > MaxWeight) {
			alertOverWeightGroup.SetActive (true);
			SetWeightValueTextColor (Color.red);
		} else {
			alertOverWeightGroup.SetActive (false);
			SetWeightValueTextColor (Color.black);
		}
	}

	// set the weight value
	private void SetWeightValueText(string value) {
		int totalWeight = CaculateTotalWeight ();
		GameObject weightValueObject = weightGroup.transform.Find ("WeightValue").gameObject;
		weightValueObject.GetComponent<Text> ().text = value;
	}

	// set the weight text color
	private void SetWeightValueTextColor(Color pColor) {
		GameObject weightValueObject = weightGroup.transform.Find ("WeightValue").gameObject;
		GameObject maxWeightValueObject = weightGroup.transform.Find ("MaxWeightValue").gameObject;
		weightValueObject.GetComponent<Text> ().color = pColor;
		maxWeightValueObject.GetComponent<Text> ().color = pColor;
	}

	// caculate all play lost's weight in current deck
	private int CaculateTotalWeight() {
		List<GameObject> playList = allPlaySlotObjectInAllDeck [currentDeckIndex];

		int totalWeight = 0;

		for (int i = 0; i < MaxPlaySlotNumber; i++) {
			CharacterSlotDataController playSlotObject = playList [i].GetComponent<CharacterSlotDataController> ();
			if (playSlotObject.GetIsEmptyData() == true) {
				continue;
			}
			int weight = playSlotObject.GetWeightData ();
			if (weight < 0) {
				continue;
			}
			totalWeight += weight;
		}

		return totalWeight;
	}

	// get the last non empty slot's index on play group, if not found => reutrn -1
	private int GetLastNonEmptyPlaySlotIndex() {
		List<GameObject> slotList = allPlaySlotObjectInAllDeck [currentDeckIndex];
		for (int i = MaxPlaySlotNumber - 1; i >= 0; i--) {
			bool isEmpty = slotList [i].GetComponent<CharacterSlotDataController> ().characterSlotData.isEmpty;
			if (isEmpty == false) {
				return i;
			}
		}
		return -1;
	}

	// remove all slot object from grid layout panel.
	private void ClearAllSlotFromTheScene () {
		playCharacterSlotLayoutPanel.transform.DetachChildren ();
		restCharacterSlotLayoutPanel.transform.DetachChildren ();
	}

	// display the specific deck data to the character menu scene 
	private void DisplayDeck(int deckIndex) {
		List<GameObject> playSlotsObject = allPlaySlotObjectInAllDeck [deckIndex];
		List<GameObject> restSlotsObject = allRestSlotObjectInAllDeck [deckIndex];

		for (int i = 0; i < MaxDeckNumber; i++) {
			CharacterSlotDataController playSlotCtl = playSlotsObject [i].GetComponent<CharacterSlotDataController> ();
			playSlotCtl.DisplayWithCharacterSlotData ();

			playSlotsObject [i].transform.SetParent (playCharacterSlotLayoutPanel.transform);
		}
		for (int i = 0; i < restSlotsObject.Count; i++) {
			CharacterSlotDataController restSlotCtl = restSlotsObject [i].GetComponent<CharacterSlotDataController> ();
			restSlotCtl.DisplayWithCharacterSlotData ();
			restSlotsObject [i].transform.SetParent (restCharacterSlotLayoutPanel.transform);
		}
		CheckOverWeight ();
	}

	// create and return an empty slot object
	private GameObject CreateNewEmptySlot() {
		GameObject newSlotObject = characterSlotPool.GetObject ();
		CharacterSlotDataController ctl = newSlotObject.GetComponent<CharacterSlotDataController> ();
		CharacterSlotData newDataInObject = ctl.characterSlotData;
		newDataInObject.isEmpty = true;
		newDataInObject.characterId = -1;
		newDataInObject.lvl = -1;
		newDataInObject.exp = -1;
		newDataInObject.weight = -1;
		newDataInObject.imagePath = "";
		return newSlotObject;
	}

	// load the all slot data from cache file.
	private void InitDataFromFile() {
		CharacterMenuData serializableData = LoadAllDeckDataFromFile ();
		allPlaySlotObjectInAllDeck = new List<List<GameObject>> ();
		allRestSlotObjectInAllDeck = new List<List<GameObject>> ();

		for (int i = 0; i < MaxDeckNumber; i++) {
			CharacterDeckData deckData = serializableData.allDeckData [i];
			CharacterSlotData[] allPlaySlotDataInOneDeck = deckData.allPlaySlotData;
			CharacterSlotData[] allRestSlotDataInOneDeck = deckData.allRestSlotData;

			//Debug.Log (allPlaySlotDataInOneDeck [0].ToString());

			List<GameObject> allPlayObjects = new List<GameObject> ();
			List<GameObject> allRestObjects = new List<GameObject> ();

			for (int j = 0; j < MaxPlaySlotNumber; j++) {
				GameObject newSlotObject = characterSlotPool.GetObject ();
				CharacterSlotDataController ctl = newSlotObject.GetComponent<CharacterSlotDataController> ();
				CharacterSlotData newDataInObject = ctl.characterSlotData;
				newDataInObject.isEmpty = allPlaySlotDataInOneDeck [j].isEmpty;
				newDataInObject.characterId = allPlaySlotDataInOneDeck [j].characterId;
				newDataInObject.lvl = allPlaySlotDataInOneDeck [j].lvl;
				newDataInObject.exp = allPlaySlotDataInOneDeck [j].exp;
				newDataInObject.weight = allPlaySlotDataInOneDeck [j].weight;
				newDataInObject.imagePath = allPlaySlotDataInOneDeck [j].imagePath;
				allPlayObjects.Add (newSlotObject);
			}

			for (int j = 0; j < allRestSlotDataInOneDeck.Length; j++) {
				GameObject newSlotObject = characterSlotPool.GetObject ();
				CharacterSlotDataController ctl = newSlotObject.GetComponent<CharacterSlotDataController> ();
				CharacterSlotData newDataInObject = ctl.characterSlotData;
				newDataInObject.isEmpty = allRestSlotDataInOneDeck [j].isEmpty;
				newDataInObject.characterId = allRestSlotDataInOneDeck [j].characterId;
				newDataInObject.lvl = allRestSlotDataInOneDeck [j].lvl;
				newDataInObject.exp = allRestSlotDataInOneDeck [j].exp;
				newDataInObject.weight = allRestSlotDataInOneDeck [j].weight;
				newDataInObject.imagePath = allRestSlotDataInOneDeck [j].imagePath;
				allRestObjects.Add (newSlotObject);
			}

			allPlaySlotObjectInAllDeck.Add (allPlayObjects);
			allRestSlotObjectInAllDeck.Add (allRestObjects);
		}
	}

	// make all slot object to format with serializable data
	private CharacterMenuData AllSlotObjectDataToSerializableData() {
		CharacterMenuData result = new CharacterMenuData();
		result.allDeckData = new CharacterDeckData[MaxDeckNumber];
		for (int i = 0; i < MaxDeckNumber; i++) {
			List<GameObject> allPlaySlotInOneDeck = allPlaySlotObjectInAllDeck [i];
			List<GameObject> allRestSlotInOneDeck = allRestSlotObjectInAllDeck [i];
			result.allDeckData [i] = new CharacterDeckData ();
			result.allDeckData[i].allPlaySlotData = new CharacterSlotData[MaxPlaySlotNumber];
			result.allDeckData[i].allRestSlotData = new CharacterSlotData[allRestSlotInOneDeck.Count];

			for (int j = 0; j < MaxPlaySlotNumber; j++) {
				CharacterSlotDataController ctl = allPlaySlotInOneDeck [j].GetComponent<CharacterSlotDataController> ();
				CharacterSlotData dataInObject = ctl.characterSlotData;
				CharacterSlotData resultSlotData = new CharacterSlotData ();
				resultSlotData.isEmpty = dataInObject.isEmpty;
				resultSlotData.characterId = dataInObject.characterId;
				resultSlotData.lvl = dataInObject.lvl;
				resultSlotData.exp = dataInObject.exp;
				resultSlotData.weight = dataInObject.weight;
				resultSlotData.imagePath = dataInObject.imagePath;
				result.allDeckData [i].allPlaySlotData [j] = resultSlotData;
			}

			for (int j = 0; j < allRestSlotInOneDeck.Count; j++) {
				CharacterSlotDataController ctl = allRestSlotInOneDeck [j].GetComponent<CharacterSlotDataController> ();
				CharacterSlotData dataInObject = ctl.characterSlotData;
				CharacterSlotData resultSlotData = new CharacterSlotData ();
				resultSlotData.isEmpty = dataInObject.isEmpty;
				resultSlotData.characterId = dataInObject.characterId;
				resultSlotData.lvl = dataInObject.lvl;
				resultSlotData.exp = dataInObject.exp;
				resultSlotData.weight = dataInObject.weight;
				resultSlotData.imagePath = dataInObject.imagePath;
				result.allDeckData [i].allRestSlotData [j] = resultSlotData;
			}
		}
		return result;
	}

	// get the cache file.
	private CharacterMenuData LoadAllDeckDataFromFile() {
		CharacterMenuData result = new CharacterMenuData ();
		string filePath = Path.Combine (Application.streamingAssetsPath, "data.json");
		if (File.Exists (filePath)) {
			string dataAsJson = File.ReadAllText (filePath);
			result = JsonUtility.FromJson<CharacterMenuData> (dataAsJson);
			return result;
		} else {
			Debug.LogError ("Cannot load character menu data from file!");
			return null;
		}
	}

	// save the all slot data to the cache file
	private void SaveAllDeckDataToFile() {
		string filePath = Path.Combine (Application.streamingAssetsPath, "data.json");
		if (File.Exists (filePath)) {
			CharacterMenuData dataNeedWrite = AllSlotObjectDataToSerializableData ();
			string dataAsJson = JsonUtility.ToJson(dataNeedWrite);
			File.WriteAllText (filePath, dataAsJson);
		} else {
			Debug.LogError ("Cannot write character menu data to file!");
		}
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}

