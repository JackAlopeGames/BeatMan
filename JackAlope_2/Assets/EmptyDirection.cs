using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyDirection : MonoBehaviour {

    // Use this for initialization
    public GameObject Player;
    public float speed;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (this.Player.GetComponent<PlayerCombat>().currentDirection == DIRECTION.Right)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.Player.transform.position.x - 3.5f, 1, this.Player.transform.position.z), Time.deltaTime * speed);
        }else if (this.Player.GetComponent<PlayerCombat>().currentDirection == DIRECTION.Left)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.Player.transform.position.x + 3.5f, 1, this.Player.transform.position.z), Time.deltaTime * speed);
        }
	}
}
