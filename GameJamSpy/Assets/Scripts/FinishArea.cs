using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishArea : MonoBehaviour
{
	public static int playerFinished = 0;
    public TimeLine timeLine;
    public GameObject prefab;

    private bool hasLoadedPrefab = false;

    private void Awake()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;


    }
	private void Start()
	{
		playerFinished = PlayerPrefs.GetInt("replayActive", 0);
        timeLine = FindObjectOfType<TimeLine>();

	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if(playerFinished == 0)
			{
				PlayerPositionRecorder.RecordPlayerPosition(other.transform);
				PlayerPositionRecorder.SavePositionData();
                timeLine.isInFinish = true;
                timeLine.gameTime = 0f;
                timeLine.isLoad = true;
			}
			setReplayActive(1);
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
	public static void setReplayActive(int value)
	{
		playerFinished = value;
		PlayerPrefs.SetInt("replayActive", value);
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
