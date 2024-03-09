using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavePlayerMove : MonoBehaviour
{
    public GameObject playerGameObject;
    public Transform playerCurrentMove;

    private float saveIntervalTime = 1f; 
    private float timeSinceLastSave = 0;
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
            timeSinceLastSave += Time.deltaTime;
            if (timeSinceLastSave >= saveIntervalTime)
            {
                timeSinceLastSave = 0;
                RecordPlayerPositionAndRotation();
                SaveRecordedData();
            }

        }    

    }

    public void RecordPlayerPositionAndRotation()
    {
        recordedPositions.Add(transform.position);
        recordedRotations.Add(transform.rotation);
    }

    public void SaveRecordedData()
    {
        PlayerData playerData = new PlayerData();
        playerData.positions = recordedPositions.ToArray();
        playerData.rotations = recordedRotations.ToArray();

        string jsonData = JsonUtility.ToJson(playerData);

        string filePath = Path.Combine(Application.dataPath, "../player_data.json");

        File.WriteAllText(filePath, jsonData);
    }

    [System.Serializable]
    public class PlayerData
    {
        public Vector3[] positions;
        public Quaternion[] rotations;
    }
}

