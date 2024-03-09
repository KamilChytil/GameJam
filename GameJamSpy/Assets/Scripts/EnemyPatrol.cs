using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float enemySpeed;
    public float enemyWait;
    public Transform[] enemyPoints;
    private int currentPointIndex = 0;
    public EnemyShoot enemyShoot;

    private float timeToWait = 0f;

    public bool isEnemyAlive = true;


    void Start()
    {
        isEnemyAlive = true;
        enemyShoot = GetComponent<EnemyShoot>();
        Transform childTransform = transform.GetChild(1);
        enemyShoot = childTransform.GetComponent<EnemyShoot>();
    }

    void Update()
    {
        if(isEnemyAlive == true)
        {
            EnemyMoveToPoint();

        }
        else 
        {
            Debug.Log("EnemyDead");

        }
    }


    private void ChangePoint()
    {
        currentPointIndex++;

        if(currentPointIndex == enemyPoints.Length)
        {
            currentPointIndex = 0;
        }

    }


    private void PlayerEntrShootArea()
    {
        Vector3 targetDirection = enemyShoot.playerLocation.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        timeToWait += Time.deltaTime;
        transform.position = transform.position;

    }

    private void RotationEnemy()
    {
        Vector3 targetDirection = enemyPoints[currentPointIndex].position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        float angleToRotate = Quaternion.Angle(transform.rotation, targetRotation);

        float direction = Mathf.Sign(Vector3.Cross(transform.forward, targetDirection).y);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 5f * angleToRotate * direction);
    }


    private void EnemyMoveToPoint()
    {

        if(enemyShoot.isPLayerInArea == true && timeToWait <= 1f)
        {
            PlayerEntrShootArea();
        }
        else
        {
            enemyShoot.isPLayerInArea = false;
            timeToWait = 0f;
            RotationEnemy();
            transform.position = Vector3.MoveTowards(transform.position, enemyPoints[currentPointIndex].position, enemySpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, enemyPoints[currentPointIndex].position) < 1f)
            {
                ChangePoint();
            }

        }


    }

}
