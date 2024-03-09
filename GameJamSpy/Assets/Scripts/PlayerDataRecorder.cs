using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerPositionRecorder : MonoBehaviour
{
    public float recordingInterval = 1f;
    public string saveFilePath = "player_data.json";
    private List<Vector3> positions = new List<Vector3>();
    private List<Quaternion> rotations = new List<Quaternion>();
    private float timer = 0f;

    public FinishArea finishArea;


    private float debugTimer = 0f;

    private void Start()
    {
        finishArea = FindAnyObjectByType<FinishArea>();
    }
    private void Update()
    {
        if(finishArea.isPlayerFinish == 0 )
        {
            timer += Time.deltaTime;
            debugTimer += Time.deltaTime;
            if (debugTimer >= 3f && debugTimer <= 3.1f)
            {
                //Debug.Log(debugTimer);
                //Debug.Log(transform.position+"save");
            }
            if (timer >= recordingInterval)
            {
                RecordPlayerPosition();
                SavePositionDataToJson();
                timer -= recordingInterval;
            }

        }
    }

    private void RecordPlayerPosition()
    {
        positions.Add(transform.position);
        rotations.Add(transform.rotation);
    }

    private void SavePositionDataToJson()
    {
        PositionData data = new PositionData();
        data.positions = positions.ToArray();
        data.rotations = rotations.ToArray();

        string jsonData = JsonUtility.ToJson(data);

        File.WriteAllText(saveFilePath, jsonData);
    }

    public void SavePositionData()
    {
        SavePositionDataToJson();
    }

    [System.Serializable]
    public class PositionData
    {
        public Vector3[] positions;
        public Quaternion[] rotations;
    }
}
