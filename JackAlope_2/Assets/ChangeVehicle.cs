using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVehicle : MonoBehaviour {

    // Use this for initialization

    public GameObject[] Vehicles;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void pickARandomVehicle()
    {
        int r = Random.Range(0, Vehicles.Length);
        for(int i=0; i < Vehicles.Length; i++)
        {
            this.Vehicles[i].SetActive(false);
        }
        this.Vehicles[r].SetActive(true);
    }
}
