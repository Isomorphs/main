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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		print ("Music Volume is: " + musicVolume);
	}

	public void quitGame () {
		Application.Quit();
	}
}
