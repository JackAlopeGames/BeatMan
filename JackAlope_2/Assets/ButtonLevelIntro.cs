using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLevelIntro : MonoBehaviour {

    // Use this for initialization

    public GameObject LevelIntroWindow;
    public GameObject LevelButton;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void buttonClickLevel()
    {
        this.LevelIntroWindow.SetActive(true);
        this.LevelIntroWindow.GetComponent<LevelIntroduction>().LevelButton = this.LevelButton;
    }
}
