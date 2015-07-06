using UnityEngine;
using System.Collections;

public class yz_1_laser_trigger_rotation : MonoBehaviour {

	/* I wrote this for testing purpose only */
	public Color PulsingColor;
	public float speed = 1f;
	public float period = 2f;

	Renderer rendering;
	Color initialColor;
	float timeElapsed;
	bool triggered;
	
	
	// Use this for initialization
	void Start () {
		rendering = GetComponent<Renderer>();
		initialColor = rendering.material.color;
		timeElapsed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(transform.position, Vector3.forward, speed);

		rendering.material.color = PulsingColor;
	
		timeElapsed += Time.deltaTime;
		if (timeElapsed > period) {
			rendering.material.color = initialColor;
			timeElapsed = 0;
		}
		rendering.material.color = Color.Lerp(rendering.material.color, initialColor, timeElapsed / period);
	}
}
