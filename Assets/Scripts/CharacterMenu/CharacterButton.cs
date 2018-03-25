using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterButton : MonoBehaviour {

	public GameObject selectedEffect;
	public GameObject infoButton;

	private PlayCharacterPanelController playCharacterPanelController;
	private bool isOnPlay;
	private bool isSelected;
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
		} else {
			if (isOnPlay == true) {
				playCharacterPanelController.UnSelectCurrentCharacter ();
			}
			isSelected = true;
			infoButton.SetActive (true);
			selectedEffect.SetActive (true);
			if (isOnPlay == true) {
				playCharacterPanelController.SetCurrentSelectedCharacter (this);
			}
		}
	}

	// Use this for initialization
	void Start () {
		isOnPlay = false;
		isSelected = false;
		playCharacterPanelController = FindObjectOfType<PlayCharacterPanelController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
