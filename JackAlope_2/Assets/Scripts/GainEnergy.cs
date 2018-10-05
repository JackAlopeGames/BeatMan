using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GainEnergy : MonoBehaviour {

    // Use this for initialization
    public GameObject SpecialAttack;
    public GameObject [] enemies;
    public GameObject player, playerSP;
    public GameObject Mask;
    public GameObject button, thunder, thunderEnergyBar;
    public GameObject fader;
    private bool EveryoneIsDead;
    private GameObject items;
    public GameObject CounterRespawn;
    bool SpecialAttackIsReady;
    public GameObject EnemyToAttack;
    public GameObject Pointer;
    public GameObject[] Enemies;
    public Vector3[] Origins;
    public GameObject SwipeControls;
    bool dontchangeEnemy;
    public float limit0, limit1;
    public GameObject EnemyWaveSystem;
    void Start () {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.SwipeControls = GameObject.FindGameObjectWithTag("SwipeControls");
        this.EnemyToAttack = player;
        this.EnemyWaveSystem = GameObject.FindGameObjectWithTag("Enemies");
	}

    public void GainEnergyPunch(float x)
    {
        this.GetComponent<Slider>().value += x;

        if (this.GetComponent<Slider>().value >= 1 && !SceneManager.GetSceneByName("Level_02").isLoaded)
        {
           this.thunderEnergyBar.SetActive(true);
          this.button.SetActive(true);
           this.thunder.SetActive(true);
            SpecialAttackIsReady = true;
        }
        else
        {
           this.thunderEnergyBar.SetActive(false);
           this.button.SetActive(false);
            this.thunder.SetActive(false);
        }
    }

    public GameObject[] restrictors;

    public void DoSpecialMove()
    {
      Time.timeScale = 1;
      this.enemies = GameObject.FindGameObjectsWithTag("Enemy");
      this.Origins = new Vector3[enemies.Length];

        if(this.EnemyToAttack!= player)
        {
            if(this.EnemyToAttack.GetComponent<HealthSystem>().CurrentHp > 0 && !this.EnemyToAttack.GetComponent<EnemyAI>().isDead)
            {
                EveryoneIsDead = false;
            }
            else
            {
                EveryoneIsDead = true;
            }
        }
        if (EveryoneIsDead || this.CounterRespawn.GetComponent<isCounterActive>().isActive || this.player.GetComponent<HealthSystem>().CurrentHp<=0 || !this.Pointer.activeInHierarchy)
        {
            return;
        }
        dontchangeEnemy = true;
        this.SwipeControls.GetComponent<Swipe>().BlockPunchTap = true;
        this.SwipeControls.GetComponent<Swipe>().BlockForTutorial = true;
        items = GameObject.FindGameObjectWithTag("ITEMS");
        restrictors = GameObject.FindGameObjectsWithTag("Restrictors");
        for (int i = 0; i < restrictors.Length; i++)
        {
            this.restrictors[i].SetActive(false);
        }

        this.GetComponent<Slider>().value = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
                this.enemies[i].GetComponent<EnemyAI>().enableAI = false;
                this.enemies[i].SetActive(false);
                this.Origins[i] = this.enemies[i].transform.position;
        }
        this.Pointer.SetActive(false);
        this.SpecialAttackIsReady = false;
        this.EnemyToAttack.SetActive(true);
        this.EnemyToAttack.GetComponent<EnemyAI>().enableAI = false;
        this.player.GetComponent<Rigidbody>().useGravity = false;
        this.player.GetComponent<Rigidbody>().isKinematic = true;
        this.player.GetComponent<CapsuleCollider>().enabled = false;
        this.Mask.SetActive(true);
        //this.EnemyToAttack.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        StartCoroutine(waitToSpecial());
    }

    bool isPlayerToTheRight;

    IEnumerator waitToSpecial()
    {
        yield return new WaitForEndOfFrame();
        this.player.transform.GetChild(0).GetComponent<Animator>().SetTrigger("SpecialAttack");
        if (player.transform.position.x < this.EnemyToAttack.transform.position.x)
        {
            isPlayerToTheRight = true;
            this.EnemyToAttack.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            isPlayerToTheRight = false;
            this.EnemyToAttack.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        yield return new WaitForSeconds(2);
        if (!EnemyToAttack.GetComponent<EnemyAI>().isDead)
        {
            items.SetActive(false);
            this.player.SetActive(false);
            
            try
            {
              //  this.EnemyToAttack.transform.position = new Vector3(this.player.transform.position.x - 3.5f, this.EnemyToAttack.transform.position.y, (this.playerSP.transform.position.z));
            }
            catch { }

            GameObject playerspClone = null;
            if (isPlayerToTheRight)
            {
                if(this.EnemyToAttack.transform.position.x - 7f < limit1) {
                    playerspClone = Instantiate(this.playerSP, new Vector3(limit1, this.playerSP.transform.position.y, this.playerSP.transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
                }
                else
                {
                    playerspClone = Instantiate(this.playerSP, new Vector3(this.EnemyToAttack.transform.position.x - 7f, this.playerSP.transform.position.y, this.playerSP.transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
                }
            }
            else
            {
                if(this.EnemyToAttack.transform.position.x + 7f > limit0)
                {
                    playerspClone = Instantiate(this.playerSP, new Vector3(limit0, this.playerSP.transform.position.y, this.playerSP.transform.position.z), Quaternion.Euler(new Vector3(0, 180, 0)));
                }
                else
                {
                    playerspClone = Instantiate(this.playerSP, new Vector3(this.EnemyToAttack.transform.position.x + 7f, this.playerSP.transform.position.y, this.playerSP.transform.position.z), Quaternion.Euler(new Vector3(0, 180, 0)));
                }
            }
         
            //  GameObject playerspClone = Instantiate(this.playerSP, new Vector3(this.player.transform.position.x + 3.5f, this.playerSP.transform.position.y, this.playerSP.transform.position.z), Quaternion.identity);
           
            playerspClone.transform.GetChild(0).GetComponent<Animator>().SetTrigger("RawRage");
            yield return new WaitForSeconds(2);
            playerspClone.GetComponent<EnemyAI>().enableAI = true;
            playerspClone.transform.GetChild(2).GetComponent<Animator>().SetTrigger("CameraSP");
         
            yield return new WaitForSeconds(2f); // time to kill them
            this.player.GetComponent<Rigidbody>().useGravity = true;
            this.player.GetComponent<Rigidbody>().isKinematic = false;
            this.player.GetComponent<CapsuleCollider>().enabled = true;
            this.Mask.SetActive(false);
            this.fader.GetComponent<UIFader>().Fade(UIFader.FADE.FadeOut, 1, 0);
           
            GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreSystem>().currentScore += 200;
            yield return new WaitForSeconds(1);
            items.SetActive(true);
            playerspClone.SetActive(false);
            Destroy(playerspClone);
            this.player.SetActive(true);

            try
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i] != null)
                    {
                        if (!enemies[i].GetComponent<EnemyAI>().isDead)
                        {
                            this.enemies[i].transform.position = Origins[i];
                            this.enemies[i].SetActive(true);
                            this.enemies[i].GetComponent<EnemyAI>().enableAI = true;
                            this.enemies[i].GetComponent<EnemyAI>().enemyState = UNITSTATE.WALK;
                            this.enemies[i].transform.GetChild(1).GetComponent<Animator>().SetTrigger("Idle");
                        }
                    }
                }
            }
            catch { }

            this.fader.GetComponent<UIFader>().Fade(UIFader.FADE.FadeIn, 1, 0);
            for (int i = 0; i < restrictors.Length; i++)
            {
                this.restrictors[i].SetActive(true);
            }

            this.EnemyWaveSystem.GetComponent<ActiveCollidersEnemies>().UpdateColliders();
        }
        else
        {
            this.GetComponent<BlockSPBoss>().ShowInstru();
            this.gameObject.GetComponent<BlockSPBoss>().ShowInstru();
            this.player.GetComponent<Rigidbody>().useGravity = true;
            this.player.GetComponent<Rigidbody>().isKinematic = false;
            this.player.GetComponent<CapsuleCollider>().enabled = true;
            this.Mask.SetActive(false);
            try
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i] != null)
                    {
                        if (!enemies[i].GetComponent<EnemyAI>().isDead)
                        {
                            this.enemies[i].transform.position = Origins[i];
                            this.enemies[i].SetActive(true);
                            this.enemies[i].GetComponent<EnemyAI>().enableAI = true;
                            this.enemies[i].GetComponent<EnemyAI>().enemyState = UNITSTATE.WALK;
                            this.enemies[i].transform.GetChild(1).GetComponent<Animator>().SetTrigger("Idle");
                        }
                    }
                }
            }
            catch { }
            for (int i = 0; i < restrictors.Length; i++)
            {
                this.restrictors[i].SetActive(true);
            }
            this.EnemyWaveSystem.GetComponent<ActiveCollidersEnemies>().UpdateColliders();
            this.GetComponent<Slider>().value = 1;
        }
        dontchangeEnemy = false;
        this.SwipeControls.GetComponent<Swipe>().BlockPunchTap = false;
        this.SwipeControls.GetComponent<Swipe>().BlockForTutorial = false;
    }
    // Update is called once per frame

    private float TimerToCheck, prevDistance, CheckIfEmpty;

	void Update () {
        if (!dontchangeEnemy && !SceneManager.GetSceneByName("Level_02").isLoaded)
        {
            if (SpecialAttackIsReady && Enemies.Length == 0 || Enemies == null)
            {
                Enemies = GameObject.FindGameObjectsWithTag("Enemy");
            }
            if (SpecialAttackIsReady && Enemies.Length > 0 || Enemies != null)
            {
                TimerToCheck += Time.deltaTime;
                if (TimerToCheck > 0.25f)
                {
                    prevDistance = 5;
                    EnemyToAttack = player;
                    CheckIfEmpty = 0;
                    for (int i = 0; i < Enemies.Length; i++)
                    {
                        if (Enemies[i] != null)
                        {
                            CheckIfEmpty++;
                            if (Vector3.Distance(this.player.transform.position, Enemies[i].transform.position) < prevDistance && Enemies[i].GetComponent<HealthSystem>().CurrentHp>0)
                            {
                                prevDistance = Vector3.Distance(this.player.transform.position, Enemies[i].transform.position);
                                EnemyToAttack = Enemies[i];
                            }
                        }
                    }
                    if (CheckIfEmpty == 0)
                    {
                        try
                        {
                            this.Pointer.SetActive(false);
                        }
                        catch { }
                        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    }
                }
            }
            if (EnemyToAttack == player && SpecialAttackIsReady && this.Pointer.activeInHierarchy)
            {
                this.Pointer.SetActive(false);
               // EnemyToAttack = player;
            }
            if (EnemyToAttack != player && SpecialAttackIsReady && this.Pointer.activeInHierarchy)
            {
                if (EnemyToAttack.GetComponent<EnemyAI>().isDead || !EnemyToAttack.activeInHierarchy)
                {
                    this.Pointer.SetActive(false);
                    EnemyToAttack = player;
                    Enemies = GameObject.FindGameObjectsWithTag("Enemy");
                }
            }
            if (EnemyToAttack != player && SpecialAttackIsReady && !this.Pointer.activeInHierarchy)
            {
                if (!EnemyToAttack.GetComponent<EnemyAI>().isDead)
                {
                    this.Pointer.SetActive(true);
                    this.button.SetActive(true);
                }
            }
            if ( SpecialAttackIsReady)
            {
                this.Pointer.transform.position = Vector3.Lerp(this.Pointer.transform.position, this.EnemyToAttack.transform.position + Vector3.up * 2.5f, Time.deltaTime * 20);
                if (Vector3.Distance(Pointer.transform.position, this.EnemyToAttack.transform.position) > 3 && this.Pointer.GetComponent<MeshRenderer>().enabled || EnemyToAttack==player)
                {
                    this.Pointer.GetComponent<MeshRenderer>().enabled = false;
                }
                else if (!this.Pointer.GetComponent<MeshRenderer>().enabled)
                {
                    this.Pointer.GetComponent<MeshRenderer>().enabled = true;
                }
            }
            if (this.GetComponent<Slider>().value < 1 && SpecialAttackIsReady)
            {
                SpecialAttackIsReady = false;
                this.Pointer.SetActive(false);
                this.EnemyToAttack = player;
            }
            if (this.GetComponent<Slider>().value >= 1 && !SpecialAttackIsReady)
            {
                SpecialAttackIsReady = true;
                Enemies = GameObject.FindGameObjectsWithTag("Enemy");
            }
        }
    }
}
