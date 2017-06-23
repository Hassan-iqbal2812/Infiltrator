using UnityEngine;
//using UnityEngine.SceneManagement;
using System.Collections;

public class Health : MonoBehaviour {

	public float maxHealth = 100, hardHealthMult = 1, masterHealthMult = 1; //Change this from the inspector

	float currentHealth; //Keeps track of current health

	bool hasShield;
	public float maxShield, hardShieldMult = 1, masterShieldMult = 1;

	public float shieldFullRechargeTime, rechargeDelayAfterHit;//How long it takes to fully recharge shield, and how long after taking dmg to wait before recharge starts
	public GameObject shieldHitEffect;


	float currentShield, rechargeDelayRemaining;

	public AudioSource damageNoise, shieldDamageNoise;

	public GameObject deathEffect;

    public int scoreValue = 10;

	public GameObject playerSummaryScreen;

	public bool isLastBoss;

	public bool invincible;

	public TimerScore playerTimerAndScore;

	public bool hasHealthRegenFromDestroyingEnemies;

	// Use this for initialization
	void Start () {
        
		if (gameObject.tag == "Player") {

			//do upgrades for the player

			//If first hull upgrade is unlocked
			if (PlayerPrefs.GetInt ("hullUpgradeLevel") >= 1) {
				maxHealth *= 1.4f;
			}
			//If second hull upgrade is unlocked
			if (PlayerPrefs.GetInt ("hullUpgradeLevel") >= 2) {
				hasHealthRegenFromDestroyingEnemies = true;
			}
			//If third hull upgrade is unlocked
			if (PlayerPrefs.GetInt ("hullUpgradeLevel") >= 3) {

			}
			//If first shield upgrade is unlocked
			if (PlayerPrefs.GetInt ("shieldUpgradeLevel") >= 1) {
				maxShield *= 1.4f;
			}
			//If second shield upgrade is unlocked
			if (PlayerPrefs.GetInt ("shieldUpgradeLevel") >= 2) {
				shieldFullRechargeTime *= 0.7f;
			}
			//If third shield upgrade is unlocked
			if (PlayerPrefs.GetInt ("shieldUpgradeLevel") >= 3) {

			}
		}

		//Apply difficulty changes to player and enemies here (reducing health and shield simulates enemies doing more damage to player
		//Health and shield is increased for enemies to make them stronger
		switch (PlayerPrefs.GetInt("Difficulty")) {
		//Nothing is changed in normal mode
		case 2: 
			maxHealth *= hardHealthMult;
			maxShield *= hardShieldMult;
			break;
		case 3: 
			maxHealth *= masterHealthMult;
			maxShield *= masterShieldMult;
			break;
		}

		currentHealth = maxHealth;

		if (maxShield != 0) {
			currentShield = maxShield;
			hasShield = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(GameObject.FindGameObjectsWithTag("Player").Length);
		if (hasShield) {
			rechargeDelayRemaining -= Time.deltaTime;

			if (rechargeDelayRemaining <= 0 && currentShield < maxShield) {

				currentShield += (Time.deltaTime / shieldFullRechargeTime) * maxShield;//Restores a portion of your shield based on time past, and the full recharge time

				if (currentShield > maxShield)//On a slow frame, the current health can be increased past its max limit by a fair amount. This prevents that from happening
					currentShield = maxShield;
			}
		}
	}

	public void TakeDamage (float damageAmount) { //Find this script on hit object and call this method, passing damage

		if (hasShield && currentShield > 0) {//If I have shield that isn't emprty

			shieldDamageNoise.Play ();

			if (currentShield > damageAmount) {//If I have more shield than the damage taken,
				currentShield -= damageAmount;//then just subtract damage from shield
				damageAmount = 0;//All the damage was dealt, so 0 damage will hit the hull

			} else {//Else. the damage is more than my shield
				damageAmount -= currentShield;//The remaining shield is used to mitigate the damage
				currentShield = 0;//The remaining shield is broken, and is now empty
			}
		} else {
			damageNoise.Play ();
		}

		currentHealth -= damageAmount;



		if (currentHealth <= 0 && !invincible) {//Death check
			Instantiate (deathEffect, transform.position, Quaternion.identity);
			//transform.FindChild ("MyExplosion(Clone)").transform.SetParent(GameObject.Find ("Player").transform);

			if (gameObject.tag == "Player") {
				Invoke ("displaySummary", 1f);//Death sequence goes here, maybe also unlock for arcade mode difficulty

				playerTimerAndScore.stopCalculatingScoreAndTime();//The boss is defeated so leave the right score and time on screen
			}
            else 
            { 
				PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + scoreValue);

				if (isLastBoss) {
					Invoke ("displaySummary", 1.8f);
					Health playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<Health> ();
					playerHealth.invincible = true;

					playerTimerAndScore.stopCalculatingScoreAndTime();//The boss is defeated so leave the right score and time on screen

                    if (PlayerPrefs.GetString("CurrentLevel") == "Level1" && PlayerPrefs.GetInt("Difficulty") == 1)
                    {
                        PlayerPrefs.SetInt("Level1UnlockedDifficulty", 2);
                    }
                    if (PlayerPrefs.GetString("CurrentLevel") == "Level1" && PlayerPrefs.GetInt("Difficulty") == 2)
                    {
                        PlayerPrefs.SetInt("Level1UnlockedDifficulty", 3);
                    }
                    if (PlayerPrefs.GetString("CurrentLevel") == "Level2" && PlayerPrefs.GetInt("Difficulty") == 1)
                    {
                        PlayerPrefs.SetInt("Level2UnlockedDifficulty", 2);
                    }
                    if (PlayerPrefs.GetString("CurrentLevel") == "Level2" && PlayerPrefs.GetInt("Difficulty") == 2)
                    {
                        PlayerPrefs.SetInt("Level2UnlockedDifficulty", 3);
                    }
                    if (PlayerPrefs.GetString("CurrentLevel") == "Level3" && PlayerPrefs.GetInt("Difficulty") == 1)
                    {
                        PlayerPrefs.SetInt("Level3UnlockedDifficulty", 2);
                    }
                    if (PlayerPrefs.GetString("CurrentLevel") == "Level3" && PlayerPrefs.GetInt("Difficulty") == 2)
                    {
                        PlayerPrefs.SetInt("Level3UnlockedDifficulty", 3);
                    }
				} else {

					if (GameObject.FindGameObjectWithTag ("Player").GetComponent<Health> ().hasHealthRegenFromDestroyingEnemies) {
						GameObject.FindGameObjectWithTag ("Player").GetComponent<Health> ().TakeDamage (-100);
					}
					GameObject.FindGameObjectWithTag ("Player").GetComponent<Health> ().TakeDamage (-100);
					Destroy (gameObject);
				}
           }
        }

		if (hasShield) {
			rechargeDelayRemaining = rechargeDelayAfterHit; //Start a delay for shield recharging
		}
	}


	public void TakeDamage (float damageAmount, Vector3 hitPosition) {//Overload with a transform

		if (hasShield && currentShield > 0) {//If I have shield that isn't emprty

			shieldDamageNoise.Play ();

			//Make the shield hit effect, if one is available
			if (shieldHitEffect != null) {
				GameObject effect = Instantiate (shieldHitEffect, hitPosition, Quaternion.FromToRotation (Vector3.up, new Vector3 (hitPosition.x - transform.position.x, hitPosition.y - transform.position.y, 0))) as GameObject;
				effect.transform.parent = transform;//make the hit effect follow the player
			}

			if (currentShield > damageAmount) {//If I have more shield than the damage taken,
				currentShield -= damageAmount;//then just subtract damage from shield
				damageAmount = 0;//All the damage was dealt, so 0 damage will hit the hull

			} else {//Else. the damage is more than my shield
				damageAmount -= currentShield;//The remaining shield is used to mitigate the damage
				currentShield = 0;//The remaining shield is broken, and is now empty
			}
		} else {
			damageNoise.Play ();
		}

		currentHealth -= damageAmount;

		if (currentHealth <= 0 && !invincible) {//Death check
			Instantiate (deathEffect, transform.position, Quaternion.identity);
			//transform.FindChild ("MyExplosion(Clone)").transform.SetParent(GameObject.Find ("Player").transform);

			if (gameObject.tag == "Player") {
				Invoke ("displaySummary", 1f);//Death sequence goes here

				playerTimerAndScore.stopCalculatingScoreAndTime();//The boss is defeated so leave the right score and time on screen
			}
			else 
			{ 
				PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + scoreValue);

				if (isLastBoss) {
					Invoke ("displaySummary", 1.8f);
					Health playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<Health> ();
					playerHealth.invincible = true;

					playerTimerAndScore.stopCalculatingScoreAndTime();//The boss is defeated so leave the right score and time on screen

                    if (PlayerPrefs.GetString("CurrentLevel") == "Level1" && PlayerPrefs.GetInt("Difficulty") == 1)
                    {
                        PlayerPrefs.SetInt("Level1UnlockedDifficulty", 2);
                    }
                    if (PlayerPrefs.GetString("CurrentLevel") == "Level1" && PlayerPrefs.GetInt("Difficulty") == 2)
                    {
                        PlayerPrefs.SetInt("Level1UnlockedDifficulty", 3);
                    }
                    if (PlayerPrefs.GetString("CurrentLevel") == "Level2" && PlayerPrefs.GetInt("Difficulty") == 1)
                    {
                        PlayerPrefs.SetInt("Level2UnlockedDifficulty", 2);
                    }
                    if (PlayerPrefs.GetString("CurrentLevel") == "Level2" && PlayerPrefs.GetInt("Difficulty") == 2)
                    {
                        PlayerPrefs.SetInt("Level2UnlockedDifficulty", 3);
                    }
                    if (PlayerPrefs.GetString("CurrentLevel") == "Level3" && PlayerPrefs.GetInt("Difficulty") == 1)
                    {
                        PlayerPrefs.SetInt("Level3UnlockedDifficulty", 2);
                    }
                    if (PlayerPrefs.GetString("CurrentLevel") == "Level3" && PlayerPrefs.GetInt("Difficulty") == 2)
                    {
                        PlayerPrefs.SetInt("Level3UnlockedDifficulty", 3);
                    }

				} else {
					Destroy (gameObject);
				}
			}
		}

		if (hasShield) {
			rechargeDelayRemaining = rechargeDelayAfterHit; //Start a delay for shield recharging
		}
	}


	public float getHealthPercent () {
		return currentHealth / maxHealth;
	}

	public float getShieldPercent () {
		return currentShield / maxShield;
	}

	void displaySummary () 
    {
		playerSummaryScreen.SetActive (true);
        //SceneManager.LoadScene ("MainMenu");
	}
}
