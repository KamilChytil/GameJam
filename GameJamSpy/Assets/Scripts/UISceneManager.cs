using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour
{
	public void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void LoadMenu()
	{
		SceneManager.LoadScene("Menu");
	}
}
