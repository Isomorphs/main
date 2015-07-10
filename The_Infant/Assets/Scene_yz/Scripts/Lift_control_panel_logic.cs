using UnityEngine;
using System.Collections;

public class Lift_control_panel_logic : MonoBehaviour {

	//public GameObject liftObject;
//	lift_movement lift_movement_control;
	Renderer[] buttons;
	// Use this for initialization
	void Start () {
//		lift_movement_control = liftObject.GetComponent<lift_movement>();

//		buttons = gameObject.GetComponentsInChildren<Renderer>();

		buttons = new Renderer[transform.childCount];
		for (var i = 0; i < transform.childCount; i++){
			int child_index = int.Parse(transform.GetChild(i).GetComponentInChildren<TextMesh>().text);
			buttons[child_index] = transform.GetChild(i).GetComponent<Renderer>();
			print (buttons[child_index].ToString());
		}

//		print(buttons[5].ToString());
	}
	public void lightUpButtons(int index, Color color){
		buttons[index].material.color = color;
		print("update color");
	}
}
