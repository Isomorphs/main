using UnityEngine;
using System.Collections;

public class MirrorPlaneMovementTemp : MonoBehaviour {

	//adjust the transform of mirror surface (plane) automatically in accordance to mirror body

	public GameObject mirrorBody; //attach the mirror body (preferably a cube) to this game object in the editor
	float thickness_of_glue;

	// Use this for initialization
	void Start () {
		//initialise the size of the mirror according to the size of the attached mirror body.
		Vector3 temp = transform.localScale;
		temp.x = mirrorBody.transform.localScale.x / 10f * 0.9f;
		temp.z = mirrorBody.transform.localScale.y / 10f * 0.9f;
		transform.localScale = temp;
		//how far is the mirror surface away from its body
		thickness_of_glue = transform.localScale.y + 0.01f;
	}
	
	// Update is called once per frame
	void Update () {
		//set position of mirror surface
		transform.position = mirrorBody.transform.position + mirrorBody.transform.forward * thickness_of_glue;

		//set rotation (rotation needs to be corrected by a 90 ratation around x_axis)
		transform.rotation = mirrorBody.transform.rotation * Quaternion.Euler(90f, 0f, 0f);

	}
}
