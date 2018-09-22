using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
                this.SavingSystem.GetComponent<SavingSystem>().MinGift = this.Min;
                Sec = 59;
                this.SavingSystem.GetComponent<SavingSystem>().SecGift = this.Sec;
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
                    Min = 29;
                    this.SavingSystem.GetComponent<SavingSystem>().MinGift = this.Min;
                  
                }
                catch { }
            }

            try
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
                    this.SavingSystem.GetComponent<SavingSystem>().AvailableGift = true;
                    this.SavingSystem.GetComponent<SavingSystem>().Save();
                }
                /*
                if (Min >=10  && Min <= 19 && this.Minutes.transform.localPosition.x != 106)
                {
                    this.Minutes.transform.localPosition = new Vector3(106, this.Minutes.transform.localPosition.y, this.Minutes.transform.localPosition.z);
                }
                else if((Min<10 || Min>=20) && this.Minutes.transform.localPosition.x != 35)
                {
                    this.Minutes.transform.localPosition = new Vector3(35, this.Minutes.transform.localPosition.y, this.Minutes.transform.localPosition.z);
                }
                */
            }
            catch { }
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
