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

	public void LetItOnPlay() {
		isOnPlay = true;
		isOnRest = false;
	}

	public void LetItOnRest() {
		isOnRest = true;
		isOnPlay = false;
	}

	public bool IsOnPlay() {
		return isOnPlay;
	}

	public bool IsOnRest() {
		return isOnRest;
	}

	public bool IsSelected() {
		return isSelected;
	}

	public void ClearSlot() {
		emptyObject.SetActive (true);
		fillObject.SetActive (false);
	}

	public void FillSlot() {
		emptyObject.SetActive (false);
		fillObject.SetActive (true);
	}

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

	public void SetCharacterSlotData(CharacterSlotData data) {
		characterSlotData = data;
	}

	public CharacterSlotData GetCharacterSlotData() {
		return characterSlotData;
	}

	public void SetCharacterImage(string imagePath) {
		if (characterImage == null) {
			Debug.Log ("Cannot get child from CharacterSlot with name: CharacterImage");
			return;
		}
		Image targetImage = GameObject.Find (imagePath).GetComponent<Image> ();
		characterImage.GetComponent<Image> ().sprite = targetImage.sprite;
		characterImage.GetComponent<Image> ().SetNativeSize ();
	}

	public void SetWeight(int weightValue) {
		GameObject weightObject = characterContent.transform.Find("Weight").gameObject;
		if (weightObject == null) {
			Debug.Log ("Cannot get child from CharacterSlot with name: Weight");
			return;
		}
		weightObject.GetComponent<Text> ().text = weightValue.ToString ();
	}

	public void SetLvl(int lvlValue) {
		GameObject lvObject = characterContent.transform.Find("LvValue").gameObject;
		if (lvObject == null) {
			Debug.Log ("Cannot get child from CharacterSlot with name: LvValue");
			return;
		}
		lvObject.GetComponent<Text> ().text = lvlValue.ToString ();
	}

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

