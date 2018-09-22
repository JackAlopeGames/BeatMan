using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowAnimation()
    {
        this.GetComponent<Animator>().SetTrigger("Monitor");
    }
}
