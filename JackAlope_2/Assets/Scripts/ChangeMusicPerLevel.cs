using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMusicPerLevel : MonoBehaviour {

    // Use this for initialization

    public AudioClip[] Songs;
    public GameObject AudioSource;
    public void Start()
    {
        try
        {
            this.AudioSource = GameObject.FindGameObjectWithTag("Music");

            if (SceneManager.GetSceneByName("MainMenu").isLoaded)
            {
                this.AudioSource.GetComponent<AudioSource>().clip = Songs[0];
            }
            else if (SceneManager.GetSceneByName("Level_01").isLoaded)
            {
                this.AudioSource.GetComponent<AudioSource>().clip = Songs[2];
            }
            else if (SceneManager.GetSceneByName("Level_02").isLoaded)
            {
                this.AudioSource.GetComponent<AudioSource>().clip = Songs[1];
            }
            this.AudioSource.GetComponent<AudioSource>().Play();
        }
        catch { }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
