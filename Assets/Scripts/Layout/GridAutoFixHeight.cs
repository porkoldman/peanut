using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GridAutoFixHeight : MonoBehaviour {

	public int columnCount;

	private const int scrollPanelRange = 20;

	private float originHeight;

	// Use this for initialization
	void Start () {
		originHeight = this.gameObject.GetComponent<RectTransform> ().rect.height;
		InvokeRepeating("AutoFixHeight", 0.0f, 1.0f);
	}

	// auto resiz height with child
	public void AutoFixHeight() {
		float width = this.gameObject.GetComponent<RectTransform>().rect.width;
		int baseNumber = Convert.ToInt16(Math.Ceiling ((float)gameObject.transform.childCount / columnCount));

		float cellHeight = this.gameObject.GetComponent<GridLayoutGroup> ().cellSize.y;
		float spaceHeight = this.gameObject.GetComponent<GridLayoutGroup> ().spacing.y;

		float newHeight = (cellHeight + spaceHeight) * baseNumber - spaceHeight - scrollPanelRange;

		if (originHeight < newHeight) {
			this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2 (width, newHeight);
		}
	}
}
