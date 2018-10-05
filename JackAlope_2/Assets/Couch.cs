using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Couch : MonoBehaviour {

    // Use this for initialization
    public GameObject dialog;

    private GameObject Player;

    private string Origninal;

    public bool speaking, finalStage2;

    private bool ShowAttackList;
    bool done;
    public GameObject AttackListScreen,AttackListScreenDojo;
    public GameObject SavingSystem;
    public GameObject SwipeControls;
    public GameObject Spot2, TrainingRobot;
    public GameObject TextBox;
    public int changeDialog;
    public int changeDialog2;
    public int changeDialog3;
    public int changeDialog4;
    public int punchingDialog;
    public Texture MortyNeedsYou, ShowmeYour, HitThat, ThosePunching, GoAhead, YouCanGrab, SwipeUp, NotMe, WhileYouAre,HeyMorty,GoodBut,TakeOrbes,TakeThis,Congrats,GoAndSave;
    public float timeStage2, timeHitThat;
    public bool startCountinStage2, startCountingForHitThat;
    public bool startCountingStage3, startCountingStage3b, startCountingStage3c;
    public bool startCountingStage4;
    public float timeStage3, timeStage3b, timeStage3c,timestage4;
    public bool stage1;
    public bool stage3;
    public bool stage4;
    float timetoMove;
    public GameObject spot1,spot3;
    public bool punchingTest, GrabHitTest, GrabHitTestFirstPart;
    public float speed;
    public bool walk,walk2;
    Vector3 destiny, destiny2;
    float timeWalked;
    public GameObject ComboText;
    public bool waitingToGrab;
    public Vector3 Origin;
    public float originalTimeGrabbing;
    public GameObject ButtonTap, TeleportWall;
    bool justBorn;
    float timeBorn;
    bool swipeUpInstruction;
    public GameObject Orbe;

    public float DialogueBoxesTime;

    void OnEnable () {
        startCountingForHitThat = false; 
        DialogueBoxesTime = 5;
        changeDialog = 0;
        changeDialog2 = 0;
        this.Player = GameObject.FindGameObjectWithTag("Player");
        Origninal = this.dialog.GetComponent<TextMesh>().text;
        Origin = this.transform.position;
        SavingSystem = GameObject.FindGameObjectWithTag("SavingSystem");

      
        if (SavingSystem.GetComponent<SavingSystem>().DojoPass == false)
        {
            this.TextBox.GetComponent<TextBoxFollowCoach>().heyMorty = true;
            this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture = HeyMorty;
            this.TextBox.SetActive(true);
            this.justBorn = true;
            this.SwipeControls.GetComponent<Swipe>().BlockGrab = true;
        }
        else
        {
            justBorn = false;
            this.TextBox.SetActive(false);
            spot1.SetActive(false);
            this.GetComponent<Animator>().SetBool("DojoPass",true);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (justBorn && timeBorn<= DialogueBoxesTime)
        {
            timeBorn += Time.deltaTime;
        }
        if (timeBorn >= DialogueBoxesTime)
        {
            justBorn = false;
            timeBorn = 0;
            if (this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture == HeyMorty)
            {
                this.TextBox.SetActive(false);
            }
            this.TextBox.GetComponent<TextBoxFollowCoach>().heyMorty = false;
        }
        if (SavingSystem.GetComponent<SavingSystem>().DojoPass)
        {
            if (Vector3.Distance(this.transform.position, this.Player.transform.position) < 2 && !ShowAttackList)
            {
                this.ShowAttackList = true;
                this.dialog.GetComponent<TextMesh>().text = "I'll teach you some moves";
                this.dialog.SetActive(true);
                AttackListScreen.SetActive(true);
            }
            if (Vector3.Distance(this.transform.position, this.Player.transform.position) > 2 && ShowAttackList)
            {
                this.dialog.GetComponent<TextMesh>().text = Origninal;
                this.ShowAttackList = false;
                AttackListScreen.SetActive(false);
            }
        }
        else
        {
            if (Vector3.Distance(this.transform.position, this.Player.transform.position) > 1)
            {
                this.transform.LookAt(new Vector3(this.Player.transform.position.x, 0, this.Player.transform.position.z));
            }
            if (speaking)
            {
                this.Player.transform.LookAt(this.transform.position * 90);
            }

            if (speaking && changeDialog == 0 && this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture != MortyNeedsYou && stage1)
            {
                this.SwipeControls.GetComponent<Swipe>().BlockForTutorial = true;
                this.SwipeControls.GetComponent<Swipe>().BlockThis();
                TextBox.SetActive(false);
                TextBox.SetActive(true);
                this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture = MortyNeedsYou;
            }
            if (speaking && changeDialog == 1 && this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture != ShowmeYour)
            {
                TextBox.SetActive(false);
                TextBox.SetActive(true);
                this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture = ShowmeYour;
            }
            if (speaking && changeDialog == 2)
            {
                spot1.SetActive(false);
                Spot2.SetActive(true);
                TrainingRobot.SetActive(true);
                speaking = false;
                TextBox.SetActive(false);
                this.SwipeControls.GetComponent<Swipe>().BlockForTutorial = false;
                this.SwipeControls.GetComponent<Swipe>().BlockPunchTap = false;
                stage1 = false;
                ButtonTap.SetActive(false);
                changeDialog = 0;

            }

            if (Vector3.Distance(this.Spot2.transform.position, this.Player.transform.position) < .4 && !this.SwipeControls.GetComponent<Swipe>().BlockForTutorial && Spot2.activeInHierarchy)
            {
                TextBox.SetActive(false);
                TextBox.SetActive(true);
                this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture = HitThat;
                this.ButtonTap.SetActive(true);
                startCountingForHitThat = true;
                this.SwipeControls.GetComponent<Swipe>().BlockForTutorial = true;
                this.SwipeControls.GetComponent<Swipe>().BlockPunchTap = true;
                this.SwipeControls.GetComponent<Swipe>().BlockThis();
                punchingTest = true;
                walk = true;
                destiny = new Vector3(this.transform.position.x - 7, this.transform.position.y, this.transform.position.z);
                this.transform.position = new Vector3(this.transform.position.x - 4, this.transform.position.y, this.transform.position.z);
                this.GetComponent<Animator>().SetBool("Walk", true);
            }
            if (startCountingForHitThat)
            {
                timeHitThat += Time.deltaTime;
            }
            if (timeHitThat > DialogueBoxesTime/2 || punchingDialog == 1)
            {
                punchingDialog = 0;
                this.ButtonTap.SetActive(false);
                TextBox.SetActive(false);
                speaking = false;
                timeHitThat = 0;
                startCountingForHitThat = false;
                this.SwipeControls.GetComponent<Swipe>().BlockPunchTap = false;
            }
            if (walk)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, destiny, Time.deltaTime * speed);
                timeWalked += Time.deltaTime;
            }
            if (walk && (timeWalked > 3f || this.transform.position == destiny))
            {
                walk = false;
                this.GetComponent<Animator>().SetBool("Walk", false);
                timeWalked = 0;
            }
            if (punchingTest && Spot2.activeInHierarchy)
            {
                timer += Time.deltaTime;
                if (timer > .1f)
                {
                    timer = 0;
                    if (this.Player.transform.position.x != this.Spot2.transform.position.x || this.Player.transform.position.z != this.Spot2.transform.position.z)
                    {
                        this.Player.transform.position = new Vector3(this.Spot2.transform.position.x, this.Player.transform.position.y, this.Spot2.transform.position.z);
                    }
                }
            }
            if (speaking && finalStage2 && !this.TextBox.activeInHierarchy)
            {
                TextBox.SetActive(false);
                TextBox.SetActive(true);
                ComboText.GetComponent<Text>().enabled = false;
                this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture = ThosePunching;
                this.SwipeControls.GetComponent<Swipe>().BlockForTutorial = true;
                this.SwipeControls.GetComponent<Swipe>().BlockPunchTap = true;
                this.TextBox.SetActive(true);
                startCountinStage2 = true;
                TrainingRobot.SetActive(false);
                //this.ButtonTap.SetActive(true);
            }
            if (startCountinStage2)
            {
                timeStage2 += Time.deltaTime;
            }
            if ((timeStage2 > DialogueBoxesTime || changeDialog2 == 1) && this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture != GoAhead && TextBox.activeInHierarchy)
            {
                TextBox.SetActive(false);
                TextBox.SetActive(true);
                this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture = GoAhead;
                TeleportWall.SetActive(true);
                this.SwipeControls.GetComponent<Swipe>().BlockForTutorial = false;
                this.SwipeControls.GetComponent<Swipe>().BlockPunchTap = false;
                speaking = false;
                timeStage2 = 4.1f;
            }
            if (timeStage2 < DialogueBoxesTime*2 && !this.ButtonTap.activeInHierarchy && this.AttackListScreenDojo.GetComponent<LevelEnterTutorialDojo>().Excellent.activeInHierarchy && !done)
            {
                StartCoroutine(WaitToTextBox());
                done = true;
            }
            if (timeStage2 > DialogueBoxesTime*2 || changeDialog2 >= 2)
            {
                this.TextBox.SetActive(false);
                startCountinStage2 = false;
                timeStage2 = 0;
                finalStage2 = false;
                speaking = false;
                changeDialog2 = 0;
                this.ButtonTap.SetActive(false);
                TeleportWall.SetActive(true);
                this.SwipeControls.GetComponent<Swipe>().BlockForTutorial = false;
                this.SwipeControls.GetComponent<Swipe>().BlockPunchTap = false;
            }

            if (speaking && stage3 && !startCountingStage3)
            {
                this.ButtonTap.SetActive(true);
                this.TextBox.SetActive(true);
                this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture = YouCanGrab;
                this.AttackListScreenDojo.GetComponent<LevelEnterTutorialDojo>().X_Grab();
                this.SwipeControls.GetComponent<SwipeControls>().distanceToGrab = 1.5f;
                startCountingStage3 = true;
            }
            if (startCountingStage3 && timeStage3 < DialogueBoxesTime)
            {
                timeStage3 += Time.deltaTime;
            }
            if ((timeStage3 >= DialogueBoxesTime || changeDialog3 >= 1) && !this.SwipeControls.GetComponent<Swipe>().grabing)
            {
                this.ButtonTap.SetActive(false);
                startCountingStage3 = false;
                this.SwipeControls.GetComponent<Swipe>().BlockForTutorial = false;
                this.SwipeControls.GetComponent<Swipe>().BlockPunchTap = false;
                speaking = false;
                timeStage3 = 0;
                changeDialog3 = 0;
                waitingToGrab = true;
            }
            if (waitingToGrab)
            {
                if (Vector3.Distance(this.transform.position, this.Player.transform.position) < 2 && !TextBox.activeInHierarchy)
                {
                    this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture = NotMe;
                    TextBox.SetActive(true);
                    TrainingRobot.GetComponent<EnemyAI>().enableAI = true;
                    TrainingRobot.GetComponent<EnemyAI>().enableAI = false;
                }
                else if (Vector3.Distance(this.transform.position, this.Player.transform.position) >= 2 && TextBox.activeInHierarchy)
                {
                    TextBox.SetActive(false);
                    TrainingRobot.GetComponent<EnemyAI>().enableAI = true;
                    TrainingRobot.GetComponent<EnemyAI>().enableAI = false;
                }
            }
            if (this.SwipeControls.GetComponent<Swipe>().grabing && stage3 && waitingToGrab)
            {
                walk2 = true;
                if (this.Player.GetComponent<PlayerMovement>().currentDirection == DIRECTION.Right)
                {
                    this.Player.transform.position = new Vector3(this.spot3.transform.position.x + 1.2f, this.transform.position.y, this.transform.position.z);
                    destiny2 = new Vector3(this.transform.position.x - 3.1f, this.transform.position.y, this.transform.position.z);
                }
                else if (this.Player.GetComponent<PlayerMovement>().currentDirection == DIRECTION.Left)
                {
                    destiny2 = new Vector3(this.transform.position.x - 4.5f, this.transform.position.y, this.transform.position.z);
                    this.Player.transform.position = new Vector3(this.spot3.transform.position.x - 1.5f, this.transform.position.y, this.transform.position.z);
                }
                this.GetComponent<Animator>().SetBool("Walk", true);
                waitingToGrab = false;
                TextBox.SetActive(false);
                this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture = WhileYouAre;
                this.SwipeControls.GetComponent<Swipe>().BlockJump = true;
                GrabHitTestFirstPart = true;
                GrabHitTest = true;
                this.ComboText.SetActive(true);
                TextBox.SetActive(true);
                this.ButtonTap.SetActive(true);
                this.originalTimeGrabbing = this.SwipeControls.GetComponent<SwipeControls>().timeGrabbingEnemy;
                this.SwipeControls.GetComponent<SwipeControls>().timeGrabbingEnemy = 10000;
                this.AttackListScreenDojo.GetComponent<LevelEnterTutorialDojo>().X_Punch();
                // this.TrainingRobot.GetComponent<DontMoveRobotTutorial>().enabled = false;
                StartCoroutine(ActiveGrabbingInCase());
                startCountingStage3b = true;
            }
            if (startCountingStage3b && timeStage3b <= DialogueBoxesTime)
            {
                timeStage3b += Time.deltaTime;
            }
            if ((changeDialog3 >= 1 || timeStage3b >= DialogueBoxesTime) && this.SwipeControls.GetComponent<Swipe>().grabing && startCountingStage3b)
            {
                TextBox.SetActive(false);
                this.ButtonTap.SetActive(false);
                changeDialog3 = 0;
                startCountingStage3b = false;
                timeStage3b = 0;
            }
            if (swipeUpInstruction && this.SwipeControls.GetComponent<Swipe>().grabing)
            {
                TextBox.SetActive(false);
                TextBox.SetActive(true);
                this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture = SwipeUp;
                swipeUpInstruction = false;
                startCountingStage3c = true;
                this.ButtonTap.SetActive(true);
            }
            if (startCountingStage3c)
            {
                timeStage3c += Time.deltaTime;
            }
            if ((this.SwipeControls.GetComponent<Swipe>().SwipeUp || timeStage3b >= DialogueBoxesTime/2 || changeDialog3 >= 1 && timeStage3c > DialogueBoxesTime/2) && startCountingStage3c)
            {
                TextBox.SetActive(false);
                startCountingStage3c = false;
                timeStage3c = 0;
                changeDialog3 = 0;
                this.ButtonTap.SetActive(false);
            }

            if (stage4 && speaking && !startCountingStage4)
            {
                TextBox.SetActive(false);
                TextBox.SetActive(true);
                this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture = GoodBut;
                startCountingStage4 = true;
                this.ButtonTap.SetActive(true);
                this.SwipeControls.GetComponent<Swipe>().BlockForTutorial = true;
                this.SwipeControls.GetComponent<Swipe>().BlockPunchTap = true;
                this.spot3.SetActive(false);
                this.TrainingRobot.SetActive(false);
            }
            if (startCountingStage4)
            {
                timestage4 += Time.deltaTime;
            }
            if ((changeDialog4 == 1 || timestage4 >= DialogueBoxesTime) && this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture == GoodBut)
            {
                TextBox.SetActive(false);
                TextBox.SetActive(true);
                this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture = TakeOrbes;
                timestage4 = 0;
            }
            if ((changeDialog4 == 2 || timestage4 >= DialogueBoxesTime) && this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture == TakeOrbes)
            {
                TextBox.SetActive(false);
                TextBox.SetActive(true);
                this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture = this.TakeThis;
                timestage4 = 0;
                this.Orbe.SetActive(true);
                this.Orbe.transform.position = new Vector3(this.transform.position.x - 1, 1.3f, this.transform.position.z);
                this.Orbe.GetComponent<Rigidbody>().AddForce(Vector3.left, ForceMode.Impulse);

            }
            if ((changeDialog4 == 3 || timestage4 >= DialogueBoxesTime) && this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture == TakeThis)
            {
                TextBox.SetActive(false);
                TextBox.SetActive(true);
                this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture = this.Congrats;
                timestage4 = 0;
            }
            if ((changeDialog4 == 4 || timestage4 >= DialogueBoxesTime) && this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture == Congrats)
            {
                TextBox.SetActive(false);
                TextBox.SetActive(true);
                this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture = this.GoAndSave;
                changeDialog4 = 0;
                timestage4 = 0;
            }
            if ((changeDialog4 == 5 || timestage4 >= DialogueBoxesTime) && this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture == GoAndSave)
            {
                TextBox.SetActive(false);
                changeDialog4 = 0;
                timestage4 = 0;
                startCountingStage4 = false;
                stage4 = false;
                speaking = false;
                //GOTOMAP
                Destroy(GameObject.FindGameObjectWithTag("UI"));
                SceneManager.LoadScene("MainMenu");

            }
        }
        //---------------------------
        if (walk2)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, destiny2, Time.deltaTime * speed);
            timeWalked += Time.deltaTime;
        }
        if (walk2 && (timeWalked > 3f || this.transform.position == destiny2))
        {
            walk2 = false;
            this.GetComponent<Animator>().SetBool("Walk", false);
            timeWalked = 0;
        }
    }

    IEnumerator WaitToTextBox()
    {
        yield return new WaitForSeconds(0.5f);
        this.ButtonTap.SetActive(true);
    }
    public void TapButton()
    {
        if (TextBox.activeInHierarchy)
        {
            if (startCountinStage2 && this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture == ThosePunching || this.TextBox.GetComponent<MeshRenderer>().materials[0].mainTexture == GoAhead)
            {
                changeDialog2 += 1;
            }
            if (stage1)
            {
                changeDialog += 1;
            }
            if (stage3 && SwipeControls.GetComponent<Swipe>().timeToMove < .5f)
            {
                changeDialog3 += 1;
            }
            if (punchingTest)
            {
                punchingDialog += 1;
            }
            if (stage4 && SwipeControls.GetComponent<Swipe>().timeToMove < .5f)
            {
                changeDialog4 += 1;
            }
        }
    }

    public void punchingTestExit()
    {
        Spot2.SetActive(false);
        this.TextBox.SetActive(false);
        this.SwipeControls.GetComponent<Swipe>().BlockForTutorial = false;
        this.SwipeControls.GetComponent<Swipe>().BlockThis();
        StartCoroutine(punchingtestFalse());
    }

    public void GrabHitTestExit()
    {
        swipeUpInstruction = true;
        StartCoroutine(GrabitHitFalse());
    }

    public void MakeEnemyDumb()
    {
        StartCoroutine(WaitToMakeItDumb());
    }

    IEnumerator WaitToMakeItDumb()
    {
        yield return new WaitForSeconds(1f);
        this.ComboText.SetActive(false);
        GrabHitTest = false;
        TrainingRobot.GetComponent<EnemyAI>().enableAI = false;
        TrainingRobot.GetComponent<DontMoveRobotTutorial>().enabled = true;
        stage3 = false;
        yield return new WaitForSeconds(.75f);
        stage4 = true;
        speaking = true;
    }

    IEnumerator GrabitHitFalse()
    {
        yield return new WaitForSeconds(.2f);
        //this.SwipeControls.GetComponent<SwipeControls>().timeGrabbingEnemy = this.originalTimeGrabbing;
        // this.ComboText.SetActive(false);
        GrabHitTestFirstPart = false;
        this.AttackListScreenDojo.GetComponent<LevelEnterTutorialDojo>().tapCount = 0;
    }
    IEnumerator punchingtestFalse()
    {
        yield return new WaitForSeconds(.2f);
        punchingTest = false;
        this.ComboText.SetActive(false);
        this.AttackListScreenDojo.GetComponent<LevelEnterTutorialDojo>().tapCount = 0;
    }

    IEnumerator ActiveGrabbingInCase()
    {
        yield return new WaitForSeconds(0.5f);
        this.Player.transform.GetChild(0).GetComponent<Animator>().SetBool("GrabHolding", true);
    }
    float timer;
    public void ShowDialog()
    {
        if (!speaking)
        {
            StartCoroutine(TimeDialog());
        }
    }

    IEnumerator TimeDialog()
    {
        dialog.SetActive(true);
        yield return new WaitForSeconds(3);
        dialog.SetActive(false);
    }
}
