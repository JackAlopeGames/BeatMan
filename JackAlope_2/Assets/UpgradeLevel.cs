using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class UpgradeLevel : MonoBehaviour {

    // Use this for initialization
    private GameObject SavingSystem, BC;
    public GameObject LevelSystem, NotEnoughCoins;
	void OnEnable () {
        this.SavingSystem = GameObject.FindGameObjectWithTag("SavingSystem");
        this.BC = GameObject.FindGameObjectWithTag("BannerController");
    }
	

    public void Upgrade()
    {
        if (SavingSystem.GetComponent<SavingSystem>().Coins >= 275)
        {
            SavingSystem.GetComponent<SavingSystem>().Coins -= 275;
            this.BC.GetComponent<BannerController>().coins -= 275;
            SavingSystem.GetComponent<SavingSystem>().Level += 1;
            this.gameObject.GetComponent<Save>().SaveNow();
            LevelSystem.GetComponent<LevelSystem>().ShowCongratLevelUp();
            Analytics.CustomEvent("Player_HasLevelUp");
        }
        else
        {
            StartCoroutine(ShowWarn());
            Debug.Log("Not enough coins");
        }
    }

    IEnumerator ShowWarn()
    {
        this.NotEnoughCoins.SetActive(true);
        yield return new WaitForSeconds(2);
        this.NotEnoughCoins.SetActive(false);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
