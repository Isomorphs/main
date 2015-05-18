using UnityEngine;
using System.Collections;

public class PickUpAction : MonoBehaviour {
	public float hoverDistance = 2f;			//item will hover in front of player by this var
	public float maxInteractionDistance = 5f;	//max distance allowed for picking up objects
//	public bool colliding = false;				//for item's movement correction
	public float carryingForce = 1f;

	private bool carrying = false;
	private GameObject mainCamera;
	private GameObject carriedItem;
	private GameObject hand;
	private Vector3 center;
	private float x = Screen.width /2;
	private float y = Screen.height /2;
	private Rigidbody itemRB;
	private BoxCollider newCollider;
	private Vector3 colliderHeight;
	private Vector3 itemPosition;
	private GameObject ItemPickingPoint;
	private Vector3 locationOfForce;
	private Vector3 holdingLocation;
	private Vector3 forceDir;

	// Use this for initialization
	void Start () {
		//Determine middle of screen using (x,y)
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		hand = GameObject.Find("Hand");
		center.Set(x,y,0);
		holdingLocation = hand.transform.position;
	}
	
	// Update is called once per frame
	void Update () {


		if(Input.GetKeyDown(KeyCode.E)) {
			if (carrying) DropObject();
			else PickUpObject();
		}

	}

	void FixedUpdate () {

		if(carrying) CarryObject();
	}

	void PickUpObject () {
		Ray ray = mainCamera.GetComponent <Camera> ().ScreenPointToRay(center);

		RaycastHit hit;

		if(Physics.Raycast (ray, out hit))  {

			if (hit.collider.tag == "PickUp" && hit.distance <= maxInteractionDistance){
				carrying = true;
				carriedItem = hit.collider.gameObject;
				itemRB = carriedItem.GetComponent<Rigidbody>();
				ItemPickingPoint = GameObject.Find("PickingPoint");
			}

				//Disable item's gravity to make it hover
//				itemRB.useGravity = false;

				//Disable item's own collider
//				carriedItem.GetComponent<Collider>().enabled = !carriedItem.GetComponent<Collider>().enabled;

				//Add new collider to player according to item.
//				colliderHeight.Set(0f, 1f, hoverDistance);
//				newCollider = this.gameObject.AddComponent <BoxCollider> ();
//				this.gameObject.GetComponent<BoxCollider> ().center =  colliderHeight;
//				this.gameObject.GetComponent<BoxCollider> ().size = carriedItem.GetComponent<BoxCollider> ().size;
		}
	}

	void CarryObject () {

		forceDir = transform.TransformPoint(holdingLocation) - ItemPickingPoint.transform.position;

		itemRB.AddForce(forceDir.normalized * carryingForce);


//		if (colliding == false) {
//			//Sync item's position according to camera
//			itemPosition.Set (0f, 1f, 0f);
//			itemPosition +=  this.transform.position + this.transform.forward * (hoverDistance + Time.deltaTime);
//			itemRB.MovePosition (itemPosition);
//		}
//		//Sync item's rotation according to player
//		carriedItem.transform.rotation = this.transform.rotation;
	}

	void DropObject () {
		carrying = false;
//		itemRB.useGravity = true;
//		Destroy (this.GetComponent<BoxCollider> () );
		carriedItem.GetComponent<Collider>().enabled = !carriedItem.GetComponent<Collider>().enabled;
		carriedItem = null;
	}
	
}
