using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


// Binary and system libraries are needed for saving

public class SaveLoadProgress : MonoBehaviour {

	int itemNo = 0;
	int triggerableNo = 0;
	int counter;

	public void Save () {
		BinaryFormatter bFormatter = new BinaryFormatter ();
		GameProgressData progress = new GameProgressData ();
		FileStream file = File.Create(Application.persistentDataPath + "/gameProgress.dat");

		// Get parent objects.
		Transform[] itemParent = GameObject.Find("Items").GetComponentsInChildren<Transform>();
		Transform[] triggerableParent = GameObject.Find ("Triggerables").GetComponentsInChildren<Transform>();

		// For testing
		print (Application.persistentDataPath);

		// Track all positions of movable items for saving
		itemNo = 0;
		foreach (Transform child in itemParent) {
			child.position = progress.itemPosition[itemNo];
			itemNo++;
		}

		triggerableNo = 0;
		foreach (Transform child in triggerableParent) {
			child.position = progress.triggerablePosition[triggerableNo];
			triggerableNo++;
		}
		
		progress.playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

		// Serialize file so that it cannot be directly modified by player
		bFormatter.Serialize(file, progress);
		file.Close();
	
	}

	public void Load () {

		// Check if saved file exists before loading
		if(File.Exists (Application.persistentDataPath + "/gameProgress.dat")) {
			BinaryFormatter bFormatter = new BinaryFormatter ();
			FileStream file = File.Open(Application.persistentDataPath + "/gameProgress.dat", FileMode.Open);
			Transform[] itemParent = GameObject.Find("Items").GetComponentsInChildren<Transform>();
			Transform[] triggerableParent = GameObject.Find ("Triggerables").GetComponentsInChildren<Transform>();

			// Deserialise info saved for retrieval
			GameProgressData progress = (GameProgressData)bFormatter.Deserialize(file);			

			file.Close ();

			// Change positions of movable items according to saved info
			itemNo = 0;
			foreach (Transform child in itemParent) {
				progress.itemPosition[itemNo] = child.position;
				itemNo++;
			}
			
			triggerableNo = 0;
			foreach (Transform child in triggerableParent) {
				progress.triggerablePosition[triggerableNo] = child.position;
				triggerableNo++;
			}

			GameObject.FindGameObjectWithTag("Player").transform.position = progress.playerPosition;
		
		}
	}
}

[Serializable]
class GameProgressData : MonoBehaviour
{
	public Vector3[] itemPosition = new Vector3[100];
	public Vector3[] triggerablePosition = new Vector3[100];
	public Vector3 playerPosition;
}
