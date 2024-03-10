using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishArea : MonoBehaviour
{
	public static bool recording = true;


	private void Start()
	{
		recording = true;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (ParadoxManager.intelDone && ParadoxManager.timeRiftDone)
			{

				Debug.Log(Time.timeSinceLevelLoad);
				if (recording)
				{
					PlayerPositionRecorder.RecordPlayerPosition(other.transform);
					PlayerPositionLoader.LoadData();
					ResetEverything();
					ParadoxManager.EndRecording();
				}
				else
				{
					ParadoxManager.Win();
				}
				recording = false;
			}
			//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
	private void ResetEverything()
	{
		TimeManager.elapsedTime = 0;
		ParadoxManager.ResetAll();
	}
}
