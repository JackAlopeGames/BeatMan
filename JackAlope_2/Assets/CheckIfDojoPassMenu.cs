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
                StartCoroutine(WaitToGive18());
                PlayButton.GetComponent<Button>().onClick.Invoke();
                MaskMonitor.SetActive(true);
                HandAnim.SetActive(true);
            }
        }
        catch { }
	}

    IEnumerator WaitToGive18()
    {
        yield return new WaitForSeconds(.5f);
        GameObject BC = GameObject.FindGameObjectWithTag("BannerController");
        if (BC.GetComponent<ThunderLoading>().ThunderCount != 18 && BC.GetComponent<ThunderLoading>().ThunderCount != 20)
        {
            BC.GetComponent<ThunderLoading>().ThunderCount = 18;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
