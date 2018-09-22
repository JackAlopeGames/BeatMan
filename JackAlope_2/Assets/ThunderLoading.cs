using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThunderLoading : MonoBehaviour {

    public int ThunderCount;
    public float Min, Sec;
    public GameObject Minutes, Seconds, Thunders;
    // Use this for initialization
    private GameObject SavingSystem;
    void Start()
    {

        this.SavingSystem = GameObject.FindGameObjectWithTag("SavingSystem");
        this.ThunderCount = SavingSystem.GetComponent<SavingSystem>().Thunders;
        this.Min = SavingSystem.GetComponent<SavingSystem>().Min;
        this.Sec = SavingSystem.GetComponent<SavingSystem>().Sec;
    }
	
	// Update is called once per frame
	void Update () {
        if (ThunderCount < 20)
        {
            Sec -= Time.deltaTime;
            this.SavingSystem.GetComponent<SavingSystem>().Sec = this.Sec;
            try
            {
                Seconds.GetComponent<Text>().text = ":" + (int)Sec;
                if(Sec < 10)
                {
                    Seconds.GetComponent<Text>().text = ":0" + (int)Sec;
                }
            }
            catch { }
            if (Sec <0)
            {
                this.Min--;
                this.SavingSystem.GetComponent<SavingSystem>().Min = this.Min;
                Sec = 59;
                this.SavingSystem.GetComponent<SavingSystem>().Sec = this.Sec;
                try
                {
                    Minutes.GetComponent<Text>().text = "0" + Min;
                }
                catch { }
            }

            if (Min < 00)
            {
                this.ThunderCount += 2;
                SavingSystem.GetComponent<SavingSystem>().Save();
                this.SavingSystem.GetComponent<SavingSystem>().Thunders = this.ThunderCount;
                try
                {
                    this.Thunders.GetComponent<Text>().text = ThunderCount + "/20";
                    Min = 9;
                    this.SavingSystem.GetComponent<SavingSystem>().Min = this.Min;
                    Minutes.GetComponent<Text>().text = "0" + this.Min;
                }
                catch { }
            }
        }
	}
}
