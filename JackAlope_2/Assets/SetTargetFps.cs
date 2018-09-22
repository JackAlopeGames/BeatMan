using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTargetFps : MonoBehaviour {

    // Use this for initialization

    public bool visible;
	void OnEnable () {
        Application.targetFrameRate = 60;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnLevelWasLoaded(int level)
    {
        Application.targetFrameRate = 60;
    }

    
}
