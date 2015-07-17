using UnityEngine;
using System.Collections;

public class Level_2_Laser_action_Bridge : MonoBehaviour {

	public Color PulsingColor;
	public float speed = 1f;
	public GameObject bridge;
	Renderer rend;
	Color initColor;
	Rigidbody rb;
	bool forceAdded = false;
	float chargingTime = 3f;
	float timeElapsed = 0f;
	bool triggered = false;

	void Start(){
		rend = GetComponent<Renderer>();
		initColor = rend.material.color;
		rb = bridge.GetComponent<Rigidbody>();
	}

	void FixedUpdate(){
		if (triggered)
			rend.material.color = Color.Lerp(rend.material.color, PulsingColor, Time.fixedDeltaTime / 3f);
	}

	void TriggeredByLaser () {
		triggered = true;
		transform.RotateAround(transform.position, transform.TransformDirection(Vector3.forward), speed);

		timeElapsed += Time.deltaTime;

		if (!forceAdded && timeElapsed > chargingTime){
			rb.AddForceAtPosition(transform.TransformVector(Vector3.forward) * rb.mass * 100000f, transform.position);
			forceAdded = true;
		}
	}
}
