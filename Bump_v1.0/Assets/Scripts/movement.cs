using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {
	
	public float speed = 2.0f;
	public float mouseSensitivity = 60f;
	public float verticalRange = 80f;
	public float armLength;
	Vector3 handLocation;
//	Ray camRay;
	private Vector3 movementV;
	private Rigidbody playerRB;
	private float rotV = 0f;
	private GameObject hand;
	
	// Use this for initialization
	void Awake () {
		playerRB = GetComponent<Rigidbody> ();
		hand = GameObject.FindWithTag("hand");
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		
		Move (h,v);
		handLocation = Camera.main.transform.position + Camera.main.transform.forward * 2f;
		hand.transform.position = Camera.main.transform.position + Camera.main.transform.forward * armLength;
		//playerRB.AddForce (Vector3.up * Input.GetAxisRaw ("Jump") * 20f);

	}
	
	void Move (float h, float v) {
		float rotH = Input.GetAxis ("Mouse X") * mouseSensitivity;
		
		
		rotV -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		rotV = Mathf.Clamp(rotV, -verticalRange, verticalRange);
		
		Camera.main.transform.localRotation = Quaternion.Euler(rotV,0,0);
		
		
		transform.Rotate(0,rotH,0);
		
		movementV.Set (h, 0f, v);
		movementV = transform.rotation * movementV.normalized * speed * Time.deltaTime;
		
		playerRB.MovePosition (transform.position + movementV);
	}
}
