using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEnemies : MonoBehaviour {

    // Use this for initialization
    public GameObject Enemies, Tutorial0, TutorialSpecialAttack, Player;
    Vector3 Pivot;
	void Start () {
        this.Enemies.SetActive(false);
        this.Pivot = new Vector3(this.Player.transform.position.x - 3, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if(Pivot.x > this.Player.transform.position.x && !this.Enemies.activeInHierarchy &!Tutorial0.activeInHierarchy && !TutorialSpecialAttack.activeInHierarchy)
        {
            this.Enemies.SetActive(true);
            this.gameObject.SetActive(false);
        }
	}
}
