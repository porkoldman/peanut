using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBtnClickTrigger : MonoBehaviour {

	public int clickedIndex = 0;
	public GameObject CardBtn;
	public GameObject CharacterBtn;
	public GameObject BaseHouseBtn;
	public GameObject ItemBtn;
	public GameObject ShopBtn;

	private void resetClickedBtnSprite() {
		var block002Sprite = GameObject.Find ("/Canvas/Material/block_002").GetComponent<Image> ().sprite;

		switch (clickedIndex) {
		case 0:
			CardBtn.GetComponent<Image> ().sprite = block002Sprite;
			break;
		case 1:
			CharacterBtn.GetComponent<Image> ().sprite = block002Sprite;
			break;
		case 2:
			BaseHouseBtn.GetComponent<Image> ().sprite = block002Sprite;
			break;
		case 3:
			ItemBtn.GetComponent<Image> ().sprite = block002Sprite;
			break;
		case 4:
			ShopBtn.GetComponent<Image> ().sprite = block002Sprite;
			break;
		}
	}

	public void PressDownCardBtn() {
		CardBtn.transform.localScale = new Vector3(0.95f, 0.95f, 1.0f);
	}

	public void PressUpCardBtn() {
		CardBtn.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
	}

	public void ClickCardBtn() {
		var block001Sprite = GameObject.Find ("/Canvas/Material/block_001").GetComponent<Image> ().sprite;
		if (clickedIndex != 0) {
			resetClickedBtnSprite();
			CardBtn.GetComponent<Image> ().sprite = block001Sprite;
			clickedIndex = 0;
		}
	}

	public void PressDownCharacterBtn() {
		CharacterBtn.transform.localScale = new Vector3(0.95f, 0.95f, 1.0f);
	}

	public void PressUpCharacterBtn() {
		CharacterBtn.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
	}


	public void ClickCharacterBtn() {
		var block001Sprite = GameObject.Find ("/Canvas/Material/block_001").GetComponent<Image> ().sprite;
		if (clickedIndex != 1) {
			resetClickedBtnSprite();
			CharacterBtn.GetComponent<Image> ().sprite = block001Sprite;
			clickedIndex = 1;
		}
	}

	public void PressDownBaseHouseBtn() {
		BaseHouseBtn.transform.localScale = new Vector3(0.95f, 0.95f, 1.0f);
	}

	public void PressUpBaseHouseBtn() {
		BaseHouseBtn.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
	}

	public void ClickBaseHouseBtn() {
		var block001Sprite = GameObject.Find ("/Canvas/Material/block_001").GetComponent<Image> ().sprite;
		if (clickedIndex != 2) {
			resetClickedBtnSprite();
			BaseHouseBtn.GetComponent<Image> ().sprite = block001Sprite;
			clickedIndex = 2;
		}

	}


	public void PressDownItemBtn() {
		ItemBtn.transform.localScale = new Vector3(0.95f, 0.95f, 1.0f);
	}

	public void PressUpItemBtn() {
		ItemBtn.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
	}

	public void ClickItemBtn() {
		var block001Sprite = GameObject.Find ("/Canvas/Material/block_001").GetComponent<Image> ().sprite;
		if (clickedIndex != 3) {
			resetClickedBtnSprite();
			ItemBtn.GetComponent<Image> ().sprite = block001Sprite;
			clickedIndex = 3;
		}
	}

	public void PressDownShopBtn() {
		ShopBtn.transform.localScale = new Vector3(0.95f, 0.95f, 1.0f);
	}

	public void PressUpShopBtn() {
		ShopBtn.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
	}

	public void ClickShopBtn() {
		var block001Sprite = GameObject.Find ("/Canvas/Material/block_001").GetComponent<Image> ().sprite;
		if (clickedIndex != 4) {
			resetClickedBtnSprite();
			ShopBtn.GetComponent<Image> ().sprite = block001Sprite;
			clickedIndex = 4;
		}
	}
}
