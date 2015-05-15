using UnityEngine;
using System.Collections;

public class ReflectionCamMovement : MonoBehaviour {

	GameObject mirror;
	Vector3 offset;
	Vector3 rot;

	// Use this for initialization
	void Start () {
		mirror = GameObject.Find ("Mirror");
	}
	
	// Update is called once per frame
	void Update () {
		offset.x = Camera.main.transform.position.x;
		offset.y = Camera.main.transform.position.y;
		offset.z = mirror.transform.position.z * 2f - Camera.main.transform.position.z;

		transform.position = offset;

		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		RaycastHit hit;
		if (Physics.Raycast (transform.position, fwd, out hit)) {
			rot = hit.point;
			Vector3 relativePos = rot - transform.position;
			Quaternion rotation = Quaternion.LookRotation (relativePos);
			transform.rotation = rotation;
		}
	}
}
