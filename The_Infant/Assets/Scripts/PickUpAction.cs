using UnityEngine;
using System.Collections;

public class PickUpAction : MonoBehaviour {
	public float hoverDistance = 2f;			//item will hover in front of player by this var
	public float maxInteractionDistance = 5f;	//max distance allowed for picking up objects
	public float smoothing = 10f;

	private bool carrying = false;
	private GameObject mainCamera;
	private GameObject carriedItem;
	private Vector3 center;
	private float x = Screen.width /2;
	private float y = Screen.height /2;
	private Rigidbody itemRB;



	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		center.Set(x,y,0);
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
				itemRB = carriedItem.GetComponent <Rigidbody> ();
			
				//Disable item's gravity to make it hover
				itemRB.useGravity = false;

				//Set's initial rotation to be same as player
				carriedItem.transform.rotation = this.transform.rotation;

				//Make carried item a child of player
				carriedItem.transform.parent = this.transform;

				//Destroy item's Rigidbody to correct item's movement
				Destroy (itemRB);
			}

		}
	}

	void CarryObject () {

		//Change position of the carried item accordingly
		carriedItem.transform.position = Vector3.Lerp(carriedItem.transform.position, mainCamera.transform.position + mainCamera.transform.forward * hoverDistance, Time.deltaTime * smoothing);
	}

	void DropObject () {
		carrying = false;
		carriedItem.AddComponent <Rigidbody> (); 	// Add back rigidbody for future use
		carriedItem.transform.parent = GameObject.Find("Items").transform;		// Remove item from player
		carriedItem = null;

	}
	
}
