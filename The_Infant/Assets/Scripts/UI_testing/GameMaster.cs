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
	public Canvas inGameHint;

	GameObject player;

	bool isPaused;
	
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");

		//initalise mouse sensitivity (and other properties)
		applySettings();
	}

	void Update () {
		//for testing
//		print ("Music Volume is: " + musicVolume);

		if (Input.GetKeyDown(KeyCode.Escape)) {
			PauseGame();
		}

	}

	public void quitGame () {
		//this.gameObject.GetComponent<SaveLoadProgress>().Save();
		Application.Quit();
	}

	public void PauseGame() {

		if (isPaused == false) {
			//activate in-game menu and pause the game.
			inGameMenu.gameObject.SetActive(true);
			inGameHint.gameObject.SetActive(false);

			Time.timeScale = 0f;

			//******************
			//add or reference your pausing scripts here :)
			//******************
			
			isPaused = true;
			print ("paused");
		}
		else {
			inGameMenu.gameObject.SetActive(false);
			inGameHint.gameObject.SetActive(true);

			applySettings();

			Time.timeScale = 1.0f;
			
			isPaused = false;
			print ("unpaused");
		}
	}

	//settings to apply when user exits the in-game menu
	void applySettings(){
		//rmb to scale down the value by 10 times
		player.GetComponent<CharacterMovement>().MouseSensitivity = mouseSensitivity / 10f;
	}
}
