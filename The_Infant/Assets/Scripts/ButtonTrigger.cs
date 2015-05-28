using UnityEngine;
using System.Collections;

public class ButtonTrigger : MonoBehaviour {
	public int levelToLoad = 1;

	LoadLevel loadLevelScript;

	// Use this for initialization
	void Start () {
		loadLevelScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<LoadLevel>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.collider.name == "Button")
			loadLevelScript.LoadNewLevel (levelToLoad);
	}
}
