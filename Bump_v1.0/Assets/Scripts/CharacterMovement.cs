 using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{

	public float speed = 2.0f;
	public float jumpSpeed = 10f;
	public float mouseSensitivity = 60f;
	public float verticalRange = 80f;

	private Vector3 movement;
	private Rigidbody playerRB;
	private float rotV = 0f;
	private bool grounded = true;

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
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		//onCollisionEnter (Collider info);

	
		Move (h, v);
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

	void OnCollisionEnter (/*Collision collision*/)
	{
		//if (collision.gameObject.tag == "Floor") {
			grounded = true;
			//Destroy(collision.gameObject);
		//}

	}

	void Jump ()
	{
	
			GetComponent<Rigidbody> ().AddForce (Vector3.up * jumpSpeed);
			//GetComponent<Rigidbody>().velocity += Vector3.up * j umpSpeed;
			grounded = false;


	}
	

}
	