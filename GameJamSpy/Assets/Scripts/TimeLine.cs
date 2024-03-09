using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLine : MonoBehaviour
{
    public List<int> solvedEnemies = new List<int>();
    public List<int> problemEnemies = new List<int>();
    public List<float> problemWhenHappend = new List<float>();
    public float gameTime;
    public static TimeLine Instance;
    public bool isInFinish= false;
    public GameObject gameObject;   

    public GameObject chara;
    public GameObject cameraDefender;
    public GameObject cameraMain;

    public Transform spawnLocation;

    private bool isSpawn = false;

    public bool isLoad;
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Destroy(chara);
            Destroy(cameraDefender);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(chara);
        DontDestroyOnLoad(cameraDefender);
        chara.SetActive(false);
        cameraDefender.SetActive(false);
    }
    private void Update()
    {
        gameTime += Time.deltaTime;
        if(isInFinish ==true && isSpawn == false)
        {
            chara.SetActive(true);
            cameraDefender.SetActive(true);
            cameraMain.SetActive(false);
            isSpawn = true;
        }

        
    }
    public void AddSolvedEnemy(int enemyID)
    {
        solvedEnemies.Add(enemyID);
        CheckSolveProblem();
        Debug.Log(enemyID);
    }

    public void AddProblemEnemy(int enemyID)
    {
        problemWhenHappend.Add(gameTime);
        problemEnemies.Add(enemyID);
        PlayerLose();
    }




    private void  PlayerLose()
    {
        bool containsDuplicates = ContainsDuplicates(problemEnemies);
        if (containsDuplicates)
            {
            Debug.Log("YOU LOSE");
        }
    }

    bool ContainsDuplicates(List<int> list)
    {
        HashSet<int> set = new HashSet<int>();

        foreach (int item in list)
        {
            if (!set.Add(item))
            {
                return true;
            }
        }

        return false;
    }
    private void CheckSolveProblem()
    {
        if (solvedEnemies != null && problemEnemies != null)
        {
            for (int i = 0; i < solvedEnemies.Count; i++)
            {
                for (int j = 0; j < problemEnemies.Count; j++)
                {
                    if (solvedEnemies[i] == problemEnemies[j])
                    {
                        //Debug.Log(j);                    
                    }
                }
            }
        }

    }
}
