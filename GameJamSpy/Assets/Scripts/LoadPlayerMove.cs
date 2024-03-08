using UnityEngine;
using System.IO;

public class LoadPlayerMove : MonoBehaviour
{
    public string filePath = "player_data.json";
    public float movementSpeed = 5f;

    private Vector3[] recordedPositions;
    private int currentPositionIndex = 0;

    public FinishArea finishArea;

    void Start()
    {
        finishArea = FindObjectOfType<FinishArea>();
        PlayerPrefs.SetInt("isPlayerFinish", 0);

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
            // Read JSON data from file
            string jsonData = File.ReadAllText(fullPath);

            // Deserialize JSON data into PlayerData object
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonData);

            // Assign recorded positions
            recordedPositions = playerData.positions;
        }
        else
        {
            Debug.LogError("File not found: " + fullPath);
        }
    }

    void Update()
    {
        // Move player along recorded positions
        MovePlayerAlongPath();
    }

    void MovePlayerAlongPath()
    {
        if (recordedPositions == null || recordedPositions.Length == 0)
            return;

        // Move towards the current position
        transform.position = Vector3.MoveTowards(transform.position, recordedPositions[currentPositionIndex], movementSpeed * Time.deltaTime);

        // Check if reached the current position
        if (Vector3.Distance(transform.position, recordedPositions[currentPositionIndex]) < 0.1f)
        {
            // Move to the next position
            currentPositionIndex = (currentPositionIndex + 1) % recordedPositions.Length;
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public Vector3[] positions;
        public Quaternion[] rotations;
    }
}
