using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishArea : MonoBehaviour
{
	public static int replayActive = 0;


	private void Start()
	{
		
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Debug.Log(Time.timeSinceLevelLoad);
			if(replayActive == 0)
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
		replayActive = value;
		PlayerPrefs.SetInt("replayActive", value);
	}
}
