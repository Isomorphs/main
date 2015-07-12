using UnityEngine;
using System.Collections;

public class Lift_control_panel_logic : MonoBehaviour {

	//Change color of the buttons on one the control panel

	Renderer[] buttons;
	// Use this for initialization
	void Awake () {
		//do the bookkeeping here. Index all the child (buttons on the panel) accordingly
		buttons = new Renderer[transform.childCount];
		for (var i = 0; i < transform.childCount; i++){
			int child_index = int.Parse(transform.GetChild(i).GetComponentInChildren<TextMesh>().text);
			buttons[child_index] = transform.GetChild(i).GetComponent<Renderer>();
			print (buttons[child_index].ToString());
		}
	}
	// change the color of child with index i into the desired color
	public void lightUpButtons(int index, Color color){
		buttons[index].material.color = color;
	}
}
