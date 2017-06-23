using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;
using System.Collections;

public class Pause : MonoBehaviour {

    public Canvas pauseMenu;

    public Button pauseButton;
    public Button resumeButton;
    public Button mainMenuButton;

	public GameObject analogToHide1, analogToHide2;

    void Start()
    {
        pauseMenu = pauseMenu.GetComponent<Canvas>();
        pauseButton = pauseButton.GetComponent<Button>();
        resumeButton = resumeButton.GetComponent<Button>();
        mainMenuButton = mainMenuButton.GetComponent<Button>();

        pauseMenu.enabled = false;
    }
	
    public void PausePress()
    {
        pauseMenu.enabled = true;
        Time.timeScale = 0;
		analogToHide1.SetActive (false);
		analogToHide2.SetActive (false);
    }

    public void ResumePress()
    {
        pauseMenu.enabled = false;
        Time.timeScale = 1;
		analogToHide1.SetActive (true);
		analogToHide2.SetActive (true);
    }

    public void HidePauseButton()
    {
        pauseButton.enabled = false;
    }
}
