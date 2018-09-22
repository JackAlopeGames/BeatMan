using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetCoinsCurrent : MonoBehaviour {

    // Use this for initialization
    public GameObject BannerController;
    public int CoinsToGive;     
	void OnEnable () {
        this.GetComponent<Text>().text = "+" +BannerController.GetComponent<BannerController>().coinsCurrent + "";
        this.CoinsToGive = BannerController.GetComponent<BannerController>().coinsCurrent;
        BannerController.GetComponent<BannerController>().coinsCurrent = 0;

    }
	public void RewardWithCoins()
    {
        GameObject ss = GameObject.FindGameObjectWithTag("SavingSystem");
        ss.GetComponent<SavingSystem>().Coins += this.CoinsToGive;
        ss.GetComponent<SavingSystem>().Save();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
