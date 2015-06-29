using UnityEngine;
using System.Collections;
using System.IO;

public class ClearSavedData : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		File.Delete(Application.persistentDataPath + "/" + "01" + "Progress.dat");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
