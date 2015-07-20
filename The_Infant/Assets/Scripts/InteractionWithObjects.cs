using UnityEngine;
using System.Collections;

public class InteractionWithObjects : MonoBehaviour {
	public float armLength = 5f;
	public float strength = 15f;
	public float throwingStrength = 50f;
	public bool AddRandomTorque = true;
	public float turning = 5f;
	public float interactionDistance = 10f;
	public LayerMask mask; //label this as "Default" in the editor mode

	RaycastHit hit;
	Ray camray;
	GameObject item;
	Rigidbody itemRB;
	float itemMass;
	bool carrying = false;
	Camera cam;
	Vector3 centre;
	Transform initTrans;
	float initMass;
	float smoothing = 1000f;
	CharacterMovement movement;
	float lastVelocity;

	GameObject lastObjectHit = null;
	KeyCode pressedKey;

	void Start(){
		cam = Camera.main.GetComponent<Camera>();
		centre = new Vector3(cam.pixelWidth / 2f, cam.pixelHeight / 2f, 0);
		initMass = GetComponent<Rigidbody>().mass;
		movement = GameObject.FindWithTag("Player").GetComponent<CharacterMovement>();
	}

	void Update(){
		//set a new cam ray in each frame
		camray = cam.ScreenPointToRay(centre);

		//if carrying anything, there is no need to do raycast.
		if (carrying || !Physics.Raycast(camray, out hit, interactionDistance, mask)){
			hit = new RaycastHit(); //reset the hit info if not hitting anything.
		}
		Debug.DrawLine(camray.origin, hit.point, Color.red, Time.deltaTime * 60);

		//pick up or throw an object
		if (Input.GetKeyDown(KeyCode.E)){
			pressedKey = KeyCode.E;
			if (carrying){
				Drop();
				item = null;
			} else {
				PickUp();
			}
		}
		if (carrying && Input.GetKeyDown(KeyCode.Q)){
			pressedKey = KeyCode.Q;
			Drop();
			Throw();
			item = null;
		}

		//Send messages accordingly
		if (hit.transform != null){
			if (lastObjectHit == null) {
				//the first object is hit.
				if (hit.transform.CompareTag("Interact")){
					hit.transform.SendMessage("OnHitByCamRay", pressedKey);
				}
				lastObjectHit = hit.transform.gameObject;
			} else if (lastObjectHit != hit.transform.gameObject){
				//a different object is hut
				print("new object hit");
				if (hit.transform.CompareTag("Interact")) hit.transform.SendMessage("OnHitByCamRay", pressedKey);
				if (lastObjectHit.CompareTag("Interact")) lastObjectHit.SendMessage("OnCamRayExit", pressedKey);
				lastObjectHit = hit.transform.gameObject;
			} else if (lastObjectHit == hit.transform.gameObject){
				//the same object is being hit.
				if (hit.transform.CompareTag("Interact")){
					lastObjectHit.SendMessage("OnCamRayStay", pressedKey);
				}
			}
		} else if (lastObjectHit != null){
			//stop hitting an object.
			if (lastObjectHit.transform.CompareTag("Interact")) lastObjectHit.SendMessage("OnCamRayExit", pressedKey);
			lastObjectHit = null;
		}

		pressedKey = KeyCode.None;
	}

	void FixedUpdate(){
		if (carrying) {
			carry();
//			print ("carrying");
		}
	}

	void PickUp(){
		if (hit.collider == null || hit.collider.attachedRigidbody == null) return;

		if (hit.collider.attachedRigidbody.mass > strength) {
			print("not strong enough");
			return;
		}

		carrying = true;
		item = hit.collider.gameObject;
		itemRB = item.GetComponent <Rigidbody> ();
		itemMass = itemRB.mass;

		this.GetComponent <Rigidbody> ().mass = initMass + itemMass;

		//Set's initial rotation to be same as player
		item.transform.rotation = this.transform.rotation;

		initTrans = item.transform.parent;

		//Make carried item a child of player
		item.transform.parent = this.transform;
		
		//Destroy item's Rigidbody to correct item's movement
		Destroy (itemRB);
		print("picked up");
	}

	void carry(){
		//Change position of the carried item accordingly
		item.transform.position = Vector3.Lerp (item.transform.position, cam.transform.position + cam.transform.forward * armLength
		                                               , Time.deltaTime * smoothing);
	}

	void Drop(){
		carrying = false;
		item.transform.parent = initTrans;
		item.AddComponent <Rigidbody> (); 	// Add back rigidbody for future use
		item.GetComponent <Rigidbody> ().mass = itemMass;
		this.GetComponent<Rigidbody> ().mass = initMass;
	}
	void Throw () {
		
		itemRB = item.GetComponent<Rigidbody>();
		//throw!
		itemRB.AddForce(cam.transform.forward * throwingStrength);
		
		//add a random rotation of the item being thrown away.
		if (AddRandomTorque)
			itemRB.AddRelativeTorque(Random.onUnitSphere * turning);
	}

	void OnCollisionStay(Collision col){

		//in case we hit the environment
		if (carrying && col != null && col.rigidbody != null){
			//compute acceleration of the rigidbody we have hit. If it exceeds an arbitrary value, prohibit this movement by the player
			if (col.rigidbody.mass * (col.rigidbody.velocity.magnitude - lastVelocity) / Time.fixedDeltaTime > 50f){
				movement.isForbidden = true;
				print ("colliding");
			}
			lastVelocity = col.rigidbody.velocity.magnitude;
		}
	}
}
