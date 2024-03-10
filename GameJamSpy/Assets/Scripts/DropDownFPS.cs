using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownFPS : MonoBehaviour
{
    private int targetFPS;

    public void Start()
    {
        FPS();
    }

    private void FPS()
    {

        int targetFPS = PlayerPrefs.GetInt("TargetFPS", 60);
        Application.targetFrameRate = targetFPS;


    }
    public void OnFPSSelectionChanged(int index)
    {

        switch (index)
        {
            case 0:
                targetFPS = 60;
                Debug.Log(targetFPS);
                break;
            case 1:
                targetFPS = 120;
                Debug.Log(targetFPS);
                break;
        }
        Debug.Log(targetFPS);
        Application.targetFrameRate = targetFPS;
        PlayerPrefs.SetInt("TargetFPS", targetFPS);
        PlayerPrefs.Save();
    }

}
