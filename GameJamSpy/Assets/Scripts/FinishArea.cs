using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishArea : MonoBehaviour
{
    public int isPlayerFinish = 0;


    private void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

            PlayerPrefs.SetInt("isPlayerFinish", 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
}
