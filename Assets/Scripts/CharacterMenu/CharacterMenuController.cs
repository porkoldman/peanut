using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMenuController : MonoBehaviour {

	public SimpleObjectPool CharacterPool;

	public GameObject PlayCharacterPanel;
	public GameObject RestCharacterPanel;


	private int MaxDeckNumber = 5;
	private CharacterMenuDataController characterMenuDataController;

	private CharacterMenuData characterMenuData;

	public void InitCharacterMenu() {
		characterMenuDataController = FindObjectOfType<CharacterMenuDataController> ();
		characterMenuData = characterMenuDataController.GetData ();

	}

	private void ShwoCharacterMenu() {
	}


}
