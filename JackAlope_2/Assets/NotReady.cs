using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotReady : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        StartCoroutine(deactiveNotify());
	}

    IEnumerator deactiveNotify()
    {
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
