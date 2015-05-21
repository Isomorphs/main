using UnityEngine;
using System.Collections;

public class LaserReceiverTest1 : MonoBehaviour {
	// the Raycasthit variable "hit" in the laser reflection script needs to be public
	// Triggers should be tagged as "LaserTrigger"


	GameObject laserSource;
	public float speed = 10f;

	// Use this for initialization
	void Start () {
		laserSource = GameObject.Find ("Laser_Source");
	}
	
	// Update is called once per frame
	void Update () {
		if (laserSource.GetComponentInChildren <Laser_Reflection> ().hit.collider.tag == "LaserTrigger")
		{
			transform.position = (transform.position + transform.up * speed * Time.deltaTime);
			print ("Trigger works!"); // Should be replaced by more level-specific codes in the future
		}
	}
}
