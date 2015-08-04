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

	//these limits are used to improve the movement of pick up object
//	float speedLimit = 10f;
//	float angularSpeedLimit = 30f;
//	float resetRange = 0.5f;
//	float dropOffRange; // if the object is further away from player's hand by this distance, the object will be dropped off automatically

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
	float smoothing = 50f;
	Quaternion rot, handRot;

	float air_resistance;

//	Vector3[] anchors;
	Vector3 hand;
	Vector3 initCoM;

	GameObject lastObjectHit = null;
	KeyCode pressedKey;

	void Start(){
		cam = Camera.main.GetComponent<Camera>();
		centre = new Vector3(cam.pixelWidth / 2f, cam.pixelHeight / 2f, 0);
		initMass = GetComponent<Rigidbody>().mass;
//
//		//six anchors to stabilise the object picked up. Can add more or remove some.
//		anchors = new Vector3[1];
//		anchors.Initialize();
//		anchors[0] = Vector3.zero;
////		anchors[0] = Vector3.up;
////		anchors[1] = -Vector3.up;
////		anchors[2] = Vector3.left;
////		anchors[3] = -Vector3.left;
////		anchors[4] = Vector3.forward;
////		anchors[5] = -Vector3.forward;

		//automatically set this for convenience. Can be commented out if need to customise this range
		//dropOffRange = armLength * 2f + cam.transform.localPosition.magnitude;
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

			//reset and smoothing out movement
//			if (itemRB.velocity.magnitude > speedLimit) itemRB.velocity = Vector3.zero;
//			if (itemRB.angularVelocity.magnitude > angularSpeedLimit) itemRB.angularVelocity = Vector3.zero;
//			if (Vector3.Distance(item.transform.position, hand) < resetRange){
//				itemRB.MovePosition(hand);
//			}
			if (Quaternion.Angle(rot, handRot) > 3f){
				itemRB.MoveRotation(Quaternion.Slerp(rot, handRot, Time.fixedDeltaTime * smoothing));
			} else {
				itemRB.MoveRotation(handRot);
			}
		}
	}

	void PickUp(){
		if (hit.collider == null || hit.collider.attachedRigidbody == null) return;

		if (hit.collider.attachedRigidbody.mass > strength) {
			print("not strong enough");
			return;
		}

		print("Picking Up");
		carrying = true;
		item = hit.collider.gameObject;
		itemRB = item.GetComponent <Rigidbody> ();
		itemMass = itemRB.mass;
		this.GetComponent <Rigidbody> ().mass = initMass + itemMass;

		itemRB.mass = 0.1f;  //Do not change this number!
		itemRB.useGravity = false;

		itemRB.drag = 0f;

		//in case some rigidbody's CoM are manually changed.
		initCoM = itemRB.centerOfMass;
		itemRB.centerOfMass = Vector3.zero;

		//critical damping
		air_resistance = Mathf.Sqrt(itemRB.mass * strength) * 2f;
		air_resistance *= 6f;

		//initialise position
//		updateHandPosition();
//		itemRB.MovePosition(hand);

		//original solution----------------
//		//Set's initial rotation to be same as player
//		item.transform.rotation = this.transform.rotation;
//
//		initTrans = item.transform.parent;
//
//		//Make carried item a child of player
//		item.transform.parent = this.transform;
//		
//		//Destroy item's Rigidbody to correct item's movement
//		Destroy (itemRB);
//		print("picked up");
		////////////////////////////////

		//Joint Solution--------------------
//		itemRB.mass = 0f;
//		item.AddComponent<SpringJoint>();
//		print("Joint added");
//		item.GetComponent<SpringJoint>().damper = 1f;
//		item.GetComponent<SpringJoint>().spring = 20f;
//		item.GetComponent<SpringJoint>().anchor = Vector3.zero;
//		item.GetComponent<SpringJoint>().autoConfigureConnectedAnchor = false;
////		item.GetComponent<FixedJoint>().enableCollision = true;
		/// //////////////////////////////
	}

	void updateHandPosition(){
		hand = Vector3.Lerp(hand, cam.transform.position + cam.transform.forward * armLength,
		                    Time.fixedDeltaTime * smoothing);
	}

	void carry(){
		//original solution -------------------------
//		//Change position of the carried item accordingly
//		item.transform.position = Vector3.Lerp (item.transform.position, cam.transform.position + cam.transform.forward * armLength
//		                                               , Time.deltaTime * smoothing);
		////////////////////////////////////////////

		//Joint Solution -------------------------------
//		item.GetComponent<SpringJoint>().connectedAnchor = Camera.main.transform.forward * armLength
//			+ Camera.main.transform.position;
//
//		Vector3 temp = item.transform.position - item.GetComponent<SpringJoint>().connectedAnchor;
//		print(temp.magnitude.ToString());
//		if (temp.magnitude < 0.8f || itemRB.velocity.magnitude > 1f){
//			itemRB.velocity = Vector3.zero;
//			print("velocity reset");
//		}
		///////////////////////////////////////////

		updateHandPosition();

		BringObjToPosition();

	}

	void BringObjToPosition(){
		rot = item.transform.rotation;
		handRot = cam.transform.rotation;

		Vector3 dist = hand - item.transform.position;

		itemRB.AddForce(dist * strength);
		itemRB.velocity -= itemRB.velocity * air_resistance * Time.fixedDeltaTime;

//		foreach (Vector3 point in anchors){
//			Vector3 dist = (hand + handRot * point) - (item.transform.position + rot * point);
//
//			//drop the object if the object is dragged by somthing
////			if (dist.magnitude > dropOffRange){
////				if (Physics.Raycast(item.transform.position, dist, dist.magnitude / 5f)){
////					Drop ();
////					return;
////				}
////			} else if (dist.magnitude < resetRange){
////				continue;
////			}
//			itemRB.AddForceAtPosition(dist * (strength), item.transform.position + rot * point);
//			//critical damping
//			itemRB.velocity -= itemRB.velocity * air_resistance * Time.fixedDeltaTime;
//			print("anchor number:" + anchors.Length);
	}

	void Drop(){
		carrying = false;
		//original solution -----------------------
//		item.transform.parent = initTrans;
//		item.AddComponent <Rigidbody> (); 	// Add back rigidbody for future use
		///////////////////////////////////

		//Joint solution --------------------------------
//		Destroy(item.GetComponent<SpringJoint>());
//		item.GetComponent <Rigidbody> ().mass = itemMass;
//		this.GetComponent<Rigidbody> ().mass = initMass;
		/// //////////////////////////////////////////
		itemRB.mass = itemMass;
		this.GetComponent<Rigidbody> ().mass = initMass;
		itemRB.centerOfMass = initCoM;
		itemRB.useGravity = true;
	}
	void Throw () {
		
		itemRB = item.GetComponent<Rigidbody>();
		//throw!
		itemRB.AddForce(cam.transform.forward * throwingStrength);
		
		//add a random rotation of the item being thrown away.
		if (AddRandomTorque)
			itemRB.AddRelativeTorque(Random.onUnitSphere * turning);
	}

}
