using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class wardrobe_control : MonoBehaviour {

	public Canvas canvas;
	public GameObject door;
	bool isOpened = false;

	void OnHitByCamRay(){
		print ("hit");
		if (!isOpened){
			canvas.GetComponentInChildren<UpdateText>().content = "Press E to open it";
			if (Input.GetKey(KeyCode.E)){
				door.GetComponent<HingeJoint>().useMotor = true;
				isOpened = true;
				canvas.GetComponentInChildren<UpdateText>().content = "";
			}
		}
	}

	void OnCamRayExit (){
		print ("Exit");
		canvas.GetComponentInChildren<UpdateText>().content = "";
	}
}
