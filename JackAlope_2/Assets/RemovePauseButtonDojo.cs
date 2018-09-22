using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemovePauseButtonDojo : MonoBehaviour {

    public GameObject pauseButton;
    public GameObject fps;
    GameObject SavingSystem;
	// Use this for initialization
	void Start () {
        try
        {
            pauseButton.SetActive(false);
            fps.GetComponent<Text>().enabled = false;
        }
        catch { }
        this.SavingSystem = this.transform.parent.GetComponent<LevelEnterTutorialDojo>().SavingSystem;
        this.SavingSystem.GetComponent<SavingSystem>().Tutorial0 = false;
        this.transform.parent.GetComponent<LevelEnterTutorialDojo>().ResetTutorial();
        try
        {
            this.transform.parent.GetComponent<LevelEnterTutorialDojo>().ColliderTutorial.SetActive(true);
        }
        catch { }
    }

    private void OnDisable()
    {
        try
        {
            pauseButton.SetActive(true);
            fps.GetComponent<Text>().enabled = true;
        }
        catch { }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
