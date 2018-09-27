using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxFollowCoach : MonoBehaviour {

    // Use this for initialization
    public GameObject coach;
    public bool heyMorty;
    public bool left;
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        /* if (!heyMorty)
         {*/
        if (!left)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.coach.transform.position.x - 2.75f, this.transform.position.y, this.coach.transform.position.z), Time.deltaTime * 10);
        }
        else
        {
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.coach.transform.position.x + 2.75f, this.transform.position.y, this.coach.transform.position.z), Time.deltaTime * 10);
        }

       // }
    }
}
