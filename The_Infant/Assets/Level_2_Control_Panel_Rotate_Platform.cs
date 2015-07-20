using UnityEngine;
using System.Collections;

public class Level_2_Control_Panel_Rotate_Platform : MonoBehaviour {

	public GameObject plat1, plat2;
	bool restored = false;
	Quaternion dest_rot_1, dest_rot_2, initRot1, initRot2;
	public Canvas canvas;
	// Use this for initialization
	void Start () {
		initRot1 = plat1.transform.rotation;
		initRot2 = plat2.transform.rotation;
	}

	void OnHitByCamRay(){
		canvas.GetComponentInChildren<UpdateText>().content = "Press E to interact";
	}

	void OnCamRayStay (KeyCode key){

		if (key == KeyCode.E){
			if (restored){
				restored = false;
			} else {
				restored = true;
			}
		}
	}

	void OnCamRayExit (){
		canvas.GetComponentInChildren<UpdateText>().content = "";
	}

	void FixedUpdate(){
		if (restored){
			dest_rot_1 = Quaternion.Euler(0f, 20f, 0f) * initRot1;
			dest_rot_2 = Quaternion.Euler(0f, -20f, 0f) * initRot2;
		} else {
			dest_rot_1 = initRot1;
			dest_rot_2 = initRot2;
		}
		plat1.transform.rotation = Quaternion.Slerp(plat1.transform.rotation, dest_rot_1, Time.fixedDeltaTime);
		plat2.transform.rotation = Quaternion.Lerp(plat2.transform.rotation, dest_rot_2, Time.fixedDeltaTime);
	}
}
