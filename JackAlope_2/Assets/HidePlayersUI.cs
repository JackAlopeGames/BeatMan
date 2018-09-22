using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePlayersUI : MonoBehaviour {

    // Use this for initialization
    public GameObject player1, player2, SaveAnim;
	void OnEnable () {
        player1.SetActive(false);
        player2.SetActive(false);
	}

    private void OnDisable()
    {
        player1.SetActive(true);
        player2.SetActive(true);
        SaveAnim.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
