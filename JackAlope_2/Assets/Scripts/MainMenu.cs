using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    // Use this for initialization
    public UIFader UI_fader;
    [Header("HUD Portrait")]
    public Sprite HUDPortrait;

    [Header("GameOver Portrait")]
    public Sprite GameOverPortrait;

    public GameObject SavingSystem;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartPlay()
    {
        GlobalAudioPlayer.PlaySFX("AccesLevel");
        UI_fader.Fade(UIFader.FADE.FadeOut, 2f, 0f);

        StartCoroutine(fadeWait());
    }

    IEnumerator fadeWait()
    {
        yield return new WaitForSeconds(2);
        // SceneManager.LoadScene("CharSelect");
        GlobalPlayerData.PlayerHUDPortrait = HUDPortrait;
        GlobalPlayerData.PlayerGameOver = GameOverPortrait;

        if (SavingSystem.GetComponent<SavingSystem>().DojoPass)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene("Level_02");
        }
    }

    public void PlayReward()
    {
        GlobalAudioPlayer.PlaySFX("ItemPickup");
        GlobalPlayerData.PlayerHUDPortrait = HUDPortrait;
        GlobalPlayerData.PlayerGameOver = GameOverPortrait;
    }
    public void Weapons()
    {

    }
    
}
