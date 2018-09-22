using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {

    bool pause;
    public GameObject SavingSystem;
    public bool NotFromPauseMenu;

    // Use this for initialization
    void Start () {
       SavingSystem =  GameObject.FindGameObjectWithTag("SavingSystem");
    }
	
    public void ButtonPause()
    {
    
            pause = !pause;
            if (pause)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
     
    }

    public void HomeButton()
    {
        try
        {
            this.SavingSystem.GetComponent<SavingSystem>().Save();
            Destroy(GameObject.FindGameObjectWithTag("UI"));
            Destroy(GameObject.FindGameObjectWithTag("AdWeapon"));
            Destroy(GameObject.FindGameObjectWithTag("ExtraCheker"));
            Destroy(this.SavingSystem);
            Destroy(GameObject.FindGameObjectWithTag("SaveWhenPaused"));
        }
        catch { }
        SceneManager.LoadScene("MainMenu");
     
    }

	// Update is called once per frame
	void Update () {
		
	}
}
