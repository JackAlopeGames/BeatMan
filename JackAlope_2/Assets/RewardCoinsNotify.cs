using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardCoinsNotify : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        StartCoroutine(deactivateBrothers());
	}

    IEnumerator deactivateBrothers()
    {
        yield return new WaitForSeconds(2);
        GameObject parent = this.transform.parent.gameObject;
        for (int i = 0; i < 3; i++)
        {
            parent.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
