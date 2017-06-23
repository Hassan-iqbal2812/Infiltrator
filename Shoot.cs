using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject plasmaShot, plasmaShotBarrelFlashEffect;

	public float plasmaShotCooldown; //how long is the cooldown after each shot for this weapon

    public float plasamaShotOriginalDamage = 30f, plasamaShotUpgradedDamage; //The damage of the projectile when it has been upgraded (set using a new varible to fix bug)

	float plasmaShotCooldownRemaining;//the remaining cooldown time for this weapon

	public GameObject rocket;
	public float rocketCooldown = 1f; //how long is the cooldown after each shot for this weapon
	float rocketCooldownRemaining;//the remaining cooldown time for this weapon
    float rocketOriginalSpeed = 20, rocketUpgradedSpeed = 40;
    float rocketOriginalBlastRadius = 5, rocketUpgradedBlastRadius = 7.5f, rocketLastUpgradeBlastRadius = 11.25f;
    float rocketOriginalDamage = 50, rocketUpgradedDamage = 75;


	public Transform leftCannonEmitPos, rightCannonEmitPos, leftRocketEmitPos, rightRocketEmitPos; //Points attacked to the ship which mark where projectiles spawn

	bool fireLeftCannonNext;

	//public Button shootButton;

	bool shootButtonPressed;

	public VirtualJoyStick directionShootingJoystick;//Used for checking direction of ship vs analog

	// Use this for initialization
	void Start () {
        //If no upgrades unlocked
        plasmaShot.gameObject.GetComponent<Projectile>().damage = plasamaShotOriginalDamage;
        plasmaShot.gameObject.GetComponent<Projectile>().projectileBounces = 0;
        plasmaShot.gameObject.GetComponent<Projectile>().lastBounceTimeoutTime = 0;

        //Apply starting upgrades here
        //If the first cannon upgrade is unlocked
        if (PlayerPrefs.GetInt("cannonUpgradeLevel") >= 1)
        {
            plasmaShotCooldown *= 0.7f; //30% increased fire rate
        }
        //If the second cannon upgrade is unlocked
        if (PlayerPrefs.GetInt("cannonUpgradeLevel") >= 2)
        {
            plasmaShot.gameObject.GetComponent<Projectile>().damage = plasamaShotUpgradedDamage;//Stops the projectile getting stronger on each play, like it did before
        }
        //If the third cannon upgrade is unlocked
        if (PlayerPrefs.GetInt("cannonUpgradeLevel") >= 3)
        {
            plasmaShot.gameObject.GetComponent<Projectile>().projectileBounces = 1;
            plasmaShot.gameObject.GetComponent<Projectile>().lastBounceTimeoutTime = 1.3f;
        }
        //If no upgrades unlocked
        rocket.gameObject.GetComponent<Projectile>().areaDamageRadius = rocketOriginalBlastRadius;
        rocket.gameObject.GetComponent<Projectile>().projectileSpeed = rocketOriginalSpeed;
        rocket.gameObject.GetComponent<Projectile>().damage = rocketOriginalDamage;
        //If the first rocket upgrade is unlocked
        if (PlayerPrefs.GetInt("rocketUpgradeLevel") >= 1)
        {
            rocket.gameObject.GetComponent<Projectile>().areaDamageRadius = rocketUpgradedBlastRadius;
        }
        //If the second rocket upgrade is unlocked
        if (PlayerPrefs.GetInt("rocketUpgradeLevel") >= 2)
        {
            rocketCooldown *= 0.7f;
        }
        //If the third rocket upgrade is unlocked
        if (PlayerPrefs.GetInt("rocketUpgradeLevel") >= 3)
        {
            rocket.gameObject.GetComponent<Projectile>().projectileSpeed = rocketUpgradedSpeed;
            rocket.gameObject.GetComponent<Projectile>().areaDamageRadius = rocketLastUpgradeBlastRadius;
            rocket.gameObject.GetComponent<Projectile>().damage = rocketUpgradedDamage;
        }
	}
	
	// Update is called once per frame
	void Update () {
		plasmaShotCooldownRemaining -= Time.deltaTime;//Reduce the remaining cooldown time
		rocketCooldownRemaining -= Time.deltaTime;

		if (shootButtonPressed || Input.GetKeyDown(KeyCode.Space)) {
			ShootGuns ();
		}
	} 


	public void startShooting () { 
		shootButtonPressed = true;
	}

	public void stopShooting () { 
		shootButtonPressed = false;
	}

	public void ShootGuns()
	{
		
		if (plasmaShotCooldownRemaining <= 0) {

			if (fireLeftCannonNext) {
				GameObject newProjectile = Instantiate (plasmaShot, leftCannonEmitPos.position, transform.rotation) as GameObject;//Saves the projectile after instantiation
				Vector2 myVelocity = gameObject.GetComponent<Rigidbody2D> ().velocity;//Gets our current velocity
				newProjectile.GetComponent<Rigidbody2D> ().velocity = new Vector2 (((myVelocity.y * transform.up.y + myVelocity.x * transform.up.x) * newProjectile.transform.up.x) / 3, ((myVelocity.y * transform.up.y + myVelocity.x * transform.up.x) * newProjectile.transform.up.y) / 3);

				GameObject barrelEffect = Instantiate (plasmaShotBarrelFlashEffect, leftCannonEmitPos.position, transform.rotation) as GameObject;
				barrelEffect.transform.SetParent (gameObject.transform);

			} else {
				GameObject newProjectile = Instantiate (plasmaShot, rightCannonEmitPos.position, transform.rotation) as GameObject;//Saves the projectile after instantiation
				Vector2 myVelocity = gameObject.GetComponent<Rigidbody2D> ().velocity;//Gets our current velocity
				newProjectile.GetComponent<Rigidbody2D> ().velocity = new Vector2 (((myVelocity.y * transform.up.y + myVelocity.x * transform.up.x) * newProjectile.transform.up.x) / 3, ((myVelocity.y * transform.up.y + myVelocity.x * transform.up.x) * newProjectile.transform.up.y) / 3);

				GameObject barrelEffect = Instantiate (plasmaShotBarrelFlashEffect, rightCannonEmitPos.position, transform.rotation) as GameObject;
				barrelEffect.transform.SetParent (gameObject.transform);
			}

			fireLeftCannonNext = !fireLeftCannonNext;
			plasmaShotCooldownRemaining = plasmaShotCooldown;//Reset the cooldown

		} 

		//Only fires rockests if the ship is pointing in the target direction, and the user has move the analog more than 20% in any direction
		if (rocketCooldownRemaining <= 0 && Vector2.Angle (transform.up, new Vector2 (directionShootingJoystick.Horizontal (), directionShootingJoystick.Vertical ())) < 10 && new Vector2 (directionShootingJoystick.Horizontal (), directionShootingJoystick.Vertical ()).magnitude > 0.2f) {

			GameObject newProjectile = Instantiate (rocket, leftRocketEmitPos.position, transform.rotation) as GameObject;//Saves the projectile after instantiation
			Vector2 myVelocity = gameObject.GetComponent<Rigidbody2D> ().velocity;//Gets our current velocity
			newProjectile.GetComponent<Rigidbody2D> ().velocity = new Vector2 (((myVelocity.y * transform.up.y + myVelocity.x * transform.up.x) * newProjectile.transform.up.x) / 3, ((myVelocity.y * transform.up.y + myVelocity.x * transform.up.x) * newProjectile.transform.up.y) / 3);

			newProjectile = Instantiate (rocket, rightRocketEmitPos.position, transform.rotation) as GameObject;//Saves the projectile after instantiation
			newProjectile.GetComponent<Rigidbody2D> ().velocity = new Vector2 (((myVelocity.y * transform.up.y + myVelocity.x * transform.up.x) * newProjectile.transform.up.x) / 3, ((myVelocity.y * transform.up.y + myVelocity.x * transform.up.x) * newProjectile.transform.up.y) / 3);

			rocketCooldownRemaining = rocketCooldown;//Reset the cooldown

		} else if (rocketCooldownRemaining <= 0 && Input.GetMouseButton (0)) {//Mouse control

			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 mouseRelativePos = new Vector2 (mouseWorldPos.x - transform.position.x, mouseWorldPos.y - transform.position.y); 
			if (Vector2.Angle (transform.up, new Vector2 (mouseRelativePos.x, mouseRelativePos.y)) < 10f) {


				GameObject newProjectile = Instantiate (rocket, leftRocketEmitPos.position, transform.rotation) as GameObject;//Saves the projectile after instantiation
				Vector2 myVelocity = gameObject.GetComponent<Rigidbody2D> ().velocity;//Gets our current velocity
				newProjectile.GetComponent<Rigidbody2D> ().velocity = new Vector2 (((myVelocity.y * transform.up.y + myVelocity.x * transform.up.x) * newProjectile.transform.up.x) / 3, ((myVelocity.y * transform.up.y + myVelocity.x * transform.up.x) * newProjectile.transform.up.y) / 3);

				newProjectile = Instantiate (rocket, rightRocketEmitPos.position, transform.rotation) as GameObject;//Saves the projectile after instantiation
				newProjectile.GetComponent<Rigidbody2D> ().velocity = new Vector2 (((myVelocity.y * transform.up.y + myVelocity.x * transform.up.x) * newProjectile.transform.up.x) / 3, ((myVelocity.y * transform.up.y + myVelocity.x * transform.up.x) * newProjectile.transform.up.y) / 3);

				rocketCooldownRemaining = rocketCooldown;//Reset the cooldown
			}
		}
	}
}
