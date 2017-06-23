using UnityEngine;
using System.Collections;

public class BossMusic : MonoBehaviour {

	public AudioSource bossMusic, normalMusic;

	public float transitionTime = 2f;

	float timeSinceTrigger;

	bool tiggered;
	bool musicChanged;



	float normalStartVolume, bossStartVolume;

	// Use this for initialization
	void Start () {
		normalStartVolume = normalMusic.volume;
		bossStartVolume = bossMusic.volume;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (tiggered && !musicChanged) {
			bossMusic.volume = 0;


			if (!bossMusic.isPlaying)
				bossMusic.Play ();

			timeSinceTrigger += Time.deltaTime;

			normalMusic.volume = normalStartVolume * Mathf.Clamp ((1  - (timeSinceTrigger / (transitionTime/2) )  ), 0, 1);


			bossMusic.volume = bossStartVolume * Mathf.Clamp ( (timeSinceTrigger / (transitionTime)  ), 0, 1) * PlayerPrefs.GetFloat ("musicVolume", 1.0f);



			if (timeSinceTrigger >= transitionTime)
				musicChanged = true;
		}

	}

	void OnTriggerEnter2D (Collider2D hit) {
		tiggered = true;
	}
}
