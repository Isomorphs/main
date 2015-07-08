using UnityEngine;
using System.Collections;

public class PickUpAction : MonoBehaviour {
	public float hoverDistance = 2f;			//item will hover in front of player by this var
	public float maxInteractionDistance = 5f;	//max distance allowed for picking up objects
	public float upOffset = 0f;					//change the way the object is held
	public float rightOffset = 0f;
	public float throwingForce = 100f;
	public float turningSpeed = 5f;
	public bool AddRandomTorque = true;
	public float smoothing = 10f;
	public float newJumpSpeed = 20000f;
	public bool carrying = false;

	private GameObject mainCamera;
	private GameObject carriedItem;
	private Vector3 center;
	private float x = Screen.width /2;
	private float y = Screen.height /2;
	private Rigidbody itemRB;
	private float itemMass;
	private float playerMass;
	private float initialJumpSpeed;
	private CharacterMovement movementScript;
	private CollisionDetectionMode continuousDetection;
	
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		center.Set(x,y,0);
		playerMass = this.GetComponent <Rigidbody> ().mass;
		movementScript = GetComponent<CharacterMovement> ();
		initialJumpSpeed = movementScript.jumpSpeed;
		continuousDetection = this.GetComponent<Rigidbody> ().collisionDetectionMode;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E)) {
			if (carrying) {
				DropObject();
				carriedItem = null;
			}
			else PickUpObject();
		}

		if(Input.GetKeyDown(KeyCode.Q)) {
			if (carrying) {
				DropObject();
				ThrowObject();
				carriedItem = null;
			}
		}
	}

	void FixedUpdate () {

		if(carrying) CarryObject();
	}

	void ThrowObject () {

		itemRB = carriedItem.GetComponent<Rigidbody>();
		//throw!
		itemRB.AddForce(mainCamera.transform.forward * throwingForce);

		//add a random rotation of the item being thrown away.
		if (AddRandomTorque)
			itemRB.AddRelativeTorque(Random.onUnitSphere * turningSpeed);
	}

	void PickUpObject () {
		Ray ray = mainCamera.GetComponent <Camera> ().ScreenPointToRay(center);

		RaycastHit hit;

		if(Physics.Raycast (ray, out hit))  {

			if ((hit.collider.tag == "PickUp" || hit.collider.tag == "Laser")
			    && hit.distance <= maxInteractionDistance){
				carrying = true;
				carriedItem = hit.collider.gameObject;
				itemRB = carriedItem.GetComponent <Rigidbody> ();

				itemMass = itemRB.mass;

				// Update player's mass
				this.GetComponent <Rigidbody> ().mass = playerMass + itemMass;

				movementScript.jumpSpeed = newJumpSpeed;
			
				//Disable item's gravity to make it hover
				itemRB.useGravity = false;

				//Set's initial rotation to be same as player
				carriedItem.transform.rotation = this.transform.rotation;

				//Make carried item a child of player
				carriedItem.transform.parent = this.transform;

				//Destroy item's Rigidbody to correct item's movement
				Destroy (itemRB);

				if (hit.collider.tag == "Laser") {
					upOffset = -1f; rightOffset = 1f;
				}
				else {
					upOffset = rightOffset = 0f;
				}
			}

		}
	}

	void CarryObject () {

		//Change position of the carried item accordingly
		carriedItem.transform.position = Vector3.Lerp (carriedItem.transform.position, mainCamera.transform.position + mainCamera.transform.forward * hoverDistance
			+ mainCamera.transform.right * rightOffset + mainCamera.transform.up * upOffset, Time.deltaTime * smoothing);

		//correct rotation of the carried item (mostly for laser pointers)
		if(carriedItem.tag == "Laser") {
			carriedItem.transform.rotation = Quaternion.LookRotation(mainCamera.transform.right, mainCamera.transform.forward);
		}
	}

	public void DropObject () {
		carrying = false;
		carriedItem.AddComponent <Rigidbody> (); 	// Add back rigidbody for future use
		carriedItem.GetComponent <Rigidbody> ().mass = itemMass;
		upOffset = rightOffset = 0f;
		carriedItem.transform.parent = GameObject.Find("Items").transform;		// Remove item from player
		this.GetComponent<Rigidbody> ().mass = playerMass;
		carriedItem.GetComponent<Rigidbody> ().collisionDetectionMode = continuousDetection;
		movementScript.jumpSpeed = initialJumpSpeed;
	}

}
