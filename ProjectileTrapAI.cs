using UnityEngine;
using System.Collections;

public class ProjectileTrapAI : MonoBehaviour {

	public GameObject projectile;//Assign a prefabricated projectile (with the projectile script attached) via the inspector 
	public float activationDistance = 20f, shotCooldown = 2f, rotationSpeed = 20f; 

	float shotCooldownRemaining, angle;


	// Update is called once per frame
	void Update () {
		shotCooldownRemaining -= Time.deltaTime;//Reduce the remaining cooldown time


		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotationSpeed * Time.deltaTime);


		if (shotCooldownRemaining <= 0) {//Checks if the cooldown has been met
			
			Vector3 relativePos = GameObject.FindGameObjectWithTag ("Player").transform.position - transform.position;//Get the relative position of the player from the enemy
			
			if (relativePos.magnitude < activationDistance) {//Don't shoot if player is out of range

				Instantiate (projectile, transform.position + transform.up / 2, Quaternion.AngleAxis(transform.rotation.eulerAngles.z + 0, Vector3.forward)); 
				Instantiate (projectile, transform.position - transform.right / 2, Quaternion.AngleAxis(transform.rotation.eulerAngles.z + 90, Vector3.forward)); 
				Instantiate (projectile, transform.position - transform.up / 2, Quaternion.AngleAxis(transform.rotation.eulerAngles.z + 180, Vector3.forward)); 
				Instantiate (projectile, transform.position + transform.right / 2, Quaternion.AngleAxis(transform.rotation.eulerAngles.z + 270, Vector3.forward)); 
			}

			angle += 45;
			if (angle > 360)
				angle -= 360;

			shotCooldownRemaining = shotCooldown;//Reset the cooldown

		}
	}
}
