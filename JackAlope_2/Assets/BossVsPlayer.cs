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
        CameraFather.GetComponent<CameraFollow>().distanceToTarget = -5;
        CameraFather.GetComponent<CameraFollow>().heightOffset = 8;
        yield return new WaitForSeconds(1);
        BossVsPlayerImage.SetActive(true);
        GlobalAudioPlayer.PlaySFX("Alert");
        Time.timeScale = 0.05f;
        yield return new WaitForSeconds(.15f);
        CameraFather.GetComponent<CameraFollow>().target = this.gameObject.GetComponent<CalmEnemies>().Player.transform;
        CameraFather.GetComponent<CameraFollow>().distanceToTarget = 10;
        CameraFather.GetComponent<CameraFollow>().heightOffset = -2.5f;
        BossVsPlayerImage.SetActive(false);
        Time.timeScale = 1;
    }
}
