using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEnemies : MonoBehaviour {

    // Use this for initialization
    public GameObject Enemies, Pivot, Tutorial0, TutorialSpecialAttack, Player;
	void Start () {
        this.Enemies.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Pivot.transform.position.x > this.Player.transform.position.x && !this.Enemies.activeInHierarchy &!Tutorial0.activeInHierarchy && !TutorialSpecialAttack.activeInHierarchy)
        {
            this.Enemies.SetActive(true);
            this.gameObject.SetActive(false);
        }
	}
}
