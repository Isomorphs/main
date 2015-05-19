using UnityEngine;
using System.Collections;

public class ChangeLevelTemp : MonoBehaviour {
	private float fadeTime;


	private IEnumerator OnCollisionEnter (Collision collision) {
		if (collision.collider.tag == "Player") {
			fadeTime = GameObject.Find("GameController").GetComponent <Fading> ().BeginFading(1);
			yield return new WaitForSeconds (fadeTime);
			Application.LoadLevel (Application.loadedLevel + 1);
		}
	}
}
