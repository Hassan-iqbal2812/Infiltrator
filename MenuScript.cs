using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public Canvas mainMenu;
    public Canvas playMenu;
    public Canvas upgradesMenu;
    public Canvas optionsMenu;
    public Canvas howToPlayMenu;
    public Canvas creditsMenu;
    public Canvas resetMenu;
    public Canvas quitMenu;

    public Text loadText;

	// Use this for initialization
	void Start () 
    {
		//PlayerPrefs.SetInt ("Score", 3000);

        mainMenu = mainMenu.GetComponent<Canvas>();
        playMenu = playMenu.GetComponent<Canvas>();
        upgradesMenu = upgradesMenu.GetComponent<Canvas>();
        optionsMenu = optionsMenu.GetComponent<Canvas>();
        howToPlayMenu = howToPlayMenu.GetComponent<Canvas>();
        creditsMenu = creditsMenu.GetComponent<Canvas>();
        resetMenu = resetMenu.GetComponent<Canvas>();
        quitMenu = quitMenu.GetComponent<Canvas>();

        loadText = loadText.GetComponent<Text>();
        loadText.enabled = false;

        playMenu.enabled = false;
        upgradesMenu.enabled = false;
        optionsMenu.enabled = false;
        howToPlayMenu.enabled = false;
        creditsMenu.enabled = false;
        resetMenu.enabled = false;
        quitMenu.enabled = false;
	}

    public void UpgradePress()
    {
        mainMenu.enabled = false;
        upgradesMenu.enabled = true;
    }

    public void OptionsPress()
    {
        mainMenu.enabled = false;
        optionsMenu.enabled = true;
    }

    public void HowToPlayPress()
    {
        mainMenu.enabled = false;
        howToPlayMenu.enabled = true;
    }

    public void CreditsPress()
    {
        mainMenu.enabled = false;
        creditsMenu.enabled = true;
    }    

    public void ResetPress()
    {
        mainMenu.enabled = false;
        resetMenu.enabled = true;
    }
    public void BackPress()
    {
        if (playMenu.enabled)
        {
            playMenu.enabled = false;
            mainMenu.enabled = true;
        }
        else if (upgradesMenu.enabled)
        {
            upgradesMenu.enabled = false;
            mainMenu.enabled = true;
        }
        else if (optionsMenu.enabled)
        {
            optionsMenu.enabled = false;
            mainMenu.enabled = true;
        }
        else if (howToPlayMenu.enabled)
        {
            howToPlayMenu.enabled = false;
            mainMenu.enabled = true;
        }
        else if (creditsMenu.enabled)
        {
            creditsMenu.enabled = false;
            mainMenu.enabled = true;
        }
    }
    public void QuitPress()
    {
        mainMenu.enabled = false;
        quitMenu.enabled = true;
    }

    public void NoPress()
    {
        mainMenu.enabled = true;
        quitMenu.enabled = false;
        resetMenu.enabled = false;
    }

    public void ResetYesPress()
    {
        PlayerPrefs.SetInt("cannonUpgradeLevel", 0);
        PlayerPrefs.SetInt("rocketUpgradeLevel", 0);
        PlayerPrefs.SetInt("hullUpgradeLevel", 0);
        PlayerPrefs.SetInt("shieldUpgradeLevel", 0);
        PlayerPrefs.SetInt("moveUpgradeLevel", 0);
		PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Level1UnlockedDifficulty", 1);
        PlayerPrefs.SetInt("Level2UnlockedDifficulty", 1);
        PlayerPrefs.SetInt("Level3UnlockedDifficulty", 1);
        PlayerPrefs.SetInt("ArcadeUnlockedDifficulty", 1);
        resetMenu.enabled = false;
        mainMenu.enabled = true;
    }

    public void PlayPress()
    {
        mainMenu.enabled = false;
        playMenu.enabled = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
