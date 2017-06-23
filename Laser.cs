using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	public float damage = 4f, repreatDamageCooldown = 0.2f;
	float damageCooldownRemaining;


	public float laserMaxLenth = 1;

	public SpriteRenderer myRenderer; 

	public float yOffsetRatePerFrame;

	float yOffset;

	float originalTiling;

	public LayerMask maskForLaser;

	public bool requriesHitToAcivate = false;
	public bool requiresHitToBeginBurst = false;

	public bool firesInBursts;
	public float burstDuration, burstCooldown;
	float burstCooldownRemaining;

	public int slowStacksOnHit = 0;
	public float slowStackDuration = 0;

	bool burstResetInProgress;

	public bool alsoDamagesEnemies = false;

	// Use this for initialization
	void Start () {
		originalTiling = myRenderer.material.mainTextureScale.y;
	}

	// Update is called once per frame
	void Update () {
		//Cooldown for damage
		damageCooldownRemaining -= Time.deltaTime;
		burstCooldownRemaining -= Time.deltaTime;

		//offset for animation effect
		yOffset += yOffsetRatePerFrame * (Time.deltaTime * 60);


		myRenderer.material.mainTextureOffset = new Vector2 (myRenderer.material.mainTextureOffset.x, yOffset);

		//This may be needed to stop the number getting too big, but that will probably never happen
		if (yOffset > 100000)
			yOffset = 0;

		if (burstCooldownRemaining <= 0 || !firesInBursts) {//If burst fire is enabled, the burst cooldown is checked
			
			RaycastHit2D ray = Physics2D.Linecast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (transform.position.x + transform.up.x * laserMaxLenth, transform.position.y + transform.up.y * laserMaxLenth), maskForLaser);


			if (ray.collider != null) {

				//The first time the ray hits something, the burst will begin and a burst reset is invoked
				if (firesInBursts && !burstResetInProgress) {
					Invoke ("finishBurst", burstDuration);
					burstResetInProgress = true;

					if (requiresHitToBeginBurst)
						requriesHitToAcivate = false;
				}

				myRenderer.enabled = true;
				gameObject.transform.GetChild (0).gameObject.SetActive (true);//Hides the laser tip


				//stretch to right size
				transform.localScale = new Vector3 (transform.localScale.x, ray.distance);
				myRenderer.material.mainTextureScale = new Vector2 (myRenderer.material.mainTextureScale.x, originalTiling * ray.distance);

				if ((ray.collider.gameObject.tag == "Player" || (ray.collider.gameObject.tag == "Enemy" && alsoDamagesEnemies)) && damageCooldownRemaining <= 0) {//Here, the line cast works to deal damage

					ray.collider.gameObject.GetComponent<Health> ().TakeDamage (damage, transform.GetChild (0).transform.position);//Call the take damage method

					if (slowStacksOnHit != 0) {
						ray.collider.gameObject.GetComponent<MoveShip> ().addFrostStack (slowStacksOnHit, slowStackDuration);
					}

					damageCooldownRemaining = repreatDamageCooldown;
				}

			} else if (!requriesHitToAcivate) {
				transform.localScale = new Vector3 (transform.localScale.x, laserMaxLenth, 1);
				myRenderer.material.mainTextureScale = new Vector2 (myRenderer.material.mainTextureScale.x, originalTiling * laserMaxLenth);

				//If the laser doesn't require a target to hit, the burst still has a duration.
				if (firesInBursts && !burstResetInProgress) {
					Invoke ("finishBurst", burstDuration);
					burstResetInProgress = true;

				}

			} else {

				myRenderer.enabled = false;
				gameObject.transform.GetChild (0).gameObject.SetActive (false);//Hides the laser tip
			}

		} else {

			myRenderer.enabled = false;
			gameObject.transform.GetChild (0).gameObject.SetActive (false);//Hides the laser tip
		}
	}

	void finishBurst () {
		burstCooldownRemaining = burstCooldown;
		burstResetInProgress = false;

		if (requiresHitToBeginBurst)
			requriesHitToAcivate = true;
	}

	/*
	void OnTriggerStay2D (Collider2D hit) {//When the projectile enters a collider with trigger area ticked
		
		if (hit.gameObject.tag == "Player" && damageCooldownRemaining <= 0) {//All enemies have the enemy tag
			
			hit.gameObject.GetComponent<Health> ().TakeDamage (damage, transform.GetChild(0).transform.position);//Call the take damage method for the enemy

			damageCooldownRemaining = repreatDamageCooldown;
		}
	}*/
}
