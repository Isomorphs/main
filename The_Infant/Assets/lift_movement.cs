using UnityEngine;
using System.Collections;

public class lift_movement : MonoBehaviour {

	public float[] level_heights = new float[10];
	public float speed;
	public int destination;
	public int maxHeight, minHeight;
	public bool[] keys = new bool[10];
	Transform trans;

	// Use this for initialization
	void Start () {
		trans = GetComponent<Transform>();
		keys[0] = true;
	}
	
	// Update is called once per frame
	void Update () {

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

		if (Mathf.Approximately(level_heights[destination], trans.position.y)) return;

		if (level_heights[destination] > trans.position.y)
			trans.Translate(Vector3.up * speed * Time.deltaTime);
		else
			trans.Translate(Vector3.up * speed * Time.deltaTime * -1f);

		for (int i = 0; i < 10; i++) {
			if (!keys[i])
				maxHeight = i - 1;
		}
	}
}
