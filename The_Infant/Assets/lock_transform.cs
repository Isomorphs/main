using UnityEngine;
using System.Collections;

public class lock_transform : MonoBehaviour {

	public GameObject anchoredObject;
	Transform Anchored_transform;

	void Start () {
		Anchored_transform = anchoredObject.GetComponent<Transform>();

	}
	void Update () {
		
	}
}
