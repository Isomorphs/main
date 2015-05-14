using UnityEngine;
using System.Collections;

public class Laser_Rotation : MonoBehaviour {

	public float speed = 10.0f;
	//public float smooth = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//float angle = speed;
		//Quaternion target = Quaternion.Euler(0, 0, angle);
		transform.RotateAround(transform.position, transform.forward, speed);
	}
}
