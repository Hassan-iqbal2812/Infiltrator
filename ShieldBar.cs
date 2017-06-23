using UnityEngine;
using System.Collections;

public class ShieldBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector3(1, GameObject.FindGameObjectWithTag ("Player").GetComponent<Health> ().getShieldPercent (), 1);
	}
}
