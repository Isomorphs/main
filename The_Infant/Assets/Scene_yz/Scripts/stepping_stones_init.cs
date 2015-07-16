using UnityEngine;
using System.Collections;

public class stepping_stones_init : MonoBehaviour {

	GameObject player;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody>();
		rb.constraints = RigidbodyConstraints.FreezeAll;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(player.transform.position, transform.position) < 100f){
			rb.constraints = RigidbodyConstraints.None;
			rb.AddForce(Vector3.forward * 10f);
		}
	}
}
