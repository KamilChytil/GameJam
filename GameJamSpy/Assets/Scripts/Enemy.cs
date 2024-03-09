using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IEnemy
{
    public int ID;
    public int onlyOneTime = 0;
    public bool isSolve = false;
    public virtual void Solve(int enemyID)
    {

    }
    public virtual void Problem()
    {

    }
}
