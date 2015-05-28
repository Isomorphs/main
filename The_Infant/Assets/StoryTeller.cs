using UnityEngine;
using System.Collections;

public class StoryTeller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		GUI.color = Color.yellow;
		GUI.TextField(new Rect(10, 10, 50, 50), "Hello World");
	}
}
