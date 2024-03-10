using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerPositionRecorder : MonoBehaviour
{
	public static float recordingInterval = .1f;
	//public static string saveFilePath = "player_data.json";

	public static List<Vector3> positions = new List<Vector3>();
	public static List<Quaternion> rotations = new List<Quaternion>();

	private static float timer = 0f;



	private void Start()
	{
		timer = 0;
		positions.Clear();
		rotations.Clear();
	}
	private void Update()
	{
		if (FinishArea.recording)
		{
			timer += Time.deltaTime;
			if (timer >= recordingInterval)
			{
				RecordPlayerPosition(transform);
				timer -= recordingInterval;
			}

		}
	}

	public static void RecordPlayerPosition(Transform t)
	{
		positions.Add(t.position);
		rotations.Add(t.rotation);
	}

}
