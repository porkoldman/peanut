using UnityEngine;
using System.Collections;

public class RemovePlaySlotButton : MonoBehaviour
{

	private CharacterMenuController characterMenuController;


	private void CheckSlotCanClear() {
		CharacterSlot target = characterMenuController.GetCurrentSelectedPlaySlot ();
		if (target == null) {
			target = characterMenuController.GetLastNonEmptyPlaySlot ();
		}
		if (target == null) {
			return;
		}

	}

	private void MakePlaySlotEmpty(int slotIndex) {
		
	}


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

