using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskAdButton : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        StartCoroutine(HideAdMask());
	}

    IEnumerator HideAdMask()
    {
        yield return new WaitForSeconds(3.5f);
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
