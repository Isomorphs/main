using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class wardrobe_control : MonoBehaviour {

//	GameObject player;
	RaycastHit hit;
	Ray ray;
	public float interactionDistance = 50f;
	public Canvas canvas;

	//bool isHit = false;
	bool isOpened = false;
	// Use this for initialization
	void Start () {
		//player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

		ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
		//Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward);
		if (Physics.Raycast(ray, out hit, interactionDistance) && (hit.collider.gameObject.name == "Wardrobe"
		                                                           || hit.collider.gameObject.name == "Wardrobe_door")){
			if (!isOpened){
				canvas.GetComponentInChildren<UpdateText>().content = "Press E to open it";
				if (Input.GetKey(KeyCode.E)){
					GetComponentInChildren<HingeJoint>().useMotor = true;
					isOpened = true;
					canvas.GetComponentInChildren<UpdateText>().content = "";
				}
			}
		} else {
			canvas.GetComponentInChildren<UpdateText>().content = "";
		}
	}


	void OnTriggerStay(Collider other){
		if (other.tag != "Player") return;
		if (Input.GetKey(KeyCode.O))
			GetComponentInChildren<HingeJoint>().useMotor = true;
	}
}
