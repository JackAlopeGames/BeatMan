using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUntilSpot1 : MonoBehaviour {

    // Use this for initialization
    public GameObject Camera;
    public BoxCollider AreaCollider;
	void Start () {
        this.Camera = GameObject.FindGameObjectWithTag("CameraFather");
        Camera.GetComponent<CameraFollow>().CurrentAreaCollider = AreaCollider;
	}
	
	// Update is called once per frame
	void Update () {
		if(Camera.GetComponent<CameraFollow>().CurrentAreaCollider != AreaCollider)
        {
            Camera.GetComponent<CameraFollow>().CurrentAreaCollider = AreaCollider;
        }
	}

    public void changeArea(BoxCollider ch)
    {
        Camera.GetComponent<CameraFollow>().CurrentAreaCollider = ch;
    }
}
