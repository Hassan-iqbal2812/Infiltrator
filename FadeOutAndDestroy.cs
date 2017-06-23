using UnityEngine;
using System.Collections;

public class FadeOutAndDestroy : MonoBehaviour {

	public SpriteRenderer myRenderer;
	public float alphaValue1, alphaValue2, alphaValue2Time, alphaValue3, alphaValue3Time;

	float currentAlphaValue;
	float timePast;

	// Use this for initialization
	void Start () {
		currentAlphaValue = alphaValue1;
		myRenderer.color = new Color (myRenderer.color.r, myRenderer.color.g, myRenderer.color.b, currentAlphaValue);
	}
	
	// Update is called once per frame
	void Update () {
		timePast += Time.deltaTime;

		if (timePast < alphaValue2Time) {
			currentAlphaValue = alphaValue1 + ((alphaValue2 - alphaValue1) * (timePast / alphaValue2Time));
		} else if (timePast < alphaValue3Time) {
			currentAlphaValue = alphaValue2 + ((alphaValue3 - alphaValue2) * (timePast / alphaValue3Time));
		} else
			Destroy (gameObject);

		myRenderer.color = new Color (myRenderer.color.r, myRenderer.color.g, myRenderer.color.b, currentAlphaValue);
	}
}
