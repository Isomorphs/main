using UnityEngine;
using System.Collections;

public class Double_Laser_Switch : MonoBehaviour {

	public GameObject laser1, laser2;
	public Canvas canvas;
	int index = 1;
	Renderer rend;

	void Start(){
		rend = GetComponent<Renderer>();
	}

	void OnCamRayStay(KeyCode key){

		canvas.GetComponentInChildren<UpdateText>().content = "Press E to switch the active laser gun\n Current active laser: Laser " + index;

		if (key == KeyCode.E){
			if (laser1.activeSelf){
				laser1.SetActive(false);
				laser2.SetActive(true);
				index = 2;
				rend.material.color = Color.yellow;
			} else {
				laser1.SetActive(true);
				laser2.SetActive(false);
				index = 1;
				rend.material.color = Color.magenta;
			}
		}
	}

	void OnCamRayExit(){
		canvas.GetComponentInChildren<UpdateText>().content = "";
	}
}
