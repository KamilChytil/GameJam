using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(InteractionZone))]
public class IntelPoint : MonoBehaviour
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
        ParadoxManager.intelDone = true;
        GameObject.Find("checkbox_intel").GetComponent<Toggle>().isOn = true;
    }
}
