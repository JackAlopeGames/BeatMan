using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfDojoPassEnemies : MonoBehaviour {

    // Use this for initialization
    private GameObject savingSystem;
	void onEnable () {
        savingSystem = GameObject.FindGameObjectWithTag("SavingSystem");

        if (!savingSystem.GetComponent<SavingSystem>().DojoPass)
        {
            this.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
