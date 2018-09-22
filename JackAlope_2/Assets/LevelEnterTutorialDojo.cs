using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEnterTutorialDojo : MonoBehaviour {

    // Use this for initialization

    public int StartWith, EndIn;
    private GameObject Enemies;
    public GameObject Walk, Punch, Jump, UpperCut, Tornado, Side, RunPunch, Back, Done, Grab;
    private bool Walk_S, Punch_S, Jump_S, UpperCut_S, Tornado_S, Side_S, RunPunch_S, Back_S, Done_S, Grab_S;
    public GameObject Swipe, ColliderTutorial, TipTutorial, InstructionText;
    public GameObject  SavingSystem;
    delegate void TutorialMethod();
    List<TutorialMethod> callButton = new List<TutorialMethod>();
    public GameObject spot1, spot2;

    void Start () {

        //  ResetTutorial();
        this.SavingSystem = GameObject.FindGameObjectWithTag("SavingSystem");
        if (ColliderTutorial == null)
        {
            ColliderTutorial = GameObject.FindGameObjectWithTag("ColliderTutorial");
        }

        if (this.SavingSystem.GetComponent<SavingSystem>().DojoPass == false)
        {
            this.Swipe.GetComponent<Swipe>().BlockGrab = true;
            StartCoroutine(WaitToActive());
            this.spot1.SetActive(true);
            this.Coach.GetComponent<Couch>().TextBox.SetActive(true);
            this.Coach.GetComponent<Animator>().SetBool("DojoPass", false);
        }
        else if(this.SavingSystem.GetComponent<SavingSystem>().DojoPass == true)
        {
            try
            {
                ColliderTutorial.SetActive(false);
                this.spot1.SetActive(false);
                this.Coach.GetComponent<Couch>().TextBox.SetActive(false);
                this.Coach.GetComponent<Animator>().SetBool("DojoPass", true);
            }
            catch { }
        }
        
    }


    IEnumerator WaitToActive()
    {
        yield return new WaitForSeconds(.5f);
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ResetTutorial()
    {
        StartWith = 0;
        tapCount = JumpCount = RunningPunchCount = 0;
        ClearStates();
        TipTutorial.GetComponent<Text>().text = ""; 
        this.Enemies = GameObject.FindGameObjectWithTag("Enemies");
        try
        {
            ColliderTutorial.transform.position = this.Enemies.GetComponent<EnemyWaveSystem>().EnemyWaves[this.Enemies.GetComponent<EnemyWaveSystem>().currentWave].AreaCollider.transform.position;
        }
        catch { }
        StartCoroutine(CheckIfTutorialIsDone());
        
    }

    IEnumerator CheckIfTutorialIsDone()
    {
        try
        {
            this.Enemies.SetActive(false);
        }
        catch { }
        if (this.SavingSystem.GetComponent<SavingSystem>().DojoPass == true)
        {
            X_Back();
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            callButton.Add(X_Walk);
            callButton.Add(X_Punch);
            callButton.Add(X_Jump);
            callButton.Add(X_UpperCut);
            callButton.Add(X_Tornado);
            callButton.Add(X_Side);
            callButton.Add(X_RunPunch);

            callButton[StartWith]();
        }
    }
    
    public void DoneButton()
    {
            ClearStates();
            if (StartWith + 1 <= EndIn)
            {
                StartWith++;
                callButton[StartWith]();
            }
            else
            {
                X_Back();

            }
        
    }

    public void X_Back()
    {
        try
        {
            ColliderTutorial.SetActive(false);
        }
        catch { }
        ClearStates();
        if(SceneManager.GetSceneByName("Level_02").isLoaded)
        {
            this.SavingSystem.GetComponent<SavingSystem>().DojoPass = true;
            this.SavingSystem.GetComponent<SavingSystem>().Save();
        }
        this.Enemies.SetActive(true);
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void X_Walk()
    {
        Walk.GetComponent<Button>().onClick.Invoke();
        ClearStates();
        Walk_S = true;
    }

    public void X_Punch()
    {
        Punch.GetComponent<Button>().onClick.Invoke();
        ClearStates();
        Punch_S = true;
    }

    public void X_Jump()
    {
        Jump.GetComponent<Button>().onClick.Invoke();
        ClearStates();
        Jump_S = true;
    }

    public void X_UpperCut()
    {
        UpperCut.GetComponent<Button>().onClick.Invoke();
        ClearStates();
        UpperCut_S = true;
    }

    public void X_Tornado()
    {
        Tornado.GetComponent<Button>().onClick.Invoke();
        ClearStates();
        Tornado_S = true;
    }

    public void X_Side()
    {
        Side.GetComponent<Button>().onClick.Invoke();
        ClearStates();
        Side_S = true;
    }

    public void X_RunPunch()
    {
        RunPunch.GetComponent<Button>().onClick.Invoke();
        ClearStates();
        RunPunch_S = true;
    }

    public void X_Grab()
    {
        Grab.GetComponent<Button>().onClick.Invoke();
        ClearStates();
        Grab_S = true;
    }

    public float tapCount, JumpCount, RunningPunchCount;
    public GameObject Coach, Excellent, moveAnimation,ButtonTap, TapAnimation,SwipeUpAnimation;
    int isZero;
    public GameObject AreaColliderSpot1,AreaCollider2;
    bool WaitToSwipeUpInstruction;
    float timeToSwipeUp;
    private void Update()
    {
        
        if(Vector3.Distance(this.spot2.transform.position, this.Swipe.GetComponent<Swipe>().Player.transform.position) < .4f && isZero==0 && spot2.activeInHierarchy)
        {
            Done.GetComponent<Button>().onClick.Invoke();
            Coach.GetComponent<Couch>().speaking = true;
            TipTutorial.SetActive(true);
            InstructionText.SetActive(true);
            isZero++;
        }
        if (Walk_S)
        {
            if (Vector3.Distance(this.spot1.transform.position, this.Swipe.GetComponent<Swipe>().Player.transform.position) < .4f && spot1.activeInHierarchy)
            {
                this.AreaColliderSpot1.GetComponent<ShowUntilSpot1>().changeArea(AreaCollider2.GetComponent<BoxCollider>());
                AreaColliderSpot1.SetActive(false);
                Coach.GetComponent<Couch>().speaking = true;
                Coach.GetComponent<Couch>().stage1 = true;
                ButtonTap.SetActive(true);
                TipTutorial.SetActive(false);
                InstructionText.SetActive(false);
                moveAnimation.SetActive(false);
                this.Swipe.GetComponent<Swipe>().BlockPunchTap = true;
            }
            else if(this.spot1.activeInHierarchy)
            {
               // TipTutorial.GetComponent<Text>().text = "Move to the circle";
            }
        }
        if (Punch_S)
        {
          /*  if (Swipe.GetComponent<Swipe>().Tap)
            {
                tapCount++;
            }*/
            if (tapCount >= 5 && Coach.GetComponent<Couch>().punchingTest)
            {
                //Done.GetComponent<Button>().onClick.Invoke();
                TipTutorial.SetActive(false);
                InstructionText.SetActive(false);
                TapAnimation.SetActive(false);
                this.Coach.GetComponent<Couch>().punchingTestExit();
                Excellent.SetActive(true);
               // ButtonTap.SetActive(true);
                this.Swipe.GetComponent<Swipe>().BlockPunchTap = true;
                Coach.GetComponent<Couch>().speaking = true;
                Coach.GetComponent<Couch>().finalStage2 = true;
                ClearStates();
               // TipTutorial.GetComponent<Text>().text = "Punch " + (5 - tapCount) + " times more";
            }
            if (tapCount >= 3 && Coach.GetComponent<Couch>().GrabHitTest)
            {
               
                TipTutorial.SetActive(false);
                InstructionText.SetActive(false);
                TapAnimation.SetActive(false);
                this.Coach.GetComponent<Couch>().GrabHitTestExit();
                ClearStates();
               // TipTutorial.GetComponent<Text>().text = "Punch " + (3 - tapCount) + " times more";
                Excellent.SetActive(true);
                WaitToSwipeUpInstruction = true;
            } 
        }
        if (WaitToSwipeUpInstruction)
        {
            timeToSwipeUp += Time.deltaTime;
            if (timeToSwipeUp > 1)
            {
                WaitToSwipeUpInstruction = false;
                Excellent.SetActive(false);
                X_Jump();
                this.Swipe.GetComponent<Swipe>().BlockJump = false;
            }
        }
        if (Jump_S)
        {
            if (Swipe.GetComponent<Swipe>().BlockJump)
            {
                Swipe.GetComponent<Swipe>().BlockJump = false;
            }
            if (Swipe.GetComponent<Swipe>().SwipeUp)
            {
                JumpCount++;
            }
            if (JumpCount >= 2 && !this.Coach.GetComponent<Couch>().GrabHitTest)
            {
                Done.GetComponent<Button>().onClick.Invoke();
            }
            if(JumpCount == 1 && this.Coach.GetComponent<Couch>().GrabHitTest)
            {
                this.Coach.GetComponent<Couch>().TrainingRobot.GetComponent<DontMoveRobotTutorial>().enabled = false;
                this.Coach.GetComponent<Couch>().MakeEnemyDumb();
                TipTutorial.SetActive(false);
                InstructionText.SetActive(false);
                SwipeUpAnimation.SetActive(false);
                this.Swipe.GetComponent<SwipeControls>().timeGrabbingEnemy = this.Coach.GetComponent<Couch>().originalTimeGrabbing;
                this.Swipe.GetComponent<Swipe>().BlockGrab = false;
                this.Swipe.GetComponent<SwipeControls>().distanceToGrab = 0.5f;
                ClearStates();
            }
          //  TipTutorial.GetComponent<Text>().text = "Jump " + (2 - JumpCount) + " times more";
        }
        if (Grab_S)
        {
            if (this.Swipe.GetComponent<Swipe>().grabing)
            {
                TipTutorial.SetActive(false);
                InstructionText.SetActive(false);
                moveAnimation.SetActive(false);
                ClearStates();
            }
        }
        if (UpperCut_S)
        {
            if (Swipe.GetComponent<Swipe>().Hold)
            {
                Done.GetComponent<Button>().onClick.Invoke();
            }
            TipTutorial.GetComponent<Text>().text = "";
        }
        if (Tornado_S)
        {
            if (Swipe.GetComponent<Swipe>().SwipeUp)
            {
                Done.GetComponent<Button>().onClick.Invoke();
            }
            TipTutorial.GetComponent<Text>().text = "";
        }
        if (Side_S)
        {
            if (Swipe.GetComponent<Swipe>().SwipeDown)
            {
                Done.GetComponent<Button>().onClick.Invoke();
            }
            TipTutorial.GetComponent<Text>().text = "";
        }
        if (RunPunch_S)
        {
            if (Swipe.GetComponent<Swipe>().SwipeLeft || Swipe.GetComponent<Swipe>().SwipeRight)
            {
                RunningPunchCount++;
            }
            if(RunningPunchCount >= 2)
            {
                Done.GetComponent<Button>().onClick.Invoke();
            }
            TipTutorial.GetComponent<Text>().text = "Swipe " + (2 - RunningPunchCount) + " times more";
        }

    }

   

    public void ClearStates() {
        Walk_S = Punch_S = Jump_S = UpperCut_S = Tornado_S = Side_S = RunPunch_S = Back_S = Done_S = Grab_S= false;
    }
    
}
