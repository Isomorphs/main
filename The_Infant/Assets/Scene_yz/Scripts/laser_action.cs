using UnityEngine;
using System.Collections;

public class laser_action : MonoBehaviour {

	/* Used for scene_yz (lift trigger) */
	public Color PulsingColor;
	public float speed = 1f;
	public float period = 2f;
	public int targetFloor, currentFloor;

//	Renderer rendering;
//	Color initialColor;
//	float timeElapsed;
	bool triggered = false, triggeredLastFrame = false;
	lift_movement lift_control;
	float level_height = 35f;  //scene specific!
	
	// Use this for initialization
	void Start () {
//		rendering = GetComponent<Renderer>();
//		initialColor = rendering.material.color;
//		timeElapsed = 0;
		lift_control = GameObject.Find("lift").GetComponent<lift_movement>();
		currentFloor = (int)(GetComponent<Transform>().position.y / level_height);
	}

	void Update () {

		if ((triggeredLastFrame && !triggered)){ //if laser stops triggering this.
			GetNewLiftKey(targetFloor, false);
			print ("update lift");
		} else if (!triggeredLastFrame && triggered){ //if laser begins triggering this.
			GetNewLiftKey(targetFloor, true);
			print ("update lift");
		}

		triggeredLastFrame = triggered;
		triggered = false;
	}

	void GetNewLiftKey(int target, bool isAccessible){
		lift_control.keys[target] = isAccessible;
		lift_control.UpdateLevelLimits();
	}

	// Update is called once per frame
	public void TriggeredByLaser () {
		//print("triggered");
		triggered = true;
		transform.RotateAround(transform.position, transform.TransformDirection(Vector3.forward), speed);
//		lift_control.UpdateLevelLimits();
//		rendering.material.color = PulsingColor;
//		timeElapsed += Time.deltaTime;
//		if (timeElapsed > period) {
//			rendering.material.color = initialColor;
//			timeElapsed = 0;
//		}
//		rendering.material.color = Color.Lerp(rendering.material.color, initialColor, timeElapsed / period);

	}


}
