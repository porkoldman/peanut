using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class CharacterSlotData {
	public bool isEmpty;
	public int characterId;
	public int lvl;
	public int weight;
	public int exp;
	public string imagePath;


	public bool IsEqual(CharacterSlotData obj) {
		return object.ReferenceEquals (this, obj);
	}
}
