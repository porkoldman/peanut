using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterButton : MonoBehaviour {

	public GameObject selectedEffect;
	public GameObject infoButton;

	private bool isSelected;

	private CharacterData characterData;

	private CharacterMenuController characterMenuController;

	public void SetUp(CharacterData data) {
		characterData = data;
	}

	public void ClickTrigger() {
		if (isSelected == true) {
			isSelected = false;
			infoButton.SetActive (false);
			selectedEffect.SetActive (false);
		} else {
			isSelected = true;
			infoButton.SetActive (true);
			selectedEffect.SetActive (true);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
