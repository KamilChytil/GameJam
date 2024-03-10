using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(ButtonArea))]
public class RestorePoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ButtonPush ()
    {
        ParadoxManager.timeRiftDone = true;
        GameObject.Find("checkbox_restore").GetComponent<Toggle>().isOn = true;
    }
}
