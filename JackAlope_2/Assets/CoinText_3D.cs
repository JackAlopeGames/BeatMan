using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinText_3D : MonoBehaviour {

    // Use this for initialization
    public int coins;
    public GameObject player;
	void OnEnable () {
        this.player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (this.player != null)
        {
            try
            {
                if (int.Parse(this.gameObject.GetComponent<TextMesh>().text) != coins)
                {
                    this.gameObject.GetComponent<TextMesh>().text = coins + "";
                }
                this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.player.transform.position.x, this.transform.position.y, this.player.transform.position.z), Time.deltaTime * 20);
            }
            catch { }
        }
	}

    public void HideThis()
    {
        StartCoroutine(HideCoins3DText());
    }
     public IEnumerator HideCoins3DText()
    {
        yield return new WaitForSeconds(1.5f);
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;

    }
}
