using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SwitchDackButton : MonoBehaviour
{
	// the deck button's index
	public int deckIndex;

	// the controller to handle character menu scene.
	private CharacterMenuController characterMenuController;

	// the controller to handle all SwitchDeckButton.
	private SwitchDeckButtonController switchDeckButtonController;

	// init when scene start.
	void Start () {
		characterMenuController = FindObjectOfType<CharacterMenuController> ();
		switchDeckButtonController = FindObjectOfType<SwitchDeckButtonController> ();
	}

	// cancel select button event.
	public void UnSelected() {
		var block005Sprite = GameObject.Find ("/Canvas/Material/block_005").GetComponent<Image> ().sprite;
		gameObject.GetComponent<Image> ().sprite = block005Sprite;
	}

	// select button event.
	public void Selected() {
		if (switchDeckButtonController.selectedIndex == deckIndex) {
			return;
		}
		switchDeckButtonController.allSwitchDeckButton [switchDeckButtonController.selectedIndex].UnSelected ();
		switchDeckButtonController.selectedIndex = deckIndex;
		characterMenuController.SwitchDeck (deckIndex);
		var block004Sprite = GameObject.Find ("/Canvas/Material/block_004").GetComponent<Image> ().sprite;
		gameObject.GetComponent<Image> ().sprite = block004Sprite;
	}
}
