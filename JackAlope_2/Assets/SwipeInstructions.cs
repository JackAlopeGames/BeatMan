using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwipeInstructions : MonoBehaviour {

    
    public GameObject Text;
    public GameObject[] Instructions = new GameObject[7];
    // Use this for initialization

    void OnEnable() {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        ShowAnimation(-1);
        this.Text.GetComponent<Text>().text = "";
        yield return new WaitForSeconds(2);
        ShowAnimation(0);
        this.Text.GetComponent<Text>().text = "Drag and move your finger on the screen to walk and run";
        yield return new WaitForSeconds(8);
        StartCoroutine(Tap());
    }

    IEnumerator Tap()
    {
        ShowAnimation(1);
        this.Text.GetComponent<Text>().text = "Tap the screen to punch or kick enemies";
        yield return new WaitForSeconds(8);
        StartCoroutine(Grab());
    }

    IEnumerator Grab()
    {
        ShowAnimation(-1);
        this.Text.GetComponent<Text>().text = "You can grab enemies if you aproach to them running";
        yield return new WaitForSeconds(5);
        ShowAnimation(1);
        this.Text.GetComponent<Text>().text = "When you are holding it, tap the screen to hit it";
        yield return new WaitForSeconds(5);
        StartCoroutine(SwipeUp());
    }

    IEnumerator SwipeUp()
    {
        ShowAnimation(2);
        this.Text.GetComponent<Text>().text = "Swipe your finger up on the screen to jump"; 
        yield return new WaitForSeconds(5);
        GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<GainEnergy>().GainEnergyPunch(0.3f);
        this.Text.GetComponent<Text>().text = "If you have enough energy points, you will do an special jump kick";
        yield return new WaitForSeconds(5);
        StartCoroutine(SwipeDown());
    } 

    IEnumerator SwipeDown()
    {
        ShowAnimation(3);
        GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<GainEnergy>().GainEnergyPunch(0.3f);
        this.Text.GetComponent<Text>().text = "Swipe your finger down on the screen to make an especial kick using energy points";
        yield return new WaitForSeconds(5);
        StartCoroutine(Hold());
    }

    IEnumerator Hold()
    {
        ShowAnimation(4);
        this.Text.GetComponent<Text>().text = "Hold your finger on the screen to charge an uppercut";
        yield return new WaitForSeconds(5);
        this.Text.GetComponent<Text>().text = "If you dont have enough energy points, you will do less damage";
        yield return new WaitForSeconds(5);
        GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<GainEnergy>().GainEnergyPunch(0.3f);
        this.Text.GetComponent<Text>().text = "Having enough energy make more damage to enemies";
        yield return new WaitForSeconds(5);
        StartCoroutine(SwipeLeftOrRight());
    }

    IEnumerator SwipeLeftOrRight()
    {
        GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<GainEnergy>().GainEnergyPunch(.3f);
        ShowAnimation(5);
        this.Text.GetComponent<Text>().text = "Swipe your finger right or left on the screen to do a running-punch attack";
        yield return new WaitForSeconds(2.5f);
        ShowAnimation(6);
        yield return new WaitForSeconds(2.5f);
        ShowAnimation(1);
        this.Text.GetComponent<Text>().text = "When you see yourself running on frenzy, tap the screen to punch the enemy on the right moment";
        yield return new WaitForSeconds(5);
        ShowAnimation(-1);
        this.Text.GetComponent<Text>().text = "";

      
        if (SceneManager.GetSceneByName("Dojo").isLoaded)
        {
            Destroy(GameObject.FindGameObjectWithTag("UI"));
            SceneManager.LoadScene("Level_01");
        }
    }

    public void ShowAnimation(int x)
    {
        for(int i = 0; i < Instructions.Length; i++)
        {
            this.Instructions[i].SetActive(false);
        }
        if (x >= 0)
        {
            this.Instructions[x].SetActive(true);
        }
    }
   
    // Update is called once per frame
    void Update () {
        
	}
}
