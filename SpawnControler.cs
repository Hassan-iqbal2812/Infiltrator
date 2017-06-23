using UnityEngine;
using System.Collections;

public class SpawnControler : MonoBehaviour {


	GameObject[] test = new GameObject[20];
	int nextHatchToSpawnFrom;
	public int droneLimit = 10;

	//These variables can be set for each difficulty
	public float initalSpawnDelayNormal, finalSpawnDelayNormal, finalSpawningTimeNormal;
	public float initalSpawnDelayHard, finalSpawnDelayHard, finalSpawningTimeHard;
	public float initalSpawnDelayMaster, finalSpawnDelayMaster, finalSpawningTimeMaster;

	public bool canAlsoSpawnFreezeDrones;
	public float timeToBeginSpawningFreezers, initialPercentageOfFreezerSpawns, finalPercentageOfFreezerSpawns;
	bool shouldStartSpawningFreezers;

	float freezerSpawnDictator; //If the percentage of freezer spawns is 0.1 (10%) then this number counts up by 0.1 on every spawns, spawing a freezer if it >= 1 then reseting

	//Depending on difficulty, these values are changed and used
	float initalSpawnDelay, finalSpawnDelay, finalSpawningTime;


	float currentSpawnDelay, remainingTimeUntilSpawn;

	public bool active;

	// Use this for initialization
	void Start () {

		//Depending on the difficult, use different spawn rate values. This is one of the factors that scales the difficulty
		switch (PlayerPrefs.GetInt("Difficulty")) {
			case 1: 
			initalSpawnDelay = initalSpawnDelayNormal;
			finalSpawnDelay = finalSpawnDelayNormal;
			finalSpawningTime = finalSpawningTimeNormal;
			break;
			case 2: 
			initalSpawnDelay = initalSpawnDelayHard;
			finalSpawnDelay = finalSpawnDelayHard;
			finalSpawningTime = finalSpawningTimeHard;
			break;
			case 3: 
			initalSpawnDelay = initalSpawnDelayMaster;
			finalSpawnDelay = finalSpawnDelayMaster;
			finalSpawningTime = finalSpawningTimeMaster;
			break;
		}
							
		remainingTimeUntilSpawn = initalSpawnDelay;

	}
	
	// Update is called once per frame
	void Update () {

		if (Time.timeSinceLevelLoad > timeToBeginSpawningFreezers)
			shouldStartSpawningFreezers = true;


		if (active) {

			remainingTimeUntilSpawn -= Time.deltaTime;

			if (remainingTimeUntilSpawn <= 0) {

				GameObject[] activeHatches = GameObject.FindGameObjectsWithTag ("ActiveSpawnHatch");

				if (nextHatchToSpawnFrom >= activeHatches.Length)
					nextHatchToSpawnFrom = 0;

				if (activeHatches [nextHatchToSpawnFrom] != null) {

					//Determine which type of enenmy to spawn (insert other enemies here)
					if (freezerSpawnDictator >= 1) {
						activeHatches [nextHatchToSpawnFrom].GetComponent<SpawnHatch> ().spawnFreezer ();
						freezerSpawnDictator = 0;
					}
					else
						activeHatches [nextHatchToSpawnFrom].GetComponent<SpawnHatch> ().spawnDrone ();


					if (shouldStartSpawningFreezers) {
						//Increment the spawn dictator by an amount between the intial and final freezer spawn percentage values based on time.  
						freezerSpawnDictator += initialPercentageOfFreezerSpawns + (finalPercentageOfFreezerSpawns - initialPercentageOfFreezerSpawns) 
							* (Mathf.Clamp (Time.timeSinceLevelLoad, 0, finalSpawningTime) / finalSpawningTime);
					}
				}


				nextHatchToSpawnFrom++;
				currentSpawnDelay = initalSpawnDelay - (initalSpawnDelay - finalSpawnDelay) * (Mathf.Clamp (Time.timeSinceLevelLoad, 0, finalSpawningTime) / finalSpawningTime);

				//work out whether or not to spawn a freezer

				remainingTimeUntilSpawn = currentSpawnDelay;
			}
		}

	}

	/*
	public void addActiveHatch(GameObject hatch) {
		test [0] = hatch;
		Debug.Log ("new hatch added");
	}*/
}
