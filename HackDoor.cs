using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HackDoor : MonoBehaviour {

	GameObject player;
	Text hackTimer;
	public float hackTimeToPass = 10, hackMaxDistance = 25;
	float timeRemaining;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		hackTimer = GameObject.Find ("HackTimer").GetComponent<Text> ();
		timeRemaining = hackTimeToPass;
	}
	
	// Update is called once per frame
	void Update () {

		if (Vector2.Distance (transform.position, player.transform.position) <= hackMaxDistance) {//Check player distance from door

			timeRemaining -= Time.deltaTime;//Reduce time

			if (timeRemaining < 0) {
				hackTimer.text = "";
				Destroy (gameObject);//Open the door
			} else {
				hackTimer.text = "Hacking in progress...\n" + timeRemaining.ToString ("F2");//Time text with time to 2 dp
			}
		}

	}
}
