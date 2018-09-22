using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour {

    // Use this for initialization
    public float HitTime;
    public float ComboPower = 1;
    public float comboTimer = 1;

	void Start () {
        this.ComboPower = 1;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (HitTime <= 0)
        {
            ComboPower = 1;
            comboTimer = 0;

        }
        else
        {
            comboTimer += Time.deltaTime;
            ComboPower = 1 * comboTimer;
           
            HitTime -= Time.deltaTime;
        }
	}
}
