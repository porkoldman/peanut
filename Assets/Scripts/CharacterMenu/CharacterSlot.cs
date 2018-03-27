using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterSlot : MonoBehaviour {

	// the object will be displayed when slot is empty. 
	public GameObject emptyObject;

	// the object will be displayed when slot is filled. 
	public GameObject fillObject;

	// when slot is highlight, the selected effect will be displayed. 
	public GameObject selectedEffect;

	// the image of character in this slot
	public GameObject characterImage;

	// the button to show character's detail info.
	public GameObject infoButton;

	// the game object that content the data of character.
	public GameObject characterContent; 

	// is the slot on play group
	private bool isOnPlay = false;

	// is the slot on rest group
	private bool isOnRest = false;

	// is the slot be selected
	private bool isSelected = false;

	// the character data in this slot.
	private CharacterSlotData characterSlotData;

	// the controller to control character menu.
	private CharacterMenuController characterMenuController;

	// mark slot as play group, it will be calculated as play slot in character menu controller.
	public void MarkAsPlayGroup() {
		isOnPlay = true;
		isOnRest = false;
	}

	// mark slot as resy group, it will be calculated as rest slot in character menu controller.
	public void MarkAsRestGroup() {
		isOnRest = true;
		isOnPlay = false;
	}

	// whether slot is play group
	public bool IsOnPlay() {
		return isOnPlay;
	}

	// whether slot is rest group
	public bool IsOnRest() {
		return isOnRest;
	}

	// whether slot is selected
	public bool IsSelected() {
		return isSelected;
	}

	// make slot empty
	public void ClearSlot() {
		emptyObject.SetActive (true);
		fillObject.SetActive (false);
	}

	// make slot fill with character data
	public void FillSlot() {
		emptyObject.SetActive (false);
		fillObject.SetActive (true);
	}

	// handle event when slot be clicked.
	public void ClickTrigger() {
		if (isSelected == true) {
			isSelected = false;
			if (characterSlotData.isEmpty == false) {
				infoButton.SetActive (false);
			}
			selectedEffect.SetActive (false);
			return;
		}
		int slotType;
		if (isOnPlay == true) {
			slotType = 0;
		} else {
			slotType = 1;
		}
		characterMenuController.CancelCurrentSelectedCharacterSlot (slotType);
		isSelected = true;
		if (characterSlotData.isEmpty == false) {
			infoButton.SetActive (true);
		}
		selectedEffect.SetActive (true);
		characterMenuController.SetCurrentSelectedCharacterSlot (this, slotType);
	}

	// set character slot data
	public void SetCharacterSlotData(CharacterSlotData data) {
		characterSlotData = data;
	}

	// get character slot data
	public CharacterSlotData GetCharacterSlotData() {
		return characterSlotData;
	}

	// set character imgage to slot.
	public void SetCharacterImage(string imagePath) {
		if (characterImage == null) {
			Debug.Log ("Cannot get child from CharacterSlot with name: CharacterImage");
			return;
		}
		Image targetImage = GameObject.Find (imagePath).GetComponent<Image> ();
		characterImage.GetComponent<Image> ().sprite = targetImage.sprite;
		characterImage.GetComponent<Image> ().SetNativeSize ();
	}

	// set weight to slot.
	public void SetWeight(int weightValue) {
		GameObject weightObject = characterContent.transform.Find("Weight").gameObject;
		if (weightObject == null) {
			Debug.Log ("Cannot get child from CharacterSlot with name: Weight");
			return;
		}
		weightObject.GetComponent<Text> ().text = weightValue.ToString ();
	}

	// set lvl to slot.
	public void SetLvl(int lvlValue) {
		GameObject lvObject = characterContent.transform.Find("LvValue").gameObject;
		if (lvObject == null) {
			Debug.Log ("Cannot get child from CharacterSlot with name: LvValue");
			return;
		}
		lvObject.GetComponent<Text> ().text = lvlValue.ToString ();
	}

	// set exp to slot.
	public void SetExp(int expValue) {
		GameObject expObject = characterContent.transform.Find("ExpValue").gameObject;
		if (expObject == null) {
			Debug.Log ("Cannot get child from CharacterSlot with name: ExpValue");
			return;
		}
		expObject.GetComponent<Text> ().text = expValue.ToString ();
	}

	// Use this for initialization
	void Start () {
		characterMenuController = FindObjectOfType<CharacterMenuController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

