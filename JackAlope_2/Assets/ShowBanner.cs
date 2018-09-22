using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBanner : MonoBehaviour {

    // Use this for initialization
    public GameObject Banner, bubble;
     public bool bubbleb;
	void OnEnable () {
        this.Banner.SetActive(true);
        if (bubbleb && GameObject.FindGameObjectWithTag("BannerController").GetComponent<ThunderLoading>().ThunderCount <20)
        {
            this.bubble.SetActive(true);
        }else
        {
            this.bubble.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
