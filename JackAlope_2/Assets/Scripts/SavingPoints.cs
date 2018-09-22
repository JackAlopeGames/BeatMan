using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingPoints : MonoBehaviour {

    // Use this for initialization
    public int savingPoints;
    public int currentPoints;
	void Start () {
    }

    // Update is called once per frame



    public GameObject ScoreWindow, CompletedWindow,HomeButton,NextHealthX2;

    public void enableScoreWindow()
    {
        StartCoroutine(EnableScoreWindow());
    }
    IEnumerator EnableScoreWindow()
    {
        yield return new WaitForSeconds(2);
        ScoreWindow.SetActive(true);
        HideOtherWindows();
    }

    public void HideOtherWindows()
    {
        try
        {
            this.CompletedWindow.SetActive(false);
            this.HomeButton.SetActive(false);
            this.NextHealthX2.SetActive(false);
        }
        catch { }
    }
}
