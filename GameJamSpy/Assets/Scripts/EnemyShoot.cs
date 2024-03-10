using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    public Transform playerLocation;

    public bool isPlayerInArea = false;

    private float timer;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerLocation = other.transform;
            isPlayerInArea = true;
            Debug.Log(timer + "timer");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            isPlayerInArea = false;
            //Debug.Log("Player leave the trigger!");
        }
    }
    void Start()
    {
    }

    void Update()
    {
        timer += Time.deltaTime;
    }
}
