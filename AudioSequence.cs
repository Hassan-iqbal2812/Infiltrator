using UnityEngine;
using System.Collections;

public class AudioSequence : MonoBehaviour {

	public AudioSource sound;

	public float delay1, delay2;

	bool playedSound1, playedSound2;

	float timePast;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timePast += Time.deltaTime;

		if (timePast >= delay1 && !playedSound1) {
			sound.Play ();
			playedSound1 = true;
		}

		if (timePast >= delay2 && !playedSound2) {
			sound.Play();
			playedSound2 = true;
		}
	}
}
