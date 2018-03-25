using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCharacterPanelController : MonoBehaviour {

	public CharacterButton playSlot1;
	public CharacterButton playSlot2;
	public CharacterButton playSlot3;
	public CharacterButton playSlot4;
	public CharacterButton playSlot5;

	private int currentDeck = 0;

	public CharacterButton currentSelectedCharacter;


	public void UnSelectCurrentCharacter() {
		if (currentSelectedCharacter == null) {
			return;
		}
		if (currentSelectedCharacter.IsOnPlay() == false) {
			return;
		}
		if (currentSelectedCharacter.IsSelected () == true) {
			currentSelectedCharacter.ClickTrigger ();
		}
	}

	public void SelectCurrentCharacter() {
		if (currentSelectedCharacter == null) {
			return;
		}
		if (currentSelectedCharacter.IsOnPlay() == false) {
			return;
		}
		if (currentSelectedCharacter.IsSelected () == false) {
			currentSelectedCharacter.ClickTrigger ();
		}
	}

	public void SetCurrentSelectedCharacter(CharacterButton obj) {
		if (currentSelectedCharacter.IsOnPlay() == false) {
			return;
		}
		currentSelectedCharacter = obj;
	}


	// Use this for initialization
	void Start () {
		playSlot1.SetOnPlay ();
		playSlot2.SetOnPlay ();
		playSlot3.SetOnPlay ();
		playSlot4.SetOnPlay ();
		playSlot5.SetOnPlay ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
