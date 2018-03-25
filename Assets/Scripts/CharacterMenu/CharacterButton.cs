using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterButton : MonoBehaviour {

	public Image border;
	public Image block;
	public Image heart;
	public Image mask;
	public Image character;
	public Text lvText;
	public Text lvValue;
	public Text expValue;
	public Text weight;


	private CharacterData characterData;

	private CharacterMenuController characterMenuController;

	public void SetUp(CharacterData data) {
		characterData = data;

		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
