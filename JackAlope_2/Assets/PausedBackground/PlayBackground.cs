using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayBackground : MonoBehaviour {

    void OnEnable()
    {
        PlayAnimation();
        if (this.gameObject.transform.parent.tag != "BG_Tutorial" && this.gameObject.transform.parent.tag != "GameOver" && this.gameObject.transform.parent.tag != "LevelComplete" &&  this.gameObject.transform.parent.tag != "MainMenu" && this.gameObject.tag != "SaveWarning" && this.transform.parent.gameObject.tag != "AttackListScreen" )
        {

            Time.timeScale = 0.05f;

        }
     
    }

    public void PlayAnimation()
    {
        if (this.gameObject.transform.parent.tag == "BG_Tutorial" || this.gameObject.transform.parent.tag == "GameOver" || this.gameObject.transform.parent.tag == "LevelComplete" || this.gameObject.transform.parent.tag == "MainMenu" ||  this.gameObject.tag == "SaveWarning" || this.transform.parent.gameObject.tag == "AttackListScreen")
        {
            this.GetComponent<Animator>().SetTrigger("PlaySlow");
        }
        else
        {
            this.GetComponent<Animator>().SetTrigger("Play");
        }
    }

    private void Update()
    {
        
        
    }
    public void BackToTimeZero()
    {
        Time.timeScale = 0;
    }
}
    