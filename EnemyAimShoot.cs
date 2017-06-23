using UnityEngine;
using System.Collections;

public class EnemyAimShoot: MonoBehaviour {

	public GameObject projectile, barrelFlashEffect;//Assign a prefabricated projectile (with the projectile script attached) via the inspector 
	public float maxShotDistance = 20f, maxShotAngle = 10f, shotCooldown = 0.5f;  

	public float percentageRandomCooldownChange = 0;

	float shotCooldownRemaining;


	//Used to make shots come from right placed (added by Ben in V6)
	public Transform ShotEmitPos1, ShotEmitPos2;

	bool fireFromEmitPos1Next = true;

	bool hasTwoEmitPoints;

	public LayerMask visionMask;

	//public bool enemySighted;

	GameObject player;


	//Stuff from drone ai
	public float rotationSpeed = 90f;

	public bool faceMoveDirIfNoSight;

	public bool shouldLeadShots;
	public float shotSpeedToUseForShotLeading = 25;



	void Start() {
		if (ShotEmitPos2 != null) {
			hasTwoEmitPoints = true;

			//50% chance to change first shot barrel, useful to make drones sync up less
			if (Random.Range (0, 2) == 0)
				fireFromEmitPos1Next = false;
		}

		player = GameObject.FindGameObjectWithTag("Player");

		shotCooldownRemaining = shotCooldown;
	}

	// Update is called once per frame
	void Update () {
		shotCooldownRemaining -= Time.deltaTime;//Reduce the remaining cooldown time

		RaycastHit2D ray = Physics2D.Linecast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (player.transform.position.x, player.transform.position.y), visionMask);
		Vector3 relativePos = GameObject.FindGameObjectWithTag ("Player").transform.position - transform.position;//Get the relative position of the player from the enemy


		if (ray.collider.gameObject.tag == "Player") {//If there is vision to the player

			if (relativePos.magnitude < maxShotDistance) {//If the player is within shooting distance


				if (shouldLeadShots) {//Change the rotation based on whether or not shot leading is on
					Vector3 shotOffset = new Vector3 (player.GetComponent<Rigidbody2D> ().velocity.x * (Mathf.Clamp(relativePos.magnitude, 1f, 50f) / shotSpeedToUseForShotLeading * 1.2f), 
						player.GetComponent<Rigidbody2D> ().velocity.y * ((Mathf.Clamp(relativePos.magnitude, 1f, 50f) / shotSpeedToUseForShotLeading * 1.2f)), 0);

					relativePos += shotOffset;
				}

				//Rotate towards the player
				float angle = (Mathf.Atan2 (relativePos.y, relativePos.x)) * Mathf.Rad2Deg - 90f;
				transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.AngleAxis (angle, Vector3.forward), rotationSpeed * Time.deltaTime);


				if (shotCooldownRemaining <= 0) {//Checks if the cooldown has been met
					

					//Finds the angle of the player from the enemy, measured from the enemy's nose (it returns a value from -180 to +180, where 0 means the player 
					//is directly in front of the player.
					//float relativeAngle = (Mathf.Atan2 (relativePos.y, relativePos.x)) * Mathf.Rad2Deg;

					float relativeAngle = Vector2.Angle (transform.up, relativePos);

					//Checks the angle is within range an
					if (Mathf.Abs (relativeAngle) < maxShotAngle) {

						if (fireFromEmitPos1Next) {
							Instantiate (projectile, ShotEmitPos1.position, transform.rotation);

							GameObject barrelEffect = Instantiate (barrelFlashEffect, ShotEmitPos1.position, transform.rotation) as GameObject;
							barrelEffect.transform.SetParent (gameObject.transform);
						} else {
							Instantiate (projectile, ShotEmitPos2.position, transform.rotation);

							GameObject barrelEffect = Instantiate (barrelFlashEffect, ShotEmitPos2.position, transform.rotation) as GameObject;
							barrelEffect.transform.SetParent (gameObject.transform);
						}

						if (hasTwoEmitPoints)//Swaps gun only if the enemy has two guns to shoot from
						fireFromEmitPos1Next = !fireFromEmitPos1Next;

						shotCooldownRemaining = shotCooldown + Random.Range (-shotCooldown * percentageRandomCooldownChange, shotCooldown * percentageRandomCooldownChange);//Reset the cooldown
					}
				}
			}
		} else if (faceMoveDirIfNoSight) {

			EnemyAi myPathfinder = gameObject.GetComponent<EnemyAi> ();
			myPathfinder.rotateToPath (rotationSpeed);
		}
	}
}
