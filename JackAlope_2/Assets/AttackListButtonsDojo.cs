using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackListButtonsDojo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void WalkRun()
    {
        this.GetComponent<Text>().text = "Drag to move";
    }

    public void Punch()
    {
        this.GetComponent<Text>().text = "Tap to attack";
    }
    public void Grab()
    {
        this.GetComponent<Text>().text = "Walk towards and enemy";
    }

    public void Jump()
    {
        this.GetComponent<Text>().text = "Swipe up and let it go";
    }

    public void UpperCut()
    {
        this.GetComponent<Text>().text = "Hold down your finger on the screen until you see a bar charging up, when the bar gets full realese your finger to make an uppercut";
    }

    public void TornadoKick()
    {
        this.GetComponent<Text>().text = "If you have enough energy points, swipe up your finger to make a tornado kick to attack the enemies on range";
    }

    public void SideKick()
    {
        this.GetComponent<Text>().text = "If you have enough energy points, swipe down your finger to make a fast kick to attack the enemies on range";
    }

    public void Running_Punch()
    {
        this.GetComponent<Text>().text = "Swipe left or right to start running on frenzy in the direction that you swipe, then tap the screen to punch the enemies on the right moment";
    }
}
