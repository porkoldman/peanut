using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMenuController : MonoBehaviour {

	public CharacterMenuData characterMenuData;
	public SimpleObjectPool CharacterSlotPool;
	public GameObject PlayCharacterSlotGroup;
	public Transform PlayCharacterSlotParent;
	public GameObject RestCharacterSlotGroup;

	private int playSlotType = 0;
	private int restSlotType = 1;

	private CharacterSlot playSlot1;
	private CharacterSlot playSlot2;
	private CharacterSlot playSlot3;
	private CharacterSlot playSlot4;
	private CharacterSlot playSlot5;
	private int currentDeck = 0;
	private CharacterSlot currentSelectedPlayCharacterSlot;
	private CharacterSlot currentSelectedRestCharacterSlot;

	void Start() {
		InitCharacterMenu ();
	}

	public void InitCharacterMenu() {
		CharacterDeckData deck1 = characterMenuData.deck1;
		CharacterSlotData[] playSlots = {deck1.playSlot1, deck1.playSlot2, deck1.playSlot3, deck1.playSlot4, deck1.playSlot5};
		for (int i = 0; i < 5; i++) {
			GameObject characterSlotObject = CharacterSlotPool.GetObject();
			characterSlotObject.transform.SetParent (PlayCharacterSlotParent);
			CharacterSlot characterSlot = characterSlotObject.GetComponent<CharacterSlot> ();
			characterSlot.SetCharacterSlotData (playSlots[i]);
			characterSlot.LetItOnPlay ();
			if (playSlots[i].isEmpty == true) {
				characterSlot.ClearSlot ();
				continue;
			}
			characterSlot.FillSlot ();
			characterSlot.SetLvl (playSlots[i].lvl);
			characterSlot.SetWeight (playSlots[i].weight);
			characterSlot.SetExp (playSlots[i].exp);
			characterSlot.SetCharacterImage (playSlots[i].imgPath);
			SetPlayCharacterSlot (i, characterSlot);
		}
	}

	public void SetPlayCharacterSlot(int index, CharacterSlot obj) {
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

	public void CancelCurrentSelectedCharacterSlot(int slotTyep) {
		CharacterSlot targetCharacterSlot;
		if (slotTyep == playSlotType) {
			targetCharacterSlot = currentSelectedPlayCharacterSlot;
		} else if (slotTyep == restSlotType) {
			targetCharacterSlot = currentSelectedRestCharacterSlot;
		} else {
			return;
		}
		if (targetCharacterSlot == null) {
			return;
		}
		if (targetCharacterSlot.IsSelected () == true) {
			targetCharacterSlot.ClickTrigger ();
		}
	}

	public void SetCurrentSelectedCharacterSlot(CharacterSlot obj, int slotTyep) {
		if (slotTyep == playSlotType) {
			currentSelectedPlayCharacterSlot = obj;
		} else if (slotTyep == restSlotType) {
			currentSelectedRestCharacterSlot = obj;
		}
	}
}
