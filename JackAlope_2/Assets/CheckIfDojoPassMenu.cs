using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckIfDojoPassMenu : MonoBehaviour {

    public GameObject SavingSystem;
    public GameObject PlayButton;
    public GameObject MaskMonitor;
    public GameObject HandAnim;
	// Use this for initialization
	void Start () {
        try
        {
            if (SavingSystem.GetComponent<SavingSystem>().DojoPass == false)
            {
                PlayButton.GetComponent<Button>().onClick.Invoke();
                MaskMonitor.SetActive(true);
                HandAnim.SetActive(true);
            }
        }
        catch { }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
