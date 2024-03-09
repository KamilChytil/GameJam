using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Enemy
{
    public override void Problem()
    {
        FindObjectOfType<TimeLine>().AddProblemEnemy(ID);

    }



    private void OnTriggerEnter(Collider other)
    {
        if (onlyOneTime == 0)
        {

            if (other.CompareTag("Player"))
            {
                onlyOneTime++;
                Problem();
            }
        }
    }
}
