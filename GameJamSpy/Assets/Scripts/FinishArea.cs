using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishArea : MonoBehaviour
{
    public int isPlayerFinish = 0;
    public TimeLine timeLine;
    public GameObject prefab;

    private bool hasLoadedPrefab = false;

    private void Awake()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;


    }
    private void Start()
    {
        timeLine = FindObjectOfType<TimeLine>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.N))
            { 
                timeLine.isInFinish = true;
                timeLine.gameTime = 0f;
                PlayerPrefs.SetInt("isPlayerFinish", 1);

                timeLine.isLoad = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("jsem tu");

        if (scene.name.Equals("SaveLoadTest"))
        {
            if (!hasLoadedPrefab && prefab != null)
            {
                Instantiate(prefab, transform.position, transform.rotation);
                hasLoadedPrefab = true;
            }
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}