using UnityEngine;
using System.Collections;

public class ColorChange : MonoBehaviour {
	public Color colorCurrent;


	// Use this for initialization
	void Start () {
		colorCurrent = GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {

		this.gameObject.GetComponent<Renderer>().material.color = Color.Lerp(colorCurrent, Color.white, Time.time/5000);
		colorCurrent = GetComponent<Renderer>().material.color;
	}

	void OnTriggerEnter (Collider info) {

		if(info.tag == "Player") {
				this.gameObject.GetComponent<Renderer>().material.color =  Color.black;
				colorCurrent = Color.black;
		}

	}
}