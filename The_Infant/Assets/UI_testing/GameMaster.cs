using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {


	//These fields are essential data for gameplay.
	//Reference may be made from UI menu and other gameObject
	//More of such may be added in the course of development.
	public float mouseSensitivity;
	public float musicVolume;
	public float soundEffectVolume;
	public float totalTimePlayed;
	public int currentLevel;
	public int numberOfPuzzleUnlocked;
	public Canvas inGameMenu;


	bool isPaused;
	
	void Start () {
		
	}

	void Update () {
		//for testing
		print ("Music Volume is: " + musicVolume);

		if (Input.GetKeyDown(KeyCode.Escape)) {
			PauseGame();
		}

	}

	public void quitGame () {
		Application.Quit();
	}

	public void PauseGame() {

		if (isPaused == false) {
			//activate in-game menu and pause the game.
			inGameMenu.gameObject.SetActive(true);

			//******************
			//add or reference your pausing scripts here :)
			//******************
			
			isPaused = true;
			print ("paused");
		}
		else {
			inGameMenu.gameObject.SetActive(false);
			
			isPaused = false;
			print ("unpaused");
		}
	}
}
