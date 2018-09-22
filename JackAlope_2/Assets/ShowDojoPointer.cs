using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDojoPointer : MonoBehaviour {

    // Use this for initialization
    public GameObject player;
    public float distance;
    public GameObject pointer;
    Vector3 origin;
	void OnEnable () {
        this.origin = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (origin.x + distance < this.player.transform.position.x && !this.pointer.activeInHierarchy)
        {
            this.pointer.SetActive(true);
        }
        else if (origin.x + distance >= this.player.transform.position.x && this.pointer.activeInHierarchy)
        {
            this.pointer.SetActive(false);
        }
	}
}
