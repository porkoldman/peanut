using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMenuController : MonoBehaviour {

	public SimpleObjectPool CharacterPool;

	public GameObject PlayCharacterPanel;
	public Transform PlayCharacterParent;
	public GameObject RestCharacterPanel;

	private CharacterPanelController characterPanelController;
	private CharacterMenuDataController characterMenuDataController;
	private CharacterMenuData characterMenuData;

	void Start() {
		InitCharacterMenu ();
	}

	public void InitCharacterMenu() {
		characterPanelController = FindObjectOfType<CharacterPanelController> ();
		characterMenuDataController = FindObjectOfType<CharacterMenuDataController> ();
		characterMenuData = characterMenuDataController.GetData ();
		CharacterDeckData deck1 = characterMenuData.deck1;
		CharacterData[] playSlots = {deck1.slot1, deck1.slot2, deck1.slot3, deck1.slot4, deck1.slot5};
		for (int i = 0; i < 5; i++) {
			Debug.Log ("13");
			GameObject characterObject = CharacterPool.GetObject ();
			characterObject.transform.SetParent (PlayCharacterParent);
			CharacterButton characterButton = characterObject.GetComponent<CharacterButton> ();
			characterButton.SetLvl (playSlots[i].lvl);
			characterButton.SetWeight (playSlots[i].weight);
			characterButton.SetExp (playSlots[i].exp);
			characterButton.SetCharacterImage (playSlots[i].imgPath);
			characterButton.SetOnPlay ();
			characterPanelController.setPlayCharacterSlot (i, characterButton);
		}
	}

	private void ShwoCharacterMenu() {
	}
}
