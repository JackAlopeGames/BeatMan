using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class LevelMonitor : MonoBehaviour {

    // Use this for initialization
    
    public GameObject [] Levels;
    public int LevelSelect;
	void Start () {
        if (LevelSelect == 0)
        {
            this.Levels[LevelSelect].transform.GetChild(1).GetComponent<Text>().text = "TRAINING ROOM";
        }
        else
        {
            this.Levels[LevelSelect].transform.GetChild(1).GetComponent<Text>().text = "LEVEL: " + LevelSelect;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void right()
    {
        
        if(LevelSelect< Levels.Length -1)
        {
            this.Levels[LevelSelect].SetActive(false);
            LevelSelect++;
            this.Levels[LevelSelect].SetActive(true);
        }
        else if(LevelSelect == Levels.Length -1)
        {
            this.Levels[LevelSelect].SetActive(false);
            LevelSelect =0;
            this.Levels[LevelSelect].SetActive(true);
        }
        this.Levels[LevelSelect].GetComponent<Animator>().SetTrigger("Scale");
        if (LevelSelect == 0)
        {
            this.Levels[LevelSelect].transform.GetChild(1).GetComponent<Text>().text = "TRAINING ROOM";
        }
        else
        {
            this.Levels[LevelSelect].transform.GetChild(1).GetComponent<Text>().text = "LEVEL: " + LevelSelect;
        }
    }

    public void left()
    {
        if (LevelSelect >0)
        {

            this.Levels[LevelSelect].SetActive(false);
            LevelSelect--;
            this.Levels[LevelSelect].SetActive(true);
        }
        else if (LevelSelect == 0)
        {
            this.Levels[LevelSelect].SetActive(false);
            LevelSelect = Levels.Length -1;
            this.Levels[LevelSelect].SetActive(true);
           
        }
        this.Levels[LevelSelect].GetComponent<Animator>().SetTrigger("Scale");
        if(LevelSelect ==0)
        {
            this.Levels[LevelSelect].transform.GetChild(1).GetComponent<Text>().text = "TRAINING ROOM";
        }
        else
        {
            this.Levels[LevelSelect].transform.GetChild(1).GetComponent<Text>().text = "LEVEL: " + LevelSelect;
        }
    }


    public void Ok()
    {
        if (LevelSelect == 0)
        {
            Analytics.CustomEvent("TrainingLevel_Started");
        }
        else
        {
            Analytics.CustomEvent("Level0" + LevelSelect + "_Started");
        }
        this.GetComponent<LevelSelection>().startLevel(this.LevelSelect);
    }

}
