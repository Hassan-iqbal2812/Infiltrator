using UnityEngine;
using System.Collections;

public class Boss1AI : MonoBehaviour {

	public GameObject leftTurret, rightTurret, mouthLaser;

	public float sprinklarAngleToCover, sprinklarDuration;
	public int numberOfSprinklarShots;

	public float waitTimeAfterSprinklarAttack, waitTimeAfterLaserAttack;
	float remainingWaitForNextAttack;

	float sprinklarShotCooldown;
	float startAngle;
	float angleStep;

	bool sprinklarAttackNext = true;

	float leftTurretCurrentAngle, rightTurretCurrentAngle;

	public float activationDistance;

	public DoorOpener jawDoorScript;

	// Use this for initialization
	void Start () {
		remainingWaitForNextAttack = 2f;
	}
	
	// Update is called once per frame
	void Update () {

		remainingWaitForNextAttack -= Time.deltaTime;//Designated wait time after last attack is counted down

		float distance = Vector2.Distance (transform.position, GameObject.Find ("Player").transform.position);

		if (distance <= activationDistance) {

			if (sprinklarAttackNext && remainingWaitForNextAttack <= 0) {
				leftTurret.GetComponent<SprinklarTurret> ().startSprinklarTurretSequence (sprinklarAngleToCover, true, numberOfSprinklarShots, sprinklarDuration);
				rightTurret.GetComponent<SprinklarTurret> ().startSprinklarTurretSequence (sprinklarAngleToCover, false, numberOfSprinklarShots, sprinklarDuration);

				sprinklarAttackNext = false;

				remainingWaitForNextAttack = waitTimeAfterSprinklarAttack;
			} else if (remainingWaitForNextAttack <= 0) { //else use the laser
				//do attack?

				jawDoorScript.doorIsOpen = true;

				Invoke ("startMouthLaser", 1f);
				Invoke ("stopMouthLaser", 4f);
				Invoke ("closeMouth", 3.5f);

				sprinklarAttackNext = true;

				remainingWaitForNextAttack = waitTimeAfterLaserAttack;
			} 
		}

	}

	void startMouthLaser() {
		mouthLaser.SetActive (true);
	}

	void stopMouthLaser() {
		mouthLaser.SetActive (false);
	}

	void closeMouth() {
		jawDoorScript.doorIsOpen = false;
	}


}
