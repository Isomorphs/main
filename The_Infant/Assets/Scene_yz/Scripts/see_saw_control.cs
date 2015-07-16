using UnityEngine;
using System.Collections;

public class see_saw_control : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GetComponent<Rigidbody>().AddRelativeTorque(Vector3.left * -10000f);
		print ("torque");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider col){
		gameObject.AddComponent<FixedJoint>();
		GetComponent<FixedJoint>().anchor = col.transform.position;
		GetComponent<FixedJoint>().breakForce = 10f;
	}
}
