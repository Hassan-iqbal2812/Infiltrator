using UnityEngine;
using System.Collections;

public class LaserTrapAI : MonoBehaviour {

	public float rotationSpeed = 20f; 
	float angle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		angle += rotationSpeed * Time.deltaTime;
		if (angle > 360)
			angle -= 360;

		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

	}
}
