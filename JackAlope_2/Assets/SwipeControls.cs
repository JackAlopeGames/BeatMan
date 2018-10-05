using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeControls : MonoBehaviour {

    // Use this for initialization
    public bool updateEveryFrame = false;
    public Swipe swipecontrols;
    private GameObject[] Enemies;
    private GameObject[] Items, EnemiesToHit, Loots;
    public GameObject Item,Player,Empty,defendButton, TargetEnemy, TargetEnemyKick, TargetEnemyPunch, Loot;
    public GameObject EnergyFill;
    public float LootReach = 2;
    public float ItemReach = 2;
    private float TimeGrabbed;
    public bool right, left;
    bool ManualRightJumpDirection;
    [Range(0,1)]
    public float _Presence, _RimPresence;
    private GameObject GlowUppercut, ParticlesUpperCut;
    bool uppercutSuccess;
    public GameObject FirePunch;
    private GameObject Energyball;
    public float distanceToGrab;
    public float timeGrabbingEnemy = 2;
    public float UpperCutTime;

    void OnEnable() {
        this._Presence = 1;
        this.Player = this.gameObject.GetComponent<Swipe>().Player;
        this.Player = GameObject.FindGameObjectWithTag("Player");
        this.GlowUppercut = GameObject.FindGameObjectWithTag("GlowMaster");
        this.ParticlesUpperCut = GameObject.FindGameObjectWithTag("UppercutCharging");
        this.Empty = GameObject.FindGameObjectWithTag("EmptyDirection");
        this.TargetEnemyKick = Empty;
        this.TargetEnemyPunch = Empty;
        this.Enemies = new GameObject[0];
        this.EnemiesToHit = new GameObject[0];
        this.TargetEnemy = this.Empty;
        this.enemiesForHold = new GameObject[0];
        UpperCutTime = .25f;
        distanceToGrab = .3f;
        if (distanceToGrab == 0)
        {
            distanceToGrab = .3f;
        }
        else
        {
            distanceToGrab = this.gameObject.GetComponent<Swipe>().SensibilityLevel2 * 0.05f;
        }
    }

    public void UpperCut()
    {
        SearchTargetEnemyPunch();
        StartCoroutine(RotateCharacter(1));
        StartCoroutine(WaitToAttackInterval(1));
        this.enemiesForHold = new GameObject[0];
    }

    IEnumerator waitToShader()
    {
        yield return new WaitForSeconds(1f);
        _Presence = 1;
        uppercutSuccess = false;
    }
	
    public void Punch()
    {
        if (!this.Player.GetComponent<PlayerMovement>().jumpInProgress || (this.Player.GetComponent<PlayerMovement>().jumpInProgress && EnergyFill.GetComponent<Slider>().value >= .15f))
        {
            
            INPUTACTION actionDown = INPUTACTION.PUNCH;
            InputManager.CombatInputEvent(actionDown);
            StartCoroutine(AtackTime());
        }
    }
    public void Kick()
    {
        if (!this.Player.GetComponent<PlayerMovement>().jumpInProgress || (this.Player.GetComponent<PlayerMovement>().jumpInProgress && EnergyFill.GetComponent<Slider>().value >= .15f))
        {
           
            INPUTACTION actionDown = INPUTACTION.KICK;
            InputManager.CombatInputEvent(actionDown);
            StartCoroutine(AtackTime());
        }
        
    }
    
    public void Jump()
    {
        SearchObjective();
        posEnemy = new Vector3(Mathf.Abs(TargetEnemyKick.transform.position.normalized.x), 0, Mathf.Abs(TargetEnemyKick.transform.position.normalized.z));
        Player.GetComponent<PlayerMovement>().JumpForce = 7;
        INPUTACTION actionDown = INPUTACTION.JUMP;
        InputManager.CombatInputEvent(actionDown);
        StartCoroutine(AtackTimeJump());
    }
    private bool sideKickBool;

    IEnumerator sideKickBoo()
    {
        yield return new WaitForSeconds(1);
        sideKickBool = false;
    }
    public void SideKick()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {

            enemies[i].GetComponent<EnemyAI>().attackInterval = 500;
        }
        sideKickBool = true;
        StartCoroutine(sideKickBoo());
        SearchObjective();
        StartCoroutine(JustRotatePlayer());
        posEnemy = new Vector3(Mathf.Abs(TargetEnemyKick.transform.position.normalized.x), 0, Mathf.Abs(TargetEnemyKick.transform.position.normalized.z));
        Player.GetComponent<PlayerMovement>().JumpForce = 7;
        INPUTACTION actionDown = INPUTACTION.JUMP;
        InputManager.CombatInputEvent(actionDown);
        StartCoroutine(AttackTimeSideKick());
    }

    public void JumpKick()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {

            enemies[i].GetComponent<EnemyAI>().attackInterval = 500;
        }
        SearchObjective();
        StartCoroutine(JustRotatePlayer());
        posEnemy = new Vector3(Mathf.Abs(TargetEnemyKick.transform.position.normalized.x), 0, Mathf.Abs(TargetEnemyKick.transform.position.normalized.z));
        Player.GetComponent<PlayerMovement>().JumpForce = 7;
        INPUTACTION actionDown = INPUTACTION.JUMP;
        InputManager.CombatInputEvent(actionDown);
        StartCoroutine(AtackTimeJumpKick());

    }
    public void Defend()
    {
        this.defendButton.GetComponent<UIButton>().pressed = true;
        StartCoroutine(DefendTime());
    }
    
    IEnumerator DefendTime()
    {
        yield return new WaitForSeconds(1);
        this.defendButton.GetComponent<UIButton>().pressed = false;
    }

    IEnumerator AttackTimeSideKick()
    {
        yield return new WaitForSeconds(.02f);
        if (this.Player.GetComponent<PlayerMovement>().jumpInProgress)
        {
            yield return new WaitForEndOfFrame();
            posEnemy = new Vector3(Mathf.Abs(TargetEnemyKick.transform.position.normalized.x), 0, Mathf.Abs(TargetEnemyKick.transform.position.normalized.z));

        }
        yield return new WaitForSeconds(.03f);
        this.Player.GetComponent<PlayerCombat>().doAttack(this.Player.GetComponent<PlayerCombat>().Specials[4], UNITSTATE.JUMPKICK, INPUTACTION.JUMP);
        if (this.Player.GetComponent<PlayerMovement>().jumpInProgress)
        {
            GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<GainEnergy>().GainEnergyPunch(-0.15f);
        }
        this.Player.GetComponent<Rigidbody>().useGravity = true;
        StartCoroutine(WaitToAttackInterval(3));
    }

    IEnumerator AtackTimeJumpKick()
    {
        yield return new WaitForSeconds(.02f);
        if (this.Player.GetComponent<PlayerMovement>().jumpInProgress)
        {
            yield return new WaitForEndOfFrame();
            posEnemy = new Vector3(Mathf.Abs(TargetEnemyKick.transform.position.normalized.x), 0, Mathf.Abs(TargetEnemyKick.transform.position.normalized.z));

        }
        yield return new WaitForSeconds(.03f);
        Punch();
        if (this.Player.GetComponent<PlayerMovement>().jumpInProgress)
        {
            

            try
            {
                GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<GainEnergy>().GainEnergyPunch(-0.15f);
            }
            catch { }
        }
        StartCoroutine(WaitToAttackInterval(3));

    }
    IEnumerator AtackTimeJump()
    {
        yield return new WaitForSeconds(.01f);
        yield return new WaitForSeconds(.9f);
        INPUTACTION actionUp = INPUTACTION.NONE;
        InputManager.CombatInputEvent(actionUp);
    }
    IEnumerator AtackTime()
    {
        yield return new WaitForSeconds(1);
        INPUTACTION actionUp = INPUTACTION.NONE;
        InputManager.CombatInputEvent(actionUp);
        updateEveryFrame = false;
    }

    IEnumerator WaitToGrab()
    {
        yield return new WaitForEndOfFrame();
        this.Player.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Grabbed");
        yield return new WaitForSeconds(1f);
        this.Player.transform.GetChild(0).GetComponent<Animator>().SetTrigger("GrabHold");
    }

    float AttackInterval, Defense;

    
    IEnumerator WaitToHitGrab()
    {
        yield return new WaitForEndOfFrame();
        try
        {
            if (this.TargetEnemy.GetComponent<HealthSystem>().CurrentHp < 2)
            {
                this.TargetEnemy.transform.GetChild(1).GetComponent<Animator>().SetTrigger("Death");
            }
            else
            {
                this.TargetEnemy.transform.GetChild(1).GetComponent<Animator>().SetTrigger("Trapped");
            }
        }
        catch { }
    }
    IEnumerator AdjustGrabPos()
    {
        yield return new WaitForSeconds(.001f);
       
        try
        {
            bool done;
            if (this.Player.GetComponent<PlayerMovement>().currentDirection == DIRECTION.Right)
            {
                this.TargetEnemy.transform.position = new Vector3(this.Player.transform.position.x - 1, 0, this.Player.transform.position.z);
                done = true;
            }
            else
            {
                this.TargetEnemy.transform.position = new Vector3(this.Player.transform.position.x + 1, 0, this.Player.transform.position.z);
                done = true;
            }

            if (!done)
            {
                StartCoroutine(AdjustGrabPos());
            }
        }
        catch { InterruptGrabbing(); }

        if (swipecontrols.grabing)
        {
            StartCoroutine(AdjustGrabPos());
        }
    }

    private bool JumpRight,JumpLeft;
    private Vector3 posEnemy;

    // Update is called once per frame


    public void InterruptUppecut()
    {
        this.Player.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Idle");
        swipecontrols.holdTimer = 0;
        this.Player.transform.GetChild(3).GetChild(2).gameObject.SetActive(false);
        this.Player.transform.GetChild(3).GetComponent<ParticleSystem>().Stop();
        this._Presence = 1;
        StartCoroutine(WaitToAttackInterval(1));
        this.enemiesForHold = new GameObject[0];
    }

    public bool RunningToPunch;
    bool DirectionRight = false;

    public void RunningAndPunch()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemies.Length; i++)
        {
           
            enemies[i].GetComponent<EnemyAI>().attackInterval = 500;
        }

        swipecontrols.runningMad = true;
        this.RunningToPunch = true;
        this._RimPresence = 0.25f;
        StartCoroutine(WaitToRunningMad());
    }
    private float playerspeed;
    IEnumerator WaitToRunningMad()
    {
        yield return new WaitForEndOfFrame();
        this.playerspeed = this.Player.GetComponent<PlayerMovement>().walkSpeed;
        this.Player.GetComponent<PlayerMovement>().walkSpeed = 5f;
        this.Player.transform.GetChild(0).GetComponent<Animator>().SetTrigger("RunningMad 0");
        this.Player.transform.GetChild(0).GetComponent<Animator>().SetBool("RunningMad",true);
    }
    

    public void InterrumptRunningAndPunch()
    {
        TimeAfterRunning = 0;
        swipecontrols.runningMad = false;
        RunningToPunch = false;
        this.Player.transform.GetChild(0).GetComponent<Animator>().SetFloat("MovementSpeed", 0);
        this.Player.transform.GetChild(0).GetComponent<Animator>().SetBool("RunningMad", false);
        this.Player.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Idle");
        this.RunningToPunch = false;
        StartCoroutine(waitToRemovePresence());
        this.Player.GetComponent<PlayerMovement>().walkSpeed = playerspeed;
        StartCoroutine(WaitToAttackInterval(3));
    }

    IEnumerator WaitToAttackInterval(int a)
    {
        yield return new WaitForSeconds(a);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (!RunningToPunch && !swipecontrols.runningMad && swipecontrols.holdTimer <= 0 && !JumpSpecialInCourse)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyAI>().attackInterval = 2;
            }
        }
    }
   
    public void RunningPunch()
    {
        InterrumptRunningAndPunch();
        try
        {
            FirePunch.GetComponent<ParticleSystem>().Play();
            if (EnergyFill.GetComponent<Slider>().value >= .15f)
            {

                if (Energyball == null)
                {
                    this.Energyball = GameObject.FindGameObjectWithTag("EnergyBall");
                }
                Energyball.GetComponent<ParticleSystem>().Play();
                
            }
        }
        catch { }
        StartCoroutine(waitToRemovePresence());
        if(EnergyFill.GetComponent<Slider>().value >= .15f)
        {
            this.Player.GetComponent<PlayerCombat>().Specials[5].damage = this.Player.GetComponent<PlayerCombat>().Specials[5].damage + 2;
        }
        this.Player.GetComponent<PlayerCombat>().doAttack(this.Player.GetComponent<PlayerCombat>().Specials[5], UNITSTATE.PUNCH, INPUTACTION.PUNCH);
        StartCoroutine(WaitForRunningPunch(FirePunch));
      
        }


    IEnumerator waitToRemovePresence()
    {
        yield return new WaitForSeconds(1);
        this._RimPresence = 0;
    }
    IEnumerator WaitForRunningPunch(GameObject FirePunch)
    {
        yield return new WaitForEndOfFrame();
        try
        {
            FirePunch.GetComponent<ParticleSystem>().Stop();
            Energyball.GetComponent<ParticleSystem>().Stop();
        }
        catch { }
        TimeAfterRunning = 0;
        if (EnergyFill.GetComponent<Slider>().value >= .15f)
        {
              GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<GainEnergy>().GainEnergyPunch(-0.15f);
        }
    }

    private float TimeAfterRunning;
    public float jumpForce = 2;
    public bool TurningRun;
    public bool FarObjective;
    public bool JumpSpecialInCourse;
    private GameObject[] enemiesForHold;

    float t;
    void Update () {
        if (distanceToGrab == 0)
        {
            distanceToGrab = swipecontrols.SensibilityLevel2 * 0.05f;
        }
        if (TargetEnemyKick == null)
        {
            TargetEnemyKick = GameObject.FindGameObjectWithTag("EmptyDirection");
        }
        if (Player.GetComponent<HealthSystem>().CurrentHp > 0)
        {
            Shader.SetGlobalFloat("_Presence", this._Presence);
            Shader.SetGlobalFloat("_RimPresence", this._RimPresence);

            if (swipecontrols.Tap && !swipecontrols.grabing && !swipecontrols.runningMad && !this.Player.GetComponent<PlayerMovement>().jumpInProgress && !RunningToPunch)
            {
                PunchOrKick();
            }

            if (TurningRun)
            {
                if (DirectionRight)
                {
                    Vector2 dir = new Vector2(600, 0);
                    dir = dir.normalized;
                    swipecontrols.InputManager.dir = dir;
                }
                else
                {
                    Vector2 dir = new Vector2(-600, 0);
                    dir = dir.normalized;
                    swipecontrols.InputManager.dir = dir;
                }
            }
            try
            {
                if (!swipecontrols.grabing || this.TargetEnemy.GetComponent<HealthSystem>().CurrentHp <= 0 || Vector3.Distance(this.Player.transform.position, this.TargetEnemy.transform.position) > 1.5f)
                {
                    this.TargetEnemy.transform.GetChild(1).GetComponent<Animator>().SetBool("Trapped", false);
                    this.Player.transform.GetChild(0).GetComponent<Animator>().SetBool("GrabHolding", false);
                }
            }
            catch { }

            if ((swipecontrols.SwipeDown) && !swipecontrols.grabing && EnergyFill.GetComponent<Slider>().value >= .15f && !RunningToPunch)
            {
                SideKick();
            }

            //*****************************************************
            if ((swipecontrols.SwipeLeft || swipecontrols.SwipeRight) && !swipecontrols.grabing && this.Player.GetComponent<PlayerCombat>().itemInRange == null)
            {
                TimeAfterRunning = 0;
                if (swipecontrols.SwipeLeft)
                {
                    DirectionRight = false;
                }
                if (swipecontrols.SwipeRight)
                {
                    DirectionRight = true;
                }

                RunningAndPunch();

            }

            if (swipecontrols.Tap && TimeAfterRunning > .5f && !punching && RunningToPunch)
            {
                RunningPunch();
                RunningToPunch = false;
            }

            if (RunningToPunch)
            {
                
                TimeAfterRunning += Time.deltaTime;
                if (DirectionRight)
                {
                    Vector2 dir = new Vector2(100, 0);
                    dir = dir.normalized;
                    swipecontrols.InputManager.dir = dir;
                }
                else
                {
                    Vector2 dir = new Vector2(-100, 0);
                    dir = dir.normalized;
                    swipecontrols.InputManager.dir = dir;
                }
            }

            if (swipecontrols.Tap && TimeAfterRunning > .5f && !punching && RunningToPunch)
            {
                RunningPunch();
            }

            //**********************************************************************

            // JUMP SYSTEM

            if (swipecontrols.grabing && swipecontrols.SwipeUp && !swipecontrols.runningMad)
            {
                ThrowGrabbed();
            }
            else if (swipecontrols.SwipeUp && !swipecontrols.grabing && !swipecontrols.runningMad && !RunningToPunch)
            {
                if (Player.transform.position.x > this.TargetEnemyKick.transform.position.x)
                {
                    this.ManualRightJumpDirection = true;
                }
                else if (Player.transform.position.x < this.TargetEnemyKick.transform.position.x)
                {
                    this.ManualRightJumpDirection = false;
                }

                if ((swipecontrols.SwipeUp) && EnergyFill.GetComponent<Slider>().value < .15 && !swipecontrols.grabing)
                {
                    Jump();
                }
                else if ((swipecontrols.SwipeUp) && EnergyFill.GetComponent<Slider>().value >= .15f && !swipecontrols.grabing)
                {
                    JumpKick();
                }
            }

            try
            {
                if (this.Player.GetComponent<PlayerMovement>().jumpInProgress && !swipecontrols.grabing && !RunningToPunch)
                {
                    if (Vector3.Distance(TargetEnemyKick.transform.position, this.Player.transform.position) > 1 && !JumpRight && !JumpLeft && (EnergyFill.GetComponent<Slider>().value >= .15f || JumpSpecialInCourse))
                    {
                        {
                            if (this.right && !JumpLeft)
                            {
                                JumpRight = true;
                            }
                            else if (this.left && !JumpRight)
                            {
                                JumpLeft = true;

                            }
                        }
                    }

                    if (this.ManualRightJumpDirection && !this.Player.GetComponent<PlayerMovement>().IsGrounded())
                    { //right manual

                        this.Player.GetComponent<PlayerMovement>().updateDirection();

                        if (EnergyFill.GetComponent<Slider>().value < .15f && !sideKickBool)
                        {
                            if (this.Player.GetComponent<PlayerMovement>().currentDirection == DIRECTION.Right)
                            {
                                //this.Player.GetComponent<Rigidbody>().AddForce(Vector3.right * -1 * this.jumpForce, ForceMode.Impulse);
                                this.Player.GetComponent<Rigidbody>().AddForce(new Vector3(posEnemy.x * -1, 0, 0) * this.jumpForce, ForceMode.Impulse);
                            }
                        }
                        /*
                        if (EnergyFill.GetComponent<Slider>().value >= .15f && !sideKickBool && this.TargetEnemyKick == Empty)
                        {
                            Debug.Log("tornadoDerecha");
                            this._RimPresence = 1;
                            //   this.Player.GetComponent<Rigidbody>().AddForce(Vector3.right * -1 * this.jumpForce * 2, ForceMode.Impulse);
                            this.Player.GetComponent<Rigidbody>().AddForce(new Vector3(posEnemy.x * -1, 0, 0) * this.jumpForce * 2, ForceMode.Impulse);
                        }
                        if (EnergyFill.GetComponent<Slider>().value >= .15f && sideKickBool && this.TargetEnemyKick == Empty)
                        {
                            Debug.Log("sidederecha");
                            this._RimPresence = 1;
                            //this.Player.GetComponent<Rigidbody>().AddForce(Vector3.right  *-1* this.jumpForce * 2, ForceMode.Impulse);
                            this.Player.GetComponent<Rigidbody>().AddForce(new Vector3(posEnemy.x * -1, 0, 0) * this.jumpForce * 3, ForceMode.Impulse);
                        }*/
                        StartCoroutine(waitToRemovePresence());
                    }
                    else if (!this.ManualRightJumpDirection && !this.Player.GetComponent<PlayerMovement>().IsGrounded())
                    {
                        //left manual

                        this.Player.GetComponent<PlayerMovement>().updateDirection();

                        if (EnergyFill.GetComponent<Slider>().value < .15f && !sideKickBool)
                        {
                            if (this.Player.GetComponent<PlayerMovement>().currentDirection == DIRECTION.Left)
                            {
                                // this.Player.GetComponent<Rigidbody>().AddForce(Vector3.right * this.jumpForce, ForceMode.Impulse);
                                this.Player.GetComponent<Rigidbody>().AddForce(new Vector3(posEnemy.x, 0, 0) * this.jumpForce, ForceMode.Impulse);
                            }
                        }
                        /*
                        if (EnergyFill.GetComponent<Slider>().value >= .15f && !sideKickBool && this.TargetEnemyKick == Empty)
                        {
                            this._RimPresence = 1;
                            //this.Player.GetComponent<Rigidbody>().AddForce(Vector3.right * this.jumpForce * 2, ForceMode.Impulse);
                            this.Player.GetComponent<Rigidbody>().AddForce(new Vector3(posEnemy.x, 0, 0) * this.jumpForce * 2, ForceMode.Impulse);
                        }
                        if (EnergyFill.GetComponent<Slider>().value >= .15f && sideKickBool && this.TargetEnemyKick == Empty)
                        {

                            this._RimPresence = 1;
                            // this.Player.GetComponent<Rigidbody>().AddForce(Vector3.right  *this.jumpForce *2, ForceMode.Impulse);
                            this.Player.GetComponent<Rigidbody>().AddForce(new Vector3(posEnemy.x, 0, 0) * this.jumpForce * 3, ForceMode.Impulse);
                        }*/
                        StartCoroutine(waitToRemovePresence());
                    }

                    if (JumpRight && !this.Player.GetComponent<PlayerMovement>().IsGrounded() )
                    {
                        this.Player.GetComponent<PlayerMovement>().updateDirection();
                        if (Vector3.Distance(TargetEnemyKick.transform.position, this.swipecontrols.Player.transform.position) < 1.5f && !FarObjective)
                        {
                            posEnemy.x = posEnemy.x + .1f;
                            FarObjective = true;
                        }
                        if ((EnergyFill.GetComponent<Slider>().value >= .15f || JumpSpecialInCourse) && !sideKickBool )
                        {
                            JumpSpecialInCourse = true;
                            this._RimPresence = 0.25f;
                            if (this.Player.transform.position.z > this.TargetEnemyKick.transform.position.z)
                            {
                                this.Player.GetComponent<Rigidbody>().AddForce(new Vector3(posEnemy.x * -1, 0, (1 - posEnemy.z) * -1) * this.jumpForce * 2, ForceMode.Impulse);
                            }
                            else if (this.Player.transform.position.z < this.TargetEnemyKick.transform.position.z)
                            {
                                this.Player.GetComponent<Rigidbody>().AddForce(new Vector3(posEnemy.x * -1, 0, 1 - posEnemy.z) * this.jumpForce * 2, ForceMode.Impulse);
                            }
                            else
                            {
                                this.Player.GetComponent<Rigidbody>().AddForce(new Vector3(posEnemy.x * -1, 0, 0) * this.jumpForce * 2, ForceMode.Impulse);
                            }
                        }
                        else if ((EnergyFill.GetComponent<Slider>().value >= .15f || JumpSpecialInCourse) && sideKickBool )
                        {
                            JumpSpecialInCourse = true;
                            this._RimPresence = 0.25f;
                            if (this.Player.transform.position.z > this.TargetEnemyKick.transform.position.z)
                            {
                                this.Player.GetComponent<Rigidbody>().AddForce(new Vector3(posEnemy.x * -1, 0, (1 - posEnemy.z) * -1) * this.jumpForce * 3, ForceMode.Impulse);
                            }
                            else if(this.Player.transform.position.z < this.TargetEnemyKick.transform.position.z)
                            {
                                this.Player.GetComponent<Rigidbody>().AddForce(new Vector3(posEnemy.x * -1, 0, 1 - posEnemy.z) * this.jumpForce * 3, ForceMode.Impulse);
                            }
                             else
                            {
                                this.Player.GetComponent<Rigidbody>().AddForce(new Vector3(posEnemy.x * -1, 0, 0) * this.jumpForce * 3, ForceMode.Impulse);
                            }

                        }
                        StartCoroutine(waitToRemovePresence());
                    }
                    else if (JumpLeft && !this.Player.GetComponent<PlayerMovement>().IsGrounded() )
                    {
                        this.Player.GetComponent<PlayerMovement>().updateDirection();
                        if (Vector3.Distance(TargetEnemyKick.transform.position, this.swipecontrols.Player.transform.position) < 1.5f && !FarObjective)
                        {
                            posEnemy.x = posEnemy.x + .1f;
                            FarObjective = true;
                        }
                        if ((EnergyFill.GetComponent<Slider>().value >= .15f || JumpSpecialInCourse) && !sideKickBool)
                        {
                            JumpSpecialInCourse = true;
                            this._RimPresence = 0.25f;
                            if (this.Player.transform.position.z > this.TargetEnemyKick.transform.position.z)
                            {
                                this.Player.GetComponent<Rigidbody>().AddForce(new Vector3(posEnemy.x, 0, (1 - posEnemy.z) * -1) * this.jumpForce * 2, ForceMode.Impulse);
                            }
                            else if (this.Player.transform.position.z < this.TargetEnemyKick.transform.position.z)
                            {
                                this.Player.GetComponent<Rigidbody>().AddForce(new Vector3(posEnemy.x, 0, 1 - posEnemy.z) * this.jumpForce * 2, ForceMode.Impulse);
                            }
                            else
                            {
                                this.Player.GetComponent<Rigidbody>().AddForce(new Vector3(posEnemy.x, 0, 0) * this.jumpForce * 2, ForceMode.Impulse);
                            }
                        }
                        else if ((EnergyFill.GetComponent<Slider>().value >= .15f || JumpSpecialInCourse) && sideKickBool)
                        {
                            JumpSpecialInCourse = true;
                            this._RimPresence = 0.25f;
                            if (this.Player.transform.position.z > this.TargetEnemyKick.transform.position.z)
                            {
                                this.Player.GetComponent<Rigidbody>().AddForce(new Vector3(posEnemy.x, 0, (1 - posEnemy.z) * -1) * this.jumpForce * 3, ForceMode.Impulse);
                            }
                            else if (this.Player.transform.position.z < this.TargetEnemyKick.transform.position.z)
                            {
                                this.Player.GetComponent<Rigidbody>().AddForce(new Vector3(posEnemy.x, 0, 1 - posEnemy.z) * this.jumpForce * 3, ForceMode.Impulse);
                            }
                            else
                            {
                                this.Player.GetComponent<Rigidbody>().AddForce(new Vector3(posEnemy.x, 0, 0) * this.jumpForce * 3, ForceMode.Impulse);
                            }

                        }
                        StartCoroutine(waitToRemovePresence());
                    }
                }
                else
                {
                    JumpRight = JumpLeft = FarObjective = JumpSpecialInCourse= false;

                }
            }
            catch { }

            try
            {
                if (TargetEnemy == null || TargetEnemy.GetComponent<EnemyAI>().isDead)
                {
                    this.Player.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Idle");
                    swipecontrols.grabing = false;
                    this.TargetEnemy = Empty;
                    this.Player.GetComponent<HealthSystem>().invulnerable = false;
                    this.TimeGrabbed = 0;
                }
            }
            catch { }

            //JumpKickDirections
            try
            {
                if (this.TargetEnemyKick.transform.position.x < this.Player.transform.position.x && !right)
                {
                    right = true;
                    left = false;
                }
                else if (this.TargetEnemyKick.transform.position.x > this.Player.transform.position.x && !left)
                {
                    right = false;
                    left = true;
                }
            }
            catch { }

            //*******************************************
            if (swipecontrols.walking && !swipecontrols.grabing && !swipecontrols.runningMad && !punching && !RunningToPunch && !this.Player.GetComponent<PlayerMovement>().jumpInProgress && swipecontrols.TimeToSwap>0.4f && !this.GetComponent<Swipe>().BlockGrab)
            {
                SearchAndGrabEnemy();
            }
            if (swipecontrols.Tap && swipecontrols.grabing)
            {
                GrabHit();
            }

            if (swipecontrols.grabing)
            {
                GrabberTimer();
            }
            //*********************************************



            if (swipecontrols.Hold && !swipecontrols.grabing && !swipecontrols.runningMad && !swipecontrols.Tap && !RunningToPunch)
            {
                UpperCut();
            }
            if (swipecontrols.holdTimer < UpperCutTime && swipecontrols.holdTimer > UpperCutTime/3 && !swipecontrols.runningMad && !swipecontrols.grabing && !RunningToPunch)
            {
                _Presence = 1 - Mathf.Clamp01(swipecontrols.holdTimer - 0.5f);
                ParticlesUpperCut.GetComponent<ParticleSystem>().Play();
                if (enemiesForHold.Length== 0)
                {
                   this.enemiesForHold = GameObject.FindGameObjectsWithTag("Enemy");
                   
                }
                if ( enemiesForHold.Length!=0)
                {
                    try
                    {
                        if (enemiesForHold[0].gameObject.GetComponent<EnemyAI>().attackInterval != 500)
                        {
                            //Charging
                            for (int i = 0; i < enemiesForHold.Length; i++)
                            {
                                enemiesForHold[i].GetComponent<EnemyAI>().attackInterval = 500;
                            }
                        }
                    }
                    catch { }
                }
            }
            else if (swipecontrols.holdTimer == 0)
            {
                try
                {
                    GlowUppercut.SetActive(false);
                }
                catch { this.GlowUppercut = GameObject.FindGameObjectWithTag("GlowMaster"); }
                ParticlesUpperCut.GetComponent<ParticleSystem>().Stop();
                if (!uppercutSuccess)
                {
                    this._Presence = 1;
                }

                if (enemiesForHold.Length == 0)
                {
                    t += Time.deltaTime;
                    if (t > 3)
                    {
                        //SEARCHING
                        this.enemiesForHold = GameObject.FindGameObjectsWithTag("Enemy");
                        t = 0;
                    }

                }

                if (enemiesForHold.Length !=0 && !RunningToPunch && !swipecontrols.runningMad)
                {
                    try
                    {
                        if (enemiesForHold[0].gameObject.GetComponent<EnemyAI>().attackInterval == 500)
                        {
                            StartCoroutine(WaitToAttackInterval(1));
                        }
                    }
                    catch
                    {
                        this.enemiesForHold = new GameObject[0];
                    }
                }
            }
            else if (swipecontrols.holdTimer > UpperCutTime && !swipecontrols.runningMad && !swipecontrols.grabing && !RunningToPunch)
            {
                if (EnergyFill.GetComponent<Slider>().value < .15f)
                {
                    this.Player.GetComponent<PlayerCombat>().Specials[6].damage = 2;
                }
                else
                {
                    this.Player.GetComponent<PlayerCombat>().Specials[6].damage = 4;
                    try
                    {
                        GlowUppercut.SetActive(true);
                    }
                    catch { this.GlowUppercut = GameObject.FindGameObjectWithTag("GlowMaster"); }
                }

                if (enemiesForHold.Length == 0)
                {
                    this.enemiesForHold = GameObject.FindGameObjectsWithTag("Enemy");

                }
                if (enemiesForHold.Length != 0)
                {
                    try
                    {
                        if (enemiesForHold[0].gameObject.GetComponent<EnemyAI>().attackInterval != 500)
                        {
                            //CHARGING
                            for (int i = 0; i < enemiesForHold.Length; i++)
                            {
                                enemiesForHold[i].GetComponent<EnemyAI>().attackInterval = 500;
                            }
                        }
                    }
                    catch { }
                }
            }
        }
    }

    IEnumerator JustRotatePlayer()
    {
        if (TargetEnemyKick != Empty)
        {
            this.Player.GetComponent<PlayerMovement>().rotationSpeed = 500;
            TurningRun = true;


            if (Vector3.Distance(this.Player.transform.position, this.TargetEnemyKick.transform.position) < 25f && TargetEnemyKick != Empty)
            {

                //player is in the left side position
                if (TargetEnemyKick.transform.position.x < Player.transform.position.x)
                {
                    //if player is looking to the left
                    if (this.Player.GetComponent<PlayerMovement>().currentDirection == DIRECTION.Left)
                    {
                        //turn player to the right
                        DirectionRight = true;
                        this.Player.GetComponent<PlayerMovement>().SetDirection(DIRECTION.Right);
                        this.Player.GetComponent<PlayerMovement>().updateDirection();
                    }
                    else
                    {
                        TurningRun = false;
                        this.Player.GetComponent<PlayerMovement>().rotationSpeed = 10;
                    }
                }
                //player is in the right side position
                else if (TargetEnemyKick.transform.position.x > Player.transform.position.x)
                {
                    //if player is looking to the right
                    if (this.Player.GetComponent<PlayerMovement>().currentDirection == DIRECTION.Right)
                    {
                        //turn player to the left;
                        DirectionRight = false;
                        this.Player.GetComponent<PlayerMovement>().SetDirection(DIRECTION.Left);
                        this.Player.GetComponent<PlayerMovement>().updateDirection();
                    }
                    else
                    {
                        TurningRun = false;
                        this.Player.GetComponent<PlayerMovement>().rotationSpeed = 10;
                    }
                }

            }
        }
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(.1f);
        TurningRun = false;
        this.Player.GetComponent<PlayerMovement>().rotationSpeed = 10;
    }

    

    public void SearchObjective()
    {
        this.TargetEnemyKick = Empty;
        this.EnemiesToHit = GameObject.FindGameObjectsWithTag("Enemy");
        float prevDist=100;
        for (int i = 0; i < EnemiesToHit.Length; i++)
        {
            if (EnemiesToHit[i].gameObject == null)
            {
                SearchObjective();
            }
            else if (Vector3.Distance(this.Player.transform.position,EnemiesToHit[i].transform.position ) <= 10f && !EnemiesToHit[i].GetComponent<EnemyAI>().isDead && EnemiesToHit[i].GetComponent<HealthSystem>().CurrentHp !=0)
            {
                if(prevDist> Vector3.Distance(this.Player.transform.position, EnemiesToHit[i].transform.position)){
                    this.TargetEnemyKick = this.EnemiesToHit[i];
                    prevDist = Vector3.Distance(this.Player.transform.position, EnemiesToHit[i].transform.position);
                }
                
            }
        }

        if (Vector3.Distance(this.Player.transform.position, TargetEnemyKick.transform.position) > 7 || EnergyFill.GetComponent<Slider>().value <.15f)
        {
            this.TargetEnemyKick = Empty;
        }


    }
  
    public void SearchTargetEnemyPunch()
    {
        if (!swipecontrols.grabing)
        {
            this.TargetEnemyPunch = Empty;
            this.EnemiesToHit = GameObject.FindGameObjectsWithTag("Enemy");
            float prevDist = 100;
            for (int i = 0; i < EnemiesToHit.Length; i++)
            {
                if (EnemiesToHit[i].gameObject == null)
                {
                    SearchTargetEnemyPunch();

                }
                else if (Vector3.Distance(this.swipecontrols.Player.transform.position, EnemiesToHit[i].transform.position) <= 10f && !EnemiesToHit[i].GetComponent<EnemyAI>().isDead && EnemiesToHit[i].GetComponent<HealthSystem>().CurrentHp != 0)
                {
                    if (prevDist > Vector3.Distance(this.Player.transform.position, EnemiesToHit[i].transform.position))
                    {
                        this.TargetEnemyPunch = this.EnemiesToHit[i];
                        
                        prevDist = Vector3.Distance(this.Player.transform.position, EnemiesToHit[i].transform.position);

                    }
                }
            }
        }
    }

    public void SearchAndGrabEnemy()
    {
        float prevDist = 100;
        this.Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < Enemies.Length; i++)
        {
            try
            {
                if (TargetEnemy == Empty)
                {
                    if (prevDist > Vector3.Distance(this.Player.transform.position, Enemies[i].transform.position))
                    {
                        this.TargetEnemy = this.Enemies[i];
                        prevDist = Vector3.Distance(this.Player.transform.position, Enemies[i].transform.position);
                    }
                }
                if ((Enemies[i].GetComponent<EnemyAI>().isGrounded && !Enemies[i].GetComponent<EnemyAI>().isDead && EnemiesToHit[i].GetComponent<HealthSystem>().CurrentHp > 0))
                {
                    if (prevDist > Vector3.Distance(this.Player.transform.position, Enemies[i].transform.position))
                    {
                        this.TargetEnemy = this.Enemies[i];
                        prevDist = Vector3.Distance(this.Player.transform.position, Enemies[i].transform.position);
                    }
                }
            }
            catch
            {
                this.Enemies = GameObject.FindGameObjectsWithTag("Enemy");
            }
        }
        if (this.TargetEnemy != Empty && Vector3.Distance(TargetEnemy.transform.position, this.Player.transform.position) <= this.distanceToGrab && this.TargetEnemy.GetComponent<HealthSystem>().CurrentHp > 0 && !this.TargetEnemy.GetComponent<EnemyAI>().isDead && this.TargetEnemy.GetComponent<EnemyAI>().isGrounded)
        {
            if (TargetEnemy.transform.position.x < Player.transform.position.x)
            {
                Vector2 dir = new Vector2(1, 0);
                dir = dir.normalized;
                swipecontrols.InputManager.dir = dir;
            }
            else
            {
                Vector2 dir = new Vector2(-1, 0);
                dir = dir.normalized;
                swipecontrols.InputManager.dir = dir;
            }
            this.Player.GetComponent<HealthSystem>().invulnerable = true;
            swipecontrols.grabing = true;
            try
            {
                this.TargetEnemy.GetComponent<EnemyAI>().enableAI = false;

                this.TargetEnemy.transform.GetChild(1).GetComponent<Animator>().SetBool("Trapped",true);
                this.Player.transform.GetChild(0).GetComponent<Animator>().SetBool("GrabHolding", true);

                if (this.Player.transform.position.x > this.TargetEnemy.transform.position.x)
                {
                    this.TargetEnemy.transform.position = new Vector3(this.Player.transform.position.x - 1, 0, this.Player.transform.position.z);
                }
                else
                {
                    this.TargetEnemy.transform.position = new Vector3(this.Player.transform.position.x + 1, 0, this.Player.transform.position.z);
                }
            }
            catch { InterruptGrabbing(); }
            StartCoroutine(WaitToGrab());
            StartCoroutine(AdjustGrabPos());
        }
    }
    
    public void GrabHit()
    {
        if (TimeGrabbed < timeGrabbingEnemy - 0.55f)
        {
            try
            {
                if (TargetEnemy.transform.position.x < Player.transform.position.x)
                {
                    Vector2 dir = new Vector2(1, 0);
                    dir = dir.normalized;
                    swipecontrols.InputManager.dir = dir;
                }
                else
                {
                    Vector2 dir = new Vector2(-1, 0);
                    dir = dir.normalized;
                    swipecontrols.InputManager.dir = dir;
                }
            }
            catch { InterruptGrabbing(); }
          //  this.Player.transform.GetChild(0).GetComponent<Animator>().SetBool("GrabHolding", false);
            this.Player.GetComponent<PlayerCombat>().doAttack(this.Player.GetComponent<PlayerCombat>().Specials[2], UNITSTATE.PUNCH, INPUTACTION.PUNCH);
            try
            {
                if (!this.TargetEnemy.GetComponent<EnemyAI>().isDead)
                {
                    StartCoroutine(WaitToHitGrab());
                }
            }
            catch { InterruptGrabbing(); }
        }else
        {
            this.Player.transform.GetChild(0).GetComponent<Animator>().SetBool("GrabHolding", true);
        }
    }
    

    public void PunchOrKick()
    {
        if (!swipecontrols.grabing)
        {
            SearchTargetEnemyPunch();
            punching = true;
            this.Items = GameObject.FindGameObjectsWithTag("Item");
            this.Item = this.Empty;
            for (int i = 0; i < Items.Length; i++)
            {

                if (Vector3.Distance(Items[i].transform.position, this.Player.transform.position) <= ItemReach)
                {
                    this.Item = this.Items[i];
                }
            }
            this.Loots = GameObject.FindGameObjectsWithTag("Loot");
            this.Loot = this.Empty;
            for (int i = 0; i < Loots.Length; i++)
            {

                if (Vector3.Distance(Loots[i].transform.position, this.Player.transform.position) <= LootReach)
                {
                    this.Loot = this.Loots[i];
                }
            }

            if (this.Loot != this.Empty)
            {
                TargetEnemyPunch = this.Loot;
            }

            if (Vector3.Distance(this.Player.transform.position, this.Item.transform.position) <= ItemReach)
            {
                Punch();
            }

            StartCoroutine(RotateCharacter(0));
        }
        
    }
    bool punching;

    IEnumerator RotateCharacter(int AttackNumber)
    {
        swipecontrols.LetPunchRotate = true;
        if (TargetEnemyPunch != Empty)
        {
            this.Player.GetComponent<PlayerMovement>().rotationSpeed = 500;
            TurningRun = true;


            if (Vector3.Distance(this.Player.transform.position, this.TargetEnemyPunch.transform.position) < 10 )
            {

                //player is in the left side position
                if (TargetEnemyPunch.transform.position.x < Player.transform.position.x)
                {
                    //if player is looking to the left
                    if (this.Player.GetComponent<PlayerMovement>().currentDirection == DIRECTION.Left)
                    {
                        //turn player to the right
                        DirectionRight = true;
                        this.Player.GetComponent<PlayerMovement>().SetDirection(DIRECTION.Right);
                        this.Player.GetComponent<PlayerMovement>().updateDirection();
                    }
                    else
                    {
                        TurningRun = false;
                        this.Player.GetComponent<PlayerMovement>().rotationSpeed = 10;
                    }

                }
                //player is in the right side position
                else if (TargetEnemyPunch.transform.position.x > Player.transform.position.x)
                {
                    //if player is looking to the right
                    if (this.Player.GetComponent<PlayerMovement>().currentDirection == DIRECTION.Right)
                    {
                        //turn player to the left;
                        DirectionRight = false;
                        this.Player.GetComponent<PlayerMovement>().SetDirection(DIRECTION.Left);
                        this.Player.GetComponent<PlayerMovement>().updateDirection();
                    }
                    else
                    {
                        TurningRun = false;
                        this.Player.GetComponent<PlayerMovement>().rotationSpeed = 10;
                    }
                }
               
            }

           
        }

        yield return new WaitForEndOfFrame();
        if (AttackNumber == 0)
        {
            int r1 = Random.Range(0, 2);
            if (r1 == 0)
            {
                Punch();
            }
            else
            {
                Kick();
            }
        }
        if (AttackNumber == 1)
        {
            this.swipecontrols.Player.GetComponent<PlayerCombat>().doAttack(this.Player.GetComponent<PlayerCombat>().Specials[6], UNITSTATE.PUNCH, INPUTACTION.PUNCH);
            uppercutSuccess = true;
            if (EnergyFill.GetComponent<Slider>().value >= .15f)
            {
                GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<GainEnergy>().GainEnergyPunch(-0.15f);
            }
            StartCoroutine(waitToShader());
        }

        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(.2f);
        TurningRun = false;
        punching = false;
        this.Player.GetComponent<PlayerMovement>().rotationSpeed = 10;
        swipecontrols.LetPunchRotate = false;
    }

    public void GrabberTimer()
    {
        this.TimeGrabbed += Time.deltaTime;
        if (this.TimeGrabbed > timeGrabbingEnemy)
        {
            ThrowGrabbed();
        }
        try
        {
            if (this.TimeGrabbed < timeGrabbingEnemy +.25f && this.TargetEnemy.GetComponent<EnemyAI>().isDead)
            {
                InterruptGrabbing();
            }
        }
        catch { InterruptGrabbing(); }
    }


    IEnumerator WaitToFinishGrab()
    {
        this.Player.GetComponent<HealthSystem>().invulnerable = false;
        float speed = this.TargetEnemy.GetComponent<EnemyAI>().walkSpeed;
         this.TargetEnemy.GetComponent<EnemyAI>().walkSpeed = 0;
        try
        {
            this.TargetEnemy.GetComponent<EnemyAI>().enableAI = false;
        }
        catch { InterruptGrabbing(); }
        if (TargetEnemy.transform.position.x < Player.transform.position.x)
        {
            Vector2 dir = new Vector2(1, 0);
            dir = dir.normalized;
            swipecontrols.InputManager.dir = dir;
        }
        else
        {
            Vector2 dir = new Vector2(-1, 0);
            dir = dir.normalized;
            swipecontrols.InputManager.dir = dir;
        }
        this.Player.transform.GetChild(0).GetComponent<Animator>().SetBool("GrabHolding", false);
        this.TargetEnemy.transform.GetChild(1).GetComponent<Animator>().SetBool("Trapped", false);
        this.TimeGrabbed = 0;


        try
        {
            TargetEnemy.GetComponent<EnemyAI>().enableAI = true;
        }
        catch { InterruptGrabbing(); }
        this.Player.GetComponent<PlayerCombat>().doAttack(this.Player.GetComponent<PlayerCombat>().Specials[6], UNITSTATE.PUNCH, INPUTACTION.PUNCH);
        if (TargetEnemy.transform.position.x < Player.transform.position.x)
        {
            this.TargetEnemy.transform.position = new Vector3(this.TargetEnemy.transform.position.x , this.TargetEnemy.transform.position.y, this.TargetEnemy.transform.position.z);
        }
        else if(TargetEnemy.transform.position.x > Player.transform.position.x)
        {
            this.TargetEnemy.transform.position = new Vector3(this.TargetEnemy.transform.position.x , this.TargetEnemy.transform.position.y, this.TargetEnemy.transform.position.z);
        }
        swipecontrols.grabing = false;
        GameObject oldTarget = this.TargetEnemy;
        this.TargetEnemy = Empty;
        
        yield return new WaitForSeconds(.45f);
      
        try
        {
            oldTarget.GetComponent<EnemyAI>().walkSpeed = speed;
            oldTarget.GetComponent<EnemyAI>().attackInterval = AttackInterval;
            oldTarget.GetComponent<EnemyAI>().defendChance = Defense;
        }
        catch { this.TargetEnemy = Empty; }
        InterruptGrabbing();
        this.Player.transform.GetChild(0).GetComponent<Animator>().SetBool("GrabHolding", false);
        this.Player.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Idle");
       

    }

    public void ThrowGrabbed()
    {
        this.AttackInterval = this.TargetEnemy.GetComponent<EnemyAI>().attackInterval;
        this.Defense = this.TargetEnemy.GetComponent<EnemyAI>().defendChance;
        this.TargetEnemy.GetComponent<EnemyAI>().defendChance = 0;
        this.TargetEnemy.GetComponent<EnemyAI>().attackInterval = 100;
        StartCoroutine(WaitToFinishGrab());
      
    }

    IEnumerator WaitToInterrumptGrabbing()
    {
        yield return new WaitForSeconds(1);
        InterruptGrabbing();
    }
    public void InterruptGrabbing()
    {
        try
        {
            for (int j = 0; j < Enemies.Length; j++)
            {
                Enemies[j].GetComponent<EnemyAI>().enableAI = true;
                this.TargetEnemy.GetComponent<EnemyAI>().defendChance = Defense;
                this.TargetEnemy.GetComponent<EnemyAI>().attackInterval = AttackInterval;
            }
            this.TargetEnemy.transform.GetChild(1).GetComponent<Animator>().SetBool("Trapped", false);
            this.Player.transform.GetChild(0).GetComponent<Animator>().SetBool("GrabHolding", false);
            this.Player.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Idle");
            this.TargetEnemy.transform.GetChild(1).GetComponent<Animator>().SetTrigger("Hit1");
            this.Player.GetComponent<HealthSystem>().invulnerable = false;
            swipecontrols.grabing = false;
            this.TargetEnemy = Empty;
            this.TimeGrabbed = 0;
        }
        catch { }
    }
  
}
