using UnityEngine;
using System.Collections;

public class MirrorPlaneMovementTemp : MonoBehaviour {

	public GameObject mirrorBody;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Transform>().rotation = mirrorBody.transform.rotation;
		GetComponent<Transform>().position = mirrorBody.transform.position;
		GetComponent<Transform>().Translate(Vector3.up * 0.051f);
	}
}
