using UnityEngine;
using System.Collections;

public class Level_2_Laser_action_rotate_platform : MonoBehaviour {
	
	public Color PulsingColor;
	public float speed = 1f;
	public float period = 2f;
	public GameObject rotating_object;
	public float rotatingSpeed;
	Renderer rend;
	Color initColor;

	bool triggered = false, triggeredLastFrame = false;
	bool rotating = false;

	void Start(){
		rend = rotating_object.GetComponent<Renderer>();
		initColor = rend.material.color;
	}
	
	void Update () {
		
		if ((triggeredLastFrame && !triggered)){ //if laser stops triggering this.
			rotating = false;
			rend.material.color = initColor;
		} else if (!triggeredLastFrame && triggered){ //if laser begins triggering this.
			rotating = true;
			rend.material.color = Color.red;
		}
		
		triggeredLastFrame = triggered;
		triggered = false;
	}

	void FixedUpdate(){
		if (rotating)
			rotating_object.transform.Rotate(Vector3.forward * rotatingSpeed);
	}
	
	// Update is called once per frame
	public void TriggeredByLaser () {
		print("triggered");
		triggered = true;
		transform.RotateAround(transform.position, Vector3.forward, speed);
	}

}
