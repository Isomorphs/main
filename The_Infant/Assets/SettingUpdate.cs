using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingUpdate : MonoBehaviour {

	//update settings after users exit options menu.
	//for now, settings include sound volumes and mouse sensitivity;

	public Slider MusicSlider;
	public Slider SoundEffectSlider;
	public Slider MouseSensitivitySlider;
	GameMaster gameMaster;
//	GameObject optionsPage;
//	GameObject mainMenuPage;

	// Use this for initialization
	void Start () {
		gameMaster = GameObject.Find("GameController").GetComponent<GameMaster>();
//		optionsPage = GameObject.Find ("OptionsPage");
//		mainMenuPage = GameObject.Find("Menu");

		//initialise the values of slide bar;
		MusicSlider.value = gameMaster.musicVolume;
		SoundEffectSlider.value = gameMaster.soundEffectVolume;
		MouseSensitivitySlider.value = gameMaster.mouseSensitivity;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateSettings () {
		gameMaster.musicVolume = MusicSlider.value;
		gameMaster.soundEffectVolume = SoundEffectSlider.value;
		gameMaster.mouseSensitivity = MouseSensitivitySlider.value;
//
//		mainMenuPage.SetActive(true);
//		optionsPage.SetActive(false);
	}
}
