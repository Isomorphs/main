using UnityEngine;
using System.Collections;

public class lift_movement : MonoBehaviour {

	public int initLevel = 0;
	public float[] level_heights = new float[10];
	public float speed;
	public int destination;
	public int maxHeight, minHeight;
	public bool[] keys = new bool[10];
	Transform trans;
	//float smoothing = 10f;
	Vector3 finalPosition;

	// Use this for initialization
	void Start () {
		trans = GetComponent<Transform>();
		keys[0] = true;
		finalPosition = trans.position;
		finalPosition.y = level_heights[initLevel];
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
		trans.position = Vector3.Lerp(trans.position, finalPosition, Time.deltaTime * speed);
	}

	public void UpdateLevelLimits (){
		if (!keys[destination]) return;

		for (int i = destination; i < 10; i++){
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
	}
}
