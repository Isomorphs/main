using UnityEngine;
using System.Collections;

public class AddforceTest : MonoBehaviour {

	GameObject hand;
	GameObject handle;
	Quaternion camRot;
	Rigidbody itemRB;
	bool holding;
	public Vector3 offset;
	Vector3 forceDir;
	public float force;

	// Use this for initialization
	void Start () {
		itemRB = GetComponent<Rigidbody>();
		hand = GameObject.FindWithTag("hand");
		handle = GameObject.Find("Handle");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump"))
			holding = true;
	}

	void FixedUpdate(){
		if (holding){
			camRot = Camera.main.transform.rotation;
			forceDir = hand.transform.position - handle.transform.position;
			itemRB.AddForceAtPosition(forceDir * force, handle.transform.position);
			itemRB.rotation = camRot;
		}
	}
}
