using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVsPlayer : MonoBehaviour {

    // Use this for initialization

    bool Displayed;
    public GameObject BossVsPlayerImage;
    public GameObject CameraFather;
	void Start () {
        CameraFather = GameObject.FindGameObjectWithTag("CameraFather");
	}
	
	// Update is called once per frame
	void Update () {
        if (!Displayed)
        {
            if (Vector3.Distance(this.gameObject.GetComponent<CalmEnemies>().Player.transform.position, this.transform.position) < 6)
            {
                Displayed = true;
                StartCoroutine(DisplayBvP());
            }
        }
	}

    IEnumerator DisplayBvP()
    {
        CameraFather.GetComponent<CameraFollow>().target = this.transform;
        yield return new WaitForSeconds(1);
        BossVsPlayerImage.SetActive(true);
        GlobalAudioPlayer.PlaySFX("Alert");
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(.3f);
        CameraFather.GetComponent<CameraFollow>().target = this.gameObject.GetComponent<CalmEnemies>().Player.transform;
        BossVsPlayerImage.SetActive(false);
        Time.timeScale = 1;
    }
}
