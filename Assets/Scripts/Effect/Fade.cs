using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
	public float fadeRate;
	public bool loop;
	public float maxAlpha;
	public float minAlpha;

	private Image image;
	private float targetAlpha;


	// Use this for initialization
	void Start () {
		this.image = this.GetComponent<Image>();
		if(this.image==null)
		{
			Debug.LogError("Error: No image on "+this.name);
		}
		this.targetAlpha = this.image.color.a;
	}

	// Update is called once per frame
	void Update () {
		Color curColor = this.image.color;
		if (loop == true) {
			if (Mathf.Abs(curColor.a - minAlpha) < 0.01f) {
				FadeIn ();
			}
			if (Mathf.Abs(curColor.a - maxAlpha) < 0.01f) {
				FadeOut ();
			}

		}
		float alphaDiff = Mathf.Abs(curColor.a - this.targetAlpha);
		if (alphaDiff>0.0001f)
		{
			curColor.a = Mathf.Lerp(curColor.a, targetAlpha, this.fadeRate * Time.deltaTime);
			this.image.color = curColor;
		}
	}

	public void FadeOut()
	{
		this.targetAlpha = minAlpha;
	}

	public void FadeIn()
	{
		this.targetAlpha = maxAlpha;
	}
}
