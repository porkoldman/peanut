using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCharacterPanelController : MonoBehaviour {

	private int currentDeck = 0;

	public CharacterButton currentSelectedCharacter;


	public void UnSelectCurrentCharacter() {
		if (currentSelectedCharacter == null) {
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
		if (currentSelectedCharacter.IsSelected () == false) {
			currentSelectedCharacter.ClickTrigger ();
		}
	}

	public void SetCurrentSelectedCharacter(CharacterButton obj) {
		currentSelectedCharacter = obj;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
