using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Upgrade : MonoBehaviour {

    public Button cannonUpgrade1;
    public Button cannonUpgrade2;
    public Button cannonUpgrade3;

    public Button rocketUpgrade1;
    public Button rocketUpgrade2;
    public Button rocketUpgrade3;

    public Button hullUpgrade1;
    public Button hullUpgrade2;
    public Button hullUpgrade3;

    public Button shieldUpgrade1;
    public Button shieldUpgrade2;
    public Button shieldUpgrade3;

    public Button moveUpgrade1;
    public Button moveUpgrade2;
    public Button moveUpgrade3;

    public Canvas upgradeCanvas;
    public Button yesButton;
    public Button noButton;

    public GUIStyle style = new GUIStyle();

    private bool notEnoughPoints = false;

    private bool cannon1Pressed = false;
    private bool cannon2Pressed = false;
    private bool cannon3Pressed = false;

    private bool cannon1Bought;
    private bool cannon2Bought;
    private bool cannon3Bought;

    private bool rocket1Pressed = false;
    private bool rocket2Pressed = false;
    private bool rocket3Pressed = false;

    private bool rocket1Bought;
    private bool rocket2Bought;
    private bool rocket3Bought;

    private bool hull1Pressed = false;
    private bool hull2Pressed = false;
    private bool hull3Pressed = false;

    private bool hull1Bought;
    private bool hull2Bought;
    private bool hull3Bought;

    private bool shield1Pressed = false;
    private bool shield2Pressed = false;
    private bool shield3Pressed = false;

    private bool shield1Bought;
    private bool shield2Bought;
    private bool shield3Bought;

    private bool move1Pressed = false;
    private bool move2Pressed = false;
    private bool move3Pressed = false;

    private bool move1Bought;
    private bool move2Bought;
    private bool move3Bought;

    public Image cannon2Line;
    public Image cannon3Line;
    public Image rocket2Line;
    public Image rocket3Line;
    public Image hull2Line;
    public Image hull3Line;
    public Image shield2Line;
    public Image shield3Line;
    public Image move2Line;
    public Image move3Line;

	public Text upgradeText;
	public Text creditText;

	void Awake() {
		creditText.text = "Credits: " + PlayerPrefs.GetInt ("Score");
	}

	// Use this for initialization
	void Start () {

		creditText.text = "Credits: " + PlayerPrefs.GetInt ("Score");

        cannonUpgrade1 = cannonUpgrade1.GetComponent<Button>();
        cannonUpgrade2 = cannonUpgrade2.GetComponent<Button>();
        cannonUpgrade3 = cannonUpgrade3.GetComponent<Button>();
        cannon2Line = cannon2Line.GetComponent<Image>();
        cannon3Line = cannon3Line.GetComponent<Image>();

        rocketUpgrade1 = rocketUpgrade1.GetComponent<Button>();
        rocketUpgrade2 = rocketUpgrade2.GetComponent<Button>();
        rocketUpgrade3 = rocketUpgrade3.GetComponent<Button>();
        rocket2Line = rocket2Line.GetComponent<Image>();
        rocket3Line = rocket3Line.GetComponent<Image>();

        hullUpgrade1 = hullUpgrade1.GetComponent<Button>();
        hullUpgrade2 = hullUpgrade2.GetComponent<Button>();
        hullUpgrade3 = hullUpgrade3.GetComponent<Button>();
        hull2Line = hull2Line.GetComponent<Image>();
        hull3Line = hull3Line.GetComponent<Image>();

        shieldUpgrade1 = shieldUpgrade1.GetComponent<Button>();
        shieldUpgrade2 = shieldUpgrade2.GetComponent<Button>();
        shieldUpgrade3 = shieldUpgrade3.GetComponent<Button>();
        shield2Line = shield2Line.GetComponent<Image>();
        shield3Line = shield3Line.GetComponent<Image>();

        moveUpgrade1 = moveUpgrade1.GetComponent<Button>();
        moveUpgrade2 = moveUpgrade2.GetComponent<Button>();
        moveUpgrade3 = moveUpgrade3.GetComponent<Button>();
        move2Line = move2Line.GetComponent<Image>();
        move3Line = move3Line.GetComponent<Image>();

        upgradeCanvas = upgradeCanvas.GetComponent<Canvas>();
        yesButton = yesButton.GetComponent<Button>();
        noButton = noButton.GetComponent<Button>();

        upgradeCanvas.enabled = false;

        CheckUpdgradeStatus();
	}
	
    public void CheckUpdgradeStatus()
    {
        if (PlayerPrefs.GetInt("cannonUpgradeLevel") >= 0)
        {
            cannonUpgrade1.image.color = Color.white;
            cannonUpgrade2.image.color = Color.gray;
            cannonUpgrade3.image.color = Color.gray;
            cannon1Bought = false;
            cannon2Bought = false;
            cannon3Bought = false;
            cannon2Line.color = Color.gray;
            cannon3Line.color = Color.gray;
        }
        if (PlayerPrefs.GetInt("cannonUpgradeLevel") >= 1)
        {
            cannon1Bought = true;
            cannonUpgrade1.image.color = Color.yellow;
            cannonUpgrade2.image.color = Color.white;
            cannon2Line.color = Color.white;
        }
        if (PlayerPrefs.GetInt("cannonUpgradeLevel") >= 2)
        {
            cannon2Bought = true;
            cannonUpgrade2.image.color = Color.yellow;
            cannonUpgrade3.image.color = Color.white;
            cannon2Line.color = Color.yellow;
            cannon3Line.color = Color.white;
        }
        if (PlayerPrefs.GetInt("cannonUpgradeLevel") >= 3)
        {
            cannon3Bought = true;
            cannonUpgrade3.image.color = Color.yellow;
            cannon3Line.color = Color.yellow;
        }
        if (PlayerPrefs.GetInt("rocketUpgradeLevel") >= 0)
        {
            rocketUpgrade1.image.color = Color.white;
            rocketUpgrade2.image.color = Color.gray;
            rocketUpgrade3.image.color = Color.gray;
            rocket1Bought = false;
            rocket2Bought = false;
            rocket3Bought = false;
            rocket2Line.color = Color.gray;
            rocket3Line.color = Color.gray;
        }
        if (PlayerPrefs.GetInt("rocketUpgradeLevel") >= 1)
        {
            rocket1Bought = true;
            rocketUpgrade1.image.color = Color.yellow;
            rocketUpgrade2.image.color = Color.white;
            rocket2Line.color = Color.white;
        }
        if (PlayerPrefs.GetInt("rocketUpgradeLevel") >= 2)
        {
            rocket2Bought = true;
            rocketUpgrade2.image.color = Color.yellow;
            rocketUpgrade3.image.color = Color.white;
            rocket2Line.color = Color.yellow;
            rocket3Line.color = Color.white;
        }
        if (PlayerPrefs.GetInt("rocketUpgradeLevel") >= 3)
        {
            rocket3Bought = true;
            rocketUpgrade3.image.color = Color.yellow;
            rocket3Line.color = Color.yellow;
        }
        if (PlayerPrefs.GetInt("hullUpgradeLevel") >= 0)
        {
            hullUpgrade1.image.color = Color.white;
            hullUpgrade2.image.color = Color.gray;
            hullUpgrade3.image.color = Color.gray;
            hull1Bought = false;
            hull2Bought = false;
            hull3Bought = false;
            hull2Line.color = Color.gray;
            hull3Line.color = Color.gray;
        }
        if (PlayerPrefs.GetInt("hullUpgradeLevel") >= 1)
        {
            hull1Bought = true;
            hullUpgrade1.image.color = Color.yellow;
            hullUpgrade2.image.color = Color.white;
            hull2Line.color = Color.white;
        }
        if (PlayerPrefs.GetInt("hullUpgradeLevel") >= 2)
        {
            hull2Bought = true;
            hullUpgrade2.image.color = Color.yellow;
            hullUpgrade3.image.color = Color.white;
            hull2Line.color = Color.yellow;
            hull3Line.color = Color.white;
        }
        if (PlayerPrefs.GetInt("hullUpgradeLevel") >= 3)
        {
            hull3Bought = true;
            hullUpgrade3.image.color = Color.yellow;
            hull3Line.color = Color.yellow;
        }
        if (PlayerPrefs.GetInt("shieldUpgradeLevel") >= 0)
        {
            shieldUpgrade1.image.color = Color.white;
            shieldUpgrade2.image.color = Color.gray;
            shieldUpgrade3.image.color = Color.gray;
            shield1Bought = false;
            shield2Bought = false;
            shield3Bought = false;
            shield2Line.color = Color.gray;
            shield3Line.color = Color.gray;
        }
        if (PlayerPrefs.GetInt("shieldUpgradeLevel") >= 1)
        {
            shield1Bought = true;
            shieldUpgrade1.image.color = Color.yellow;
            shieldUpgrade2.image.color = Color.white;
            shield2Line.color = Color.white;
        }
        if (PlayerPrefs.GetInt("shieldUpgradeLevel") >= 2)
        {
            shield2Bought = true;
            shieldUpgrade2.image.color = Color.yellow;
            shieldUpgrade3.image.color = Color.white;
            shield2Line.color = Color.yellow;
            shield3Line.color = Color.white;
        }
        if (PlayerPrefs.GetInt("shieldUpgradeLevel") >= 3)
        {
            shield3Bought = true;
            shieldUpgrade3.image.color = Color.yellow;
            shield3Line.color = Color.yellow;
        }
        if (PlayerPrefs.GetInt("moveUpgradeLevel") >= 0)
        {
            moveUpgrade1.image.color = Color.white;
            moveUpgrade2.image.color = Color.gray;
            moveUpgrade3.image.color = Color.gray;
            move1Bought = false;
            move2Bought = false;
            move3Bought = false;
            move2Line.color = Color.gray;
            move3Line.color = Color.gray;
        }
        if (PlayerPrefs.GetInt("moveUpgradeLevel") >= 1)
        {
            move1Bought = true;
            moveUpgrade1.image.color = Color.yellow;
            moveUpgrade2.image.color = Color.white;
            move2Line.color = Color.white;
        }
        if (PlayerPrefs.GetInt("moveUpgradeLevel") >= 2)
        {
            move2Bought = true;
            moveUpgrade2.image.color = Color.yellow;
            moveUpgrade3.image.color = Color.white;
            move2Line.color = Color.yellow;
            move3Line.color = Color.white;
        }
        if (PlayerPrefs.GetInt("moveUpgradeLevel") >= 3)
        {
            move3Bought = true;
            moveUpgrade3.image.color = Color.yellow;
            move3Line.color = Color.yellow;
        }
    }

	public void Cannon1Press()
    {
        if (!cannon1Bought)
        {
            upgradeCanvas.enabled = true;
            cannon1Pressed = true;
        }
    }

    public void Cannon2Press()
    {
        if (cannon1Bought && !cannon2Bought)
        {
            upgradeCanvas.enabled = true;
            cannon2Pressed = true;
        }
    }

    public void Cannon3Press()
    {
        if (cannon2Bought && !cannon3Bought)
        {
            upgradeCanvas.enabled = true;
            cannon3Pressed = true;
        }
    }

    public void Rocket1Press()
    {
        if (!rocket1Bought)
        {
            upgradeCanvas.enabled = true;
            rocket1Pressed = true;
        }
    }

    public void Rocket2Press()
    {
        if (rocket1Bought && !rocket2Bought)
        {
            upgradeCanvas.enabled = true;
            rocket2Pressed = true;
        }
    }

    public void Rocket3Press()
    {
        if (rocket2Bought && !rocket3Bought)
        {
            upgradeCanvas.enabled = true;
            rocket3Pressed = true;
        }
    }

    public void Hull1Press()
    {
        if (!hull1Bought)
        {
            upgradeCanvas.enabled = true;
            hull1Pressed = true;
        }
    }

    public void Hull2Press()
    {
        if (hull1Bought && !hull2Bought)
        {
            upgradeCanvas.enabled = true;
            hull2Pressed = true;
        }
    }

    public void Hull3Press()
    {
        if (hull2Bought && !hull3Bought)
        {
            upgradeCanvas.enabled = true;
            hull3Pressed = true;
        }
    }

    public void Shield1Press()
    {
        if (!shield1Bought)
        {
            upgradeCanvas.enabled = true;
            shield1Pressed = true;
        }
    }

    public void Shield2Press()
    {
        if (shield1Bought && !shield2Bought)
        {
            upgradeCanvas.enabled = true;
            shield2Pressed = true;
        }
    }

    public void Shield3Press()
    {
        if (shield2Bought && !shield3Bought)
        {
            upgradeCanvas.enabled = true;
            shield3Pressed = true;
        }
    }

    public void Move1Press()
    {
        if (!move1Bought)
        {
            upgradeCanvas.enabled = true;
            move1Pressed = true;
        }
    }

    public void Move2Press()
    {
        if (move1Bought && !move2Bought)
        {
            upgradeCanvas.enabled = true;
            move2Pressed = true;
        }
    }

    public void Move3Press()
    {
        if (move2Bought && !move3Bought)
        {
            upgradeCanvas.enabled = true;
            move3Pressed = true;
        }
    }
    public void YesPress()
    {
        if (cannon1Pressed && PlayerPrefs.GetInt("Score") >= 500)
        {
            PlayerPrefs.SetInt("cannonUpgradeLevel", 1);
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - 500);
            upgradeCanvas.enabled = false;
            cannon1Pressed = false;
            cannon1Bought = true;
            cannonUpgrade1.image.color = Color.yellow;
            cannonUpgrade2.image.color = Color.white;
            cannon2Line.color = Color.white;
        }
        else if (cannon1Pressed && PlayerPrefs.GetInt("Score") < 500)
        {
            notEnoughPoints = true;
        }
        else if (cannon2Pressed && PlayerPrefs.GetInt("Score") >= 1200)
        {
            PlayerPrefs.SetInt("cannonUpgradeLevel", 2);
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - 1200);
            upgradeCanvas.enabled = false;
            cannon2Pressed = false;
            cannon2Bought = true;
            cannonUpgrade2.image.color = Color.yellow;
            cannonUpgrade3.image.color = Color.white;
            cannon2Line.color = Color.yellow;
            cannon3Line.color = Color.white;
        }
        else if (cannon2Pressed && PlayerPrefs.GetInt("Score") < 1200)
        {
            notEnoughPoints = true;
        }
        else if (cannon3Pressed && PlayerPrefs.GetInt("Score") >= 2500)
        {
            PlayerPrefs.SetInt("cannonUpgradeLevel", 3);
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - 2500);
            upgradeCanvas.enabled = false;
            cannon3Pressed = false;
            cannon3Bought = true;
            cannonUpgrade3.image.color = Color.yellow;
            cannon3Line.color = Color.yellow;
        }
        else if (cannon3Pressed && PlayerPrefs.GetInt("Score") < 2500)
        {
            notEnoughPoints = true;
        }
        else if (rocket1Pressed && PlayerPrefs.GetInt("Score") >= 500)
        {
            PlayerPrefs.SetInt("rocketUpgradeLevel", 1);
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - 500);
            upgradeCanvas.enabled = false;
            rocket1Pressed = false;
            rocket1Bought = true;
            rocketUpgrade1.image.color = Color.yellow;
            rocketUpgrade2.image.color = Color.white;
            rocket2Line.color = Color.white;
        }
        else if (rocket1Pressed && PlayerPrefs.GetInt("Score") < 500)
        {
            notEnoughPoints = true;
        }
        else if (rocket2Pressed && PlayerPrefs.GetInt("Score") >= 1200)
        {
            PlayerPrefs.SetInt("rocketUpgradeLevel", 2);
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - 1200);
            upgradeCanvas.enabled = false;
            rocket2Pressed = false;
            rocket2Bought = true;
            rocketUpgrade2.image.color = Color.yellow;
            rocketUpgrade3.image.color = Color.white;
            rocket2Line.color = Color.yellow;
            rocket3Line.color = Color.white;
        }
        else if (rocket2Pressed && PlayerPrefs.GetInt("Score") < 1200)
        {
            notEnoughPoints = true;
        }
        else if (rocket3Pressed && PlayerPrefs.GetInt("Score") >= 2500)
        {
            PlayerPrefs.SetInt("rocketUpgradeLevel", 3);
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - 2500);
            upgradeCanvas.enabled = false;
            rocket3Pressed = false;
            rocket3Bought = true;
            rocketUpgrade3.image.color = Color.yellow;
            rocket3Line.color = Color.yellow;
        }
        else if (rocket3Pressed && PlayerPrefs.GetInt("Score") < 2500)
        {
            notEnoughPoints = true;
        }
        else if (hull1Pressed && PlayerPrefs.GetInt("Score") >= 500)
        {
            PlayerPrefs.SetInt("hullUpgradeLevel", 1);
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - 500);
            upgradeCanvas.enabled = false;
            hull1Pressed = false;
            hull1Bought = true;
            hullUpgrade1.image.color = Color.yellow;
            hullUpgrade2.image.color = Color.white;
            hull2Line.color = Color.white;
        }
        else if (hull1Pressed && PlayerPrefs.GetInt("Score") < 500)
        {
            notEnoughPoints = true;
        }
        else if (hull2Pressed && PlayerPrefs.GetInt("Score") >= 1200)
        {
            PlayerPrefs.SetInt("hullUpgradeLevel", 2);
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - 1200);
            upgradeCanvas.enabled = false;
            hull2Pressed = false;
            hull2Bought = true;
            hullUpgrade2.image.color = Color.yellow;
            hullUpgrade3.image.color = Color.white;
            hull2Line.color = Color.yellow;
            hull3Line.color = Color.white;
        }
        else if (hull2Pressed && PlayerPrefs.GetInt("Score") < 1200)
        {
            notEnoughPoints = true;
        }
        else if (hull3Pressed && PlayerPrefs.GetInt("Score") >= 2500)
        {
            PlayerPrefs.SetInt("hullUpgradeLevel", 3);
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - 2500);
            upgradeCanvas.enabled = false;
            hull3Pressed = false;
            hull3Bought = true;
            hullUpgrade3.image.color = Color.yellow;
            hull3Line.color = Color.yellow;
        }
        else if (hull3Pressed && PlayerPrefs.GetInt("Score") < 2500)
        {
            notEnoughPoints = true;
        }
        else if (shield1Pressed && PlayerPrefs.GetInt("Score") >= 500)
        {
            PlayerPrefs.SetInt("shieldUpgradeLevel", 1);
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - 500);
            upgradeCanvas.enabled = false;
            shield1Pressed = false;
            shield1Bought = true;
            shieldUpgrade1.image.color = Color.yellow;
            shieldUpgrade2.image.color = Color.white;
            shield2Line.color = Color.white;
        }
        else if (shield1Pressed && PlayerPrefs.GetInt("Score") < 500)
        {
            notEnoughPoints = true;
        }
        else if (shield2Pressed && PlayerPrefs.GetInt("Score") >= 1200)
        {
            PlayerPrefs.SetInt("shieldUpgradeLevel", 2);
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - 1200);
            upgradeCanvas.enabled = false;
            shield2Pressed = false;
            shield2Bought = true;
            shieldUpgrade2.image.color = Color.yellow;
            shieldUpgrade3.image.color = Color.white;
            shield2Line.color = Color.yellow;
            shield3Line.color = Color.white;
        }
        else if (shield2Pressed && PlayerPrefs.GetInt("Score") < 1200)
        {
            notEnoughPoints = true;
        }
        else if (shield3Pressed && PlayerPrefs.GetInt("Score") >= 2500)
        {
            PlayerPrefs.SetInt("shieldUpgradeLevel", 3);
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - 2500);
            upgradeCanvas.enabled = false;
            shield3Pressed = false;
            shield3Bought = true;
            shieldUpgrade3.image.color = Color.yellow;
            shield3Line.color = Color.yellow;
        }
        else if (shield3Pressed && PlayerPrefs.GetInt("Score") < 2500)
        {
            notEnoughPoints = true;
        }
        else if (move1Pressed && PlayerPrefs.GetInt("Score") >= 500)
        {
            PlayerPrefs.SetInt("moveUpgradeLevel", 1);
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - 500);
            upgradeCanvas.enabled = false;
            move1Pressed = false;
            move1Bought = true;
            moveUpgrade1.image.color = Color.yellow;
            moveUpgrade2.image.color = Color.white;
            move2Line.color = Color.white;
        }
        else if (move1Pressed && PlayerPrefs.GetInt("Score") < 500)
        {
            notEnoughPoints = true;
        }
        else if (move2Pressed && PlayerPrefs.GetInt("Score") >= 1200)
        {
            PlayerPrefs.SetInt("moveUpgradeLevel", 2);
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - 1200);
            upgradeCanvas.enabled = false;
            move2Pressed = false;
            move2Bought = true;
            moveUpgrade2.image.color = Color.yellow;
            moveUpgrade3.image.color = Color.white;
            move2Line.color = Color.yellow;
            move3Line.color = Color.white;
        }
        else if (move2Pressed && PlayerPrefs.GetInt("Score") < 1200)
        {
            notEnoughPoints = true;
        }
        else if (move3Pressed && PlayerPrefs.GetInt("Score") >= 2500)
        {
            PlayerPrefs.SetInt("moveUpgradeLevel", 3);
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - 2500);
            upgradeCanvas.enabled = false;
            move3Pressed = false;
            move3Bought = true;
            moveUpgrade3.image.color = Color.yellow;
            move3Line.color = Color.yellow;
        }
        else if (move3Pressed && PlayerPrefs.GetInt("Score") < 2500)
        {
            notEnoughPoints = true;
        }

		creditText.text = "Credits: " + PlayerPrefs.GetInt ("Score");
    }

    public void NoPress()
    {
        upgradeCanvas.enabled = false;
        notEnoughPoints = false;
        cannon1Pressed = false;
        cannon2Pressed = false;
        cannon3Pressed = false;
        rocket1Pressed = false;
        rocket2Pressed = false;
        rocket3Pressed = false;
        hull1Pressed = false;
        hull2Pressed = false;
        hull3Pressed = false;
        shield1Pressed = false;
        shield2Pressed = false;
        shield3Pressed = false;
        move1Pressed = false;
        move2Pressed = false;
        move3Pressed = false;
    }

    public void OnGUI()
    {
        if (cannon1Pressed)
        {
			upgradeText.text = "Cannon Upgrade 1\nDecrease shot cooldown by 30%\nWould you like to purchase\nthis upgrade for 500 credits?";
        }
        else if (cannon2Pressed)
        {
			upgradeText.text = "Cannon Upgrade 2\nIncrease damage by 50%\nWould you like to purchase\nthis upgrade for 1200 credits?";
        }
        else if (cannon3Pressed)
        {
			upgradeText.text = "Cannon Upgrade 3\nBouncing shots\nWould you like to purchase\nthis upgrade for 2500 credits?";
        }
        else if (rocket1Pressed)
        {
			upgradeText.text = "Rocket Upgrade 1\nIncrease blast radius by 50%\nWould you like to purchase\nthis upgrade for 500 credits?";
        }
        else if (rocket2Pressed)
        {
			upgradeText.text = "Rocket Upgrade 2\nDecrease rocket cooldown by 30%\nWould you like to purchase\nthis upgrade for 1200 credits?";
        }
        else if (rocket3Pressed)
        {
			upgradeText.text = "Rocket Upgrade 3\nRockets travel 2x faster with 50% more damage and blast radius\nWould you like to purchase\nthis upgrade for 2500 credits?";
        }
        else if (hull1Pressed)
        {
			upgradeText.text = "Hull Upgrade 1\nIncrease hull strength by 40%\nWould you like to purchase\nthis upgrade for 500 credits?";
        }
        else if (hull2Pressed)
        {
			upgradeText.text = "Hull Upgrade 2\nRestores 5% of your hull's strength whenever you destroy an enemy\nWould you like to purchase\nthis upgrade for 1200 credits?";
        }
        else if (hull3Pressed)
        {
			upgradeText.text = "Hull Upgrade 3\nExplosive Jettison\nWould you like to purchase\nthis upgrade for 2500 credits?";
        }
        else if (shield1Pressed)
        {
			upgradeText.text = "Shield Upgrade 1\nIncrease shield capacity by 40%\nWould you like to purchase\nthis upgrade for 500 credits?";
        }
        else if (shield2Pressed)
        {
			upgradeText.text = "Shield Upgrade 2\nShield recharge time decreased by 30% \nWould you like to purchase\nthis upgrade for 1200 credits?";
        }
        else if (shield3Pressed)
        {
			upgradeText.text = "Shield Upgrade 3\nNewtonian defense\nWould you like to purchase\nthis upgrade for 2500 credits?";
        }
        else if (move1Pressed)
        {
			upgradeText.text = "Movement Upgrade 1\nIncrease acceleration and breaking by 25%\nWould you like to purchase\nthis upgrade for 500 credits?";
        }
        else if (move2Pressed)
        {
			upgradeText.text = "Movement Upgrade 2\nIncrease forward, strafing and reversing top speed by 25%\nWould you like to purchase\nthis upgrade for 1200 credits?";
        }
        else if (move3Pressed)
        {
			upgradeText.text = "Movement Upgrade 3\nTrickster\nWould you like to purchase\nthis upgrade for 2500 credits?";
        }

        if (notEnoughPoints)
        {
			upgradeText.text = "You cannot afford this upgrade. Play levels to earn more credits";

			cannon1Pressed = false;
			cannon2Pressed = false;
			cannon3Pressed = false;
			rocket1Pressed = false;
			rocket2Pressed = false;
			rocket3Pressed = false;
			hull1Pressed = false;
			hull2Pressed = false;
			hull3Pressed = false;
			shield1Pressed = false;
			shield2Pressed = false;
			shield3Pressed = false;
			move1Pressed = false;
			move2Pressed = false;
			move3Pressed = false;
        }
    }
}
