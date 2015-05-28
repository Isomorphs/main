using UnityEngine;
using System.Collections;

public class Laser_Rotation : MonoBehaviour {

	/* I wrote this for testing purpose only */
	public Color PulsingColor;
	public float speed = 1f;
	public float openingSpeed = 1f;
	public float period = 2f;
	GameObject door1;
	GameObject door2;
	Renderer rendering;
	Color initialColor;
	float timeElapsed;
	bool triggered;


	// Use this for initialization
	void Start () {
		door1 = GameObject.Find("MiddleWall");
		door2 = GameObject.Find("FinalGate");
		rendering = GetComponent<Renderer>();
		initialColor = rendering.material.color;
		timeElapsed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(transform.position, Vector3.up, speed);
//		if (triggered)
//		{
//			timeElapsed += Time.deltaTime;
//			if (timeElapsed == period)
//				timeElapsed = 0;
//			rendering.material.color = Color.Lerp(rendering.material.color, initialColor, timeElapsed / period);
//		}
	}

	public void OpenTheDoor () {
		print("opening the door");
		rendering.material.color = PulsingColor;
		door1.transform.position = (door1.transform.position + Vector3.up * openingSpeed * Time.deltaTime * -1f);
		if (door1.transform.position.y < -5f)
			door2.transform.position = (door2.transform.position + Vector3.up * openingSpeed * Time.deltaTime * -1f);

		timeElapsed += Time.deltaTime;
		if (timeElapsed == period) {
			rendering.material.color = initialColor;
			timeElapsed = 0;
		}
		rendering.material.color = Color.Lerp(rendering.material.color, initialColor, timeElapsed / period);
	}
}
