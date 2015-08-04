 using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
	// For jumping to work normally, all "jumpable" objects need to be tagged as "Environment"

	public float speed = 20f;
	public float jumpSpeed = 4000f;
	public float verticalRange = 60f;
	public bool isForbidden = false;


	float mouseSensitivity;
	private Vector3 movement;
	private Rigidbody playerRB;
	private float rotV = 0f;
	private bool grounded = true;
//	private float strength = 15f;
//	private float lastVelocity = 0f;

	float initialMass;

	Quaternion previousRot;
//	Vector3 previousPos;

	public float MouseSensitivity {
		get {
			return mouseSensitivity;
		}
		set {
			mouseSensitivity = value;
		}
	}

	// Use this for initialization
	void Awake ()
	{
		playerRB = GetComponent<Rigidbody> ();
		initialMass = playerRB.mass;
	
	}

	void Update () {
		if (Input.GetButtonDown ("Jump") && grounded == true)
			Jump ();

//		if ((playerRB.velocity.magnitude - lastVelocity) / Time.deltaTime * playerRB.mass > strength){
//			isForbidden = true;
//		}
//		lastVelocity = playerRB.velocity.magnitude;
	}
	
	void FixedUpdate ()
	{
		if (isForbidden){
			transform.rotation = previousRot;
//			transform.position = previousPos;
			isForbidden = false;
		} else {
			float h = Input.GetAxisRaw ("Horizontal");
			float v = Input.GetAxisRaw ("Vertical");
			
			Move (h, v);
		}
		previousRot = playerRB.transform.rotation;
//		previousPos = playerRB.transform.position;

		if (Input.GetButtonDown ("Jump") && grounded == true)
			Jump ();
	}

	void Move (float h, float v)
	{
		float rotH = Input.GetAxis ("Mouse X") * mouseSensitivity;


		rotV -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		rotV = Mathf.Clamp (rotV, -verticalRange, verticalRange);

		Camera.main.transform.localRotation = Quaternion.Euler (rotV, 0, 0);

		transform.Rotate (0, rotH, 0);

		movement.Set (h, 0f, v);
		movement = transform.rotation * movement.normalized * (speed * initialMass / playerRB.mass) * Time.deltaTime;

		playerRB.MovePosition (transform.position + movement);
	}

	void OnTriggerEnter (Collider other)
	{

			grounded = true;


	}

	void OnTriggerExit (Collider other)
	{
			grounded = false;

		
	}

	void Jump ()
	{
	
			playerRB.AddForce (Vector3.up * jumpSpeed);
			grounded = false;


	}
	

}
	