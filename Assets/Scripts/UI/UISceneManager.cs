using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
	private void Update()
	{
		/*if (Input.GetKeyDown(KeyCode.R))
		{
			ReloadScene();
		}*/
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			LoadMenu();
		}
	}

	public void NextLevel()
    {
        int indexLevel = SceneManager.GetActiveScene().buildIndex;

        if (indexLevel != SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(indexLevel + 1);
            return;
        }
        SceneManager.LoadScene("Menu");



    }
}
