using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour {

    // Use this for initialization
    public int Hits;
    public float ComboTime;
    public GameObject Coach, Tutorial;

    void OnEnable()
    {
        this.Hits = 0;
        this.GetComponent<Text>().enabled = false;
        if (SceneManager.GetSceneByName("Level_02").isLoaded )
        {
            this.transform.position = new Vector3(this.transform.localPosition.x + 10, this.transform.localPosition.y - 300, this.transform.localPosition.z);
        }
    }
	// Update is called once per frame
	void Update () {

        if (SceneManager.GetSceneByName("Level_02").isLoaded)
        {
            if (this.GetComponent<Text>().text != Tutorial.GetComponent<LevelEnterTutorialDojo>().tapCount + "/ 5 Hits" && this.GetComponent<Text>().text != Tutorial.GetComponent<LevelEnterTutorialDojo>().tapCount + "/ 3 Hits")
            {
                try
                {
                    if (Coach.GetComponent<Couch>().punchingTest || Coach.GetComponent<Couch>().GrabHitTestFirstPart)
                    {
                        if (Coach.GetComponent<Couch>().punchingTest)
                        {
                            this.GetComponent<Text>().text = Tutorial.GetComponent<LevelEnterTutorialDojo>().tapCount + "/ 5 Hits";
                        }
                        else if(Coach.GetComponent<Couch>().GrabHitTestFirstPart)
                        {
                            this.GetComponent<Text>().text = Tutorial.GetComponent<LevelEnterTutorialDojo>().tapCount + "/ 3 Hits";
                        }  
                        this.GetComponent<Text>().enabled = true;
                        this.GetComponent<Animator>().SetTrigger("ComboText");
                    }
                    else
                    {
                        if (this.GetComponent<Text>().text != Hits + " COMBO")
                        {
                            this.GetComponent<Text>().text = Hits + " COMBO";
                            this.GetComponent<Text>().enabled = true;
                            this.GetComponent<Animator>().SetTrigger("ComboText");
                        }
                    }
                }
                catch { }
            }
           
        }
        else
        {
            if (this.GetComponent<Text>().text != Hits + " COMBO")
            {
                this.GetComponent<Text>().text = Hits + " COMBO";
                this.GetComponent<Text>().enabled = true;
                this.GetComponent<Animator>().SetTrigger("ComboText");
            }
        }
            
        if (ComboTime < 2)
        {
            this.ComboTime += Time.deltaTime;
            this.GetComponent<Text>().enabled = true;
        }
        if (ComboTime > 2)
        {
            this.Hits = 0;
            this.GetComponent<Text>().enabled = false;
        }
	}
}
