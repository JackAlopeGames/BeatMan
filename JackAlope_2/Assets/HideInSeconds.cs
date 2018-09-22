using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInSeconds : MonoBehaviour {

    // Use this for initialization
    public float HideSeconds;
	void OnEnable () {
        StartCoroutine(Hide());
	}

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(HideSeconds);
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
