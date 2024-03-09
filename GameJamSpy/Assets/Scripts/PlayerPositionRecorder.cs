using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerPositionRecorder : MonoBehaviour
{
	public static float recordingInterval = .1f;
	public static string saveFilePath = "player_data.json";

	private static List<Vector3> positions = new List<Vector3>();
	private static List<Quaternion> rotations = new List<Quaternion>();

	private static float timer = 0f;



	private void Start()
	{
		timer = 0;
		positions.Clear();
		rotations.Clear();
	}
	private void Update()
	{
		if (FinishArea.replayActive == 0)
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

	private static void SavePositionDataToJson()
	{
		PositionData data = new PositionData();
		data.positions = positions.ToArray();
		data.rotations = rotations.ToArray();

		string jsonData = JsonUtility.ToJson(data);

		File.Delete(saveFilePath);
		File.WriteAllText(saveFilePath, jsonData);
	}

	public static void SavePositionData()
	{
		SavePositionDataToJson();
	}

}
[System.Serializable]
public class PositionData
{
	public Vector3[] positions;
	public Quaternion[] rotations;
}
