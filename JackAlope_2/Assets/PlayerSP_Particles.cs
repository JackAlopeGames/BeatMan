using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSP_Particles : MonoBehaviour {

    // Use this for initialization

    public ParticleSystem ps;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayParticle()
    {
        ps.Play();
        this.transform.parent.GetComponent<EnemyAI>().enableAI = false;
    }
}
