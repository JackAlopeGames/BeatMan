using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarsScore : MonoBehaviour {

    // Use this for initialization

    public GameObject star1, star2, star3;
    public int TotalScore, CurrentScore;
    public GameObject Manager;
    public GameObject AudioSource;
    public AudioClip ContinueSong;
    private GameObject BannerController;
    private GameObject SavingSystem;
    void OnEnable () {
        this.AudioSource = GameObject.FindGameObjectWithTag("Music");
        BannerController = GameObject.FindGameObjectWithTag("BannerController");
        SavingSystem = GameObject.FindGameObjectWithTag("SavingSystem");
        try
        {
            AudioSource.GetComponent<AudioSource>().Pause();
        }
        catch { }
        GlobalAudioPlayer.PlaySFX("Victory");
        TotalScore = this.Manager.GetComponent<SavingPoints>().savingPoints;
        this.SavingSystem.GetComponent<SavingSystem>().Points += this.TotalScore;
        this.SavingSystem.GetComponent<SavingSystem>().Save();
        CurrentScore =  this.Manager.GetComponent<SavingPoints>().currentPoints;
        StartCoroutine(StarsLoad());
        

    }
	
    IEnumerator StarsLoad()
    {
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        
        this.CurrentScore= this.Manager.GetComponent<SavingPoints>().currentPoints;
        yield return new WaitForSeconds(1);
        if (CurrentScore >= 450)
        {
            star1.SetActive(true);
            GlobalAudioPlayer.PlaySFX("GunShot");
            BannerController.GetComponent<BannerController>().stars++;
            SavingSystem.GetComponent<SavingSystem>().Stars++;

        }
        yield return new WaitForSeconds(1);
        if (CurrentScore >= 900)
        {
            star2.SetActive(true);
            GlobalAudioPlayer.PlaySFX("GunShot");
            BannerController.GetComponent<BannerController>().stars++;
            SavingSystem.GetComponent<SavingSystem>().Stars++;
        }
        yield return new WaitForSeconds(1);
        if (CurrentScore >= 1250)
        {
            star3.SetActive(true);
            GlobalAudioPlayer.PlaySFX("GunShot");
            BannerController.GetComponent<BannerController>().stars++;
            SavingSystem.GetComponent<SavingSystem>().Stars++;
        }
        try
        {
            this.AudioSource.GetComponent<AudioSource>().clip = ContinueSong;
            AudioSource.GetComponent<AudioSource>().Play();
        }
        catch { }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
