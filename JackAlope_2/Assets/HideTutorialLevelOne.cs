using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideTutorialLevelOne : MonoBehaviour {

    // Use this for initialization
    public GameObject SavingSystem;
    public GameObject ButtonTutorial;
    public GameObject BackButton;
    public GameObject PauseScript;
	void OnEnable () {
        try
        {
            SavingSystem = GameObject.FindGameObjectWithTag("SavingSystem");
        }
        catch { }
        if(this.SavingSystem.GetComponent<SavingSystem>().DojoPass == false){
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
            this.BackButton.GetComponent<Button>().onClick.Invoke();
        }
	}

  

    // Update is called once per frame
    void Update () {
        if (this.SavingSystem.GetComponent<SavingSystem>().DojoPass && this.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
	}

    public void DojoPassTrue()
    {
        this.PauseScript.GetComponent<PauseGame>().NotFromPauseMenu = true;
       // SavingSystem.GetComponent<SavingSystem>().DojoPass = true;
       // this.SavingSystem.GetComponent<SavingSystem>().Save();
        //this.ButtonTutorial.GetComponent<Button>().onClick.Invoke();
    }
}
