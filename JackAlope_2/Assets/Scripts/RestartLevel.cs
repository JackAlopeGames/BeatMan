using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RestartLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RestartThisLevel()
    {
        Destroy(GameObject.FindGameObjectWithTag("UI"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartInExchangeOfLightings()
    {
        GameObject BC = GameObject.FindGameObjectWithTag("BannerController");
        GameObject SS = GameObject.FindGameObjectWithTag("SavingSystem");
        if (BC.GetComponent<ThunderLoading>().ThunderCount >= 2)
        {
            BC.GetComponent<ThunderLoading>().ThunderCount -= 2;
            SS.GetComponent<SavingSystem>().Thunders -= 2;
            RestartThisLevel();
        }
        else
        {
            Debug.Log("You need lightings");
        }
    }
}
