using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerPositionLoader : MonoBehaviour
{
    public string loadFilePath = "player_data.json"; 
    public float movementSpeed = 5f;

    private List<Vector3> positions = new List<Vector3>();
    private List<Quaternion> rotations = new List<Quaternion>();
    
    private int currentIndex = 0;


    public FinishArea finishArea;

    
    public int OnOffPLayerFinis = 0;

    public float timer = 0f;
    public float nextSlide = 1f;
    private float debugTimer = 0f;


    private void Start()
    {
        
        finishArea = FindAnyObjectByType<FinishArea>();
        if(OnOffPLayerFinis == 0)
        {
            PlayerPrefs.SetInt("isPlayerFinish", 0);

        }

        finishArea.isPlayerFinish = PlayerPrefs.GetInt("isPlayerFinish", 0);
        if(finishArea.isPlayerFinish == 1)
        {
            LoadPositionDataFromJson();

        }
    }

    private void Update()
    {

        if(finishArea.isPlayerFinish == 1)
        {

            timer += Time.deltaTime;
            if(timer >= nextSlide)
            {
                if (currentIndex < positions.Count)
                {
                    transform.position = positions[currentIndex];
                    transform.rotation = rotations[currentIndex];



                        currentIndex++;

                }
                
                timer -= nextSlide;
            }
            else
            {
                if(currentIndex<positions.Count -1)
                {

                    transform.position = Vector3.Lerp(positions[currentIndex], positions[currentIndex + 1],timer/nextSlide);
                    transform.rotation = Quaternion.Lerp(rotations[currentIndex], rotations[currentIndex + 1], timer / nextSlide);
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
        string jsonData = File.ReadAllText(loadFilePath);
        PositionData data = JsonUtility.FromJson<PositionData>(jsonData);

        if (data != null)
        {
            positions = data.positions;
            rotations = data.rotations;
        }
        else
        {
            Debug.LogWarning("Nepodaøilo se naèíst data.");
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
