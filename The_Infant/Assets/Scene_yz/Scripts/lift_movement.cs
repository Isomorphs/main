using UnityEngine;
using System.Collections;

public class lift_movement : MonoBehaviour {
	
	static int num_levels = 10; //number of levels in total
	
	public int initLevel = 0;
	public float speed;
	public int destination;
	public int maxHeight, minHeight;

	//enter the height of each level in the editor
	public float[] level_heights = new float[num_levels];
	
	//store the keys to each level
	public bool[] keys = new bool[num_levels];

	//drag panels into this array in the editor mode
	public GameObject[] control_panels = new GameObject[num_levels];

	Transform trans;
	//float smoothing = 10f;

	//The position where the lift tries to reach in every frame
	Vector3 finalPosition;

	// Use this for initialization
	void Awake () {
		trans = GetComponent<Transform>();
		keys[0] = true;
		finalPosition = trans.position;
		finalPosition.y = level_heights[initLevel];
	}

	void Start(){
		UpdateLevelLimits();
	}
	
	// Update is called once per frame
	void Update () {
		//just in case these controls are needed.
//		if (Input.GetKey(KeyCode.Alpha0))
//			destination = 0;
//		if (Input.GetKey(KeyCode.Alpha1))
//			destination = 1;
//		if (Input.GetKey(KeyCode.Alpha2))
//			destination = 2;
//		if (Input.GetKey(KeyCode.Alpha3))
//			destination = 3;
//		if (Input.GetKey(KeyCode.Alpha4))
//			destination = 4;

		if (Input.GetKeyDown(KeyCode.RightBracket) && destination < maxHeight)
			destination++;
		if (Input.GetKeyDown(KeyCode.LeftBracket) && destination > minHeight)
			destination--;

		if (Input.GetKeyDown(KeyCode.B)){
		    UpdateLevelLimits();
			print (maxHeight.ToString());
		}
		finalPosition.y = level_heights[destination];

	}

	//move the lift!
	void FixedUpdate(){
		trans.position = Vector3.Lerp(trans.position, finalPosition, Time.fixedDeltaTime * speed);
	}

	//update the color of all panels' buttons
	void UpdateControlPanel(){
		Color color_to_set;
		foreach (GameObject panel in control_panels){
			for (int i = 0; i < num_levels; i++){
				if (panel == null) break;

				if (i >= minHeight && i <= maxHeight)
					color_to_set = Color.green;
				else
					color_to_set = Color.red;

				panel.GetComponent<Lift_control_panel_logic>().lightUpButtons(i, color_to_set);
			}
		}
	}

	//Update the values of max/min heights whenever keys are changed. Update the controls panels too.
	public void UpdateLevelLimits (){
		//the lift will be stuck if the key to the current level is lost!
		if (!keys[destination]) {
			maxHeight = minHeight = destination;
			UpdateControlPanel();
			return;
		}

		for (int i = destination; i < num_levels; i++){
			if (keys[i])
				continue;
			else {
				maxHeight = i - 1;
				break;
			}
		}

		for (int i = destination; i >= 0; i--){
			if (keys[i])
				continue;
			else {
				minHeight = i + 1;
				break;
			}
		}
		UpdateControlPanel();
	}
}
