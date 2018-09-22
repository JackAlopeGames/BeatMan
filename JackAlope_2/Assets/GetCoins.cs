using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetCoins : MonoBehaviour {

    // Use this for initialization

    private float coins;
    public GameObject controller;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(coins != controller.GetComponent<BannerController>().coins)
        {
            coins = controller.GetComponent<BannerController>().coins;
            this.GetComponent<Text>().text = coins + "";
        }
	}
}
