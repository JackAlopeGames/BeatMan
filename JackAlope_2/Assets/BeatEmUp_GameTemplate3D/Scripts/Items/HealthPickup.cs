﻿using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class HealthPickup : MonoBehaviour {

	public int RestoreHP = 1;
	public string pickupSFX = "";
	public GameObject pickupEffect;
	public float pickUpRange = 1;
	private GameObject[] Players;
    private GameObject WS;
    bool JustLooted;
    float timeJustLoot;
	void Start(){
		Players = GameObject.FindGameObjectsWithTag("Player");
        WS = GameObject.FindGameObjectWithTag("WeaponStore");
        JustLooted = true;
       
    }

	void LateUpdate(){
        if (JustLooted)
        {
            timeJustLoot += Time.deltaTime;
        }
        if (timeJustLoot > 1)
        {
            JustLooted = false;
            
            foreach (GameObject player in Players)
            {
                if (player)
                {
                    float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

                    //player is in pickup range
                    if (distanceToPlayer < pickUpRange)
                        AddHealthToPlayer(player);
                }
            }
        }
	}

	//add health to player
	void AddHealthToPlayer(GameObject player){


            HealthSystem hs = player.GetComponent<HealthSystem>();
        
            if (hs != null)
            {

            //restore hp to unit
                if (hs.ExtraHp > 0)
                {
                    hs.ExtraHp += 6;
                    hs.SendUpdateEvent();
            }
            else
                {

                    hs.AddHealth(RestoreHP);
                    Analytics.CustomEvent("HealthKit_PickedUp");
                }

            }
            else
            {
                Debug.Log("no health system found on GameObject '" + player.gameObject.name + "'.");
            }

            //WS.GetComponent<WeaponStore>().SetItemType("HealthPickUp"); //STORE IT

            //show pickup effect
            if (pickupEffect != null)
            {
                GameObject effect = GameObject.Instantiate(pickupEffect);
                effect.transform.position = transform.position;
            }

            //play sfx
            if (pickupSFX != "")
            {
                GlobalAudioPlayer.PlaySFXAtPosition(pickupSFX, transform.position);
            }

            Destroy(gameObject);
        
	}

	#if UNITY_EDITOR 
	void OnDrawGizmos(){

		//Show pickup range
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (transform.position, pickUpRange); 

	}
	#endif
}
