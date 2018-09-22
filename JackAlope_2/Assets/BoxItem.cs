using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class BoxItem : MonoBehaviour {

    [Header("Weapon Settings")]
    public Weapon weapon;
    public int boxIndex;
    // Use this for initialization
    [Range(0,4)]
    public int ItemType;
    public Sprite[] WeaponSprite = new Sprite[4];

    public bool isWeapon;

	void OnEnable () {
        this.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = WeaponSprite[ItemType];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UseWeapon()
    {
        StartCoroutine(WaitToMissTap());
       
    }
    
    public void RefreshSprite()
    {
        this.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = WeaponSprite[ItemType];
    }
    IEnumerator WaitToMissTap()
    {
        yield return new WaitForSeconds(0.1f);
        this.transform.parent.GetComponent<WeaponStore>().boxPressed = this.gameObject;
        GameObject pc = GameObject.FindGameObjectWithTag("Player");

        if (isWeapon)
        {
            if (pc.GetComponent<PlayerCombat>().weaponBone.childCount == 4)
            {
                pc.GetComponent<PlayerCombat>().equipWeapon(weapon);
                this.transform.parent.GetComponent<WeaponStore>().WeaponUsed();
            }
        }
        else
        {
            HealthSystem hs = pc.GetComponent<HealthSystem>();

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

                    hs.AddHealth(5);
                    Analytics.CustomEvent("HealthKit_Used");
                }

            }
            else
            {
                Debug.Log("no health system found on GameObject '" + pc.gameObject.name + "'.");
            }
            this.transform.parent.GetComponent<WeaponStore>().WeaponUsed();
        }
    }
  
}
