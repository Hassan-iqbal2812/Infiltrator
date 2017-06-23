using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerScore : MonoBehaviour {

    private float minutes, seconds;

	public Text time, score;

	public int startScore, scoreForLevel;

	bool keepCalulatingScoreAndTime = true, finalTimeSaved;

	float finishTime;

	// Use this for initialization
	void Start () {
		startScore = PlayerPrefs.GetInt ("Score");
	}
	
	// Update is called once per frame
	void Update () 
    {
		if (keepCalulatingScoreAndTime) {
			minutes = Mathf.FloorToInt (Time.timeSinceLevelLoad / 60);
			seconds = Time.timeSinceLevelLoad % 60;

			scoreForLevel = PlayerPrefs.GetInt ("Score") - startScore;
		} else if (!finalTimeSaved) {
			finishTime = Time.timeSinceLevelLoad;
			finalTimeSaved = true;
		}

		time.text = "Time: " + minutes.ToString ("F0") + ":" + seconds.ToString ("F2");
		//GUI.Label(new Rect(Screen.width / 10f, Screen.height / 5f, 400, 100), "Time: " + minutes.ToString("F0") + ":" + seconds.ToString("F2"));

		score.text = "Score: " + scoreForLevel;
	}

    /*void OnGUI()
    {
		time.text = "Time: " + minutes.ToString ("F0") + ":" + seconds.ToString ("F2");
		//GUI.Label(new Rect(Screen.width / 10f, Screen.height / 5f, 400, 100), "Time: " + minutes.ToString("F0") + ":" + seconds.ToString("F2"));

		scoreForLevel = PlayerPrefs.GetInt ("Score") - startScore;

		score.text = "Score: " + scoreForLevel;
        //GUI.Label(new Rect(Screen.width / 10f, Screen.height / 4f, 400, 100), "Score: " + PlayerPrefs.GetInt("Score"));
    }*/

	public void stopCalculatingScoreAndTime () {
		keepCalulatingScoreAndTime = false;
	}

	public float getFinishTime () {
		return finishTime;
	}

}
