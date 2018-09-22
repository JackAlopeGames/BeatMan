using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdForCoinsMenu : MonoBehaviour {

    // Use this for initialization

    public GameObject coins, mask;
    public float Min, Sec;
    public GameObject SavingSystem, Seconds, Minutes, Bubble;

    void  OnEnable () {

        this.SavingSystem = GameObject.FindGameObjectWithTag("SavingSystem");
        this.SavingSystem.GetComponent<SavingSystem>().Load();
        this.Min = this.SavingSystem.GetComponent<SavingSystem>().MinCoins;
        this.Sec = this.SavingSystem.GetComponent<SavingSystem>().SecCoins;

    }
	
	// Update is called once per frame
	void Update () {
        if(this.SavingSystem == null)
        {
            this.SavingSystem = GameObject.FindGameObjectWithTag("SavingSystem");
        }
        if (SceneManager.GetSceneByName("MainMenu").isLoaded)
        {
            if (this.SavingSystem.GetComponent<SavingSystem>().FiftyPass && coins.GetComponent<GetCoinsMenu>().CoinsToGive != 75)
            {
                coins.GetComponent<GetCoinsMenu>().CoinsToGive = 75;
            }
        }
        if (Min >= 10)
        {
            this.Min = 9;
        }
        if(Min==0 && Sec == 0)
        {
            this.SavingSystem.GetComponent<SavingSystem>().AvailableCoins = true;
        }
        
        if (!this.SavingSystem.GetComponent<SavingSystem>().AvailableCoins)
        {
            Sec -= Time.deltaTime;
            this.SavingSystem.GetComponent<SavingSystem>().SecCoins = this.Sec;
            try
            {
                Seconds.GetComponent<Text>().text = ":" + (int)Sec;
                if (Sec < 10)
                {
                    Seconds.GetComponent<Text>().text = ":0" + (int)Sec;
                }
            }
            catch { }
            if (Sec < 0)
            {
                this.Min--;
                this.SavingSystem.GetComponent<SavingSystem>().MinCoins = this.Min;
                Sec = 59;
                this.SavingSystem.GetComponent<SavingSystem>().SecCoins = this.Sec;
                try
                {
                    Minutes.GetComponent<Text>().text = "0" + Min;
                }
                catch { }
            }

            if (Min < 00)
            {
               
                try
                {
                    this.Minutes.SetActive(false);
                    this.Seconds.SetActive(false);
                    Min = 9;
                    this.SavingSystem.GetComponent<SavingSystem>().MinCoins = this.Min;
                    Minutes.GetComponent<Text>().text = "0" + this.Min;
                }
                catch { }
            }

            try
            {
                if (Minutes.GetComponent<Text>().text != "0" + this.Min)
                {
                    Minutes.GetComponent<Text>().text = "0" + this.Min;
                }
            }
            catch { }

            try
            {
                if (!mask.active)
                {
                    mask.SetActive(true);
                    coins.transform.parent.gameObject.SetActive(false);
                    coins.SetActive(false);
                    this.Bubble.SetActive(true);
                    this.Minutes.SetActive(true);
                    this.Seconds.SetActive(true);
                }

                if (Min <= 0 && Sec <= 1)
                {
                    mask.SetActive(false);
                    coins.transform.parent.gameObject.SetActive(true);
                    coins.SetActive(true);
                    this.Bubble.SetActive(false);
                    this.Minutes.SetActive(false);
                    this.Seconds.SetActive(false);
                    this.SavingSystem.GetComponent<SavingSystem>().AvailableCoins = true;
                    coins.GetComponent<GetCoinsMenu>().CoinsToGive = 50;
                    this.SavingSystem.GetComponent<SavingSystem>().Save();
                }
            }
            catch { }
        }
    }

    public void WantCoins()
    {
        if (coins.GetComponent<GetCoinsMenu>().CoinsToGive == 75)
        {
            try
            {
            mask.SetActive(true);
            coins.transform.parent.gameObject.SetActive(false);
            coins.SetActive(false);
            this.SavingSystem.GetComponent<SavingSystem>().AvailableCoins = false;
            coins.GetComponent<GetCoinsMenu>().CoinsToGive = 50;
            this.Min = 9;
            this.SavingSystem.GetComponent<SavingSystem>().MinCoins = this.Min;
            this.Sec = 59;
            this.SavingSystem.GetComponent<SavingSystem>().SecCoins = this.Sec;

            this.Bubble.SetActive(true);
            this.Minutes.SetActive(true);
             this.Seconds.SetActive(true);

            this.SavingSystem.GetComponent<SavingSystem>().FiftyPass = false;
            }
            catch { }
           
        }
        else
        {
            this.SavingSystem.GetComponent<SavingSystem>().FiftyPass = true;
            coins.GetComponent<GetCoinsMenu>().CoinsToGive = 75;
          
        }
        this.SavingSystem.GetComponent<SavingSystem>().Save();
    }
}
