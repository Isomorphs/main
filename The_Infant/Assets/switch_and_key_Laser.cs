using UnityEngine;
using System.Collections;

public class switch_and_key_Laser : MonoBehaviour {

	//used for switching on laser pointer
	public GameObject key;
	public GameObject Laser_emitter;
	TextMesh textmesh;
//	bool unlocked = false;

	void Start(){
		textmesh = GetComponentInChildren<TextMesh>();
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject == key && !other.transform.parent.CompareTag("Player")){
			unlock();
		}
	}

	void OnTriggerExit (Collider other){
		if (other.gameObject == key && !other.CompareTag("Player")){
			lock_switch();
		}
	}

	void unlock(){
//		unlocked = true;
		Laser_emitter.SetActive(true);
		textmesh.text = "Good Job!";
		Destroy(key.GetComponent<Rigidbody>());
		key.transform.rotation = Quaternion.LookRotation(Vector3.up);
		key.transform.position = transform.position + Vector3.up * 2f;
	}

	void lock_switch(){
		Laser_emitter.SetActive(false);
	}
}
