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

	// Use this for initialization
	void Start () {
		characterPanelController = FindObjectOfType<CharacterPanelController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
