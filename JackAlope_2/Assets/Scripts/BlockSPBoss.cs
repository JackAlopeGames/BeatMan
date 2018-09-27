using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSPBoss : MonoBehaviour {

    public GameObject Instrunction;
    public GameObject CounterRespawn;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void CheckForWave()
    {
        /*
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
      
        
        if (enemies.Length <= 0)
        {
            //block this
            StartCoroutine(showInstruction());
        }
        for(int i =0; i < enemies.Length; i++)
        {
            if(enemies[i].GetComponent<HealthSystem>().CurrentHp > 0)
            {
                EveryoneIsDead = false;
            }else
            {
                cont++;
                EveryoneIsDead = true;
            }
        }
        if (cont == enemies.Length || this.gameObject.GetComponent<GainEnergy>().EnemyToAttack.GetComponent<HealthSystem>().CurrentHp<=0)
        {
            EveryoneIsDead = true;
        }
        else
        {
            EveryoneIsDead = false;
        }
        if (EveryoneIsDead)
        {
            StartCoroutine(showInstruction());
            EveryoneIsDead = false;
        }*/

        if(this.GetComponent<GainEnergy>().EnemyToAttack == this.GetComponent<GainEnergy>().player || CounterRespawn.activeInHierarchy)
        {
            ShowInstru();
        }
    }

    public void ShowInstru()
    {
        StartCoroutine(showInstruction());
    }

    IEnumerator showInstruction()
    {
        this.Instrunction.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        this.Instrunction.SetActive(false);
    }
}
