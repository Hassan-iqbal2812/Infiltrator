using UnityEngine;
using System.Collections;

public class TriggerTurnOn : MonoBehaviour {

	public GameObject[] stuffToTurnOn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D hit) {
		for (int i = 0; i < stuffToTurnOn.Length; i++) {
			stuffToTurnOn [i].SetActive (true);
		}

		GameObject.Find ("SpawnControl").SetActive (false);

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		for (int i = 0; i < enemies.Length; i++) {
			if (enemies[i].name == "Ben'sDrone(Clone)") {
				EnemyAi droneAI = enemies [i].GetComponent<EnemyAi>();
				droneAI.target = GameObject.Find ("DronesComeHereToDisapear").transform;
				Destroy(enemies[i], 3f);
			}
		} 



	}



}
