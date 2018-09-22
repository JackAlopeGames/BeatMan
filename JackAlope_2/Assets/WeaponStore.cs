using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponStore : MonoBehaviour {

    public GameObject Item1, Item2, Item3;
    public GameObject[] MyItems;
    public GameObject boxPressed;
    [Range(0,3)]
    public int ItemAmount;
	// Use this for initialization
	void Start () {
		MyItems = new GameObject[] { Item1, Item2, Item3 };
        for(int i=0; i < ItemAmount; i++)
        {
            MyItems[i].SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetWeaponType(Weapon w)
    {
        int t = 0;
        if (w.weaponName == "CyberSword")
        {
            t = 3;
        }
        if (w.weaponName == "Gun")
        {
            t = 1;
        }
        if (w.weaponName == "Knife")
        {
            t = 2;
        }
        if (ItemAmount < 3)
        {
            MyItems[ItemAmount].GetComponent<BoxItem>().ItemType = t - 1;
            MyItems[ItemAmount].GetComponent<BoxItem>().weapon = w;
            MyItems[ItemAmount].GetComponent<BoxItem>().isWeapon = true;
            this.ItemAmount++;
        }
        else
        {
            MyItems[ItemAmount-1].GetComponent<BoxItem>().ItemType = t - 1;
            MyItems[ItemAmount-1].GetComponent<BoxItem>().weapon = w;
            MyItems[ItemAmount - 1].GetComponent<BoxItem>().RefreshSprite();
            MyItems[ItemAmount-1].GetComponent<BoxItem>().isWeapon = true;
        }

        for (int i = 0; i < ItemAmount; i++)
        {
            MyItems[i].SetActive(true);
        }
    }

    public void SetItemType(string s)
    {
        int t = 0;
        if (s == "HealthPickUp")
        {
            t = 4;
        }
        if (ItemAmount < 3)
        {
            MyItems[ItemAmount].GetComponent<BoxItem>().ItemType = t - 1;
            MyItems[ItemAmount].GetComponent<BoxItem>().isWeapon = false;
            this.ItemAmount++;

        }
        else
        {
            MyItems[ItemAmount-1].GetComponent<BoxItem>().ItemType = t - 1;
            MyItems[ItemAmount - 1].GetComponent<BoxItem>().RefreshSprite();
            MyItems[ItemAmount-1].GetComponent<BoxItem>().isWeapon = false;
        }
        for (int i = 0; i < ItemAmount; i++)
        {
            MyItems[i].SetActive(true);
        }
    }

    public void WeaponUsed()
    {
        int startIndex = boxPressed.GetComponent<BoxItem>().boxIndex;
        while (startIndex < ItemAmount -1)
        {
            this.MyItems[startIndex].GetComponent<BoxItem>().weapon = this.MyItems[startIndex + 1].GetComponent<BoxItem>().weapon;
            this.MyItems[startIndex].GetComponent<BoxItem>().ItemType = this.MyItems[startIndex + 1].GetComponent<BoxItem>().ItemType;
            this.MyItems[startIndex].transform.GetChild(0).GetComponent<Image>().sprite = this.MyItems[startIndex + 1].transform.GetChild(0).GetComponent<Image>().sprite;
            startIndex++;
        }
        for (int i = 0; i < ItemAmount; i++)
        {
            MyItems[i].SetActive(false);
        }
        this.ItemAmount -= 1;
        for (int i = 0; i < ItemAmount; i++)
        {
            MyItems[i].SetActive(true);
        }

    }

}
