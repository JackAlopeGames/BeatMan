using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

    // Use this for initialization

    public GameObject Music;
    public GameObject SoundFX;
    public GameObject FpsScript;
    public GameObject MbutOn, MbutOff, FXbutOn, FXbutOff, FpsOn, FpsOff;
    public GameObject Fps;
    bool MusicB = true;
    bool SoundFXB = true;
    bool FpsShow = true;

	void OnEnable () {
        this.FpsScript = GameObject.FindGameObjectWithTag("FpsManager");
        this.SoundFX = GameObject.FindGameObjectWithTag("AudioPlayer");
        try
        {
            this.Music = GameObject.FindGameObjectWithTag("Music");

            if (this.Music.GetComponent<AudioSource>().mute == true)
            {
                this.MusicB = false;
                MbutOff.SetActive(false);
                MbutOn.SetActive(true);
            }
            if (this.SoundFX.GetComponent<AudioSource>().mute == true)
            {
                this.SoundFXB = false;
                FXbutOff.SetActive(false);
                FXbutOn.SetActive(true);
            }
            if (this.FpsScript.GetComponent<SetTargetFps>().visible)
            {
                FpsShow = false;
                FpsOff.SetActive(true);
                FpsOn.SetActive(false);
                this.Fps.SetActive(true); 
            }
        }
        catch { }
    }
	
    public void MusicSwitch()
    {
        GlobalAudioPlayer.PlaySFX("POP");
        if (MusicB)
        {
            this.Music.GetComponent<AudioSource>().mute = true;
        }
        else
        {
            this.Music.GetComponent<AudioSource>().mute = false;
        }
        this.MusicB = !this.MusicB;
    }

    public void SoundFXSwitch()
    {
        GlobalAudioPlayer.PlaySFX("POP");
        if (SoundFXB)
        {
            this.SoundFX.GetComponent<AudioSource>().mute = true;
        }
        else
        {
            this.SoundFX.GetComponent<AudioSource>().mute = false;
        }
        this.SoundFXB = !this.SoundFXB;
    }

    public void FpsShowSwitch()
    {
        GlobalAudioPlayer.PlaySFX("POP");
        if (FpsShow)
        {
            this.FpsScript.GetComponent<SetTargetFps>().visible = true;
            this.Fps.SetActive(true);
        }
        else
        {
            this.FpsScript.GetComponent<SetTargetFps>().visible = false;
            this.Fps.SetActive(false);
        }
        this.FpsShow = !this.FpsShow;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
