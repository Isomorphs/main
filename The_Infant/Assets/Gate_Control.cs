using UnityEngine;
using System.Collections;

public class Gate_Control : MonoBehaviour {

	public GameObject gate1, gate2;
	Vector3 dest1, dest2;
	public float smoothing = 0.1f;
	public Canvas canvas;

	bool opening = false;
	// Use this for initialization
	void Start () {
		dest1 = gate1.transform.position + Vector3.left * 15f;
		dest2 = gate2.transform.position + Vector3.left * -15f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (opening){
			gate1.transform.position = Vector3.Lerp(gate1.transform.position, dest1, Time.fixedDeltaTime * smoothing);
			gate2.transform.position = Vector3.Lerp(gate2.transform.position, dest2, Time.fixedDeltaTime * smoothing);
		}
	}

	void OnHitByCamRay (){
		canvas.GetComponentInChildren<UpdateText>().content = "Press E to open the door";
	}

	void OnCamRayExit() {
		canvas.GetComponentInChildren<UpdateText>().content = "";
	}

	void OnCamRayStay () {
		if (Input.GetKeyDown(KeyCode.E)){
			opening = true;
			canvas.GetComponentInChildren<UpdateText>().content = "";
		}
	}
}
