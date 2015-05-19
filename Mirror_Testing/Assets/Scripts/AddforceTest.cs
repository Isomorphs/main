using UnityEngine;
using System.Collections;

public class AddforceTest : MonoBehaviour {

	GameObject hand;
	GameObject handle;
	Quaternion camRot;
	Rigidbody itemRB;
	bool holding = false;
	Vector3 forceDir;
	public float force;

	// Use this for initialization
	void Start () {
		itemRB = GetComponent<Rigidbody>();
		hand = GameObject.FindWithTag("hand");
		handle = GameObject.Find("handle");
	}
	
	// Update is called once per frame
	void Update () {
		//get user input (Space bar for picking up items)
		if(Input.GetButtonDown("Jump"))
		{
			if (holding){
				holding = false;
				itemRB.useGravity = true;
			}
			else {
				holding = true;
				//disable gravity for smooth movement
				itemRB.useGravity = false;
			}
		}
	}

	void FixedUpdate(){
		if (holding){
			//get the direction to which the player is facing
			camRot = Camera.main.transform.rotation;

			//and direction of the force needed to be applied
			forceDir = hand.transform.position - handle.transform.position;

			//set transform of the item's rigidbody
			itemRB.AddForceAtPosition(forceDir * force, handle.transform.position, ForceMode.Force);
			itemRB.MoveRotation(camRot);
		}
	}
}
