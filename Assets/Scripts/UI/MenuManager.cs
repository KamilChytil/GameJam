using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

	private GameObject menuScreen;
	private GameObject selectLevelScreen;
	//private GameObject settingScreen;

	//public Toggle musicToggle;
	//public Toggle soundToggle;


	// Start is called before the first frame update
	void Start()
	{
		menuScreen = GameObject.Find("Menu");
		selectLevelScreen = GameObject.Find("SelectLevelScreen");
		//settingScreen = GameObject.Find("SettingScreen");

		//musicToggle.isOn = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
		//soundToggle.isOn = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;


		menuScreen.SetActive(true);
		//settingScreen.SetActive(false);
		selectLevelScreen.SetActive(false);
		//SetMusicEnabled(musicToggle.isOn);
		//SetSoundEnabled(soundToggle.isOn);
	}





	public void QuitButton()
	{
		Application.Quit();
	}
	public void ToggleMusic(bool isEnabled)
	{
		SetMusicEnabled(isEnabled);
		PlayerPrefs.SetInt("MusicEnabled", isEnabled ? 1 : 0);
	}

	public void ToggleSound(bool isEnabled)
	{
		SetSoundEnabled(isEnabled);
		PlayerPrefs.SetInt("SoundEnabled", isEnabled ? 1 : 0);
	}

	private void SetMusicEnabled(bool isEnabled)
	{
		// AudioListener.pause = !isEnabled;
	}

	private void SetSoundEnabled(bool isEnabled)
	{

		//AudioManager.instance.SetSoundEnabled(isEnabled);
	}
	public void BackButton()
	{
		menuScreen.SetActive(true);
		//settingScreen.SetActive(false);
		selectLevelScreen.SetActive(false);


	}


	public void OpenSettingScreen()
	{
		//settingScreen.SetActive(true);
		menuScreen.SetActive(false);
		selectLevelScreen.SetActive(false);
	}

	public void OpenLevelScreen()
	{
		//settingScreen.SetActive(false);
		menuScreen.SetActive(false);
		selectLevelScreen.SetActive(true);
	}

	public void StartGame()
	{
		SceneManager.LoadScene("Level1");
	}

	public void LoadLevelById(string levelName)
	{
		SceneManager.LoadScene(levelName);

	}


	// Update is called once per frame
	void Update()
	{

	}
}
