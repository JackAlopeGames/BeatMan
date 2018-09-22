using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SavingSystem : MonoBehaviour {

	public static SavingSystem savingSystem;
    public int Points, Thunders, Stars, Coins,Level;
    public float Min, Sec, MinCoins, SecCoins, MinGift, SecGift;
    public GameObject SaveWarning, PlayerAutoUI, PlayerAutoLevel;
    public bool Tutorial0, AvailableCoins, AvailableGift, FiftyPass, DojoPass;

    public void FetchInMyGame() // this changes depending of your unity project
    {
        GameObject BC = GameObject.FindGameObjectWithTag("BannerController");
        this.Sec =BC.GetComponent<ThunderLoading>().Sec;
        this.Min = BC.GetComponent<ThunderLoading>().Min;
        this.Thunders = BC.GetComponent<ThunderLoading>().ThunderCount;
        this.Coins = BC.GetComponent<BannerController>().coins;
        this.Stars = BC.GetComponent<BannerController>().stars;
    }

	public void Awake (){ //preventing that the GameObject with the script doesnt destroy on load.
        this.Level = 1;

        Load();
		if (savingSystem == null) { 
			DontDestroyOnLoad (gameObject);
			savingSystem = this;
		} else if(savingSystem !=this){
			Destroy (gameObject);
		}

	}

	public void Save(){
        //creates a binary formatter and a file
       if( File.Exists(Application.persistentDataPath + "/ScoreSavig.dat")){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/ScoreSavig.dat");
            //creates a object to save the data to.
            SaveData data = new SaveData();
            data.Level += this.Level;
            data.Points += this.Points;
            data.Thunders += this.Thunders;
            data.Coins += this.Coins;
            data.Stars += Stars;
            data.Min += this.Min;
            data.Sec += this.Sec;
            data.Tutorial0 = this.Tutorial0;
            data.MinCoins += this.MinCoins;
            data.SecCoins += this.SecCoins;
            data.AvailableCoins = AvailableCoins;
            data.AvailableGift = AvailableGift;
            data.MinGift = this.MinGift;
            data.SecGift = this.SecGift;
            data.FiftyPass = FiftyPass;
            data.DojoPass = DojoPass;
            //writes the object to the file and close it
            bf.Serialize(file, data);
            file.Close();
        }else{
            try
            {
                if (SaveWarning != null || (this.PlayerAutoUI != null && PlayerAutoLevel != null))
                {
                    this.SaveWarning.transform.GetChild(0).gameObject.SetActive(true);
                    if (this.PlayerAutoUI != null || PlayerAutoLevel != null)
                    {
                        this.PlayerAutoUI.SetActive(false);
                        this.PlayerAutoLevel.SetActive(false);
                    }
                }
            }
            catch { }
        }

       
    }

    public void CreatFile()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/ScoreSavig.dat");
        //creates a object to save the data to.
        SaveData data = new SaveData();
        data.Level += this.Level;
        data.Points += this.Points;
        data.Thunders += this.Thunders;
        data.Coins += this.Coins;
        data.Stars += Stars;
        data.Min += this.Min;
        data.Sec += this.Sec;
        data.Tutorial0 = Tutorial0;
        data.SecCoins += this.SecCoins;
        data.MinCoins += this.MinCoins;
        data.AvailableCoins = AvailableCoins;
        data.AvailableGift = AvailableGift;
        data.MinGift = this.MinGift;
        data.SecGift = this.SecGift;
        data.FiftyPass = FiftyPass;
        data.DojoPass = DojoPass;
        //writes the object to the file and close it
        bf.Serialize(file, data);
        file.Close();
    }

	public void Load(){
		//creates a binary formatter file to open it, instead of writing it.
		if (File.Exists (Application.persistentDataPath + "/ScoreSavig.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/ScoreSavig.dat", FileMode.Open);
			SaveData data = (SaveData)bf.Deserialize (file); //translate it back as playing data
			file.Close ();
            this.Level = data.Level;
			this.Points = data.Points;
            this.Thunders = data.Thunders;
            this.Stars = data.Thunders;
            this.Min = data.Min;
            this.Sec = data.Sec;
            this.Coins = data.Coins;
            this.Tutorial0 = data.Tutorial0;
            this.SecCoins = data.SecCoins;
            this.MinCoins = data.MinCoins;
            this.MinGift = data.MinGift;
            this.SecGift = data.SecGift;
            this.FiftyPass = data.FiftyPass;
            this.DojoPass = data.DojoPass;
            AvailableCoins = data.AvailableCoins;
            AvailableGift = data.AvailableGift;

        }
	}

	public void Delete(){ //function to delete the saved data and restart everything.
		if (File.Exists (Application.persistentDataPath + "/ScoreSavig.dat")) {
			File.Delete(Application.persistentDataPath + "/ScoreSavig.dat");
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(SaveWarning == null)
        {
            SaveWarning = GameObject.FindGameObjectWithTag("SaveWarning");

        }
        if (SceneManager.GetSceneByName("MainMenu").isLoaded)
        {
            if(PlayerAutoLevel == null)
            {
                this.PlayerAutoLevel = GameObject.FindGameObjectWithTag("PlayerLevelUp");
            }
            if(PlayerAutoUI == null)
            {
                this.PlayerAutoUI = GameObject.FindGameObjectWithTag("PlayerAutoMenu");
            }
        }
	}


}
[Serializable]
class SaveData{
    public int Points, Thunders, Stars,Coins,Level;
    public float Min, Sec, MinCoins, SecCoins, MinGift, SecGift;
    public bool Tutorial0, AvailableCoins, AvailableGift, FiftyPass, DojoPass;
}