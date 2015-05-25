using UnityEngine;
using System.Collections;

public class ChangeLevelTemp : MonoBehaviour {
//	bool changingLevel = false;
//
//	void Update () {
//		if (changingLevel)
//		{
//			Application.LoadLevel(Application.loadedLevel);
//			changingLevel = false;
//		}
//	}
//
//	public void ReloadLevel () {
//		changingLevel = true;
//	}

	//public string levelName;
	AsyncOperation async;
	
	public void StartLoading() {
		StartCoroutine("load");
	}
	
	IEnumerator load() {

		Debug.LogWarning("ASYNC LOAD STARTED - " +
		                 "DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");
		//levelName = "TestLevel";
		async = Application.LoadLevelAsync(Application.loadedLevel);
		async.allowSceneActivation = false;
		yield return async;
	}
	
	public void ActivateScene() {
		async.allowSceneActivation = true;
	}
}
/*
	private IEnumerator OnCollisionEnter (Collision collision) {
		if (collision.collider.tag == "Player") {

			fadeTime = GameObject.Find("GameController").GetComponent <Fading> ().BeginFading(1);
			print ("Load Level starts");
			yield return new WaitForSeconds (fadeTime);
			Application.LoadLevel (Application.loadedLevel + 1);
		}
	}
	*/
