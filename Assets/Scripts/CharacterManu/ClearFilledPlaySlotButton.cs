using UnityEngine;
using System.Collections;

public class ClearFilledPlaySlotButton : MonoBehaviour
{

	private CharacterMenuController characterMenuController;


	public void OnClicked() {
		characterMenuController.ClearFilledPlaySlot ();
	}

	// Use this for initialization
	void Start ()
	{
		characterMenuController = FindObjectOfType<CharacterMenuController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

