using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterButton : MonoBehaviour {
	public GameObject selectedEffect;
	public GameObject infoButton;

	private CharacterPanelController characterPanelController;
	private bool isOnPlay = false;
	private bool isSelected = false;
	private CharacterData characterData;
	private CharacterMenuController characterMenuController;

	public void SetUp(CharacterData data) {
		characterData = data;
	}

	public void SetOnPlay() {
		isOnPlay = true;
	}

	public void SetOnRest() {
		isOnPlay = false;
	}

	public bool IsOnPlay() {
		return isOnPlay;
	}

	public bool IsSelected() {
		return isSelected;
	}

	public void ClickTrigger() {
		if (isSelected == true) {
			isSelected = false;
			infoButton.SetActive (false);
			selectedEffect.SetActive (false);
			return;
		}
		if (isOnPlay == true) {
			characterPanelController.UnSelectCurrentPlayCharacter ();
			characterPanelController.SetCurrentSelectedPlayCharacter (this);
		} else {
			characterPanelController.UnSelectCurrentRestCharacter ();
			characterPanelController.SetCurrentSelectedRestCharacter (this);
		}
		isSelected = true;
		infoButton.SetActive (true);
		selectedEffect.SetActive (true);
	}

	public void SetCharacterImage(string imgPath) {
		GameObject maskObject = gameObject.transform.Find("Mask").gameObject;
		GameObject imageObject = maskObject.transform.Find("CharacterImage").gameObject;
		if (imageObject == null) {
			Debug.Log ("Cannot get child from CharacterButton with name: CharacterImage");
			return;
		}
		Image targetImage = GameObject.Find (imgPath).GetComponent<Image> ();
		imageObject.GetComponent<Image> ().sprite = targetImage.sprite;
		imageObject.GetComponent<Image> ().SetNativeSize ();
	}

	public void SetWeight(int weightValue) {
		GameObject weightObject = gameObject.transform.Find("Weight").gameObject;
		if (weightObject == null) {
			Debug.Log ("Cannot get child from CharacterButton with name: Weight");
			return;
		}
		weightObject.GetComponent<Text> ().text = weightValue.ToString ();
	}

	public void SetLvl(int lvlValue) {
		GameObject lvObject = gameObject.transform.Find("LvValue").gameObject;
		if (lvObject == null) {
			Debug.Log ("Cannot get child from CharacterButton with name: LvValue");
			return;
		}
		lvObject.GetComponent<Text> ().text = lvlValue.ToString ();
	}

	public void SetExp(int expValue) {
		GameObject expObject = gameObject.transform.Find("ExpValue").gameObject;
		if (expObject == null) {
			Debug.Log ("Cannot get child from CharacterButton with name: ExpValue");
			return;
		}
		expObject.GetComponent<Text> ().text = expValue.ToString ();
	}

	// Use this for initialization
	void Start () {
		characterPanelController = FindObjectOfType<CharacterPanelController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

