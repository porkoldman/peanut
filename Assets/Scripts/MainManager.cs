using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var baseBtn = GameObject.Find("/Canvas/MenuBtn/BaseHouseBtn");
		var baseBtnImageLinker = baseBtn.GetComponent<Image> ();
		var block001Image = GameObject.Find("/Canvas/Material/block_001").GetComponent<Image>();
		baseBtnImageLinker.sprite = block001Image.sprite;
		baseBtnImageLinker.SetNativeSize ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
