using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelection : MonoBehaviour {

    public GameObject LevelIntroWindow;
    public Sprite ItemSprite;
    public int ItemBox;

	// Use this for initialization
	void OnEnable () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void ItemSelected()
    {
        LevelIntroWindow.GetComponent<LevelIntroduction>().changeItem(ItemBox, ItemSprite);
        this.gameObject.SetActive(false);
    }
    
    public void ItemBoxIndex(int index)
    {
        ItemBox = index;
        this.gameObject.SetActive(true);
    }
    
}
