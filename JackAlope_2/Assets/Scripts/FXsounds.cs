using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXsounds : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void pop()
    {
        GlobalAudioPlayer.PlaySFX("POP");
    }

    public void GunShot()
    {
        GlobalAudioPlayer.PlaySFX("GunShot");
    }

    public void AccessLevel()
    {
        GlobalAudioPlayer.PlaySFX("AccesLevel");
    }

    public void Item()
    {
        GlobalAudioPlayer.PlaySFX("ItemPickup");
    }

    public void ActivateRobot()
    {
        GlobalAudioPlayer.PlaySFX("Activate");
    }
}
