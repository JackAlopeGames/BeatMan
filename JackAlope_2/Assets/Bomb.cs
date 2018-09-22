using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            //BOOM
            GlobalAudioPlayer.PlaySFXAtPosition("Bomb",this.transform.position);
            this.GetComponent<MeshRenderer>().enabled = false;
            this.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
            this.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(disableBox());
        }
    }

    IEnumerator disableBox()
    {
        yield return new WaitForSeconds(.5f);
        this.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
    }
}
