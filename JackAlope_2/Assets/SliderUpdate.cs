using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUpdate : MonoBehaviour {

    // Use this for initialization
    public GameObject ControlSensibility;
    public GameObject SwipeControls;
	void OnEnable () {
        SwipeControls = GameObject.FindGameObjectWithTag("SwipeControls");
        ControlSensibility = GameObject.FindGameObjectWithTag("FpsManager");
        this.GetComponent<Slider>().value = ControlSensibility.GetComponent<ControlSensibility>().Level;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GetComponent<Slider>().value != ControlSensibility.GetComponent<ControlSensibility>().Level)
        {
            ControlSensibility.GetComponent<ControlSensibility>().Level = (int)this.GetComponent<Slider>().value;
            try
            {
                SwipeControls.GetComponent<Swipe>().UpdateSensibility();
            }
            catch { }
        }
	}
}
