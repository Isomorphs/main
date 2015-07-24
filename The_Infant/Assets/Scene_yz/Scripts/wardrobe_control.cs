using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class wardrobe_control : MonoBehaviour {

	public Canvas canvas;
	public GameObject door;
	bool isOpened = false;
	JointMotor motor;

	void Awake (){
		motor = door.GetComponent<HingeJoint>().motor;
	}

	void OnHitByCamRay(){
		print ("hit");
		if (!isOpened){
			canvas.GetComponentInChildren<UpdateText>().content = "Press E to open it";
		} else {
			canvas.GetComponentInChildren<UpdateText>().content = "Press E to close it";
		}
	}

	void OnCamRayExit (){
		print ("Exit");
		canvas.GetComponentInChildren<UpdateText>().content = "";
	}

	void OnCamRayStay (KeyCode key){
		if (key == KeyCode.E){
			if (!isOpened){
				print("open!");
				motor.targetVelocity = -30f;
				door.GetComponent<HingeJoint>().motor = motor;
				foreach (Collider collider in GetComponents<BoxCollider>()){
					if (collider.isTrigger == true) collider.enabled = false;
				}
				isOpened = true;
				canvas.GetComponentInChildren<UpdateText>().content = "Press E to close it";
			} else {
				print("close!");
				motor.targetVelocity = 30f;
				door.GetComponent<HingeJoint>().motor = motor;
				foreach (Collider collider in GetComponents<BoxCollider>()){
					if (collider.isTrigger == true) collider.enabled = true;
				}
				isOpened = false;
				canvas.GetComponentInChildren<UpdateText>().content = "Press E to open it";
			}
		}
	}
}
