using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoom : MonoBehaviour {

    // Use this for initialization
    public GameObject player;
	void Start () {
        this.player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

    IEnumerator knockDown(GameObject collision)
    {
        collision.transform.GetChild(0).GetComponent<Animator>().SetTrigger("KnockDown_Up");
        yield return new WaitForSeconds(.3f);
        collision.transform.GetChild(0).GetComponent<Animator>().SetTrigger("KnockDown_Down");
        yield return new WaitForSeconds(.3f);
        collision.transform.GetChild(0).GetComponent<Animator>().SetTrigger("KnockDown_End");
        yield return new WaitForSeconds(.3f);
        collision.transform.GetChild(0).GetComponent<Animator>().SetTrigger("StandUp");
        yield return new WaitForSeconds(.1f);
        collision.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Idle");
        collision.gameObject.GetComponent<UnitState>().currentState = UNITSTATE.IDLE;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerAuto" || other.gameObject.tag == "Player" || other.gameObject == player)
        {
            //BOOM HIT
            GlobalAudioPlayer.PlaySFX("PlayerDeath");
            other.gameObject.GetComponent<UnitState>().currentState = UNITSTATE.KNOCKDOWN;
            other.gameObject.GetComponent<HealthSystem>().SubstractHealth(4);
            StartCoroutine(knockDown(other.gameObject));
        }
    }

 
}
