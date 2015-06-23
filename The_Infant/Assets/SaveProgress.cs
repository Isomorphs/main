using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


// Binary and system libraries are needed for saving

public class SaveProgress : MonoBehaviour {

	//GameObject itemParent;
	//GameObject triggerableParent;
	Vector3[] itemPosition = new Vector3[100];
	Vector3[] triggerablePosition = new Vector3[100];
	Vector3 playerPosition;
	int i = 0;
	int j = 0;

	public void InfoToSave () {

		Transform[] itemParent = GameObject.Find("Items").GetComponentsInChildren<Transform>();
		Transform[] triggerableParent = GameObject.Find ("Triggerables").GetComponentsInChildren<Transform>();

		foreach (Transform child in itemParent) {
			itemPosition[i] = child.position;
			i++;
		}

		foreach (Transform child in triggerableParent) {
			triggerablePosition[j] = child.position;
			j++;
		}

		playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
	
	}

	public void Save () {
		BinaryFormatter bFormatter = new BinaryFormatter ();
		GameProgressData progress = new GameProgressData ();
		int counter;

		FileStream file = File.Create(Application.persistentDataPath + "/gameProgress.dat");
		//for testing
		print (Application.persistentDataPath);


		for (counter=0;counter<= i; counter++) {
			progress.itemPosition[counter] = itemPosition[counter];
		}

		for (counter=0;counter<= j; counter++) {
			progress.triggerablePosition[counter] = triggerablePosition[counter];
		}

		progress.playerPosition = playerPosition;

		bFormatter.Serialize(file, progress);
		file.Close();
	
	}
}

[Serializable]
class GameProgressData : MonoBehaviour
{
	public Vector3[] itemPosition = new Vector3[100];
	public Vector3[] triggerablePosition = new Vector3[100];
	public Vector3 playerPosition;
}
