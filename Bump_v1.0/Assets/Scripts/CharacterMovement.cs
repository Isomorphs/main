using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	public float speed = 2.0f;
	public float jumpSpeed = 10f;
	public float mouseSensitivity = 60f;
	public float verticalRange = 80f;

	private Vector3 movement;
	private Rigidbody playerRB;
	private float rotV = 0f;
	private bool grounded = true;

	// Use this for initialization
	void Awake () {
		playerRB = GetComponent<Rigidbody> ();
	
	}
	

	void FixedUpdate () {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		//onCollisionEnter (Collider info);

		Jump ();
	
		Move (h,v);
	}

	void Move (float h, float v) {
		float rotH = Input.GetAxis ("Mouse X") * mouseSensitivity;


		rotV -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		rotV = Mathf.Clamp(rotV, -verticalRange, verticalRange);

		Camera.main.transform.localRotation = Quaternion.Euler(rotV,0,0);


		transform.Rotate(0,rotH,0);

		movement.Set (h, 0f, v);
		movement = transform.rotation * movement.normalized * speed * Time.deltaTime;

		playerRB.MovePosition (transform.position + movement);
	}

	void Jump () {
		if (Input.GetButtonDown ("Jump")) {
			if (grounded) {
			GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed);
			//GetComponent<Rigidbody>().velocity += Vector3.up * j umpSpeed;
				grounded = false;
			}
		}

		else grounded = true;
	}
	
	void onCollisionEnter (Collider info) {
		if (info.tag == "Floor")
			grounded = true;
	}
}
	