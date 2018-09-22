using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetThunders : MonoBehaviour {
    // Use this for initialization

    private float thunders;
    public GameObject controller;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (thunders != controller.GetComponent<ThunderLoading>().ThunderCount)
        {
            thunders = controller.GetComponent<ThunderLoading>().ThunderCount;
            this.GetComponent<Text>().text = thunders + "/20";
        }
    }
}
