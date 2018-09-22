using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipGenerator : MonoBehaviour {


    private string[] Tips = new string[] {"Destroy objects or defeat enemies to get coins.","Having enough energy points allows you to do a special attack.","Fill up your energy bar by punching enemies.","Swipe up your finger to jump, if you have enough combo energy, you might do an special move.", "If you have enough combo energy, try to swipe your finger left ot right to do a running-punch especial attack.", "When you are using the running-punch special attack, try to tap the screen when you are close to an enemy to hit it.", "If you have enough combo energy, try to swipe up your finger down and you might to a special kick attack.", "Try to be close of an enemy before using a special attack, otherwise you might fail and lose energy.", "If you run directly to an enemy, you can grab it and attack it when you are holding it.", "It is easier to grab enemies from behind to avoid their frontal attacks.", "When you are grabbing an enemy, swipe up your finger to let it go.", "Coins are important, you might use them in the future to get special rewards.", "If you look on boxes or barreels, you may find some helping items and coins." };
    private string[] BasicTips = new string[] { "Destroy objects or defeat enemies to get coins.", "Having enough energy points allows you to do a special attack.", "Fill up your energy bar by punching enemies.", "If you run directly to an enemy, you can grab it and attack it when you are holding it.", "It is easier to grab enemies from behind to avoid their frontal attacks.", "When you are grabbing an enemy, swipe up your finger to let it go.", "Coins are important, you might use them in the future to get special rewards.", "If you look on boxes or barreels, you may find some helping items and coins."};
    private int r;
    public bool ShowFullTips;
	// Use this for initialization
	void OnEnable () {
        if (!ShowFullTips)
        {
            this.r = Random.Range(0, BasicTips.Length);
            if (this.GetComponent<Text>().text != BasicTips[r].ToUpper())
            {
                this.GetComponent<Text>().text = BasicTips[r].ToUpper();
            }
        }
        else
        {
            this.r = Random.Range(0, Tips.Length);
            if (this.GetComponent<Text>().text != Tips[r].ToUpper())
            {
                this.GetComponent<Text>().text = Tips[r].ToUpper();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!ShowFullTips)
        {
            if (this.GetComponent<Text>().text != BasicTips[r].ToUpper())
            {
                this.GetComponent<Text>().text = BasicTips[r].ToUpper();
            }
        }
        else
        {
            if (this.GetComponent<Text>().text != Tips[r].ToUpper())
            {
                this.GetComponent<Text>().text = Tips[r].ToUpper();
            }
        }
    }
}
