using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour {


    public GameObject SavingSystem;
	// Use this for initialization
	void Start () {
       this.SavingSystem =  GameObject.FindGameObjectWithTag("SavingSystem");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Dojo()
    {
        this.SavingSystem.GetComponent<SavingSystem>().Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_02");
    }
    public void Level_01()
    {
       
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_01");
    }
    public void Level_02()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PhaseOne");

    }
    public void Level_03()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PhaseOne");
    }
    public void Level_04()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PhaseOne");
    }
    public void Level_05()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PhaseOne");
    }
    public void Level_06()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PhaseOne");
    }


    public void startLevel(int i)
    {
        if (i == 0)
        {
            Dojo();
        }
        if (i == 1)
        {
            Level_01();
        }
    }
}
