using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SolveProblems
{

    public override void Solve(int enemyID)
    {
        FindObjectOfType<TimeLine>().AddSolvedEnemy(enemyID);
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    solveOnlyOneTime++;
                    Enemy enemy = other.GetComponent<Enemy>();
                    Destroy(enemy.gameObject);
                    Solve(enemy.ID);
                }

           
        }
        else if (other.CompareTag("Door"))
        {

                if (Input.GetKeyDown(KeyCode.F))
                {
                    solveOnlyOneTime++;
                    Enemy enemy = other.GetComponent<Enemy>();
                Destroy(enemy.gameObject);
                Solve(enemy.ID);
                }

           
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Defender"))
        {
            Debug.Log("YOU LOSE");
        }
    }
}
