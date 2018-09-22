using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFpsPosition : MonoBehaviour {

    // Use this for initialization
    bool change;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangePosition()
    {
        change = !change;
        if (change)
        {
            this.gameObject.GetComponent<RectTransform>().position = new Vector3(this.gameObject.GetComponent<RectTransform>().position.x, this.gameObject.GetComponent<RectTransform>().position.y - 5, this.gameObject.GetComponent<RectTransform>().position.z);
        }
        else
        {
            this.gameObject.GetComponent<RectTransform>().position = new Vector3(this.gameObject.GetComponent<RectTransform>().position.x, this.gameObject.GetComponent<RectTransform>().position.y + 5, this.gameObject.GetComponent<RectTransform>().position.z);
        }
    }
}
