using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxAttackersPerWave : MonoBehaviour {

    // Use this for initialization
    int currentWave;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(this.gameObject.GetComponent<EnemyWaveSystem>().currentWave != currentWave)
        {
            currentWave = this.gameObject.GetComponent<EnemyWaveSystem>().currentWave;
        }
        if((currentWave == 5 || currentWave == 2) && this.gameObject.GetComponent<EnemyWaveSystem>().MaxAttackers != 3)
        {
            this.gameObject.GetComponent<EnemyWaveSystem>().MaxAttackers = 3;
        }else if(this.gameObject.GetComponent<EnemyWaveSystem>().MaxAttackers != 2)
        {
            this.gameObject.GetComponent<EnemyWaveSystem>().MaxAttackers = 2;
        }
	}
}
