using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCollidersEnemies : MonoBehaviour {

    // Use this for initialization
    public GameObject[] Colliders;
    public int currentWave;
	void Start () {
        for (int i = 0; i < Colliders.Length; i++)
        {
            Colliders[i].SetActive(false);
        }
        currentWave = this.gameObject.GetComponent<EnemyWaveSystem>().currentWave;
        Colliders[currentWave].SetActive(true);
    }

   
	
	// Update is called once per frame
	void Update () {

		if(currentWave != this.gameObject.GetComponent<EnemyWaveSystem>().currentWave)
        {
            for(int i=0; i < Colliders.Length; i++)
            {
                Colliders[i].SetActive(false);
            }
            currentWave = this.gameObject.GetComponent<EnemyWaveSystem>().currentWave;
            try
            {
                Colliders[currentWave].SetActive(true);
            }
            catch { }
        }
	}

    public void UpdateColliders()
    {
        for (int i = 0; i < Colliders.Length; i++)
        {
            Colliders[i].SetActive(false);
        }
        currentWave = this.gameObject.GetComponent<EnemyWaveSystem>().currentWave;
        try
        {
            Colliders[currentWave].SetActive(true);
        }
        catch { }
    }
}
