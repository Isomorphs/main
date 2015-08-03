using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadChosenLevelFromMenu : MonoBehaviour {

	public InputField inputForGameLoading;
	LoadLevel loadlevel;

	// Use this for initialization
	void Start () {
		loadlevel = GameObject.Find("GameController").GetComponent<LoadLevel>();
	}

	//convert string to an int and load the selected level
	public void LoadChosenLevel () {
		loadlevel.LoadNewLevel(int.Parse(inputForGameLoading.text.ToString()));
	}

}
