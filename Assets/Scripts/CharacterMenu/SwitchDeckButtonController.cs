using UnityEngine;
using System.Collections;

public class SwitchDeckButtonController : MonoBehaviour
{
	// record all buttons that need to be controlled 
	public SwitchDackButton[] allSwitchDeckButton;

	// record button's index that is selected now.
	public int selectedIndex = 0 ;
}
