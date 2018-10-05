using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour {

    // Use this for initialization

    public GameObject bomb;
    GameObject cloneBomb;
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ThrowBomb()
    {
        if (cloneBomb == null)
        {
            cloneBomb = Instantiate(bomb, this.transform.position, Quaternion.identity);
        }
        else
        {
            cloneBomb.transform.position = this.transform.position;
            cloneBomb.GetComponent<MeshRenderer>().enabled = true;
        }
        cloneBomb.GetComponent<Rigidbody>().useGravity = true;
    }
}
