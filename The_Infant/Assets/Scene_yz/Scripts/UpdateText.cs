using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateText : MonoBehaviour {

	public string content;
	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = content;
	}
}
