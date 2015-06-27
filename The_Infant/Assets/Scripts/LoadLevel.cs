using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {
	
	public int lvlToLoad;

	//int i;
	float fadingTime;
	AsyncOperation async;

	//If a parameter is given, load the given scene.
	public void LoadNewLevel (int i) {
		lvlToLoad = i;
		StartCoroutine(LoadOneLevel(i));
	}

	//if no parameter is given, reload the current level.
	public void LoadNewLevel () {
		StartCoroutine(LoadOneLevel(Application.loadedLevel));
	}

	//print a message for debugging.
	void OnLevelWasLoaded () {
		print ("Level was loaded");
	}

	//open another thread for loading a scene indexed by i.
	IEnumerator LoadOneLevel(int i) {
		yield return StartCoroutine(FadeAndWait());
		async = Application.LoadLevelAsync(i);
		print ("Starting Loading");
		yield return async;
	}

	//this is the fading effect. optional.
	IEnumerator FadeAndWait() {
		fadingTime = GetComponent<Fading_Original>().BeginFading(1);
		print(fadingTime);
		yield return new WaitForSeconds(fadingTime);
	}
}
