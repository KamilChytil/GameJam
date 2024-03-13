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
			if (recording)
			{
				if (ParadoxManager.isIntelPickUp && ParadoxManager.isTimeRiftActive)
				{
					recording = false;
					PlayerPositionRecorder.RecordPlayerPosition(other.transform);
					PlayerPositionLoader.LoadData();
					ParadoxManager.EndRecording();
					ResetEverything();
				}
			}
			else if (other.GetComponent<PlayerMovement>().passive && ParadoxManager.lastTime <= TimeManager.elapsedTime+1)
			{
				ParadoxManager.Win();
			}

			//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
	private void ResetEverything()
	{

		ParadoxManager.ResetAll();
	}
}
