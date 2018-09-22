using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour {

    public int MaxHp = 20;
    public int CurrentHp = 20;
    public int ExtraHp;
    public bool invulnerable, Extra;
    public delegate void OnHealthChange(float percentage, GameObject GO);
    public static event OnHealthChange onHealthChange;
    private GameObject ScoreSystem;
    public GameObject Coin, CoinSack;
    GameObject Tutorial;
    private GameObject Coach;

    void Start() {
        // this.CoinSack = GameObject.FindGameObjectWithTag("CoinsSack");
        // this.Coin = GameObject.FindGameObjectWithTag("Coin");
        if (SceneManager.GetSceneByName("Level_02").isLoaded)
        {
            Coach = GameObject.FindGameObjectWithTag("Coach");
        }
        try
        {
            Tutorial = GameObject.FindGameObjectWithTag("AttackListScreenDojo");
            this.ScoreSystem = GameObject.FindGameObjectWithTag("ScoreSystem");
            if (GameObject.FindGameObjectWithTag("ExtraCheker").GetComponent<ExtraLife>().extra)
            {
                this.ExtraHp = 20;
            }
        }
        catch { }
        SendUpdateEvent();
	}

    
    //substract health
    public void SubstractHealth(int damage){
        if (Extra)
        {
            ExtraHp = Mathf.Clamp(ExtraHp -= damage, 0, MaxHp);
            if (ExtraHp <= 0)
            {
                GameObject.FindGameObjectWithTag("ExtraCheker").GetComponent<ExtraLife>().extra = false;
                GameObject.FindGameObjectWithTag("ExtraCheker").GetComponent<ExtraLife>().UpdateExtra();
                invulnerable = false;
                Extra = false;
                
            }
        }
        if (!invulnerable){

			//reduce hp
			CurrentHp = Mathf.Clamp(CurrentHp -= damage, 0, MaxHp);
            
			//Health reaches 0
			if (isDead()) gameObject.SendMessage("Death", SendMessageOptions.DontRequireReceiver);
		}

        if(this.gameObject.tag == "Enemy" && !invulnerable)
        {
            //this.ScoreSystem.GetComponent<Combo>().HitTime = 3;
            GameObject Combo = GameObject.FindGameObjectWithTag("ComboText");
            Combo.GetComponent<ComboSystem>().ComboTime = 0;
            Combo.GetComponent<ComboSystem>().Hits++;
            try
            {
                if (SceneManager.GetSceneByName("Level_02").isLoaded)
                {
                    if (Coach.GetComponent<Couch>().punchingTest || Coach.GetComponent<Couch>().GrabHitTest)
                    {
                        Tutorial.GetComponent<LevelEnterTutorialDojo>().tapCount++;
                    }
                }
            }
            catch { Debug.Log("ERROR"); }
            this.ScoreSystem.GetComponent<ScoreSystem>().currentScore += 5+ Combo.GetComponent<ComboSystem>().Hits;
            GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<GainEnergy>().GainEnergyPunch(0.03f);
            GameObject maxText = Instantiate(GameObject.FindGameObjectWithTag("MaxText"), new Vector3(this.transform.position.x, 2.5f, this.transform.position.z),Quaternion.identity);
            maxText.transform.Rotate(0, 180, 0);
            if(5 + Combo.GetComponent<ComboSystem>().Hits > 10)
            {
                maxText.GetComponent<TextMesh>().text = "MAX";
                maxText.transform.GetChild(0).GetComponent<TextMesh>().text = "MAX";
            }
            else
            {
                maxText.GetComponent<TextMesh>().text = 5 + Combo.GetComponent<ComboSystem>().Hits + "";
                maxText.transform.GetChild(0).GetComponent<TextMesh>().text = 5 + Combo.GetComponent<ComboSystem>().Hits + "";
            }
            Destroy(maxText, .5f);
            GameObject HP3D = Instantiate(GameObject.FindGameObjectWithTag("Hp_Text3D"), new Vector3(this.transform.position.x, 2.2f, this.transform.position.z), Quaternion.identity);
            HP3D.GetComponent<Get3DHP>().Enemy = this.gameObject;
            HP3D.transform.Rotate(0, 180, 0);
            Destroy(HP3D, 1.5f);
        }
        try
        { 
            if (this.gameObject.tag == "Player" && GameObject.FindGameObjectWithTag("Controlls").transform.GetChild(1).GetComponent<Swipe>().grabing)
            {
                GameObject.FindGameObjectWithTag("Controlls").transform.GetChild(1).GetComponent<SwipeControls>().InterruptGrabbing();
            }
            if(this.gameObject.tag == "Player")
            {
                Debug.Log("Interrumpt");
                GameObject.FindGameObjectWithTag("SwipeControls").GetComponent<SwipeControls>().InterruptGrabbing();
                GameObject.FindGameObjectWithTag("SwipeControls").GetComponent<SwipeControls>().InterruptUppecut();
                GameObject.FindGameObjectWithTag("SwipeControls").GetComponent<SwipeControls>().InterrumptRunningAndPunch();
            }
        }
        catch { }
        
        

		//update Health Event
		SendUpdateEvent();
	}

    //add health
    public void AddHealth(int amount){
		CurrentHp = Mathf.Clamp(CurrentHp += amount, 0, MaxHp);
		SendUpdateEvent();
	}
		
	//health update event
	public void SendUpdateEvent(){
		float CurrentHealthPercentage = 1f/MaxHp * CurrentHp;
		if(onHealthChange != null) onHealthChange(CurrentHealthPercentage, gameObject);
	}

    //death
    
    bool isDead(){
        if (CurrentHp == 0 && this.gameObject.tag !="Player" && this.transform.GetChild(0).gameObject.tag !="Boss")
        {
            float maxDis = this.Coin.GetComponent<Coin>().maxdistance;
          //  this.Coin.GetComponent<Coin>().Maxdistance = 0;
            GameObject coin1 = Instantiate(this.Coin, new Vector3(this.transform.position.x, this.transform.position.y+2, this.transform.position.z), Quaternion.identity);
            GameObject coin2 = Instantiate(this.Coin, new Vector3(this.transform.position.x, this.transform.position.y+2, this.transform.position.z), Quaternion.identity);
            coin1.GetComponent<Coin>().Maxdistance = 0;
            coin2.GetComponent<Coin>().Maxdistance = 0;
            coin1.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 2, -1) * 2, ForceMode.Impulse);
            coin2.GetComponent<Rigidbody>().AddForce(new Vector3(1, 2, 1) * 2, ForceMode.Impulse);
            StartCoroutine(coin1.GetComponent<Coin>().refreshMaxDistance(maxDis));
            StartCoroutine(coin2.GetComponent<Coin>().refreshMaxDistance(maxDis));
        }
        if (CurrentHp == 0 && this.gameObject.tag != "Player" && this.transform.GetChild(0).gameObject.tag == "Boss")
        {
            
            float maxDis = this.Coin.GetComponent<Coin>().maxdistance;
            GameObject CoinsSack = Instantiate(this.CoinSack, new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z), Quaternion.identity);
            CoinsSack.transform.Rotate(0, 184, 0);
            CoinsSack.GetComponent<Coin>().Maxdistance = 0;
            // GameObject player = GameObject.FindGameObjectWithTag("Player");
            CoinsSack.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 2, -1) * 2, ForceMode.Impulse);
            this.CoinSack.GetComponent<Coin>().maxdistance = 10;
            this.CoinSack.GetComponent<Coin>().speed = 15;
            StartCoroutine(CoinsSack.GetComponent<Coin>().refreshMaxDistance(maxDis));
            StartCoroutine(this.CoinSack.GetComponent<Coin>().refreshMaxDistance(maxDis));
        }
        return CurrentHp == 0;
	}

   
}
