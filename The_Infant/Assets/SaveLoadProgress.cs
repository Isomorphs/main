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
	float tempX;
	float tempY;
	float tempZ;
	Vector3 temp;

	void Vector3Converter(Vector3 input) {
		tempX = input.x;
		tempY = input.y;
		tempZ = input.z;
	}

	public void Save () {
		BinaryFormatter bFormatter = new BinaryFormatter ();
		GameProgressData progress = new GameProgressData ();
		FileStream file = File.Create(Application.persistentDataPath + "/gameProgress.dat");

		// Get parent objects' transform
		Transform[] itemParent = GameObject.Find("Items").GetComponentsInChildren<Transform>();
		Transform[] triggerableParent = GameObject.Find ("Triggerables").GetComponentsInChildren<Transform>();

		// For testing
		print (Application.persistentDataPath);

		// Track all positions of movable items for saving
		itemNo = 0;
		foreach (Transform child in itemParent) {
			Vector3Converter(child.position);
			progress.itemPosX[itemNo] = tempX;
			progress.itemPosY[itemNo] = tempY;
			progress.itemPosZ[itemNo] = tempZ;
			itemNo++;
		}
		
		triggerableNo = 0;
		foreach (Transform child in triggerableParent) {
			Vector3Converter(child.position);
			progress.triggerablePosX[triggerableNo] = tempX;
			progress.triggerablePosY[triggerableNo] = tempY;
			progress.triggerablePosZ[triggerableNo] = tempZ;
			triggerableNo++;
		}

		Vector3Converter(GameObject.FindGameObjectWithTag("Player").transform.position);
		progress.playerPosX = tempX;
		progress.playerPosY = tempY;
		progress.playerPosZ = tempZ;

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

			// Deserialise saved file to retrieve position info
			GameProgressData progress = (GameProgressData)bFormatter.Deserialize(file);			

			file.Close ();

			// Change positions of movable items according to saved info
			itemNo = 0;
			if(itemParent != null) {
				foreach (Transform child in itemParent) {
					temp = child.position;
					temp.x = progress.itemPosX[itemNo];
					temp.y = progress.itemPosY[itemNo];
					temp.z = progress.itemPosZ[itemNo];
					child.position = temp;
					itemNo++;
				}
			}
	
			triggerableNo = 0;
			if(triggerableParent != null) {
				foreach (Transform child in triggerableParent) {
					temp = child.position;
					temp.x = progress.triggerablePosX[triggerableNo];
					temp.y = progress.triggerablePosY[triggerableNo];
					temp.z = progress.triggerablePosZ[triggerableNo];
					child.position = temp;
					triggerableNo++;
				}
			}

			temp = GameObject.FindGameObjectWithTag("Player").transform.position; 
			temp.x = progress.playerPosX;
			temp.y = progress.playerPosY;
			temp.z = progress.playerPosZ;
			GameObject.FindGameObjectWithTag("Player").transform.position = temp; 
		}
	}
	
}

// Vector3 is not serialisable in unity so we have to manually(kinda) change every component (cry face)
[Serializable]
class GameProgressData
{
	public float[] itemPosX = new float[100];
	public float[] itemPosY = new float[100];
	public float[] itemPosZ = new float[100];
	public float[] triggerablePosX = new float[100];
	public float[] triggerablePosY = new float[100];
	public float[] triggerablePosZ = new float[100];
	public float playerPosX;
	public float playerPosY;
	public float playerPosZ;
}

