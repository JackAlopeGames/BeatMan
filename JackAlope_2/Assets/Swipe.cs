using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swipe : MonoBehaviour {

    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown, hold; 
    private Vector2 startTouch, swipeDelta;
    private bool isDraging =false; 
    public Vector2 SwipeDelta {  get { return swipeDelta; } }

    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    public bool Tap { get { return tap; } set { tap = value; } }
    public bool Hold { get { return hold; } }
    public GameObject Player { get { return player; } }

    private InputManager inputmanager;
    public InputManager InputManager { get { return inputmanager; } }
    private GameObject player;
    private Animator Anim;
    private float TimeToRun;
    private GameObject Pointer;
    private GameObject RotPointer;
    public bool walking;
    public bool grabing;
    // Use this for initialization
    private bool right, left, down, up,upper, middle;

    public float centerX, centerY;
    public GameObject fingerPrint;
    private SwipeControls swipe;
    private float HoldTimer;
    private bool DraggingMoving = true;
    private bool RunningMad;
    public bool runningMad { get { return this.RunningMad; } set { this.RunningMad = value; } }
    public bool draggingMoving { get { return DraggingMoving; } set { DraggingMoving = value; } }
    public float holdTimer { get { return HoldTimer; } set { HoldTimer = value; } }
    public float TimeToSwap;
    public bool allowToZero;
    public bool AllowToZero { get { return this.allowToZero; } set { this.allowToZero = value; } }
    public bool JustJumped;
    private float timeMoving;
    public float TimeMoving { get { return timeMoving; } set { timeMoving = value; } }
    private bool NotHold;
    private float HoldForStartHold;
    public float magnitude;
    float magnitudejump;
    public GameObject energybar;
    public float timeToMove;
    float timetoSwapForLeftRight;
    float timeDivideded;
    public  float walkspeed;
    Vector2 Center;
    float PrevPosY;
    float Changes,Same;
    bool DirRight;
    bool notRotate = false;
    float maxlarge;
    float ang;
    Vector2 dir = Vector2.zero;
    float Decix, screen, Lerx, Deciy, Lery;
    float x, y;
    public bool LetPunchRotate;
    public int SensibilityLevel, SensibilityLevel2;
    private GameObject ControlSensibility;
    public float UpperCutTime;
    public bool BlockForTutorial, BlockPunchTap;
    public bool BlockGrab, BlockRunningPunch, BlockJump,BlockHold;
    public void Start()
    {
        BlockRunningPunch = true;
        BlockHold = true;
        UpperCutTime = .25f;
        try
        {
            ControlSensibility = GameObject.FindGameObjectWithTag("FpsManager");
            this.SensibilityLevel = ControlSensibility.GetComponent<ControlSensibility>().Level;
            SensibilityLevel2 = this.SensibilityLevel;
        }
        catch { }
        energybar = GameObject.FindGameObjectWithTag("EnergyBar");
        inputmanager = GameObject.FindObjectOfType<InputManager>();
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.Anim = this.player.transform.GetChild(0).GetComponent<Animator>();
        this.Pointer = GameObject.FindGameObjectWithTag("Pointer");
        this.RotPointer = GameObject.FindGameObjectWithTag("RotPointer");
        float screenWidth = Screen.width / Screen.dpi;
        float screenHeight = Screen.height / Screen.dpi;
        float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));
        DirRight = true;
        HoldForStartHold = 0;
        HoldTimer = 0;
        hold = false;
        //Debug.Log("Getting device inches: " + diagonalInches);

        if (diagonalInches > 6.5f)
        {

            // Tablets
            magnitude = Screen.width / 6.5f;
            magnitudejump = Screen.width / 8.5f;
            timeDivideded = 1.5f;
        }
        else
        {
            magnitude = Screen.width / 3.5f;
            magnitudejump = Screen.width / 4.5f;
            timeDivideded = 1.5f;
        }
        magnitude = Screen.width / 10;
        try
        {
            SensibilityLevel = 11 - SensibilityLevel;
            magnitude = Screen.width / 10 * .16f * SensibilityLevel;
        }
        catch {
            magnitude = Screen.width / 10;
        }

        try
        {
            UpdateSensibility();
        }
        catch { }

    }

    public void UpdateSensibility()
    {
        this.SensibilityLevel = ControlSensibility.GetComponent<ControlSensibility>().Level;
        SensibilityLevel = 11 - SensibilityLevel;
        if (SensibilityLevel == 6)
        {
            magnitude = Screen.width / 10 * .16f * SensibilityLevel;
            SensibilityLevel2 = 12 - SensibilityLevel;
            this.GetComponent<SwipeControls>().distanceToGrab = SensibilityLevel2 * 0.05f;
        }
        else if(SensibilityLevel>6)
        {
            magnitude = Screen.width / 10 * .16f * SensibilityLevel *3;
            SensibilityLevel2 = 12 - SensibilityLevel;
            this.GetComponent<SwipeControls>().distanceToGrab = SensibilityLevel2 * 0.05f * 3;
        }
        else if (SensibilityLevel < 6)
        {
            magnitude = Screen.width / 10 * .16f * SensibilityLevel / 2;
            SensibilityLevel2 = 12 - SensibilityLevel;
            this.GetComponent<SwipeControls>().distanceToGrab = SensibilityLevel2 * 0.05f * 3;
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
        right = left =down=up = upper= middle = false;
        HoldTimer = 0;
        timeMoving = 0;
        TimeToSwap = 0;
        walkspeed = 0;
        if (timeToMove > 1 && !RunningMad)
        {
           this.Anim.SetTrigger("Idle");
        }
        timeToMove = 0;
        PrevPosY = 0;
        Changes = 0;
        Same = 0;
        notRotate = false;
    }
    

    IEnumerator JustJumpedWait()
    {
        JustJumped = true;
        yield return new WaitForSeconds(.8f);
        this.player.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Idle");
        JustJumped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (BlockGrab && this.GetComponent<SwipeControls>().distanceToGrab!=0)
        {
            this.GetComponent<SwipeControls>().distanceToGrab = 0;
        }
        tap = swipeDown = swipeUp = swipeLeft = swipeRight = hold = false;
        
        if (Input.GetMouseButton(0))
        {
            timeToMove += Time.deltaTime;
            if (HoldForStartHold <= .15f && !grabing && !BlockForTutorial && !BlockHold)
            {
                HoldForStartHold += Time.deltaTime;
            }
            if (this.HoldTimer <= UpperCutTime && !NotHold && HoldForStartHold>=.15f && !grabing && !BlockForTutorial && !BlockHold)
            {
                this.HoldTimer += Time.deltaTime;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            HoldForStartHold = 0;
            HoldTimer = 0;
            hold = false;
            try
            {
                this.fingerPrint.SetActive(true);
            }
            catch { }
            centerX = Input.mousePosition.x;
            centerY = Input.mousePosition.y;
            Center = new Vector2(centerX, centerY);
            try
            {
                this.fingerPrint.transform.position = new Vector2(centerX, centerY);
            }
            catch { }
            isDraging = true;
            startTouch = Input.mousePosition;

            if(this.player.GetComponent<PlayerCombat>().currentDirection == DIRECTION.Right)
            {
                this.DirRight = true;
            }
            else if(this.player.GetComponent<PlayerCombat>().currentDirection == DIRECTION.Left)
            {
                this.DirRight = false;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            try
            {
                this.fingerPrint.SetActive(false);
            }
            catch { }
            if(this.HoldTimer >= UpperCutTime && !NotHold)
            {
                hold = true;
            }else
            {
                if (timeMoving < .1f && !JustJumped && !BlockPunchTap)
                {
                    tap = true;
                }
            }
            isDraging = false;

            if (!BlockForTutorial)
            {

                //antes estaba la magnitud completa y sin timedivided
                if (DirRight && this.player.GetComponent<PlayerCombat>().currentDirection == DIRECTION.Right || !DirRight && this.player.GetComponent<PlayerCombat>().currentDirection == DIRECTION.Left)
                {
                    if (swipeDelta.magnitude >= (magnitude) / 2 && TimeToSwap < (.4f) && !JustJumped && !BlockJump)
                    {
                        x = swipeDelta.x;
                        y = swipeDelta.y;
                        if ((Mathf.Abs(x) > Mathf.Abs(y)))
                        {

                        }
                        else
                        {
                            //up or down
                            if (y < 0)
                            {
                                tap = false;
                                swipeDown = true;
                                if (energybar.GetComponent<Slider>().value >= .15f)
                                {
                                    StartCoroutine(JustJumpedWait());
                                }
                                try
                                {
                                    fingerPrint.SetActive(false);
                                }
                                catch { }

                            }
                            else
                            {
                                tap = false;
                                swipeUp = true;
                                StartCoroutine(JustJumpedWait());
                            }
                            Reset();
                            NotHold = true;
                        }

                    }
                }

                //antes estaba timetoswapforleftright y magnitude completa
                //the 0.35 worked before
                if (swipeDelta.magnitude >= (magnitude) / 2 && TimeToSwap < (.4f) && !JustJumped && !BlockRunningPunch)
                {
                    timetoSwapForLeftRight = 0.25f;
                    x = swipeDelta.x;
                    y = swipeDelta.y;
                    if ((Mathf.Abs(x) > Mathf.Abs(y)))
                    {
                        //left or right
                        if (x < 0)
                        {
                            tap = false;

                            swipeLeft = true;
                        }
                        else
                        {
                            tap = false;
                            swipeRight = true;
                        }
                        Reset();
                        NotHold = true;
                    }


                }
            }
            Reset();
        }
        

      
        //calculate distance
        if (DraggingMoving)
        {
           
            if (isDraging)
            {

                 this.TimeToSwap += Time.deltaTime;
                
                if (Input.touches.Length > 0)
                {
                    swipeDelta = Input.touches[0].position - startTouch;

                }
                else if (Input.GetMouseButton(0))
                {

                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
                }

            }
            else
            {
                swipeDelta = Vector2.zero;
                this.HoldTimer = 0;
                this.HoldForStartHold = 0;
            }

            if (SwipeDelta.magnitude > 20)
            {
                tap = false;
            }

            if (!RunningMad && timetoSwapForLeftRight != .30f / timeDivideded)
            {
                timetoSwapForLeftRight = .30f / timeDivideded;
            }

            
            if (!this.player.GetComponent<PlayerMovement>().jumpInProgress && !Tap && !hold && !swipeLeft && !swipeRight && !swipeDown && !swipeUp && Input.GetMouseButton(0) && (Vector2)Input.mousePosition != startTouch && !grabing && (this.player.GetComponent<UnitState>().currentState != UNITSTATE.KICK || this.player.GetComponent<UnitState>().currentState != UNITSTATE.PUNCH || this.player.GetComponent<UnitState>().currentState != UNITSTATE.HIT) && !BlockForTutorial)
            {
                if (RunningMad)
                {
                    this.gameObject.GetComponent<SwipeControls>().InterrumptRunningAndPunch();
                    this.Anim.SetBool("StartRunning", true);
                }

               
                this.player.transform.GetChild(4).GetComponent<ParticleSystem>().Play();
                    
                tap = false;
                hold = false;
            

                Vector2 pos = (Vector2)Input.mousePosition - new Vector2(Screen.width * ((centerX * 100) / Screen.width) / 100, Screen.height * ((centerY * 100) / Screen.height) / 100);


                //float walkspeed = Vector2.Distance(Center, pos) / 100;
                if (Mathf.Abs(pos.y) > Mathf.Abs(pos.x) && Mathf.Abs(pos.x) < Screen.width / 10)
                {
                    // walkspeed = 10;
                }
                else
                {
                    // walkspeed = 5;
                }

                Decix = Mathf.Abs(pos.x) / 1000;
                screen = Screen.width;
                Lerx = (Decix) / (screen/1000);
                Deciy = Mathf.Abs(pos.y) / 1000;
                Lery = (Deciy) / (screen / 1000);
                    
                   
                walkspeed = Mathf.Lerp(4.5f, 20, Mathf.Clamp01(Lery - Lerx));

               

               // Debug.Log(Center +  " " + new Vector2(pos.x, pos.y));
               
                dir = new Vector2(pos.x, pos.y);
                dir = dir.normalized;


                //******AVOID TO ROTATE******
                if(PrevPosY + screen/20 < Mathf.Abs(pos.y) && Mathf.Abs(pos.x) < Mathf.Abs(pos.y))
                {
                    PrevPosY = Mathf.Abs(pos.y);
                    Changes++;
                }
                else if(PrevPosY== Mathf.Abs(pos.y) && TimeToSwap<.4f)
                {
                    Same++;
                }

               
                if(((pos.x <0 && DirRight && TimeToSwap<.4f || pos.x>0 && !DirRight && TimeToSwap<.4f) &&  Changes>=2 && Same<=10 && !SwipeLeft && !SwipeRight) || swipeDown || swipeUp)
                {
                    notRotate = true;
                }
                else
                {
                    notRotate = false;
                }
                //**************************************

                if (allowToZero && Vector2.Distance(Center, Center + pos) > Screen.width/10 && !JustJumped  && !NotHold)
                {
                      
                    this.HoldTimer = 0;
                    HoldForStartHold = 0;
                    NotHold = true;
                }
                if (allowToZero && !JustJumped && this.timeToMove > 0.1f && NotHold && !notRotate)
                {
                       
                    inputmanager.dir = dir;
                    this.HoldTimer = 0;
                    HoldForStartHold = 0;
                    timeMoving += Time.deltaTime;
                }

                maxlarge = screen - Mathf.Abs(centerX);
                
                this.Pointer.GetComponent<SpriteRenderer>().color = Color.Lerp(new Color32(0, 255, 255, 255), new Color32(0, 150, 255, 255), ((100 * Vector2.Distance(Center, Center + pos)) / maxlarge) / 100);

                Vector3 mouse_pos = Input.mousePosition;
                ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                this.RotPointer.transform.rotation = Quaternion.Euler(-90, 0, ang * -1);

                if (Vector2.Distance(Center, Center + pos) < (Screen.width / 10) && walkspeed >4.5f)
                {
                    //this.walking = true;
                    this.player.GetComponent<PlayerMovement>().walkSpeed =  walkspeed - (walkspeed/2);
                    this.Anim.SetBool("StartRunning", false);
                }
                else if(Vector2.Distance(Center, Center + pos) > (Screen.width / 10))
                {
                    this.walking = true;
                    this.Anim.SetBool("StartRunning", true);
                    this.player.GetComponent<PlayerMovement>().walkSpeed =  walkspeed;
                }
                    
            }
            else if(this.walking || this.Anim.GetBool("StartRunning") || this.player.transform.GetChild(4).GetComponent<ParticleSystem>().isPlaying || inputmanager.dir != Vector2.zero || NotHold || this.Pointer.GetComponent<SpriteRenderer>().color != new Color32(255, 255, 255, 0) )
            {
                this.player.transform.GetChild(4).GetComponent<ParticleSystem>().Stop();
                this.walking = false;
                this.Anim.SetBool("StartRunning", false);
                if (!RunningMad && !Tap && !LetPunchRotate)
                {
                    inputmanager.dir = Vector2.zero;
                }
                NotHold = false;
                this.Pointer.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);

            }
            
        }
    }

    public void BlockThis()
    {
        this.player.transform.GetChild(4).GetComponent<ParticleSystem>().Stop();
        this.walking = false;
        this.Anim.SetBool("StartRunning", false);
        if (!RunningMad && !Tap && !LetPunchRotate)
        {
            inputmanager.dir = Vector2.zero;
        }
        NotHold = false;
        this.Pointer.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
    }
    // Vector2 pos = Vector2.zero;
    //Vector2 Center = Vector2.zero;
    /*
    // width per percentage and height y rounded divded by 100
    if (Input.mousePosition.y < Screen.height / 3)
    {
        pos = (Vector2)Input.mousePosition - new Vector2(Screen.width * ((centerX * 100) / Screen.width) / 100, Screen.height / ((Screen.height - centerY)/100));
    }
    else
    {
        pos = (Vector2)Input.mousePosition - new Vector2(Screen.width * ((centerX * 100) / Screen.width) / 100, Screen.height / ((Screen.height - centerY) / 100));
    }


    // width per percentage and height y rounded divded by 100
    if (Input.mousePosition.y < Screen.height / 3)
    {
        Center = (Vector2)Input.mousePosition - new Vector2(Screen.width * ((Input.mousePosition.x * 100) / Screen.width) / 100, Screen.height / (Screen.width / 100));
    }
    else
    {
        Center = (Vector2)Input.mousePosition - new Vector2(Screen.width * ((Input.mousePosition.x * 100) / Screen.width) / 100, Screen.height / (Screen.width / 100));
    }
    //Debug.Log(Center);*/


    /***********************************************


    //Debug.Log(Input.mousePosition);
    /*
    if (Input.mousePosition.y < Screen.height / 3 && !up && !upper)
    {
        down = true;
        up = false;
        upper = false;
    }
    else if (Input.mousePosition.y > Screen.height / 3 &&(Input.mousePosition.y < ((Screen.height) - (Screen.height / 3))) && !down && !upper)
    {
        up = true;
        down = false;
        upper = false;
    }
    else if (Input.mousePosition.y > ((Screen.height) -(Screen.height / 3)) && !down && !up)
    {
        upper = true;
        up = true;
        down = false;
    }


    if (Input.mousePosition.x < ((Screen.width/2) -100) && !right && !middle )
    {
        left = true;
        right = false;
    }else if(Input.mousePosition.x > ((Screen.width / 2) +100)&& !left & !middle)
    {
        right = true;
        left = false;
    }
    else if(Input.mousePosition.x < ((Screen.width / 2) + 100) && !left && Input.mousePosition.x > ((Screen.width / 2) - 100) && !right)
    {
        middle = true;
        right = false;
        left = false;
    }

    if (right &&down )
    {
        pos = (Vector2)Input.mousePosition - new Vector2((Screen.width *.8f), Screen.height / 8);
    }else if(right && up)
    {
        pos = (Vector2)Input.mousePosition - new Vector2((Screen.width / 6) * 5, Screen.height / 4);
    }
    else if (right && upper)
    {
        pos = (Vector2)Input.mousePosition - new Vector2((Screen.width / 6) * 5, Screen.height /2 );
    }
    if (left && down)
    {
        pos = (Vector2)Input.mousePosition - new Vector2(Screen.width / 3, Screen.height / 8);
    }
    else if(left && up)
    {
        pos = (Vector2)Input.mousePosition - new Vector2(Screen.width / 3, Screen.height / 4);
    }
    else if (right && upper)
    {
        pos = (Vector2)Input.mousePosition - new Vector2(Screen.width / 3, Screen.height /2);
    }
    if (middle && down)
    {
        pos = (Vector2)Input.mousePosition - new Vector2(Screen.width / 2, Screen.height / 8);
    }
    else if (middle && up)
    {
        pos = (Vector2)Input.mousePosition - new Vector2(Screen.width / 2, Screen.height / 4);
    }
    else if (middle && upper)
    {
        pos = (Vector2)Input.mousePosition - new Vector2(Screen.width / 2, Screen.height / 2);
    }*/
}
