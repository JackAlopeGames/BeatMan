using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontMoveRobotTutorial : MonoBehaviour {

    // Use this for initialization
    public Vector3 origin;
    public GameObject spot2, spot3;
	void OnEnable () {
        //origin = this.transform.position;
        //StartCoroutine(holdThisPosition());
	}
	
	// Update is called once per frame
	void Update () {
        if (spot2 != null && spot3 != null)
        {
            if (spot2.activeInHierarchy)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(spot2.transform.position.x - 1.2f, this.transform.position.y, this.spot2.transform.position.z), Time.deltaTime * 10);
            }
            if (spot3.activeInHierarchy)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(spot3.transform.position.x, this.transform.position.y, this.spot3.transform.position.z), Time.deltaTime * 10);
            }
        }
	}

    IEnumerator holdThisPosition()
    {
        yield return new WaitForSeconds(1);
        this.transform.position = origin;
        Debug.Log("Moviendo");
    }
}
