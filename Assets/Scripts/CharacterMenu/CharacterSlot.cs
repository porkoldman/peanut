using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterSlot : MonoBehaviour {

	public GameObject emptyObject;
	public GameObject fillObject;
	public GameObject selectedEffect;

	public GameObject characterImage;
	public GameObject characterSlotObject;
	public GameObject infoButton;

	private bool isOnPlay = false;
	private bool isOnRest = false;
	private bool isSelected = false;
	private CharacterSlotData characterSlotData;

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
		characterMenuController.SetCurrentSelectedCharacterSlot (this, slotType);

		isSelected = true;
		if (characterSlotData.isEmpty == false) {
			infoButton.SetActive (true);
		}
		selectedEffect.SetActive (true);
	}

	public void SetCharacterSlotData(CharacterSlotData data) {
		characterSlotData = data;
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
		GameObject weightObject = characterSlotObject.transform.Find("Weight").gameObject;
		if (weightObject == null) {
			Debug.Log ("Cannot get child from CharacterSlot with name: Weight");
			return;
		}
		weightObject.GetComponent<Text> ().text = weightValue.ToString ();
	}

	public void SetLvl(int lvlValue) {
		GameObject lvObject = characterSlotObject.transform.Find("LvValue").gameObject;
		if (lvObject == null) {
			Debug.Log ("Cannot get child from CharacterSlot with name: LvValue");
			return;
		}
		lvObject.GetComponent<Text> ().text = lvlValue.ToString ();
	}

	public void SetExp(int expValue) {
		GameObject expObject = characterSlotObject.transform.Find("ExpValue").gameObject;
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

