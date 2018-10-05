using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GiftForCoins : MonoBehaviour {

    // Use this for initialization
    public GameObject coins, mask;
    public float Min, Sec;
    public GameObject SavingSystem, Seconds, Minutes, Bubble;

    void OnEnable()
    {

        this.SavingSystem = GameObject.FindGameObjectWithTag("SavingSystem");
        this.SavingSystem.GetComponent<SavingSystem>().Load();
        this.Min = this.SavingSystem.GetComponent<SavingSystem>().MinGift;
        this.Sec = this.SavingSystem.GetComponent<SavingSystem>().SecGift;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Min == 0 && Sec == 0)
        {
            this.SavingSystem.GetComponent<SavingSystem>().AvailableGift = true;
        }
        */
        if (!this.SavingSystem.GetComponent<SavingSystem>().AvailableGift)
        {
            Sec -= Time.deltaTime;
            this.SavingSystem.GetComponent<SavingSystem>().SecGift = this.Sec;
            if (SceneManager.GetSceneByName("MainMenu").isLoaded)
            {
                Seconds.GetComponent<Text>().text = ":" + (int)Sec;
                if (Sec < 10)
                {
                    Seconds.GetComponent<Text>().text = ":0" + (int)Sec;
                }
            }
            if (Sec < 0)
            {
                this.Min--;
                this.SavingSystem.GetComponent<SavingSystem>().MinGift = this.Min;
                Sec = 59;
                this.SavingSystem.GetComponent<SavingSystem>().SecGift = this.Sec;
                if (SceneManager.GetSceneByName("MainMenu").isLoaded)
                {
                    Minutes.GetComponent<Text>().text = "0" + Min;
                }
            }

            if (Min < 00)
            {

                Min = 29;
                this.SavingSystem.GetComponent<SavingSystem>().MinGift = this.Min;
                if (SceneManager.GetSceneByName("MainMenu").isLoaded)
                {
                    this.Minutes.SetActive(false);
                    this.Seconds.SetActive(false);
                }
            }

            if (SceneManager.GetSceneByName("MainMenu").isLoaded)
            {

                if (this.Min < 10)
                {
                    if (Minutes.GetComponent<Text>().text != "0" + this.Min)
                    {
                        Minutes.GetComponent<Text>().text = "0" + this.Min;
                    }
                }
                else
                {
                    Minutes.GetComponent<Text>().text = this.Min + "";
                }

                if (!mask.activeInHierarchy)
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
                    this.SavingSystem.GetComponent<SavingSystem>().AvailableGift = true;
                    this.SavingSystem.GetComponent<SavingSystem>().Save();
                }
            }

            if (Min <= 0 && Sec <= 1)
            {
                this.SavingSystem.GetComponent<SavingSystem>().AvailableGift = true;
                this.SavingSystem.GetComponent<SavingSystem>().Save();
            }

        }
    }

    public void WantCoins()
    {
        try
        {
            mask.SetActive(true);
            coins.transform.parent.gameObject.SetActive(false);
            coins.SetActive(false);
            this.SavingSystem.GetComponent<SavingSystem>().AvailableGift = false;
            this.Min = 29;
            this.SavingSystem.GetComponent<SavingSystem>().MinGift = this.Min;
            this.Sec = 59;
            this.SavingSystem.GetComponent<SavingSystem>().SecGift = this.Sec;

            this.Bubble.SetActive(true);
            this.Minutes.SetActive(true);
            this.Seconds.SetActive(true);
        }
        catch { }

        this.SavingSystem.GetComponent<SavingSystem>().Save();
    }
}
