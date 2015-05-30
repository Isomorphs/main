using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadChosenLevelFromMenu : MonoBehaviour {

	public InputField inputForGameLoading;
	LoadLevel loadlevel;

	// Use this for initialization
	void Start () {
		//inputForGameLoading = GameObject.Find("LoadGameInput");
		loadlevel = GameObject.Find("GameController").GetComponent<LoadLevel>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//convert string to an int and load the selected level
	public void LoadChosenLevel () {
		loadlevel.LoadNewLevel(int.Parse(inputForGameLoading.text.ToString()));
	}

}
