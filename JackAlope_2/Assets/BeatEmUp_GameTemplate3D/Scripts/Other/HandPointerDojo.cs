using UnityEngine;
using UnityEngine.UI;

public class HandPointerDojo : MonoBehaviour {

	public Image sprite;
	public float speed = 0.8f;
	public float delayBeforeStart = 1;
	public string sfx = "";
	public AnimationCurve alphaCurve;
	public bool HandActive;
	float t=0;
	float startTime;

	void Awake(){
		sprite.enabled = false;
        HandActive = true;
	}

	void Update(){

		//sprite alpha
		if (t > 0 && Time.time > startTime) {
			sprite.enabled = true;
			t -= Time.deltaTime * speed;
			sprite.color = new Color (1, 1, 1, alphaCurve.Evaluate (1 - t));
		} else {
			sprite.enabled = false;
		}

		//iterate
		if (HandActive && t <= 0 && Time.time > startTime) {
			t = 1;
			if (sfx != "") GlobalAudioPlayer.PlaySFX (sfx);
		} 
	}

}
