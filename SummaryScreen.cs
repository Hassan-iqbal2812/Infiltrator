using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SummaryScreen : MonoBehaviour {

	public TimerScore timerAndScoreScript;
	public Health playerHealth;

	int attackScore, defenseScore, speedScore, totalScore;
	public Text attackScoreText, defenseScoreText, speedScoreText, totalScoreText, creditsText, titleText;
	public bool inArcadeMode;

	public float benchMarkTime = 150;
	public int scorePerSecUnderBench = 10;

	public GameObject analogToHide1, analogToHide2;

	public AudioSource victory, failure;

	//int startScore;

	void Start () {
		//startScore = PlayerPrefs.GetInt ("Score");
		//player = GameObject.FindGameObjectWithTag ("Player");
		//Time.timeScale = 1f;

	}


	// Use this for initialization
	void Awake () {
		Time.timeScale = 0f;

		analogToHide1.SetActive (false);
		analogToHide2.SetActive (false);

		attackScore = timerAndScoreScript.scoreForLevel;

		if (!inArcadeMode) {
			defenseScore = Mathf.Clamp(Mathf.RoundToInt(playerHealth.getHealthPercent() * 300), 0, 300);

			if (playerHealth.getHealthPercent() == 1) {
			defenseScore += 150;
			}


			defenseScore = Mathf.RoundToInt (defenseScore);

			if (playerHealth.getHealthPercent () > 0) {
				speedScore = Mathf.RoundToInt (Mathf.Clamp (benchMarkTime - timerAndScoreScript.getFinishTime(), 0, benchMarkTime) * scorePerSecUnderBench);

				titleText.text = "Victory";
				titleText.color = new Color (0, 1, 75f/255f);

				victory.Play ();

				defenseScoreText.text = "Defense Score: " + defenseScore;
				speedScoreText.text = "Speed Score: " + speedScore;
			} else {
				failure.Play ();
				titleText.text = "Defeat";
				titleText.color = new Color (1f, 60f/255f, 60f/255f);

				defenseScoreText.text = "Defense Score: N/A";
				speedScoreText.text = "Speed Score: N/A";
			}

		} else {

			failure.Play ();

			defenseScore = Mathf.RoundToInt(timerAndScoreScript.getFinishTime() * scorePerSecUnderBench);

			defenseScoreText.text = "Survial Score: " + defenseScore;
			speedScoreText.text = "";
		}

		totalScore = attackScore + defenseScore + speedScore;

		attackScoreText.text = "Attack Score: " + attackScore;

		totalScoreText.text = "Total Score: " + totalScore;



		creditsText.text = "Credits = " + PlayerPrefs.GetInt ("Score") + " + " + totalScore + " = " + (PlayerPrefs.GetInt ("Score") + totalScore);

		PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + totalScore);


	}

	public void loadMenu() {
		Time.timeScale = 1f;
		Application.LoadLevel (0);
	}

	public void reload() {
		Time.timeScale = 1f;
		Application.LoadLevel (Application.loadedLevel);
	}


}
