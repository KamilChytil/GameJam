using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    public Transform playerLocation;

    public bool isPLayerInArea = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            isPLayerInArea = true;
            Debug.Log("Player entered the trigger!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            isPLayerInArea = false;
            Debug.Log("Player leave the trigger!");
        }
    }
    void Start()
    {
    }

    void Update()
    {
        
    }
}
