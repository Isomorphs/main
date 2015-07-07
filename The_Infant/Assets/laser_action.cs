using UnityEngine;
using System.Collections;

public class laser_action : MonoBehaviour {

	/* I wrote this for testing purpose only */
	public Color PulsingColor;
	public float speed = 1f;
	public float period = 2f;
	public int targetFloor, currentFloor;

	Renderer rendering;
	Color initialColor;
	float timeElapsed;
	bool triggered = false;
	lift_movement lift_control;
	float level_height = 35f;  //scene specific!
	
	// Use this for initialization
	void Start () {
		rendering = GetComponent<Renderer>();
		initialColor = rendering.material.color;
		timeElapsed = 0;
		lift_control = GameObject.Find("lift").GetComponent<lift_movement>();
		currentFloor = (int)(GetComponent<Transform>().position.y / level_height);
	}

	void Update () {
		if (triggered){
			if (lift_control.maxHeight < targetFloor)
				lift_control.maxHeight = targetFloor;
		}
		else if (lift_control.maxHeight >= targetFloor){
			lift_control.maxHeight = currentFloor;
			rendering.material.color = initialColor;
		}
		triggered = false;

//		timeElapsed += Time.deltaTime;
//		if (timeElapsed > period) {
//			rendering.material.color = initialColor;
//			timeElapsed = 0;
//		}
//		rendering.material.color = Color.Lerp(rendering.material.color, initialColor, timeElapsed / period);

	}
	
	// Update is called once per frame
	public void TriggeredByLaser () {
		//print(triggeredLaserFrame);
		triggered = true;
		transform.RotateAround(transform.position, Vector3.forward, speed);

		rendering.material.color = PulsingColor;
	

	}


}
