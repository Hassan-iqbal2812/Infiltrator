using UnityEngine;
using System.Collections;

public class DoorOpener : MonoBehaviour {

	public bool doorIsOpen;
	public Transform closedPos, openPos;
	public float speed = 2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (doorIsOpen) {

			transform.position = Vector3.MoveTowards (transform.position, openPos.position, speed * (Time.deltaTime * 60));

		} else {

			transform.position = Vector3.MoveTowards (transform.position, closedPos.position, speed * (Time.deltaTime * 60));

		}

	}
}
