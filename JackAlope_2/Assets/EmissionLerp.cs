using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionLerp : MonoBehaviour {

    // Use this for initialization
    float t = 0;
    public Color i, o;
    [Range(1,10)]
    public float speed;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        t = Mathf.PingPong(Time.time * speed, 1);
        try
        {
            this.GetComponent<SpriteRenderer>().material.SetColor("_EmissionColor", Color.Lerp(i, o, t));
            this.GetComponent<SpriteRenderer>().material.color = Color.Lerp(i, o, t);
            this.GetComponent<SpriteRenderer>().material.SetColor("_TintColor", Color.Lerp(i, o, t));
            this.GetComponent<SpriteRenderer>().material.SetColor("_EmissiveColor", Color.Lerp(i, o, t));
        }
        catch { }
    }
}
