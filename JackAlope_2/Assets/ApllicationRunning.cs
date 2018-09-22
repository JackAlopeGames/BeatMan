using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApllicationRunning : MonoBehaviour
{

    // Use this for initialization

    public GameObject SavingSystem;
    void Start()
    {
        Application.runInBackground = true;
    }


    public void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            // Game is paused, start service to get notifications
           Application.runInBackground = true;
          SavingSystem.GetComponent<SavingSystem>().Save();
        }
        else
        {
            // Game is unpaused, stop service notifications. 
            Application.runInBackground = false;
        }
    }
}
