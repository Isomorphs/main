using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateNumberForSlider : MonoBehaviour {

	//**********************
	//this script let a text object update its content according to the value of a slider. Used in user preference 
	//menus.
	//**********************
	
	Text txt;

	//insert the slider from which this text references value
	public Slider sliderInfo;

	void Start () {
		txt = GetComponent<Text>();
	}
	
	void Update () {

		//First, truncate the decimal part using mathf.
		//then, ToString() converts the value from double to a string.
		txt.text = Mathf.FloorToInt(sliderInfo.value).ToString();
	}
}
