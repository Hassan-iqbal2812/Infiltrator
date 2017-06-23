using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		
		transform.localScale = new Vector3(1,GameObject.FindGameObjectWithTag ("Player").GetComponent<Health> ().getHealthPercent (), 1);
	}

}
