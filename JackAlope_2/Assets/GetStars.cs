using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetStars : MonoBehaviour {


    // Use this for initialization

    private float stars;
    public GameObject controller;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (stars != controller.GetComponent<BannerController>().stars)
        {
            stars = controller.GetComponent<BannerController>().stars;
            this.GetComponent<Text>().text = stars + "";
        }
    }
}
