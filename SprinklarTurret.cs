using UnityEngine;
using System.Collections;

public class SprinklarTurret : MonoBehaviour {

	public Transform emissionPoint;

	public GameObject sprinklarShot;

	float sprinklarShotCooldown;
	float currentFiringAngle;
	float angleStep;

	int numberOfShotsFired, numberOfShotsToFire;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void startSprinklarTurretSequence (float angleToCover, bool rotateClockwise, int numberOfShots, float sprinklarDuration) {

		numberOfShotsToFire = numberOfShots; 

		sprinklarShotCooldown = sprinklarDuration / numberOfShots;
		currentFiringAngle = angleToCover / 2;//This is also the starting angle
		angleStep = angleToCover / (numberOfShots -1);//Works out how much to change the angle by for each shot "(numberOfShots -1)" makes it so the last shot is at the maximum angle

		if (rotateClockwise) {
			angleStep = -angleStep;//
		} else {
			currentFiringAngle = -currentFiringAngle;
		}

		shotIteration ();
	}

	void shotIteration () {

		shootTurretAtAngle (currentFiringAngle);//Shoot the turret at the current angle

		currentFiringAngle += angleStep;//Increase the firing angle for the shot
		numberOfShotsFired++;//Increase the shot count

		if (numberOfShotsFired < numberOfShotsToFire)//If not all shots have been fired, then keep going
			Invoke ("shotIteration", sprinklarShotCooldown);//Calls this same function to happen again after a delay based on the sprinkle duration
		else
			numberOfShotsFired = 0;//Reset the counter as the function finishes iterating
	}


	void shootTurretAtAngle (float angle) {
		Instantiate (sprinklarShot, emissionPoint.position, Quaternion.AngleAxis (angle + emissionPoint.rotation.eulerAngles.z, Vector3.forward)); 
	}
}
