using UnityEngine;
using System.Collections;

public class ButtonTrigger : MonoBehaviour {
	public int levelToLoad = 1;

	LoadLevel loadLevelScript;
	GameObject gameController;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		loadLevelScript = gameController.GetComponent<LoadLevel>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.collider.name == "Button")
			gameController.GetComponent<SaveLoadProgress>().Save ();
			loadLevelScript.LoadNewLevel (levelToLoad);
	}
}
