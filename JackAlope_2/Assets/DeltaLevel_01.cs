using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaLevel_01 : MonoBehaviour {

    // Use this for initialization
    public GameObject Player,center,DeltaFront;
    bool running, stop;
    public GameObject cameraFather;
    public GameObject Tutorial;
    public GameObject TextBoxDelta, TextBoxPlayer;
    public Texture TheyFoundUs, CalmDown, FollowedUsHere, HaveToEscape, FightYourWay, OnTheOtherSide, GoodLuck;
    public int TimeToReadLimit;
    bool startCounting;
    public float time;
    public int ButtonIndex;
    public float distanceFromPlayer;
    float targetposition;
    public GameObject Button;
    bool stage2;
    public GameObject levelcollider,referenceObjectOut;
    public GameObject Controls;

	void Start () {
        Controls.GetComponent<Swipe>().BlockForTutorial = true;
        Controls.GetComponent<Swipe>().BlockPunchTap = true;
        targetposition = this.Player.transform.position.x - distanceFromPlayer;
        float midleCenter = (distanceFromPlayer / 2);
        center.transform.localPosition = new Vector3(midleCenter, 0, 0);
        cameraFather = GameObject.FindGameObjectWithTag("CameraFather");
        cameraFather.GetComponent<CameraFollow>().target = this.DeltaFront.transform;
        this.TextBoxDelta.GetComponent<MeshRenderer>().materials[0].mainTexture = TheyFoundUs;
    }
	
	// Update is called once per frame
	void Update () {
        if (!Tutorial.activeInHierarchy)
        {
            if (transform.position.x < targetposition && !running && !stop)
            {
                this.transform.GetChild(1).GetComponent<Animator>().SetBool("Running", true);
                running = true;
            }
            if (transform.position.x < targetposition && running && !stop)
            {
                this.transform.position = new Vector3(this.transform.position.x + Time.deltaTime * 3, this.transform.position.y, this.transform.position.z);
            }
            else if(transform.position.x >= targetposition && running && !stop)
            {
                this.transform.GetChild(1).GetComponent<Animator>().SetBool("Running", false);
                running = false;
                stop = true;
            }
            
            if(stop && this.TextBoxDelta.GetComponent<MeshRenderer>().materials[0].mainTexture == TheyFoundUs && !stage2)
            {
                stage2 = true;
                this.TextBoxPlayer.SetActive(false);
                this.TextBoxDelta.SetActive(false);
                this.TextBoxPlayer.GetComponent<MeshRenderer>().materials[0].mainTexture = CalmDown;
                cameraFather.GetComponent<CameraFollow>().target = this.center.transform;
                this.TextBoxPlayer.SetActive(true);
                startCounting = true;
                Button.SetActive(true);
            }
            if (startCounting)
            {
                time += Time.deltaTime;
            }
            if ((time >=TimeToReadLimit && time< TimeToReadLimit * 2 || ButtonIndex==1) && this.TextBoxDelta.GetComponent<MeshRenderer>().materials[0].mainTexture != FollowedUsHere)
            {
               this.TextBoxPlayer.SetActive(false);
               this.TextBoxDelta.SetActive(false);
               this.TextBoxDelta.GetComponent<MeshRenderer>().materials[0].mainTexture = FollowedUsHere;
               this.TextBoxDelta.SetActive(true);
                time = TimeToReadLimit;
               ButtonIndex = 1;
            }
            if ((time >= TimeToReadLimit*2 && time < TimeToReadLimit * 3|| ButtonIndex == 2) && this.TextBoxDelta.GetComponent<MeshRenderer>().materials[0].mainTexture != HaveToEscape)
            {
                this.TextBoxDelta.SetActive(false);
                this.TextBoxDelta.GetComponent<MeshRenderer>().materials[0].mainTexture = HaveToEscape;
                this.TextBoxDelta.SetActive(true);
                time = TimeToReadLimit * 2;
                ButtonIndex = 2;
            }
            if ((time >= TimeToReadLimit*3 && time < TimeToReadLimit * 4 || ButtonIndex == 3) && this.TextBoxDelta.GetComponent<MeshRenderer>().materials[0].mainTexture != FightYourWay)
            {
                this.TextBoxDelta.SetActive(false);
                this.TextBoxDelta.GetComponent<MeshRenderer>().materials[0].mainTexture = FightYourWay;
                this.TextBoxDelta.SetActive(true);
                time = TimeToReadLimit * 3;
                ButtonIndex = 3;
            }
            if ((time >= TimeToReadLimit*4 && time < TimeToReadLimit * 5 || ButtonIndex == 4) && this.TextBoxDelta.GetComponent<MeshRenderer>().materials[0].mainTexture != OnTheOtherSide)
            {
                this.TextBoxDelta.SetActive(false);
                this.TextBoxDelta.GetComponent<MeshRenderer>().materials[0].mainTexture = OnTheOtherSide;
                this.TextBoxDelta.SetActive(true);
                time = TimeToReadLimit * 4;
                ButtonIndex = 4;
            }
            if ((time >= TimeToReadLimit * 5 && time < TimeToReadLimit * 6|| ButtonIndex == 5) && this.TextBoxDelta.GetComponent<MeshRenderer>().materials[0].mainTexture != GoodLuck)
            {
                this.TextBoxDelta.SetActive(false);
                this.TextBoxDelta.GetComponent<MeshRenderer>().materials[0].mainTexture = GoodLuck;
                this.TextBoxDelta.SetActive(true);
                time = TimeToReadLimit * 5;
                ButtonIndex = 5;
            }
            if ((time >= TimeToReadLimit * 6 && time < TimeToReadLimit * 7 || ButtonIndex == 6) && startCounting)
            {
                levelcollider.GetComponent<BoxCollider>().enabled = false;
                this.TextBoxDelta.SetActive(false);
                startCounting = false;
                ButtonIndex = 0;
                Button.SetActive(false);
                targetposition = this.Player.transform.position.x + 10;
                stop = false;
                cameraFather.GetComponent<CameraFollow>().target = this.Player.transform;
            }
            if (this.gameObject.transform.position.x > this.referenceObjectOut.transform.position.x && !levelcollider.GetComponent<BoxCollider>().enabled)
            {
                levelcollider.GetComponent<BoxCollider>().enabled = true;
                referenceObjectOut.GetComponent<Animator>().SetTrigger("MoveCar");
                Controls.GetComponent<Swipe>().BlockForTutorial = false;
                Controls.GetComponent<Swipe>().BlockPunchTap = false;
                this.gameObject.SetActive(false);
            }
        }
    }

    public void ButtonIndexAdd()
    {
        ButtonIndex += 1;
    }
}
