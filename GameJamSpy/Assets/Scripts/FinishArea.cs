using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishArea : MonoBehaviour
{
	public static int playerFinished = 0;


	private void Start()
	{
		playerFinished = PlayerPrefs.GetInt("replayActive", 0);
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if(playerFinished == 0)
			{
				PlayerPositionRecorder.RecordPlayerPosition(other.transform);
				PlayerPositionRecorder.SavePositionData();
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
}
