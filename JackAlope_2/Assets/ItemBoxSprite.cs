using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoxSprite : MonoBehaviour {

    // Use this for initialization
    public Sprite childSprite;
    public GameObject itemSelectionScreen;
    void Start()
    {

    }
	
	// Update is called once per frame
	void Update () {
        if (childSprite == null || this.childSprite != this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite)
        {
            this.childSprite = this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite;
        }
	}
    public void thisSprite()
    {
        itemSelectionScreen.GetComponent<ItemSelection>().ItemSprite = childSprite;
    }
}
