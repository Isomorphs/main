using UnityEngine;
using System.Collections;

public class ButtonTriggerHorizontal : MonoBehaviour {

	public int levelToLoad = 1;

	GameObject gameController;
	LoadLevel loadLevel;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		loadLevel = gameController.GetComponent<LoadLevel>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision collision) {
		// For horizontal button, uncomment the part below and comment out the other parts.
		//if (collision.collider.GetComponent<Rigidbody> ().mass >= 51)
//			loadLevel.LoadNewLevel (levelToLoad);

		// For vertical button, uncomment the part below and comment out the other parts.
		//loadLevel.LoadNewLevel (levelToLoad);

	}
}
