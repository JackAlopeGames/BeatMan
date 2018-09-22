using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetLevel : MonoBehaviour {

    // Use this for initialization
    private GameObject SavingSystem;
	void OnEnable () {
        this.SavingSystem = GameObject.FindGameObjectWithTag("SavingSystem");
        if (SceneManager.GetSceneByName("MainMenu").isLoaded)
        {
            this.gameObject.GetComponent<UnityEngine.UI.Text>().text = "Level: " + SavingSystem.GetComponent<SavingSystem>().Level + "";
        }
        else
        {
            this.gameObject.GetComponent<UnityEngine.UI.Text>().text = SavingSystem.GetComponent<SavingSystem>().Level + "";
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetSceneByName("MainMenu").isLoaded && this.gameObject.GetComponent<UnityEngine.UI.Text>().text != "Level: " + SavingSystem.GetComponent<SavingSystem>().Level + "")
        {
            this.gameObject.GetComponent<UnityEngine.UI.Text>().text = "Level: " + SavingSystem.GetComponent<SavingSystem>().Level + "";
        }
    }
}
