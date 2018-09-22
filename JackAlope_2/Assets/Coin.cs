using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour {

    // Use this for initialization
    private GameObject player;
    public float maxdistance;
    private float realdistance;
    public float speed;
    public float Maxdistance { get { return maxdistance; } set {  maxdistance = value; } }
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        try
        {
            this.realdistance = Vector3.Distance(this.player.transform.position, this.transform.position);

            if (realdistance < maxdistance)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, this.player.transform.position, Time.deltaTime * speed);
            }
            if (realdistance < .2f)
            {
                this.gameObject.layer = 11;
            }
        }
        catch { }
	}

    public IEnumerator refreshMaxDistance( float prevDist)
    {
        yield return new WaitForSeconds(1);
        this.maxdistance = prevDist;
    }

    

    public void OnCollisionEnter(Collision collision)   
    {
      
        if (collision.gameObject == this.player || collision.gameObject.tag == "Player" || collision.gameObject.tag == "PlayerAuto")
        {
            GlobalAudioPlayer.PlaySFX("Coin");
            GameObject particles = Instantiate(GameObject.FindGameObjectWithTag("StarParticles"), this.transform.position + Vector3.up, Quaternion.identity);
            particles.transform.Rotate(new Vector3(-90, 0, 0));
            particles.GetComponent<ParticleSystem>().Play();
            Destroy(particles, 1);
            GameObject text = GameObject.FindGameObjectWithTag("CoinsText");
            GameObject BC = GameObject.FindGameObjectWithTag("BannerController");
            GameObject SS = GameObject.FindGameObjectWithTag("SavingSystem");
            try
            {
                if (this.gameObject.tag == "Coin")
                {
                    BC.GetComponent<BannerController>().coins++;
                    BC.GetComponent<BannerController>().coinsCurrent++;
                    SS.GetComponent<SavingSystem>().Coins++;
                }
                if(this.gameObject.tag == "CoinsSack")
                {
                    BC.GetComponent<BannerController>().coins+=10;
                    BC.GetComponent<BannerController>().coinsCurrent+=10;
                    SS.GetComponent<SavingSystem>().Coins+=10;
                }
            }
            catch { }
           
            GameObject CoinsText_3D = GameObject.FindGameObjectWithTag("CoinsText_3D");
            CoinsText_3D.GetComponent<MeshRenderer>().enabled = true;
            CoinsText_3D.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            CoinsText_3D.GetComponent<CoinText_3D>().coins++;
            CoinsText_3D.GetComponent<CoinText_3D>().HideThis();
            Destroy(this.gameObject);
            
        }
    }

   
}
