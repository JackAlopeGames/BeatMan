using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour {

    // Use this for initialization
    public AudioClip introsong;
    private GameObject Music;
	void Start () {
        this.Music = GameObject.FindGameObjectWithTag("Music");

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SprayCan()
    {
        GlobalAudioPlayer.PlaySFX("SprayCan");
    }
    public void Spray()
    {
        GlobalAudioPlayer.PlaySFX("Spray");
    }
    public void IntroSong()
    {
        this.Music.GetComponent<AudioSource>().clip = introsong;
        this.Music.GetComponent<AudioSource>().Play();
    }
}
