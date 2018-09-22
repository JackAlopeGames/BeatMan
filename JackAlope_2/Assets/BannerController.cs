using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerController : MonoBehaviour {

    // Use this for initialization
    public int coins, stars, coinsCurrent;
    private GameObject SavingSystem;
	void Start () {
        this.SavingSystem = GameObject.FindGameObjectWithTag("SavingSystem");
        this.coins = SavingSystem.GetComponent<SavingSystem>().Coins;
        this.stars = SavingSystem.GetComponent<SavingSystem>().Stars;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
