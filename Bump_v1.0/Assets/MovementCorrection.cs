using UnityEngine;
using System.Collections;

public class MovementCorrection : MonoBehaviour {
	GameObject player;
	PickUpAction pickUpScript;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		pickUpScript = player.GetComponent<PickUpAction> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
