using UnityEngine;
using System.Collections;

public class SpawnHatch : MonoBehaviour {

	public GameObject drone, freezer;
	public float activationDistance = 30, deactivationDistance = 40;

	public DoorOpener rightDoorOpener, LeftDoorOpener;

	public Transform mySpawnPoint;

	Transform player;
	SpawnControler theOvermindScript;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		theOvermindScript = GameObject.Find ("SpawnControl").GetComponent<SpawnControler>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Vector2.Distance (new Vector2 (player.position.x, player.position.y), new Vector2 (transform.position.x, transform.position.y)) < activationDistance) {
			gameObject.tag = ("ActiveSpawnHatch");

		} else if (Vector2.Distance (new Vector2 (player.position.x, player.position.y), new Vector2 (transform.position.x, transform.position.y)) > deactivationDistance) {
			gameObject.tag = ("Untagged");
		}


	}

	public void spawnDrone() {
		int dronesInPlay = GameObject.FindGameObjectsWithTag ("Drone").Length;

		if (dronesInPlay < theOvermindScript.droneLimit) {
			
			rightDoorOpener.doorIsOpen = true;
			LeftDoorOpener.doorIsOpen = true;

			Invoke ("dispatchDrone", 0.5f);
			Invoke ("closeDoors", 1f);
		}
	}

	public void spawnFreezer() {
		int dronesInPlay = GameObject.FindGameObjectsWithTag ("Drone").Length;

		if (dronesInPlay < theOvermindScript.droneLimit) {

			rightDoorOpener.doorIsOpen = true;
			LeftDoorOpener.doorIsOpen = true;

			Invoke ("dispatchFreezer", 0.5f);
			Invoke ("closeDoors", 1f);
		}
	}

	void dispatchDrone () {
		GameObject droneInstance = Instantiate (drone, mySpawnPoint.position, transform.rotation) as GameObject;
		droneInstance.GetComponent<Rigidbody2D> ().AddRelativeForce (Vector2.up * 1800);
	}

	void dispatchFreezer () {
		GameObject droneInstance = Instantiate (freezer, mySpawnPoint.position, transform.rotation) as GameObject;
		droneInstance.GetComponent<Rigidbody2D> ().AddRelativeForce (Vector2.up * 1800);
	}

	void closeDoors () {
		rightDoorOpener.doorIsOpen = false;
		LeftDoorOpener.doorIsOpen = false;
	}

}
