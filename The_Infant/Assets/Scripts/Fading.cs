using UnityEngine;
using System.Collections;

public class Fading : MonoBehaviour {
	//Remember to add the scenes used into Build settings for this script to work.

	public Texture2D fadeOutTexture;
	public float fadeSpeed = 0.8f;

	private int drawDepth = -100000;		//A low number to ensure that the fadeOutTexture is drawn last in the scene
	private float alpha = 0f;			//The texture's alpha value
	private int fadeDir = 0;			//Fading direction of texture (in = -1; out = 1)

//	public bool fadingComplete {
//		get {
//			return fadingComplete;
//		}
//		set {
//			fadingComplete = value;
//		}
//	}

	void OnGUI () {
		if (fadeDir == 0)
			return;

		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);	//Clamp alpha to be within 0-1.

		print("alpha: " + alpha.ToString());

		if ((fadeDir == 1 && alpha == fadeDir) || (alpha == 0 && fadeDir == -1)) {
//			fadingComplete = true;
			fadeDir = 0;
		}
		
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);	//Set the color of the black fading texture to remain the same and its Alpha to alpha.
		GUI.depth = drawDepth;													//Make the black texture rendered on top.
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);

	}

	public float BeginFading (int direction) {
		print ("Begin fading");
		fadeDir = direction;
//		fadingComplete = false;

		return fadeSpeed;				//Return fadeSpeed to time the loadLevel function.
	}

//	public float BeginFading (int direction) {
//		//print ("Begin fading");
//		fadeDir = direction;
//		return fadeSpeed;				//Return fadeSpeed to time the loadLevel function.
//	}

	private void OnLevelWasLoaded () {
		print ("level loaded");
		alpha = 1;
		BeginFading (-1);				//Begin fading when the next level is loaded.
	}
}
