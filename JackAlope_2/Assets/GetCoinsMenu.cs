using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetCoinsMenu : MonoBehaviour {

    // Use this for initialization
    public GameObject BannerController;
    public int CoinsToGive;
    void OnEnable()
    {
        this.GetComponent<Text>().text = "+" +CoinsToGive+ "";

    }
    public void RewardWithCoins()
    {
        GameObject.FindGameObjectWithTag("SavingSystem").GetComponent<SavingSystem>().Coins += this.CoinsToGive;
        this.BannerController.GetComponent<BannerController>().coins += this.CoinsToGive;
    }
    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<Text>().text != "+" + CoinsToGive + "")
        {
            this.GetComponent<Text>().text = "+" + CoinsToGive + "";
        }
    }
}
