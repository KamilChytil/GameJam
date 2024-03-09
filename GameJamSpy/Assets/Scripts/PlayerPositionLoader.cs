using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.Experimental.RestService;

public class PlayerPositionLoader : MonoBehaviour
{

	private List<Vector3> positions = new List<Vector3>();
	private List<Quaternion> rotations = new List<Quaternion>();

	private int currentIndex = 0;

	public int OnOffPLayerFinis = 0;

	private float timer = 0f;
	private float debugTimer = 0f;

	public static Vector2 moveDir = new Vector2();


	private void Start()
	{

		if (OnOffPLayerFinis == 0)
		{
			FinishArea.setReplayActive(0);
		}

		
		if (FinishArea.playerFinished == 1)
		{
			LoadPositionDataFromJson();

		}
	}

	private void Update()
	{

		if (FinishArea.playerFinished == 1)
		{

			if (currentIndex < positions.Count)
			{
				timer += Time.deltaTime;
				if (timer >= PlayerPositionRecorder.recordingInterval)
				{
					currentIndex++;
					//transform.position = positions[currentIndex];
					//transform.rotation = rotations[currentIndex];


					timer -= PlayerPositionRecorder.recordingInterval;
				}
				else
				{
					int nextIndex = Mathf.Min(currentIndex+1, positions.Count-1);
					transform.position = Vector3.Lerp(positions[currentIndex], positions[nextIndex], timer / PlayerPositionRecorder.recordingInterval);
					moveDir = (positions[nextIndex] - positions[currentIndex]).normalized;
					transform.rotation = Quaternion.Lerp(rotations[currentIndex], rotations[nextIndex], timer / PlayerPositionRecorder.recordingInterval);
				}
			}
			debugTimer += Time.deltaTime;
			if (debugTimer >= 3f && debugTimer <= 3.1f)
			{
				//Debug.Log(debugTimer);

				//Debug.Log(transform.position + "Load");
			}
		}

	}

	public void LoadPositionDataFromJson()
	{
		string jsonData = File.ReadAllText(PlayerPositionRecorder.saveFilePath);
		PositionData data = JsonUtility.FromJson<PositionData>(jsonData);

		if (data != null)
		{
			positions = data.positions;
			rotations = data.rotations;
		}
		else
		{
			Debug.LogWarning("Data failed to load.");
		}
	}

	public void LoadData()
	{
		LoadPositionDataFromJson();
	}

	[System.Serializable]
	public class PositionData
	{
		public List<Vector3> positions;
		public List<Quaternion> rotations;
	}
}
