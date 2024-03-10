using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DRopDownScreen : MonoBehaviour
{

    private void Start()
    {
        Screen.fullScreen = true;
    }

    public void OnDisplayModeChanged(int index)
    {
        switch (index)
        {
            case 0:
                Screen.fullScreen = true;
                Debug.Log("full");
                PlayerPrefs.SetInt("DisplayMode", 0);
                break;
            case 1: 
                Screen.fullScreen = false;
                PlayerPrefs.SetInt("DisplayMode", 1);
                Debug.Log("nofull");
                break;
        }
        PlayerPrefs.Save();
    }

}
