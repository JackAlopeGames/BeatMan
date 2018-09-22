using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetMin : MonoBehaviour {

    // Use this for initialization
    private float Min;
    private  GameObject SavingSystem;
    void Start()
    {
        SavingSystem = GameObject.FindGameObjectWithTag("SavingSystem");
    }

    // Update is called once per frame
    void Update()
    {
        if (Min != SavingSystem.GetComponent<SavingSystem>().Min)
        {
            Min = SavingSystem.GetComponent<SavingSystem>().Min;
            this.GetComponent<Text>().text = "0" + Min + "";
        }
    }
}
