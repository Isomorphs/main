using UnityEngine;
using System.Collections;

public class SceneTrigger : MonoBehaviour {
	public int Level;
	GameObject controller;

	void Awake () {
		controller = GameObject.Find("GameController");
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player")
			controller.GetComponent<LoadLevel>().LoadNewLevel(Level);
	}
}
