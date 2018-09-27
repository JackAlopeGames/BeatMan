using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Save : MonoBehaviour {

    // Use this for initialization

    public GameObject saveAnimation;
    private GameObject SavingSystem;

	void OnEnable () {
       // SaveNow();
	}

    public void SaveNow()
    {
        GameObject BC = GameObject.FindGameObjectWithTag("BannerController");
        if (BC.GetComponent<ThunderLoading>().ThunderCount != 18 && BC.GetComponent<ThunderLoading>().ThunderCount != 20)
        {
            BC.GetComponent<ThunderLoading>().ThunderCount += 18;
        }
        this.SavingSystem = GameObject.FindGameObjectWithTag("SavingSystem");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/ScoreSavig.dat");
        SaveData data = new SaveData();
        bf.Serialize(file, data);
        file.Close();
        this.SavingSystem.GetComponent<SavingSystem>().Thunders += 18;

        this.SavingSystem.GetComponent<SavingSystem>().Save();
        StartCoroutine(ShowAnim());
    }

    IEnumerator ShowAnim()
    {
        try
        {
            this.saveAnimation.SetActive(true);
        }
        catch { }
        yield return new WaitForSeconds(3);
        try
        {
            this.saveAnimation.SetActive(false);
        }
        catch { }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
