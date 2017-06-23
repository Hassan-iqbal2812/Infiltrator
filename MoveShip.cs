using UnityEngine;
using System.Collections;


public class MoveShip : MonoBehaviour {
	
	//The starting values given here are the defaults for when the scrip is first attached to an object (like if you put it on a new ship)
	//They mainly serve as a reference for what sort of values should be given.
	public float forwardAccel = 15, backwardAccel = 6, strafeAccel = 10, strafeAndReverseSpeedLimit = 12, forwardSpeedLimit = 20 /* backwardSpeedLimit = 3, 
	, strafeSpeedLimit = 7, maxSpeedMagnitude = 14*/;
	//boostAccelerationMultiplier = 1.25f, maxBoostSpeedMagnitude = 20;

	float speedLimitToUse;//Speed limit which is calculated based on ship's facing

	public float rotationSpeed;
	
	//public float maxBoostDuration = 3, fullBoostRechargeTime = 3, boostRechargeDelay = 0.5f;
	//float remainingBoostDuration, remainingBoostRechargeDelay;
	
	//bool boostButtonPressed;
	
	public AudioSource thrusterSound;
	
	//Both should probably range from about 0.5 to 1
	float thrusterIdleVolume, thrusterIdlePitch;
	
	public GameObject rightThruster, leftThruster, rightFrontThrust, leftFrontThrust;
	
	
	//Virtual joystick
	public VirtualJoyStick movementJoystick, directionShootingJoystick;
	
	Vector2 velocityToCheck; //Used as a holder for velocity, so it can be checked before being applied
	Vector2 velocityToAdd; //Another holder for velocity, after it has been checked, but not yet applied
	float angleDifference;

	float originalDrag;
	public float wallHitSlow = 1;

	Vector2 myVelocity;

	//More holders
	ParticleSystem.EmissionModule theParticleSystem;
	ParticleSystem.MinMaxCurve theParticleSystemsRateCurve;

	int frostStacks;
	float percentageFrostSlow;

	/*Vector3 curAc, zeroAc;
	float GetAxisV;
	float GetAxisH;*/
	
	
	// Use this for initialization
	void Start () {




		if (PlayerPrefs.GetInt ("realisticMovementOnBool") == 1) {
			forwardAccel = 27;
			backwardAccel = 32;
			strafeAccel = 32;
			strafeAndReverseSpeedLimit = 17;
			forwardSpeedLimit = 21;
			gameObject.GetComponent<Rigidbody2D> ().drag = 0.2f;
		} else {
			forwardAccel = 50;
			backwardAccel = 50;
			strafeAccel = 50;
			strafeAndReverseSpeedLimit = 16;
			forwardSpeedLimit = 18;
			gameObject.GetComponent<Rigidbody2D> ().drag = 2;
		}


		originalDrag = gameObject.GetComponent<Rigidbody2D> ().drag;

		//If first move upgrade is unlocked
		if (PlayerPrefs.GetInt("moveUpgradeLevel") >= 1)
		{
			forwardAccel *= 1.25f;
			backwardAccel *= 1.25f;
			strafeAccel *= 1.25f;
		}
		//If second move upgrade is unlocked
		if (PlayerPrefs.GetInt("moveUpgradeLevel") >= 2)
		{
			forwardSpeedLimit *= 1.25f;
			strafeAndReverseSpeedLimit *= 1.25f;
			//strafeSpeedLimit *= 1.4f;
		}
		//If third move upgrade is unlocked
		if (PlayerPrefs.GetInt("moveUpgradeLevel") >= 3)
		{
			
		}


		//remainingBoostDuration = maxBoostDuration;
		
		//ResetAxes ();
		
		thrusterIdleVolume = thrusterSound.volume;
		thrusterIdlePitch = thrusterSound.pitch;
	}
	
	
	
	// Update is called once per frame
	void Update () {
		
		/*//mobile input stuff
		float smooth = 1, sensV = 1, sensH = 1;
		CurAc = Vector3.Lerp(curAc, Input.acceleration-zeroAc, Time.deltaTime/smooth);
		GetAxisV = Mathf.Clamp(curAc.y * sensV, -1, 1);
		GetAxisH = Mathf.Clamp(curAc.x * sensH, -1, 1);*/

		float previousAngle = transform.rotation.eulerAngles.z;

		myVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;//Gets our current velocity, used for sound, and speed checks
		float currentSpeed = myVelocity.magnitude;

			//Analog Rotating stuff
		if (directionShootingJoystick.Vertical () < 0) {//Checks if the shoot analog/keys has negative vertial direction. If so, a negative rotation is applied (ask Ben if needed).
			transform.rotation = Quaternion.RotateTowards (
				transform.rotation, Quaternion.AngleAxis (
				-Vector2.Angle (Vector2.right, new Vector2 (directionShootingJoystick.Horizontal (), directionShootingJoystick.Vertical ())) - 90f,
				Vector3.forward),
				rotationSpeed * (Time.deltaTime * 60)  /* (1.3f - Mathf.Clamp( (myVelocity.magnitude  / maxSpeedMagnitude), 0.3f, 1f))*/  );
						
		} else if (directionShootingJoystick.Vertical () != 0 || directionShootingJoystick.Horizontal() != 0) {//Checks if the shoot analog/keys is in use in any other direction
			transform.rotation = Quaternion.RotateTowards (
				transform.rotation, Quaternion.AngleAxis (
				Vector2.Angle (Vector2.right, new Vector2 (directionShootingJoystick.Horizontal (), directionShootingJoystick.Vertical ())) - 90f,
				Vector3.forward),
				rotationSpeed * (Time.deltaTime * 60) /* (1.3f - Mathf.Clamp( (myVelocity.magnitude  / maxSpeedMagnitude), 0.3f, 1f))*/  );
					
		}  else if (movementJoystick.Vertical () < 0) {//Checks if the move analog/keys has negative vertial direction. If so, a negative rotation is applied (ask Ben if needed).
			transform.rotation = Quaternion.RotateTowards (
				transform.rotation, Quaternion.AngleAxis (
				-Vector2.Angle (Vector2.right, new Vector2 (movementJoystick.Horizontal (), movementJoystick.Vertical ())) - 90f,
				Vector3.forward),
				rotationSpeed * (Time.deltaTime * 60)/* (1.3f - Mathf.Clamp( (myVelocity.magnitude  / maxSpeedMagnitude), 0.3f, 1f))*/);
			
		} else if (movementJoystick.Vertical () != 0 || movementJoystick.Horizontal() != 0) { //Checks if the move analog/keys is in use in any other direction
			transform.rotation = Quaternion.RotateTowards (
				transform.rotation, Quaternion.AngleAxis (
				Vector2.Angle (Vector2.right, new Vector2 (movementJoystick.Horizontal (), movementJoystick.Vertical ())) - 90f,
				Vector3.forward),
				rotationSpeed  * (Time.deltaTime * 60) /* (1.3f - Mathf.Clamp( (myVelocity.magnitude  / maxSpeedMagnitude), 0.3f, 1f))*/ );
			
		}

		//Should ship also start shooting?
		if (directionShootingJoystick.Vertical () != 0 || directionShootingJoystick.Horizontal () != 0) {

			gameObject.GetComponent<Shoot> ().startShooting ();

		}
		/*//This is purely a control for PC/Mac. It checks keyboard input at the same time so it only works to shoot as you move, but doesn't work on mobile touch screens
		else if (Input.GetMouseButton (0) && (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D))) {//Mouse control for pc/mac where mouse is used to aim and shoot

			gameObject.GetComponent<Shoot> ().startShooting ();

			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 mouseRelativePos = new Vector2 (mouseWorldPos.x - transform.position.x, mouseWorldPos.y - transform.position.y); 

			if (mouseRelativePos.y > 0) {
				transform.rotation = Quaternion.RotateTowards (
					transform.rotation, Quaternion.AngleAxis (
					Vector2.Angle (Vector2.right, new Vector2 (mouseRelativePos.x, mouseRelativePos.y)) - 90f,
					Vector3.forward),
					rotationSpeed * (Time.deltaTime * 60) /* (1.3f - Mathf.Clamp( (myVelocity.magnitude  / maxSpeedMagnitude), 0.3f, 1f))*//*);
			} else {
				transform.rotation = Quaternion.RotateTowards (
					transform.rotation, Quaternion.AngleAxis (
					-Vector2.Angle (Vector2.right, new Vector2 (mouseRelativePos.x, mouseRelativePos.y)) - 90f,
					Vector3.forward),
					rotationSpeed * (Time.deltaTime * 60) /* (1.3f - Mathf.Clamp( (myVelocity.magnitude  / maxSpeedMagnitude), 0.3f, 1f))*//*);
			}
		}*/ else {
			gameObject.GetComponent<Shoot> ().stopShooting ();//As the shoot analog wasn't in use, don't shoot
		}





		//Zero all the thrusters
		theParticleSystem = rightThruster.GetComponent<ParticleSystem> ().emission;
		theParticleSystemsRateCurve = theParticleSystem.rate;
		theParticleSystemsRateCurve.constantMax = 0;
		theParticleSystem.rate = theParticleSystemsRateCurve;

		theParticleSystem = leftThruster.GetComponent<ParticleSystem> ().emission;
		theParticleSystemsRateCurve = theParticleSystem.rate;
		theParticleSystemsRateCurve.constantMax = 0;
		theParticleSystem.rate = theParticleSystemsRateCurve;

		//Increase particles to front thrusts
		theParticleSystem = rightFrontThrust.GetComponent<ParticleSystem>().emission ;
		theParticleSystemsRateCurve = theParticleSystem.rate;
		theParticleSystemsRateCurve.constantMax  = 0;
		theParticleSystem.rate = theParticleSystemsRateCurve;

		theParticleSystem = leftFrontThrust.GetComponent<ParticleSystem>().emission ;
		theParticleSystemsRateCurve = theParticleSystem.rate;
		theParticleSystemsRateCurve.constantMax = 0;
		theParticleSystem.rate = theParticleSystemsRateCurve;
		//All thrusters are now zeroed

		float newAngle = transform.rotation.eulerAngles.z;

		float rotationAmount = newAngle - previousAngle;

		if (rotationAmount > 0) {
			//Increase particles to rear thrusts
			theParticleSystem = rightThruster.GetComponent<ParticleSystem> ().emission;
			theParticleSystemsRateCurve = theParticleSystem.rate;
			theParticleSystemsRateCurve.constantMax = 100;
			theParticleSystem.rate = theParticleSystemsRateCurve;

			theParticleSystem = leftFrontThrust.GetComponent<ParticleSystem>().emission ;
			theParticleSystemsRateCurve = theParticleSystem.rate;
			theParticleSystemsRateCurve.constantMax = 100;
			theParticleSystem.rate = theParticleSystemsRateCurve;
		} else if (rotationAmount < 0) {
			theParticleSystem = leftThruster.GetComponent<ParticleSystem> ().emission;
			theParticleSystemsRateCurve = theParticleSystem.rate;
			theParticleSystemsRateCurve.constantMax = 100;
			theParticleSystem.rate = theParticleSystemsRateCurve;

			//Increase particles to front thrusts
			theParticleSystem = rightFrontThrust.GetComponent<ParticleSystem>().emission ;
			theParticleSystemsRateCurve = theParticleSystem.rate;
			theParticleSystemsRateCurve.constantMax  = 100;
			theParticleSystem.rate = theParticleSystemsRateCurve;
		}


		//Movement and sound
		Vector2 moveAngleInput = new Vector2 (movementJoystick.Horizontal (), movementJoystick.Vertical ());
		//Vector2 myVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;//Gets our current velocity, used for sound, and speed checks
		
		
		float thrustMagnitude = moveAngleInput.magnitude;//This is just for sound
		//float percentOfThrustFromRear = (180 - Mathf.Abs(Vector2.Angle (Vector2.right, new Vector2 (transform.up.x,transform.up.y)) - Vector2.Angle (Vector2.right, new Vector2 (movementJoystick.Horizontal (), movementJoystick.Vertical ())))) / 180;
		thrusterSound.pitch = thrusterIdlePitch + myVelocity.magnitude / 30 + (thrustMagnitude / 3); //Increase pitch a bit more for faster sound
		thrusterSound.volume = thrusterIdleVolume + myVelocity.magnitude / 100 + (thrustMagnitude / 3);

		//This works good as well
		//velocityToAdd += moveAngleInput * (((180 - angleDifference) / 360) + 0.5f);



		//angle of analog from nose
		angleDifference = ( Vector2.Angle (moveAngleInput, new Vector2 (transform.up.x, transform.up.y)));

		if (angleDifference < 90) {

			//Increases speed limit when going forward
			speedLimitToUse = Mathf.Clamp (strafeAndReverseSpeedLimit + ((forwardSpeedLimit - strafeAndReverseSpeedLimit) * (1 - angleDifference / 90)), currentSpeed, forwardSpeedLimit);

			//Add velocity for forward movement, using accerlation curve
			velocityToAdd += (moveAngleInput * (1 - angleDifference / 90) * forwardAccel * Time.deltaTime) * (1.5f - Mathf.Clamp ((currentSpeed / forwardSpeedLimit - 4), 0.5f, 1f));
		} else {
			speedLimitToUse = Mathf.Clamp (strafeAndReverseSpeedLimit, currentSpeed, forwardSpeedLimit);
		}

		speedLimitToUse *= (1 - percentageFrostSlow);

		//Debug.Log (currentSpeed);

		//Inrcase velocity in move direction accordingly

		//Limit magnitude based on newly calculated top speed


		/*
		//Try to apply some amount forward velocity
		angleDifference = ( Vector2.Angle (moveAngleInput, new Vector2 (transform.up.x, transform.up.y)));
		if (angleDifference < 90) {

			//velocity to add is here
			velocityToCheck = moveAngleInput * (1 - angleDifference / 90) * forwardAccel * Time.deltaTime;
			velocityToCheck *= 1.5f - Mathf.Clamp( (myVelocity.magnitude  / maxSpeedMagnitude - 4), 0.5f, 1f);//Better working one

			//float currentSpeed = myVelocity.y * -transform.up.y + myVelocity.x * -transform.up.x;
			float newSpeed = (myVelocity.y + velocityToCheck.y) * transform.up.y + (myVelocity.x + velocityToCheck.x) * transform.up.x;

			//Don't add it if it puts you over the speed limit
			if (newSpeed < forwardSpeedLimit * (1 - angleDifference / 90)) {
				velocityToAdd += velocityToCheck;
			}
		}
		*/

		//Increase particles to rear thrusts
		theParticleSystem = rightThruster.GetComponent<ParticleSystem>().emission ;
		theParticleSystemsRateCurve = theParticleSystem.rate;
		theParticleSystemsRateCurve.constantMax += ((1 - angleDifference / 90) * 300);
		theParticleSystem.rate = theParticleSystemsRateCurve;
		
		theParticleSystem = leftThruster.GetComponent<ParticleSystem>().emission ;
		theParticleSystemsRateCurve = theParticleSystem.rate;
		theParticleSystemsRateCurve.constantMax += ((1 - angleDifference / 90) * 300);
		theParticleSystem.rate = theParticleSystemsRateCurve;

		/*
		//Try to apply some amount of backward velocity
		angleDifference = ( Vector2.Angle (moveAngleInput, new Vector2 (-transform.up.x, -transform.up.y)));
		if (angleDifference < 90) {
			
			velocityToCheck = /*-transform.up*//* moveAngleInput * (1 - angleDifference / 90) * backwardAccel * Time.deltaTime;
			
			//float currentSpeed = myVelocity.y * -transform.up.y + myVelocity.x * -transform.up.x;
			float newSpeed = (myVelocity.y + velocityToCheck.y) * -transform.up.y + (myVelocity.x + velocityToCheck.x) * -transform.up.x;

			//Don't add it if it puts you over the speed limit
			if (newSpeed < backwardSpeedLimit * (1 - angleDifference / 90)) {
				velocityToAdd += velocityToCheck;
			}
		}*/

		angleDifference = ( Vector2.Angle (moveAngleInput, new Vector2 (-transform.up.x, -transform.up.y)));
		if (angleDifference < 90) {
			velocityToAdd += moveAngleInput * (1 - angleDifference / 90) * backwardAccel * Time.deltaTime;
		}

		//Increase particles to front thrusts
		theParticleSystem = rightFrontThrust.GetComponent<ParticleSystem>().emission ;
		theParticleSystemsRateCurve = theParticleSystem.rate;
		theParticleSystemsRateCurve.constantMax += ((1 - angleDifference / 90) * 50) + 0;
		theParticleSystem.rate = theParticleSystemsRateCurve;

		theParticleSystem = leftFrontThrust.GetComponent<ParticleSystem>().emission ;
		theParticleSystemsRateCurve = theParticleSystem.rate;
		theParticleSystemsRateCurve.constantMax += ((1 - angleDifference / 90) * 50) + 0;
		theParticleSystem.rate = theParticleSystemsRateCurve;


		/*
		//Try to apply some amount of rightward velocity
		angleDifference = ( Vector2.Angle (moveAngleInput, new Vector2 (transform.right.x, transform.right.y)));
		if (angleDifference < 90) {
			
			velocityToCheck = moveAngleInput * (1 - angleDifference / 90) * strafeAccel * Time.deltaTime;
			
			//float currentSpeed = myVelocity.y * transform.right.y + myVelocity.x * transform.right.x;
			float newSpeed = (myVelocity.y + velocityToCheck.y) * transform.right.y + (myVelocity.x + velocityToCheck.x) * transform.right.x;

			//Don't add it if it puts you over the speed limit
			if (newSpeed < strafeSpeedLimit * (1 - angleDifference / 90)) {
				velocityToAdd += velocityToCheck;
			}
		}*/

		angleDifference = ( Vector2.Angle (moveAngleInput, new Vector2 (transform.right.x, transform.right.y)));
		if (angleDifference < 90) {
			velocityToAdd += moveAngleInput * (1 - angleDifference / 90) * strafeAccel * Time.deltaTime;
		}

		theParticleSystem = leftFrontThrust.GetComponent<ParticleSystem>().emission ;
		theParticleSystemsRateCurve = theParticleSystem.rate;
		theParticleSystemsRateCurve.constantMax += ((1 - angleDifference / 90) * 50) + 0;
		theParticleSystem.rate = theParticleSystemsRateCurve;

		/*
		//Try to apply some amount of leftward velocity
		angleDifference = ( Vector2.Angle (moveAngleInput, new Vector2 (-transform.right.x, -transform.right.y)));
		if (angleDifference < 90) {
			
			velocityToCheck = moveAngleInput * (1 - angleDifference / 90) * strafeAccel * Time.deltaTime;
			
			//float currentSpeed = myVelocity.y * -transform.right.y + myVelocity.x * -transform.right.x;
			float newSpeed = (myVelocity.y + velocityToCheck.y) * -transform.right.y + (myVelocity.x + velocityToCheck.x) * -transform.right.x;
			//Debug.Log (newSpeed);
			//Don't add it if it puts you over the speed limit
			if (newSpeed < strafeSpeedLimit * (1 - angleDifference / 90)) {
				velocityToAdd += velocityToCheck;
			}
		}*/

		angleDifference = ( Vector2.Angle (moveAngleInput, new Vector2 (-transform.right.x, -transform.right.y)));
		if (angleDifference < 90) {
			velocityToAdd += moveAngleInput * (1 - angleDifference / 90) * strafeAccel * Time.deltaTime;
		}

		//Increase particles to front right thrusts
		theParticleSystem = rightFrontThrust.GetComponent<ParticleSystem>().emission ;
		theParticleSystemsRateCurve = theParticleSystem.rate;
		theParticleSystemsRateCurve.constantMax += ((1 - angleDifference / 90) * 50) + 0;
		theParticleSystem.rate = theParticleSystemsRateCurve;
		

		//IMPROVE
		//Ship accelerates slower at speed, similar to a plane
		//Debug.Log(1.0f - (myVelocity.magnitude / maxSpeedMagnitude));

		//velocityToAdd *= 1.5f - Mathf.Clamp( (myVelocity.magnitude  / maxSpeedMagnitude - 4), 0.5f, 1f);//Old one
		//velocityToAdd *= 1.5f - Mathf.Clamp( (myVelocity.magnitude  / maxSpeedMagnitude - 4), 0.5f, 1f);//Better working one

		//velocityToAdd *= 2f - Mathf.Clamp( (myVelocity.magnitude  / maxSpeedMagnitude) * 1.5f, 1f, 1.5f);//Other decent alternative

		//Slow acceleration
		velocityToAdd *= (1 - percentageFrostSlow);

		gameObject.GetComponent<Rigidbody2D> ().velocity += velocityToAdd;//Add the velocity
		//Debug.Log (gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude);

		velocityToAdd = Vector2.zero;//Reset the velocity to add value, ready for the next update
		
		//If the ship's velocity is above the max speed, it is reduced to the max speed instead.
		if (gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude > /*maxSpeedMagnitude*/ speedLimitToUse) {
			gameObject.GetComponent<Rigidbody2D> ().velocity = gameObject.GetComponent<Rigidbody2D> ().velocity.normalized * speedLimitToUse;
		} 

		//Debug.Log (gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude);

		//UNSURE
		/*if (gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude < 10f && moveAngleInput.magnitude < 0.5f) {
			gameObject.GetComponent<Rigidbody2D> ().drag = originalDrag + (1 - (gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude / 10)) * 1.2f;
			//gameObject.GetComponent<Rigidbody2D> ().drag = originalDrag + 1;
		} else 
			gameObject.GetComponent<Rigidbody2D> ().drag = originalDrag;
		*/


		/*//stuff for recharging boost
		remainingBoostRechargeDelay -= Time.deltaTime;
		if (remainingBoostRechargeDelay <= 0) {
			remainingBoostDuration += (maxBoostDuration / fullBoostRechargeTime) * Time.deltaTime;
			if (remainingBoostDuration > maxBoostDuration)
				remainingBoostDuration = maxBoostDuration;
		}*/
	}

	public void addFrostStack (int stacksToAdd, float duration) {
		frostStacks += stacksToAdd;

		//Slows the player using a curve based on frostStacks / forstStacks^0.6. Look at the graph of (x / x^0.6) * 0.1 to see the percentage slow for x number of stacks
		//https://www.google.co.uk/webhp?sourceid=chrome-instant&ion=1&espv=2&ie=UTF-8#q=(x+%2F+x%5E0.6)+*+0.1
		//Note that for laser drones, they may apply up to 10 stacks each
		percentageFrostSlow = Mathf.Clamp((frostStacks / Mathf.Pow (frostStacks, 0.6f)) * 0.1f /*changed to half effect*/, 0f, 0.5f);
		Debug.Log (frostStacks + " = " + percentageFrostSlow);




		//Invoke a remove call for each stack after its duration has elapsed 
		for (int i = 0; i < stacksToAdd; i++) {
			Invoke ("removeFrostStack", duration);
		}

	}

	void removeFrostStack () {
		frostStacks--;

		if (frostStacks == 0) {
			percentageFrostSlow = 0;
		} else {
			percentageFrostSlow = Mathf.Clamp ((frostStacks / Mathf.Pow (frostStacks, 0.6f)) * 0.1f, 0f, 0.5f);
		}
					
		Debug.Log (frostStacks + " = " + percentageFrostSlow);
	}

	void OnCollisionEnter2D (Collision2D hit) {
		if (hit.gameObject.tag == "Wall") {

			//Debug.Log((Vector3.Angle(hit.contacts [0].normal, transform.up) / 180));

			gameObject.GetComponent<Rigidbody2D> ().drag = ((Vector3.Angle(hit.contacts [0].normal, transform.up) / 180) - 0.3f) * wallHitSlow * (myVelocity.magnitude  / forwardSpeedLimit);

			CancelInvoke("restoreDrag");
			Invoke ("restoreDrag", 0.7f);

		}
	}

	void restoreDrag() {
		gameObject.GetComponent<Rigidbody2D> ().drag = originalDrag;
	}

	/*public void startBoost () {
		boostButtonPressed = true;
	}
	
	public void stopBoost () {
		boostButtonPressed = false;
	}
	
	public float getRemainingBoostPercent () {
		return remainingBoostDuration / maxBoostDuration;
	}*/
	
	/*void ResetAxes(){ //this is used to calibrate the tilt control
		zeroAc = Input.acceleration;
		curAc = Vector3.zero;
	}*/
	
}
