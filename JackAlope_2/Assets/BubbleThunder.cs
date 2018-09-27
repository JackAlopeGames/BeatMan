using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleThunder : MonoBehaviour {

    // Use this for initialization
    public GameObject BC;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (BC.GetComponent<ThunderLoading>().ThunderCount >= 20 && this.gameObject.GetComponent<Image>().enabled)
        {
            this.gameObject.GetComponent<Image>().enabled = false;
            this.transform.GetChild(0).GetComponent<Text>().enabled = false;
            this.transform.GetChild(1).GetComponent<Text>().enabled = false;
        }
        else if (BC.GetComponent<ThunderLoading>().ThunderCount < 20 && !this.gameObject.GetComponent<Image>().enabled)
        {
            this.gameObject.GetComponent<Image>().enabled = true;
            this.transform.GetChild(0).GetComponent<Text>().enabled = true;
            this.transform.GetChild(1).GetComponent<Text>().enabled = true;
        }
	}
}
