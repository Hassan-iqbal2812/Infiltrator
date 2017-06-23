using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public bool shouldFollowPlayer = true;
	public float zAxisPos = -10;

	public float speedMultiplier = 1;

	Vector3 startPos;

	// Use this for initialization
	void Start () {
		startPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if (shouldFollowPlayer) {
			transform.position = new Vector3(startPos.x + (GameObject.FindGameObjectWithTag ("Player").transform.position.x - startPos.x) * speedMultiplier, startPos.y +(GameObject.FindGameObjectWithTag ("Player").transform.position.y - startPos.y) * speedMultiplier, zAxisPos);

		}

	}
}
