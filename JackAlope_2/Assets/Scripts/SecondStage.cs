using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondStage : MonoBehaviour {

    // Use this for initialization
    public GameObject Player;
    public GameObject Enemies;
    public GameObject UI;
    public GameObject StartStage2;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(this.Player.transform.position.x < this.transform.position.x && this.gameObject.layer != 11 && Enemies.GetComponent<EnemyWaveSystem>().currentWave==2)
        {
            UI.GetComponent<UIManager>().UI_fader.Fade(UIFader.FADE.FadeIn, 2f, 0f);
            this.Player.transform.position = new Vector3(this.transform.position.x - 5f, this.Player.transform.position.y, this.Player.transform.position.z);
            this.gameObject.layer = 11;
            StartStage2.SetActive(true);
            Enemies.GetComponent<EnemyWaveSystem>().UpdateCameraCollider();
            this.gameObject.SetActive(true);
        }
	}
}
