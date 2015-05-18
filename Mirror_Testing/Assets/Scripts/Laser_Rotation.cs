using UnityEngine;
using System.Collections;

public class Laser_Rotation : MonoBehaviour {

	/* I wrote this for testing purpose only */

	public float speed = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {;
		transform.RotateAround(transform.position, transform.forward, speed);
	}
}
