using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CalmEnemies : MonoBehaviour {

    // Use this for initialization

    public GameObject a, b, c, d,e,f;
    public int actualspot, jumpInter;
    public GameObject[] spots;
    public float AttackInterval, CloseDistance, speed, speedspecial;
    public GameObject Player, SwipeControls;
    private bool specialChange;
    public bool boss;
    public int distanceToDetectPlayer;
	void OnEnable () {
        if (boss)
        {
            jumpInter = 1;
        }
        else
        {
            jumpInter = 2;
        }
        actualspot = Random.Range(0, spots.Length);
        this.Player = GameObject.FindGameObjectWithTag("Player");
        this.speed = this.GetComponent<EnemyAI>().walkSpeed;
        this.spots = new GameObject[] { this.a, this.b, this.c, this.d ,this.e,this.f};
        this.AttackInterval = this.GetComponent<EnemyAI>().attackInterval;
        this.CloseDistance = this.GetComponent<EnemyAI>().attackRangeDistance;
        this.SwipeControls = GameObject.FindGameObjectWithTag("SwipeControls");
        StartCoroutine(HuntingTime());
	}
	
	// Update is called once per frame
	void Update () {
        if (SwipeControls != null)
        {
            if (SwipeControls.GetComponent<SwipeControls>().RunningToPunch && !specialChange)
            {
                speedspecial = this.GetComponent<EnemyAI>().walkSpeed;
                this.GetComponent<EnemyAI>().walkSpeed = 0.1f;
                speed = 0.1f;
                specialChange = true;
            }

            if (this.GetComponent<EnemyAI>().walkSpeed == 0.1f && !SwipeControls.GetComponent<SwipeControls>().RunningToPunch)
            {
                this.GetComponent<EnemyAI>().walkSpeed = this.speedspecial;
                specialChange = false;
            }
        }
        else
        {
            this.SwipeControls = GameObject.FindGameObjectWithTag("SwipeControls");
        }
	}

    IEnumerator HuntingTime()
    {
        speed = this.GetComponent<EnemyAI>().walkSpeed;
        if (this.GetComponent<EnemyAI>().walkSpeed != 0.1f)
        {
            this.GetComponent<EnemyAI>().walkSpeed = 2.5f;
            if (boss)
            {
                this.GetComponent<EnemyAI>().walkSpeed = 3;
            }
        }
        try
        {
            if (!SwipeControls.GetComponent<SwipeControls>().RunningToPunch && !SwipeControls.GetComponent<Swipe>().runningMad && SwipeControls.GetComponent<Swipe>().holdTimer <= 0 && !SwipeControls.GetComponent<SwipeControls>().JumpSpecialInCourse)
            {
                this.GetComponent<EnemyAI>().attackInterval = 1000;
            }
        }
        catch { this.GetComponent<EnemyAI>().attackInterval = 1000; }
        int rSpot = Random.Range(0, 2);
        if(rSpot == 0)
        { 
            if (actualspot == 0 || (actualspot == 1 && jumpInter==2))
            {
                this.GetComponent<EnemyAI>().target = spots[spots.Length - 1];
                actualspot = spots.Length - 1;
            }
            else
            {
                this.GetComponent<EnemyAI>().target = spots[actualspot - jumpInter];
                actualspot = actualspot - jumpInter;
            }
        }
        else if (rSpot == 1)
        {
            if (actualspot == spots.Length - 1 || (actualspot == spots.Length-2 && jumpInter ==2))
            {
                this.GetComponent<EnemyAI>().target = spots[0];
                actualspot = 0;
            }
            else
            {
                this.GetComponent<EnemyAI>().target = spots[actualspot + jumpInter];
                actualspot = actualspot + jumpInter;
            }
        }
        this.transform.LookAt(this.Player.transform);

        if (Vector3.Distance(this.spots[rSpot].transform.position, this.transform.position) < 1.5f)
        {
            this.GetComponent<EnemyAI>().attackRangeDistance = Vector3.Distance(this.spots[rSpot].transform.position, this.transform.position);
            this.GetComponent<EnemyAI>().target = this.Player;
        }
        int r = Random.Range(2, 4);
        yield return new WaitForSeconds(r);
        if (Vector3.Distance(this.transform.position, this.Player.transform.position) < distanceToDetectPlayer)
        {
            if ((!boss && this.transform.parent.GetComponent<EnemyWaveSystem>().EnemyWaves[this.transform.parent.GetComponent<EnemyWaveSystem>().EnemyWaves.Length - 1].EnemyList.Count > 1) || SceneManager.GetSceneByName("Level_02").isLoaded)
            {
                this.GetComponent<EnemyAI>().walkSpeed = speed;
                this.GetComponent<EnemyAI>().target = this.Player;
                int r2 = Random.Range(6, 9);
                try
                {
                    if (!SwipeControls.GetComponent<SwipeControls>().RunningToPunch && !SwipeControls.GetComponent<Swipe>().runningMad && SwipeControls.GetComponent<Swipe>().holdTimer <= 0 && !SwipeControls.GetComponent<SwipeControls>().JumpSpecialInCourse && !SceneManager.GetSceneByName("Level_02").isLoaded)
                    {
                        this.GetComponent<EnemyAI>().attackInterval = 2;
                    }
                }
                catch { }
                this.GetComponent<EnemyAI>().attackRangeDistance = CloseDistance;
                yield return new WaitForSeconds(r2);
            }
            if (boss && (this.transform.parent.GetComponent<EnemyWaveSystem>().EnemyWaves[this.transform.parent.GetComponent<EnemyWaveSystem>().currentWave].EnemyList.Count <= 1 || this.GetComponent<HealthSystem>().CurrentHp < this.GetComponent<HealthSystem>().MaxHp / 2))
            {
                this.GetComponent<EnemyAI>().walkSpeed = 3;
                this.GetComponent<EnemyAI>().target = this.Player;
                this.GetComponent<EnemyAI>().attackInterval = 0;
            }
            else
            {
                StartCoroutine(HuntingTime());
            }
        }
        else
        {
            StartCoroutine(HuntingTime());
        }
        yield return null;
    }
}
