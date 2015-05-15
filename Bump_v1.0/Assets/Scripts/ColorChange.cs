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

	void OnCollisionEnter (Collision collision) {

		if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "PickUp") {
				this.gameObject.GetComponent<Renderer>().material.color =  Color.black;
				colorCurrent = Color.black;
		}

	}
}