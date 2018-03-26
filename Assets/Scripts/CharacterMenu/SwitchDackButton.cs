using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SwitchDackButton : MonoBehaviour
{
	public int deckIndex;

	private CharacterMenuController characterMenuController;
	private SwitchDeckButtonController switchDeckButtonController;

	void Start () {
		characterMenuController = FindObjectOfType<CharacterMenuController> ();
		switchDeckButtonController = FindObjectOfType<SwitchDeckButtonController> ();
	}

	public void UnHightLight() {
		var block005Sprite = GameObject.Find ("/Canvas/Material/block_005").GetComponent<Image> ().sprite;
		gameObject.GetComponent<Image> ().sprite = block005Sprite;
	}

	public void HightLight() {
		if (switchDeckButtonController.selectedIndex == deckIndex) {
			return;
		}
		switchDeckButtonController.allSwitchDeckButton [switchDeckButtonController.selectedIndex].UnHightLight ();
		switchDeckButtonController.selectedIndex = deckIndex;
		characterMenuController.SwitchDeckData (deckIndex);
		var block004Sprite = GameObject.Find ("/Canvas/Material/block_004").GetComponent<Image> ().sprite;
		gameObject.GetComponent<Image> ().sprite = block004Sprite;
	}
}
