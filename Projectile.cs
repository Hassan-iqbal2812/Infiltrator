using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	
	public float projectileSpeed = 10, damage = 30;//Change these in inspector

	//public float duration = 1f;

	public bool belongsToPlayer; //says whether the projectile is from the player or not (I.e. who it can damage)


	public GameObject collisionParticleEffect;//The particle effect produced when the projectile hits a wall or enemy (sparks or explosion probably)

	public float areaDamageRadius = 0, radiusForMaxDamage = 0;//If the projectile has aoe damage then set this accordingly.
	bool hasAoe;
	public GameObject projectileTrail;

	public bool projectileShouldSpin;

	public int projectileBounces;
	int bouncesRemaining;
	public float lastBounceTimeoutTime;

	float nextColDelayAfterBounce = 0.1f, remainingDelayAfterBounce;//In case a bouncing shot hits two coliders at once, it should still bounce
	bool hasHitNonBouncable;

	public float expirationTime = 100f;
	float timePast;

	// Use this for initialization
	void Start () {
		if (areaDamageRadius != 0) {
			hasAoe = true;
		}

		bouncesRemaining = projectileBounces;

		gameObject.GetComponent<Rigidbody2D> ().velocity += new Vector2 (projectileSpeed * transform.up.x, projectileSpeed * transform.up.y);
	}

	void Update () {
		if (remainingDelayAfterBounce > 0)
			remainingDelayAfterBounce -= Time.deltaTime;

		if (projectileShouldSpin)
			transform.Rotate (Vector3.forward, 800 * Time.deltaTime);

		timePast += Time.deltaTime;

		if (timePast > expirationTime)
			destroy ();

		//gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (projectileSpeed * transform.up.x * Time.deltaTime, projectileSpeed * transform.up.y * Time.deltaTime);

	}


	//This stuff didn't work nicely, so I'm no longer using trigger boxes for projectiles, just regular boxes
	/*
	void OnTriggerEnter2D (Collider2D hit) {//When the projectile enters a collider with trigger area ticked
		//Debug.Log ("collision");

		if (belongsToPlayer && hit.gameObject.tag == "Enemy") {//All enemies have the enemy tag

			hit.gameObject.GetComponent<Health> ().TakeDamage (damage);//Call the taje damage method for the enemy

		} else if (!belongsToPlayer && hit.gameObject.tag == "Player") {
			hit.gameObject.GetComponent<Health> ().TakeDamage (damage);//Call the taje damage method for the player
		} 

		//Debug.Log (hit.bounds.ClosestPoint(transform.position) - hit.gameObject.transform.position);
		//Vector3 normal = hit.bounds.ClosestPoint (transform.position) - hit.gameObject.transform.position;

		//Debug.Log (GetPointOfContact().normal);

		Vector3 normal = GetPointOfContact ().normal;
		normal.z = 0;


		//Vector3 normal = hit.contacts[0].normal;
		//Quaternion rot = Quaternion.LookRotation (normal);

		//Debug.Log (normal);

		//Instantiate (collisionParticleEffect, transform.position, rot);

	}


	private RaycastHit GetPointOfContact()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit))
		{ 
			Debug.Log (hit.normal);
			return hit;
		}
		return hit;
	}*/


	//works but not with trigger
	void OnCollisionEnter2D (Collision2D hit) {
		hasHitNonBouncable = false;

		//Aoe stuff
		if (hasAoe && belongsToPlayer) {//Checks if the projectile is from the player and has aoe

			GameObject[] Enemies = GameObject.FindGameObjectsWithTag ("Enemy");//Find all enemies in the game

			foreach (GameObject enemy in Enemies) {//Search through all enemies

				float distance = Vector3.Distance (transform.position, enemy.transform.position);//Find the distance between the projectile and the enemies

				if (distance < areaDamageRadius) {//If the target is within AOE range

					//Work out damage here!
					if (distance < radiusForMaxDamage) {
						enemy.gameObject.GetComponent<Health> ().TakeDamage (damage, transform.position);//Take full damage
					} else {
						//Take proportional to distance, where a small distance equals nearly full damage, and a large distance is nearly 0 damage
						enemy.gameObject.GetComponent<Health> ().TakeDamage (damage * (1 - (distance / areaDamageRadius)), transform.position);
					}
				}
			}

			Enemies = GameObject.FindGameObjectsWithTag ("LaserImmuneEnemy");//Find all enemies in the game

			foreach (GameObject enemy in Enemies) {//Search through all enemies

				float distance = Vector3.Distance (transform.position, enemy.transform.position);//Find the distance between the projectile and the enemies

				if (distance < areaDamageRadius) {//If the target is within AOE range

					//Work out damage here!
					if (distance < radiusForMaxDamage) {
						enemy.gameObject.GetComponent<Health> ().TakeDamage (damage, transform.position);//Take full damage
					} else {
						//Take proportional to distance, where a small distance equals nearly full damage, and a large distance is nearly 0 damage
						enemy.gameObject.GetComponent<Health> ().TakeDamage (damage * (1 - (distance / areaDamageRadius)), transform.position);
					}
				}
			}

		} else if (belongsToPlayer && (hit.gameObject.tag == "Enemy" || hit.gameObject.tag == "LaserImmuneEnemy") ) {//Checks for player hitting enemies. All enemies have the enemy tag
			hasHitNonBouncable = true;
			hit.gameObject.GetComponent<Health> ().TakeDamage (damage, transform.position);//Call the take damage method for the enemy
			
		} else if (!belongsToPlayer && hit.gameObject.tag == "Player") {//Checks for enemies hitting player
			hasHitNonBouncable = true;
			hit.gameObject.GetComponent<Health> ().TakeDamage (damage, transform.position);//Call the take damage method for the player
			
		} else if (bouncesRemaining > 0) {//If the projectile should bounce

			//transform.rotation = Quaternion.AngleAxis(180 - 90 - (Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg) - /*(Mathf.Atan2(hit.contacts [0].normal.y, hit.contacts [0].normal.x) * Mathf.Rad2Deg)*/, Vector3.forward);

			//transform.rotation = Quaternion.AngleAxis((180 -(Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg))+ 2 *(Mathf.Atan2(hit.contacts [0].normal.y, hit.contacts [0].normal.x) * Mathf.Rad2Deg) -90, Vector3.forward);

			//gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (projectileSpeed * transform.up.x, projectileSpeed * transform.up.y);

			bouncesRemaining--;


			remainingDelayAfterBounce = nextColDelayAfterBounce;

			if (bouncesRemaining <= 0)
				Invoke ("destroy", lastBounceTimeoutTime);

			return;
		} 

		if (hasHitNonBouncable || remainingDelayAfterBounce <= 0) {
			//Produces sparks/explosions when the projectile hits anything. The effect is rotated to the surface normal, which is important for sparks
			Vector3 normal = hit.contacts [0].normal;
			Quaternion rot = Quaternion.FromToRotation (Vector3.up, normal);
			Instantiate (collisionParticleEffect, transform.position, rot);

			if (projectileTrail != null)
				projectileTrail.transform.parent = null;
		
			//projectileTrail.GetComponent<Destroyer> ().Invoke ("SelfDestruct", 1f); //Not necessary

			Destroy (gameObject);
		}
	}

	void destroy () {
		Instantiate (collisionParticleEffect, transform.position, transform.rotation);
		Destroy (gameObject);
	}

}
