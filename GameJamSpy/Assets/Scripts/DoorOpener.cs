using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ButtonArea))]
public class DoorOpener : MonoBehaviour
{
    public Door[] doors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPush()
    {
        foreach (var door in doors)
        {
            door.Open();
        }
    }
}
