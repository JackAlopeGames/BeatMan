using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEnterTutorial : MonoBehaviour {

    // Use this for initialization

    public int StartWith, EndIn;
    public GameObject Enemies;
    public GameObject Walk, Punch, Jump, UpperCut, Tornado, Side, RunPunch, Back, Done;
    private bool Walk_S, Punch_S, Jump_S, UpperCut_S, Tornado_S, Side_S, RunPunch_S, Back_S, Done_S;
    public GameObject Swipe, ColliderTutorial, TipTutorial;
    public GameObject  SavingSystem;
    delegate void TutorialMethod();
    List<TutorialMethod> callButton = new List<TutorialMethod>();

    void Start () {

        //  ResetTutorial();
        this.SavingSystem = GameObject.FindGameObjectWithTag("SavingSystem");
        if (ColliderTutorial == null)
        {
            ColliderTutorial = GameObject.FindGameObjectWithTag("ColliderTutorial");
        }

        if (SavingSystem.GetComponent<SavingSystem>().DojoPass)
        {
            if (this.SavingSystem.GetComponent<SavingSystem>().Tutorial0 == false)
            {
                StartCoroutine(WaitToActive());
                ColliderTutorial.SetActive(true);
            }
            else
            {
                ColliderTutorial.SetActive(false);
            }
        }
        ColliderTutorial.SetActive(false);
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
       // this.Enemies = GameObject.FindGameObjectWithTag("Enemies");
        try
        {
            ColliderTutorial.transform.position = this.Enemies.GetComponent<EnemyWaveSystem>().EnemyWaves[this.Enemies.GetComponent<EnemyWaveSystem>().currentWave].AreaCollider.transform.position;
        }
        catch { }
        StartCoroutine(CheckIfTutorialIsDone());
        
    }

    IEnumerator CheckIfTutorialIsDone()
    {
        
        this.Enemies.SetActive(false);

        if (this.SavingSystem.GetComponent<SavingSystem>().Tutorial0 == true)
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
        if(SceneManager.GetSceneByName("Level_01").isLoaded)
        {
            this.SavingSystem.GetComponent<SavingSystem>().Tutorial0 = true;
            this.SavingSystem.GetComponent<SavingSystem>().Save();
        }
       // this.Enemies.SetActive(true);
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

    float tapCount, JumpCount, RunningPunchCount;

    private void Update()
    {
        if(SavingSystem.GetComponent<SavingSystem>().DojoPass && SavingSystem.GetComponent<SavingSystem>().Tutorial0 && this.ColliderTutorial.activeInHierarchy)
        {
            this.ColliderTutorial.SetActive(false);
        }
        if (Walk_S)
        {
            if(Swipe.GetComponent<Swipe>().TimeMoving >= 6)
            {
                Done.GetComponent<Button>().onClick.Invoke();
            }
            TipTutorial.GetComponent<Text>().text = "Move arround " + (int)(6f - Swipe.GetComponent<Swipe>().TimeMoving) + " seconds more";
        }
        if (Punch_S)
        {
            if (Swipe.GetComponent<Swipe>().Tap)
            {
                tapCount++;
            }
            if (tapCount >= 3)
            {
                Done.GetComponent<Button>().onClick.Invoke();
            }
            TipTutorial.GetComponent<Text>().text = "Punch " + (3 - tapCount) + " times more";
        }
        if (Jump_S)
        {
            if (Swipe.GetComponent<Swipe>().SwipeUp)
            {
                JumpCount++;
            }
            if (JumpCount >= 2)
            {
                Done.GetComponent<Button>().onClick.Invoke();
            }
            TipTutorial.GetComponent<Text>().text = "Jump " + (2 - JumpCount) + " times more";
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
        Walk_S = Punch_S = Jump_S = UpperCut_S = Tornado_S = Side_S = RunPunch_S = Back_S = Done_S = false;
    }
}
