using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanelController : MonoBehaviour {

	public CharacterButton playSlot1;
	public CharacterButton playSlot2;
	public CharacterButton playSlot3;
	public CharacterButton playSlot4;
	public CharacterButton playSlot5;

	private int currentDeck = 0;
	public CharacterButton currentSelectedPlayCharacter;
	public CharacterButton currentSelectedRestCharacter;

	// Use this for initialization
	void Start () {
	}

	public void setPlayCharacterSlot(int index, CharacterButton obj) {
		switch (index) {
		case 0:
			playSlot1 = obj;
			break;
		case 1:
			playSlot2 = obj;
			break;
		case 2:
			playSlot3 = obj;
			break;
		case 3:
			playSlot4 = obj;
			break;
		case 4:
			playSlot5 = obj;
			break;
		}
	}

	public void UnSelectCurrentPlayCharacter() {
		if (currentSelectedPlayCharacter == null) {
			return;
		}
		if (currentSelectedPlayCharacter.IsSelected () == true) {
			currentSelectedPlayCharacter.ClickTrigger ();
		}
	}

	public void SelectCurrentPlayCharacter() {
		if (currentSelectedPlayCharacter == null) {
			return;
		}
		if (currentSelectedPlayCharacter.IsSelected () == false) {
			currentSelectedPlayCharacter.ClickTrigger ();
		}
	}

	public void SetCurrentSelectedPlayCharacter(CharacterButton obj) {
		currentSelectedPlayCharacter = obj;
	}

	public void UnSelectCurrentRestCharacter() {
		if (currentSelectedRestCharacter == null) {
			return;
		}
		if (currentSelectedRestCharacter.IsSelected () == true) {
			currentSelectedRestCharacter.ClickTrigger ();
		}
	}

	public void SelectCurrentRestCharacter() {
		if (currentSelectedRestCharacter == null) {
			return;
		}
		if (currentSelectedRestCharacter.IsSelected () == false) {
			currentSelectedRestCharacter.ClickTrigger ();
		}
	}

	public void SetCurrentSelectedRestCharacter(CharacterButton obj) {
		currentSelectedRestCharacter = obj;
	}


	// Update is called once per frame
	void Update () {
		
	}
}
