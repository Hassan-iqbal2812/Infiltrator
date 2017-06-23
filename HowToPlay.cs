using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HowToPlay : MonoBehaviour {

    public Image move, shoot, reverse, turret, drone, time, score, dodge, health, generator;
    public Text instructionText;

    private int instruction = 1;
    private int maxInstruction = 12;

	// Use this for initialization
	void Start () {

        move = move.GetComponent<Image>();
        shoot = shoot.GetComponent<Image>();
        reverse = reverse.GetComponent<Image>();
        turret = turret.GetComponent<Image>();
        drone = drone.GetComponent<Image>();
        time = time.GetComponent<Image>();
        score = score.GetComponent<Image>();
        dodge = dodge.GetComponent<Image>();
        health = health.GetComponent<Image>();
        generator = generator.GetComponent<Image>();
        instructionText = instructionText.GetComponent<Text>();

        shoot.enabled = false;
        reverse.enabled = false;
        turret.enabled = false;
        drone.enabled = false;
        time.enabled = false;
        score.enabled = false;
        dodge.enabled = false;
        health.enabled = false;
        generator.enabled = false;
	}

    public void ChangeImage()
    {
        switch (instruction)
        {
            case 1:
                move.enabled = true;
                shoot.enabled = false;
                reverse.enabled = false;
                turret.enabled = false;
                drone.enabled = false;
                time.enabled = false;
                score.enabled = false;
                dodge.enabled = false;
                health.enabled = false;
                generator.enabled = false;
                break;
            case 2:
                move.enabled = false;
                shoot.enabled = true;
                reverse.enabled = false;
                turret.enabled = false;
                drone.enabled = false;
                time.enabled = false;
                score.enabled = false;
                dodge.enabled = false;
                health.enabled = false;
                generator.enabled = false;
                break;
            case 3:
                move.enabled = false;
                shoot.enabled = false;
                reverse.enabled = true;
                turret.enabled = false;
                drone.enabled = false;
                time.enabled = false;
                score.enabled = false;
                dodge.enabled = false;
                health.enabled = false;
                generator.enabled = false;
                break;
            case 4:
                move.enabled = false;
                shoot.enabled = false;
                reverse.enabled = false;
                turret.enabled = true;
                drone.enabled = false;
                time.enabled = false;
                score.enabled = false;
                dodge.enabled = false;
                health.enabled = false;
                generator.enabled = false;
                break;
            case 5:
                move.enabled = false;
                shoot.enabled = false;
                reverse.enabled = false;
                turret.enabled = false;
                drone.enabled = true;
                time.enabled = false;
                score.enabled = false;
                dodge.enabled = false;
                health.enabled = false;
                generator.enabled = false;
                break;
            case 6:
                move.enabled = false;
                shoot.enabled = false;
                reverse.enabled = false;
                turret.enabled = false;
                drone.enabled = false;
                time.enabled = true;
                score.enabled = false;
                dodge.enabled = false;
                health.enabled = false;
                generator.enabled = false;
                break;
            case 7:
                move.enabled = false;
                shoot.enabled = false;
                reverse.enabled = false;
                turret.enabled = false;
                drone.enabled = false;
                time.enabled = false;
                score.enabled = true;
                dodge.enabled = false;
                health.enabled = false;
                generator.enabled = false;
                break;
            case 8:
                move.enabled = false;
                shoot.enabled = false;
                reverse.enabled = false;
                turret.enabled = false;
                drone.enabled = false;
                time.enabled = false;
                score.enabled = false;
                dodge.enabled = true;
                health.enabled = false;
                generator.enabled = false;
                break;
            case 9:
                move.enabled = false;
                shoot.enabled = false;
                reverse.enabled = false;
                turret.enabled = false;
                drone.enabled = false;
                time.enabled = false;
                score.enabled = false;
                dodge.enabled = false;
                health.enabled = true;
                generator.enabled = false;
                break;
            case 10:
                move.enabled = false;
                shoot.enabled = false;
                reverse.enabled = false;
                turret.enabled = false;
                drone.enabled = false;
                time.enabled = false;
                score.enabled = false;
                dodge.enabled = false;
                health.enabled = true;
                generator.enabled = false;
                break;
            case 11:
                move.enabled = false;
                shoot.enabled = false;
                reverse.enabled = false;
                turret.enabled = false;
                drone.enabled = false;
                time.enabled = false;
                score.enabled = false;
                dodge.enabled = false;
                health.enabled = true;
                generator.enabled = false;
                break;
            case 12:
                move.enabled = false;
                shoot.enabled = false;
                reverse.enabled = false;
                turret.enabled = false;
                drone.enabled = false;
                time.enabled = false;
                score.enabled = false;
                dodge.enabled = false;
                health.enabled = false;
                generator.enabled = true;
                break;
        }
    }
	
    public void NextPress()
    {
        if (instruction < maxInstruction)
        {
            instruction++;
        }
        else
        {
            instruction = 1;
        }

        ChangeImage();
    }

    public void BackPress()
    {
        if (instruction > 1)
        {
            instruction--;
        }
        else
        {
            instruction = maxInstruction;
        }

        ChangeImage();
    }

    public void OnGUI()
    {
        switch (instruction)
        {
            case 1:
                instructionText.text = "Move your ship with the left analog stick"; 
                break;
            case 2:
                instructionText.text = "To shoot move the right analog stick in the direction you want to shoot in";
                break;
            case 3:
                instructionText.text = "The ship can travel faster in the forward direction, so reversing and shooting will slow you down a bit";
                break;
            case 4:
                instructionText.text = "Stationary turrets are placed around the levels, they will shoot you when you get in their range";
                break;
            case 5:
                instructionText.text = "Drones spawn from drone hatches, they will follow and shoot you";
                break;
            case 6:
                instructionText.text = "The longer you take the more the drones will spawn";
                break;
            case 7:
                instructionText.text = "For each enemey that you destroy your score will go up";
                break;
            case 8:
                instructionText.text = "Avoid the laser beams that are shot by the enemey";
                break;
            case 9:
                instructionText.text = "Your shield decreases when you are hit by enemies";
                break;
            case 10:
                instructionText.text = "Once your shield runs out, you start losing health";
                break;
            case 11:
                instructionText.text = "Your shield recharges after a few seconds without being hit";
                break;
            case 12:
                instructionText.text = "Destroy the generators to open the laser fences";
                break;
        }
        
    }

}
