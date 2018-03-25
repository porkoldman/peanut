using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterButton : MonoBehaviour {

	public GameObject selectedEffect;
	public GameObject infoButton;

	private PlayCharacterPanelController playCharacterPanelController;

	private bool isSelected;

	private CharacterData characterData;

	private CharacterMenuController characterMenuController;

	public void SetUp(CharacterData data) {
		characterData = data;
	}


	public bool IsSelected() {
		return isSelected;
	}

	public void ClickTrigger() {
		if (isSelected == true) {
			isSelected = false;
			infoButton.SetActive (false);
			selectedEffect.SetActive (false);
		} else {
			playCharacterPanelController.UnSelectCurrentCharacter ();
			isSelected = true;
			infoButton.SetActive (true);
			selectedEffect.SetActive (true);
			playCharacterPanelController.SetCurrentSelectedCharacter (this);
		}
	}

	// Use this for initialization
	void Start () {
		isSelected = false;
		playCharacterPanelController = FindObjectOfType<PlayCharacterPanelController> ();


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
