using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountDownGO : MonoBehaviour {
    public Text Counter;
    public GameObject WarningWindow;
    public GameObject FinalGameOverText, retry,home,coins;
    // Use this for initialization
    void Start () {
	}
	public IEnumerator CountDown()
    {
        yield return new WaitForSeconds(2);
        this.Counter.gameObject.SetActive(true);
        this.Counter.text = "15";
        yield return null;
        yield return new WaitForSeconds(1);
        for (int i = 0; i < 14; i++)
        {
            yield return new WaitForSeconds(1);
            this.Counter.text = int.Parse(this.Counter.text) - 1 + "";
        }
        yield return new WaitForSeconds(1);
        this.Counter.text = "GAME OVER";
        yield return new WaitForSeconds(1);
        this.Counter.text = "";
        this.WarningWindow.SetActive(false);
        this.FinalGameOverText.SetActive(true);
        this.retry.SetActive(true);
        this.home.SetActive(true);
        this.coins.SetActive(true);
        /*this.Counter.gameObject.SetActive(false);
        Destroy(GameObject.FindGameObjectWithTag("UI"));
        Destroy(GameObject.FindGameObjectWithTag("AdWeapon"));
        Destroy(GameObject.FindGameObjectWithTag("ExtraCheker"));
        SceneManager.LoadScene("MainMenu");*/

    }

    public void CountDownManual()
    {
        StartCoroutine(CountDown());
    }
	// Update is called once per frame
	void Update () {
		
	}
}
