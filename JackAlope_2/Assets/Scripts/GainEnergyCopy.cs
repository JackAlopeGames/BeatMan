using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GainEnergyCopy : MonoBehaviour {

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

    void Start () {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.SwipeControls = GameObject.FindGameObjectWithTag("SwipeControls");
        this.EnemyToAttack = player;
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
      this.enemies = GameObject.FindGameObjectsWithTag("Enemy");
      this.Origins = new Vector3[enemies.Length];
        /*if (this.enemies.Length >0)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].GetComponent<HealthSystem>().CurrentHp > 0)
                {
                    EveryoneIsDead = false;
                }
                else
                {
                    EveryoneIsDead = true;
                }
            }
            }
            */

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
        this.SwipeControls.GetComponent<Swipe>().BlockPunchTap = true;
        this.SwipeControls.GetComponent<Swipe>().BlockForTutorial = true;
        items = GameObject.FindGameObjectWithTag("ITEMS");
        restrictors = GameObject.FindGameObjectsWithTag("Restrictors");
        for (int i = 0; i < restrictors.Length; i++)
        {
            this.restrictors[i].SetActive(false);
        }

        this.GetComponent<Slider>().value = 0;
            
        this.player = GameObject.FindGameObjectWithTag("Player");
        /*for (int i = 0; i < enemies.Length; i++)
        {
            this.enemies[i].GetComponent<EnemyAI>().enableAI = false;
        }*/
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
        
        StartCoroutine(waitToSpecial());
    }

    IEnumerator waitToSpecial()
    {
        yield return new WaitForEndOfFrame();
        this.player.transform.GetChild(0).GetComponent<Animator>().SetTrigger("SpecialAttack");
        //yield return new WaitForSeconds(2);
        //this.SpecialAttack.GetComponent<Animator>().SetTrigger("SP"); /// WE ARE NOT USING SPRITE ANIMATION
        yield return new WaitForSeconds(2);
        if (!EnemyToAttack.GetComponent<EnemyAI>().isDead)
        {
            items.SetActive(false);
            this.player.SetActive(false);
            /*
            for (int i = 0; i < enemies.Length; i++)
            {
                // this.enemies[i].GetComponent<HealthSystem>().SubstractHealth(100);
                try
                {
                    if (i >= 4)
                    {
                        this.enemies[i].transform.position = new Vector3(this.player.transform.position.x - 5.5f, this.enemies[i].transform.position.y, (this.playerSP.transform.position.z) + i - 4);
                    }
                    else if (i >= 2)
                    {
                        this.enemies[i].transform.position = new Vector3(this.player.transform.position.x - 4.5f, this.enemies[i].transform.position.y, (this.playerSP.transform.position.z) + i - 2);
                    }
                    else
                    {
                        this.enemies[i].transform.position = new Vector3(this.player.transform.position.x - 3.5f, this.enemies[i].transform.position.y, (this.playerSP.transform.position.z) + i);
                    }
                }
                catch { }
            }*/
            try
            {
                this.EnemyToAttack.transform.position = new Vector3(this.player.transform.position.x - 3.5f, this.EnemyToAttack.transform.position.y, (this.playerSP.transform.position.z));
            }
            catch { }

            GameObject firesp = GameObject.FindGameObjectWithTag("FireSP");
            try
            {
                // firesp.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                // firesp.transform.GetChild(1).GetComponent<ParticleSystem>().Play(); // WE ARE NOT USING PARTICLES
            }
            catch { }
            GameObject playerspClone = Instantiate(this.playerSP, new Vector3(this.player.transform.position.x + 3.5f, this.playerSP.transform.position.y, this.playerSP.transform.position.z), Quaternion.identity);
            /* playerspClone.GetComponent<EnemyAI>().enableAI = true;
             yield return new WaitForSeconds(.1f);
             playerspClone.GetComponent<EnemyAI>().enableAI = false;*/
            playerspClone.transform.GetChild(0).GetComponent<Animator>().SetTrigger("RawRage");
            yield return new WaitForSeconds(2);
            playerspClone.GetComponent<EnemyAI>().enableAI = true;
            playerspClone.transform.GetChild(2).GetComponent<Animator>().SetTrigger("CameraSP");
            /*  if(GameObject.FindGameObjectWithTag("Enemies").GetComponent<EnemyWaveSystem>().currentWave == GameObject.FindGameObjectWithTag("Enemies").GetComponent<EnemyWaveSystem>().EnemyWaves.Length -1)
              {
                  try
                  {
                      playerspClone.GetComponent<EnemyAI>().AttackList[0].damage = this.enemies[this.enemies.Length].GetComponent<HealthSystem>().MaxHp / 2;
                  }
                  catch {
                      playerspClone.GetComponent<EnemyAI>().AttackList[0].damage = 35 / 2;
                  }
              }*/
            yield return new WaitForSeconds(2f); // time to kill them
            this.player.GetComponent<Rigidbody>().useGravity = true;
            this.player.GetComponent<Rigidbody>().isKinematic = false;
            this.player.GetComponent<CapsuleCollider>().enabled = true;
            this.Mask.SetActive(false);
            this.fader.GetComponent<UIFader>().Fade(UIFader.FADE.FadeOut, 1, 0);
            try
            {
                firesp.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                firesp.transform.GetChild(1).GetComponent<ParticleSystem>().Stop();
            }
            catch { }
            GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreSystem>().currentScore += 200;
            yield return new WaitForSeconds(1);
            items.SetActive(true);
            playerspClone.SetActive(false);
            Destroy(playerspClone,2);
            this.player.SetActive(true);
            /*  for (int i = 0; i < enemies.Length; i++)
              {
                  try
                  {
                      this.enemies[i].GetComponent<EnemyAI>().enableAI = true;
                      this.enemies[i].GetComponent<EnemyAI>().enemyState = UNITSTATE.WALK;
                      this.enemies[i].transform.GetChild(1).GetComponent<Animator>().SetTrigger("Idle");
                  }
                  catch { }
              }*/

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
        }
        else
        {
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
            this.GetComponent<Slider>().value = 1;
        }
        this.SwipeControls.GetComponent<Swipe>().BlockPunchTap = false;
        this.SwipeControls.GetComponent<Swipe>().BlockForTutorial = false;
    }
    // Update is called once per frame

    private float TimerToCheck, prevDistance, CheckIfEmpty;

	void Update () {
        if (SpecialAttackIsReady && Enemies.Length==0 || Enemies==null)
        {
            Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
        if (SpecialAttackIsReady && Enemies.Length > 0 || Enemies != null)
        {
            TimerToCheck += Time.deltaTime;
            if (TimerToCheck > 0.25f)
            {
                prevDistance = 10;
                EnemyToAttack = player;
                CheckIfEmpty = 0;
                for(int i=0; i < Enemies.Length; i++)
                {
                    if (Enemies[i] != null)
                    {
                        CheckIfEmpty++;
                        if (Vector3.Distance(this.player.transform.position, Enemies[i].transform.position) < prevDistance)
                        {
                            prevDistance = Vector3.Distance(this.player.transform.position, Enemies[i].transform.position);
                            EnemyToAttack = Enemies[i];
                        }
                    }
                }
                if (CheckIfEmpty == 0)
                {
                    this.Pointer.SetActive(false);
                    Enemies = GameObject.FindGameObjectsWithTag("Enemy");
                }
            }
        }
        if(EnemyToAttack==player && SpecialAttackIsReady && this.Pointer.activeInHierarchy)
        {
            this.Pointer.SetActive(false);
            EnemyToAttack = player;
        }
        if (EnemyToAttack != player && SpecialAttackIsReady && this.Pointer.activeInHierarchy)
        {
            if (EnemyToAttack.GetComponent<EnemyAI>().isDead)
            {
                this.Pointer.SetActive(false);
                EnemyToAttack = player;
            }
        }
        if (EnemyToAttack != player && SpecialAttackIsReady && !this.Pointer.activeInHierarchy)
        {
            if (!EnemyToAttack.GetComponent<EnemyAI>().isDead)
            {
                this.Pointer.SetActive(true);
            }
        }
        if (EnemyToAttack != player && SpecialAttackIsReady)
        {
            this.Pointer.transform.position = Vector3.Lerp(this.Pointer.transform.position, this.EnemyToAttack.transform.position + Vector3.up * 3, Time.deltaTime * 20);
            
        }
        if(this.GetComponent<Slider>().value < 1 && SpecialAttackIsReady)
        {
            SpecialAttackIsReady = false;
            this.Pointer.SetActive(false);
            this.EnemyToAttack = player;
        }
        if (this.GetComponent<Slider>().value >= 1 && !SpecialAttackIsReady)
        {
            SpecialAttackIsReady = true;
        }
    }
}
