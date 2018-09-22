using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxFollowCoach : MonoBehaviour {

    // Use this for initialization
    public GameObject coach;
    public bool heyMorty;
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
       /* if (!heyMorty)
        {*/
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.coach.transform.position.x - 3, this.transform.position.y, this.coach.transform.position.z), Time.deltaTime * 10);
       // }
    }
}
