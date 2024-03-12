using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class PlayerPositionLoader : MonoBehaviour
{

	private static Vector3[] positions;
	private static Quaternion[] rotations;

	private int currentIndex = 0;

	public bool setReplayInactive = false;

	private static float timer = 0f;

	public static Vector2 moveDir = new Vector2();


	private void Start()
	{
		timer = 0;
		currentIndex = 0;
	}

	private void Update()
	{
		if (TimeManager.running && !FinishArea.recording)
		{

			if (currentIndex < positions.Length)
			{
				timer += Time.deltaTime;
				if (timer >= PlayerPositionRecorder.recordingInterval)
				{
					currentIndex++;

					timer -= PlayerPositionRecorder.recordingInterval;
				}
				else
				{
					int nextIndex = Mathf.Min(currentIndex + 1, positions.Length - 1);
					transform.position = Vector3.Lerp(positions[currentIndex], positions[nextIndex], timer / PlayerPositionRecorder.recordingInterval);
					moveDir = (positions[nextIndex] - positions[currentIndex]).normalized;
					transform.rotation = Quaternion.Lerp(rotations[currentIndex], rotations[nextIndex], timer / PlayerPositionRecorder.recordingInterval);
				}
			}
		}

	}

	public static void LoadData()
	{
		positions = PlayerPositionRecorder.positions.ToArray();
		rotations = PlayerPositionRecorder.rotations.ToArray();
	}

}
