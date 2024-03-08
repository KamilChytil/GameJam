using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavePlayerMove : MonoBehaviour
{
    public GameObject playerGameObject;
    public Transform playerCurrentMove;

    public int saveIntervalFrames = 60; 
    private int framesSinceLastSave = 0;
    private List<Vector3> recordedPositions = new List<Vector3>();
    private List<Quaternion> recordedRotations = new List<Quaternion>();

    public FinishArea finishArea;
    // Start is called before the first frame update
    void Start()
    {
        playerGameObject = GameObject.Find("Player");

        playerCurrentMove = playerGameObject.transform;

        finishArea = FindAnyObjectByType<FinishArea>();

    }



    void Update()
    {
        if(finishArea.isPlayerFinish == 0)
        {
            framesSinceLastSave++;
            if (framesSinceLastSave >= saveIntervalFrames)
            {
                framesSinceLastSave = 0;
                RecordPlayerPositionAndRotation();
                SaveRecordedData();
            }

        }    

    }

    void RecordPlayerPositionAndRotation()
    {
        // Record player position and rotation
        recordedPositions.Add(transform.position);
        recordedRotations.Add(transform.rotation);
    }

    void SaveRecordedData()
    {
        // Create a data object to hold recorded positions and rotations
        PlayerData playerData = new PlayerData();
        playerData.positions = recordedPositions.ToArray();
        playerData.rotations = recordedRotations.ToArray();

        // Convert the data object to JSON
        string jsonData = JsonUtility.ToJson(playerData);

        // Determine the file path outside of the Unity project
        string filePath = Path.Combine(Application.dataPath, "../player_data.json");

        // Write the JSON data to a file
        File.WriteAllText(filePath, jsonData);
    }

    // Data structure to hold player position and rotation
    [System.Serializable]
    public class PlayerData
    {
        public Vector3[] positions;
        public Quaternion[] rotations;
    }
}

