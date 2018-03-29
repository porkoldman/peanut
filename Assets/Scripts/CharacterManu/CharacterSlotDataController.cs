using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlotDataController : MonoBehaviour
{
	// the object will be displayed when slot is empty. 
	public GameObject emptyObject;

	// the object will be displayed when slot is filled. 
	public GameObject fillObject;

	// when slot is highlight, the selected effect will be displayed. 
	public GameObject selectedEffect;

	// the button to show character's detail info.
	public GameObject infoButton;

	// the image object of character in this slot
	public GameObject characterImage;

	// the lvl object of character in this slot
	public GameObject characterLvl;

	// the weight object of character in this slot
	public GameObject characterWeight;

	// the exp object of character in this slot
	public GameObject characterExp;

	// the character data in this slot.
	public CharacterSlotData characterSlotData;

	// whether the slot is selected
	public bool isSelected = false;

	// a controller to control all character menu.
	private CharacterMenuController characterMenuController;

	void Start() {
		characterMenuController = FindObjectOfType<CharacterMenuController> ();
	}

	// display this slot.
	public void DisplayWithCharacterSlotData() {
		if (characterSlotData == null) {
			return;
		}

		// if slot is empty => show empty slot.
		if (characterSlotData.isEmpty == true) {
			emptyObject.SetActive (true);
			fillObject.SetActive (false);
			return;
		}

		// display the slot with specific image and data.
		Image targetImage = GameObject.Find (characterSlotData.imagePath).GetComponent<Image> ();
		characterImage.GetComponent<Image> ().sprite = targetImage.sprite;
		characterImage.GetComponent<Image> ().SetNativeSize ();
		characterLvl.GetComponent<Text> ().text = characterSlotData.lvl.ToString ();
		characterWeight.GetComponent<Text> ().text = characterSlotData.weight.ToString ();
		characterExp.GetComponent<Text> ().text = characterSlotData.exp.ToString ();
		emptyObject.SetActive (false);
		fillObject.SetActive (true);
	}


	// a toggle function to handle slot's selected event.
	public void SelectedToggle() {
		int slotType = CheckSlotOnWhichGroup();
		if (slotType == -1) {
			return;
		}
		int slotIndex = CheckSlotIndex ();
		if (slotIndex == -1) {
			return;
		}
		if (isSelected == true) {
			SetUnSelected ();
			if (slotType == 0) {
				characterMenuController.SetSelectedPlaySlotIndex (-1);
			}
			if (slotType == 1) {
				characterMenuController.SetSelectedRestSlotIndex (-1);
			}
		} else {
			SetSelected ();
			if (slotType == 0) {
				characterMenuController.SetSelectedPlaySlotIndex (slotIndex);
			}
			if (slotType == 1) {
				characterMenuController.SetSelectedRestSlotIndex (slotIndex);
			}
		}
	}

	// let slot to be selected
	public void SetSelected() {
		int slotType = CheckSlotOnWhichGroup();
		if (slotType == -1) {
			return;
		}
		int slotIndex = CheckSlotIndex ();
		if (slotIndex == -1) {
			return;
		}
		isSelected = true;
		if (characterSlotData.isEmpty == false) {
			infoButton.SetActive (true);
		}
		selectedEffect.SetActive (true);
	}

	// cancel select slot
	public void SetUnSelected() {
		int slotType = CheckSlotOnWhichGroup();
		if (slotType == -1) {
			return;
		}
		int slotIndex = CheckSlotIndex ();
		if (slotIndex == -1) {
			return;
		}
		isSelected = false;
		if (characterSlotData.isEmpty == false) {
			infoButton.SetActive (false);
		}
		selectedEffect.SetActive (false);

	}

	// check slot's index in list it belong to, if not in all list  => return -1
	private int CheckSlotIndex() {
		int slotType = CheckSlotOnWhichGroup();
		if (slotType == -1) {
			return -1;
		}
		int slotIndex = -1;
		if (slotType == 0) {
			slotIndex = characterMenuController.CheckPlaySlotObjectIndex (gameObject);
		}
		if (slotType == 1) {
			slotIndex = characterMenuController.CheckRestSlotObjectIndex (gameObject);
		}

		return slotIndex; 
	}

	// check slot in which group(play or rest), 0 => play group, 1 => rest group, -1 => not found
	private int CheckSlotOnWhichGroup() {
		if (gameObject.transform.IsChildOf (characterMenuController.playCharacterSlotLayoutPanel.transform) == true) {
			return 0;
		}
		if (gameObject.transform.IsChildOf (characterMenuController.restCharacterSlotLayoutPanel.transform) == true) {
			return 1;
		}
		// unknow group
		return -1;
	}

}

