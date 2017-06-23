using UnityEngine;
using System.Collections;

public class LaserFence : MonoBehaviour {

	public GameObject[] Batteries;

	bool allBatterieisDestroyed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		allBatterieisDestroyed = true; //Consider the batteries to be destroyed, then check if they aren't destroyed

		foreach (GameObject battery in Batteries) {
			if (battery != null) {
				allBatterieisDestroyed = false; //If they aren't destroyed, then make a note
				break;
			}
		}

		if (allBatterieisDestroyed)
			Destroy (gameObject);

	}
}
