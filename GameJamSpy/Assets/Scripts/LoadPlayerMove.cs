using UnityEngine;
using System.IO;

public class LoadPlayerMove : MonoBehaviour
{
    public string filePath = "player_data.json";
    public float movementSpeed = 4f;

    private Vector3[] recordedPositions;
    private int currentPositionIndex = 0;

    public FinishArea finishArea;

    void Start()
    {
        finishArea = FindObjectOfType<FinishArea>();
        //NUTNÉ ZAPNOUT PRO RESET poté zakomentovat pomocí //
        PlayerPrefs.SetInt("isPlayerFinish", 0);
        movementSpeed = 4f;
        finishArea.isPlayerFinish = PlayerPrefs.GetInt("isPlayerFinish", 0);

        if (finishArea.isPlayerFinish == 1)
        {
            LoadRecordedData();
        }
    }

    void LoadRecordedData()
    {
        string fullPath = Path.Combine(Application.dataPath, "../", filePath);

        if (File.Exists(fullPath))
        {
            string jsonData = File.ReadAllText(fullPath);

            PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonData);

            recordedPositions = playerData.positions;
        }
        else
        {
            Debug.LogError("File not found: " + fullPath);
        }
    }

    void Update()
    {
        MovePlayerAlongPath();
    }

    void MovePlayerAlongPath()
    {
        if (recordedPositions == null || recordedPositions.Length == 0)
            return;
        if(currentPositionIndex < recordedPositions.Length)
        {
            Debug.Log(movementSpeed * Time.deltaTime+ "movementSpeed * Time.deltaTime");
            Debug.Log(recordedPositions[currentPositionIndex] + "recordedPositions[currentPositionIndex]");
            transform.position = Vector3.MoveTowards(transform.position, recordedPositions[currentPositionIndex], movementSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, recordedPositions[currentPositionIndex]) < 0.1f)
            {
                currentPositionIndex = (currentPositionIndex + 1);
            }

        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public Vector3[] positions;
        public Quaternion[] rotations;
    }
}
