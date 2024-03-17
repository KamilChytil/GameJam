using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTuret : MonoBehaviour
{
    ParadoxCauser[] paradoxCauser;

    public static bool isTuretDisable = false;

    // Start is called before the first frame update
    void Start()
    {
        GetParadoxCauserScriptFromTurets();
    }   
    // Update is called once per frame
    void Update()
    {
        
    }



   void GetParadoxCauserScriptFromTurets()
    {
        GameObject gameObjectTurets = GameObject.Find("Turets");
        int getAllTuretCount = gameObjectTurets.transform.childCount;

        GameObject[] turets = new GameObject[getAllTuretCount];
        paradoxCauser = new ParadoxCauser[getAllTuretCount];

        for (int i = 0; i  < getAllTuretCount; i++)
        {

            turets[i] = gameObjectTurets.transform.GetChild(i).gameObject;
            paradoxCauser[i] = turets[i].GetComponentInChildren<ParadoxCauser>();
        }



    }


    private void ResolveTuretParadox(ParadoxCauser[] paradoxCauser)
    {

        for(int i = 0;i <paradoxCauser.Length;i++)
        {
            paradoxCauser[i].ResolveParadox();

        }

    }


    private void ButtonPush()
    {
        isTuretDisable = true;
        ResolveTuretParadox(paradoxCauser);
    }

}