 using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
	// For jumping to work normally, all "jumpable" objects need to be tagged as "Environment"

	public float speed = 2.0f;
	public float jumpSpeed = 10f;
	public float mouseSensitivity = 60f;
	public float verticalRange = 80f;
	public bool isForbidden = false;

	private Vector3 movement;
	private Rigidbody playerRB;
	private float rotV = 0f;
	private bool grounded = true;

	Quaternion previousRot;
	Vector3 previousPos;

	// Use this for initialization
	void Awake ()
	{
		playerRB = GetComponent<Rigidbody> ();
	
	}

	void Update () {
		if (Input.GetButtonDown ("Jump") && grounded == true)
			Jump ();
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
		previousPos = playerRB.transform.position;
	}

	void Move (float h, float v)
	{
		float rotH = Input.GetAxis ("Mouse X") * mouseSensitivity;


		rotV -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		rotV = Mathf.Clamp (rotV, -verticalRange, verticalRange);

		Camera.main.transform.localRotation = Quaternion.Euler (rotV, 0, 0);

		transform.Rotate (0, rotH, 0);

		movement.Set (h, 0f, v);
		movement = transform.rotation * movement.normalized * speed * Time.deltaTime;

		playerRB.MovePosition (transform.position + movement);
	}

	void OnCollisionStay (Collision collision)
	{
		if (collision.gameObject.tag == "Environment") {
			grounded = true;
			//Destroy(collision.gameObject);
		}

	}

	void OnCollisionExit (Collision collision)
	{
		if (collision.gameObject.tag == "Environment") {
			grounded = false;
			//Destroy(collision.gameObject);
		}
		
	}

	void Jump ()
	{
	
			playerRB.AddForce (Vector3.up * jumpSpeed);
			grounded = false;


	}
	

}
	