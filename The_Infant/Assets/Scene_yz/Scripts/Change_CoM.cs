using UnityEngine;
using System.Collections;

public class Change_CoM : MonoBehaviour {
	//CoM can't be changed in the editor so I have to write this shit
	public Vector3 CentreOfMass;
	void Start () {
		GetComponent<Rigidbody>().centerOfMass = CentreOfMass;
	}
}
