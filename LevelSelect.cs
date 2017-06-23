using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSelect : MonoBehaviour {

    public Canvas playCanvas;

    public Text level1;
    public Text level2;
    public Text level3;
    public Text arcade;

    public Image normal;
    public Image hard;
    public Image master;

    public Button hardButton;
    public Button masterButton;

	public Text levelDescription;
    public Text loadText;

    private string currentLevel;

	// Use this for initialization
	void Start () 
    {
        playCanvas = playCanvas.GetComponent<Canvas>();

        level1 = level1.GetComponent<Text>();
        level2 = level2.GetComponent<Text>();
        level3 = level3.GetComponent<Text>();
        arcade = arcade.GetComponent<Text>();

		normal = normal.GetComponent<Image>();
        hard = hard.GetComponent<Image>();
        master = master.GetComponent<Image>();

        hardButton = hardButton.GetComponent<Button>();
        masterButton = masterButton.GetComponent<Button>();

        loadText = loadText.GetComponent<Text>();

        normal.color = Color.yellow;

        level2.enabled = false;
        level3.enabled = false;
        arcade.enabled = false;
        currentLevel = "level1";
	}

    public void CheckAvailableDifficulty()
    {
        switch (currentLevel)
        {
            case "level1":
                switch (PlayerPrefs.GetInt("Level1UnlockedDifficulty"))
                {
                    case 1:
                        hardButton.interactable = false;
                        masterButton.interactable = false;
                        break;
                    case 2:
                        hardButton.interactable = true;
                        masterButton.interactable = false;
                        break;
                    case 3:
                        hardButton.interactable = true;
                        masterButton.interactable = true;
                        break;
                }
                break;
            case "level2":
                switch (PlayerPrefs.GetInt("Level2UnlockedDifficulty"))
                {
                    case 1:
                        hardButton.interactable = false;
                        masterButton.interactable = false;
                        break;
                    case 2:
                        hardButton.interactable = true;
                        masterButton.interactable = false;
                        break;
                    case 3:
                        hardButton.interactable = true;
                        masterButton.interactable = true;
                        break;
                }
                break;
            case "level3":
                switch (PlayerPrefs.GetInt("Level3UnlockedDifficulty"))
                {
                    case 1:
                        hardButton.interactable = false;
                        masterButton.interactable = false;
                        break;
                    case 2:
                        hardButton.interactable = true;
                        masterButton.interactable = false;
                        break;
                    case 3:
                        hardButton.interactable = true;
                        masterButton.interactable = true;
                        break;
                }
                break;
            case "arcade":
                switch (PlayerPrefs.GetInt("ArcadeUnlockedDifficulty"))
                {
                    case 1:
                        hardButton.interactable = false;
                        masterButton.interactable = false;
                        break;
                    case 2:
                        hardButton.interactable = true;
                        masterButton.interactable = false;
                        break;
                    case 3:
                        hardButton.interactable = true;
                        masterButton.interactable = true;
                        break;
                }
                break;
        }
    }
	
	public void NextPress()
    {
        NormalPress();
        CheckAvailableDifficulty();

        switch (currentLevel)
        {
            case "level1":
                level1.enabled = false;
                level2.enabled = true;
                currentLevel = "level2";
			    levelDescription.text = "Coming soon!";
                break;
            case "level2":
                level2.enabled = false;
                level3.enabled = true;
                currentLevel = "level3";
			    levelDescription.text = "Coming soon!";
                break;
            case "level3":
                level3.enabled = false;
                arcade.enabled = true;
                currentLevel = "arcade";
				levelDescription.text = "Arcade\n*Drones constantly spawn.\n*Destroy as many as you can.\n*Survive for as long as you can.\n*here, laser traps will destroy drones, so use them to your advantage";
                break;
            case "arcade":
                arcade.enabled = false;
                level1.enabled = true;
                currentLevel = "level1";
			    levelDescription.text = "Level 1\n*You must get to the end of the level and defeat the boss.\n*The longer you take, the more drones will spawn.\n*Laser fences can be shut down by destroying all generators.";
                break;
        }
    }

    public void PreviousPress()
    {
        NormalPress();
        CheckAvailableDifficulty();

        switch (currentLevel)
        {
            case "level1":
                level1.enabled = false;
                arcade.enabled = true;
                currentLevel = "arcade";
			    levelDescription.text = "Arcade\n*Drones constantly spawn.\n*Destroy as many as you can.\n*Survive for as long as you can.\n*here, laser traps will destroy drones, so use them to your advantage";
                break;
            case "arcade":
                arcade.enabled = false;
                level3.enabled = true;
                currentLevel = "level3";
			    levelDescription.text = "Coming soon!";
                break;
            case "level3":
                level3.enabled = false;
                level2.enabled = true;
                currentLevel = "level2";
			    levelDescription.text = "Coming soon!";
                break;
            case "level2":
                level2.enabled = false;
                level1.enabled = true;
                currentLevel = "level1";
			    levelDescription.text = "Level 1\n*You must get to the end of the level and defeat the boss.\n*The longer you take, the more drones will spawn.\n*Laser fences can be shut down by destroying all generators.";
                break;
        }
    }

    public void NormalPress()
    {
        PlayerPrefs.SetInt("Difficulty", 1);

        normal.color = Color.yellow;

        switch (currentLevel)
        {
            case "level1":
                if (PlayerPrefs.GetInt("Level1UnlockedDifficulty") < 2)
                {
                    hard.color = Color.gray;
                }
                else
                {
                    hard.color = Color.white;
                }
                if (PlayerPrefs.GetInt("Level1UnlockedDifficulty") < 3)
                {
                    master.color = Color.gray;
                }
                else
                {
                    master.color = Color.white;
                }
                break;
            case "level2":
                if (PlayerPrefs.GetInt("Level2UnlockedDifficulty") < 2)
                {
                    hard.color = Color.gray;
                }
                else
                {
                    hard.color = Color.white;
                }
                if (PlayerPrefs.GetInt("Level2UnlockedDifficulty") < 3)
                {
                    master.color = Color.gray;
                }
                else
                {
                    master.color = Color.white;
                }
                break;
            case "level3":
                if (PlayerPrefs.GetInt("Level3UnlockedDifficulty") < 2)
                {
                    hard.color = Color.gray;
                }
                else
                {
                    hard.color = Color.white;
                }
                if (PlayerPrefs.GetInt("Level3UnlockedDifficulty") < 3)
                {
                    master.color = Color.gray;
                }
                else
                {
                    master.color = Color.white;
                }
                break;
            case "arcade":
                if (PlayerPrefs.GetInt("ArcadeUnlockedDifficulty") < 2)
                {
                    hard.color = Color.gray;
                }
                else
                {
                    hard.color = Color.white;
                }
                if (PlayerPrefs.GetInt("ArcadeUnlockedDifficulty") < 3)
                {
                    master.color = Color.gray;
                }
                else
                {
                    master.color = Color.white;
                }
                break;
        }
    }

    public void HardPress()
    {
        PlayerPrefs.SetInt("Difficulty", 2);

        normal.color = Color.white;
        hard.color = Color.yellow;

        switch (currentLevel)
        {
            case "level1":
                if (PlayerPrefs.GetInt("Level1UnlockedDifficulty") < 3)
                {
                    master.color = Color.gray;
                }
                else
                {
                    master.color = Color.white;
                }
                break;
            case "level2":
                if (PlayerPrefs.GetInt("Level2UnlockedDifficulty") < 3)
                {
                    master.color = Color.gray;
                }
                else
                {
                    master.color = Color.white;
                }
                break;
            case "level3":
                if (PlayerPrefs.GetInt("Level3UnlockedDifficulty") < 3)
                {
                    master.color = Color.gray;
                }
                else
                {
                    master.color = Color.white;
                }
                break;
            case "arcade":
                if (PlayerPrefs.GetInt("ArcadeUnlockedDifficulty") < 3)
                {
                    master.color = Color.gray;
                }
                else
                {
                    master.color = Color.white;
                }
                break;
        }
    }

    public void MasterPress()
    {
        PlayerPrefs.SetInt("Difficulty", 3);

        normal.color = Color.white;
        hard.color = Color.white;
        master.color = Color.yellow;
    }

    public void StartPress()
    {
        
        if (currentLevel == "level1")
        {
            //SceneManager.LoadScene("scene1");

			Time.timeScale = 1;
			playCanvas.enabled = false;
			loadText.enabled = true;

			Application.LoadLevel(1);    
            PlayerPrefs.SetString("CurrentLevel", "Level1");
        }
        else if (currentLevel == "level2")
        {
            //SceneManager.LoadScene("scene2");
            //PlayerPrefs.SetString("CurrentLevel", "Level2");
        }
        else if (currentLevel == "level3")
        {
            //SceneManager.LoadScene("scene3");
            //PlayerPrefs.SetString("CurrentLevel", "Level3");
        }
        else if (currentLevel == "arcade")
        {
			Time.timeScale = 1;
			playCanvas.enabled = false;
			loadText.enabled = true;

			//SceneManager.LoadScene("arcade");
			Application.LoadLevel("Arcade");
            PlayerPrefs.SetString("CurrentLevel", "Arcade");
        }
    }
}
