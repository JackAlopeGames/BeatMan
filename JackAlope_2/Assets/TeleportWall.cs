using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportWall : MonoBehaviour
{

    // Use this for initialization
    public GameObject player, Coach, Spot3, EnemyTraining,TextBox , Swipe, AreaCollider3;
    GameObject UI;
    void OnEnable()
    {

        StartCoroutine(CheckIfPlayerGoes());
        UI = GameObject.FindGameObjectWithTag("UI");
    }

    IEnumerator CheckIfPlayerGoes()
    {
        if(this.player.transform.position.x < this.transform.position.x && this.UI !=null)
        {
            ResetThis();
        }
        else
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(CheckIfPlayerGoes());
        }
        yield return null;
    }

    public void ResetThis()
    {
        this.AreaCollider3.SetActive(true);
        GameObject.FindGameObjectWithTag("CameraFather").GetComponent<CameraFollow>().CurrentAreaCollider = AreaCollider3.GetComponent<BoxCollider>();
        UI.GetComponent<UIManager>().UI_fader.Fade(UIFader.FADE.FadeIn, 2f, 0f);
        this.Spot3.SetActive(true);
        this.TextBox.SetActive(false);
        this.EnemyTraining.SetActive(true);
        this.Coach.transform.position = this.Coach.GetComponent<Couch>().Origin;
        this.Coach.GetComponent<Couch>().startCountinStage2 = false;
        this.Coach.GetComponent<Couch>().timeStage2 = 0;
        this.Coach.GetComponent<Couch>().finalStage2 = false;
        this.Coach.GetComponent<Couch>().speaking = false;
        this.Coach.GetComponent<Couch>().changeDialog2 = 0;
            this.Coach.GetComponent<Couch>().ButtonTap.SetActive(false);
        this.player.transform.position = new Vector3(this.Coach.transform.position.x - 3, this.player.transform.position.y, this.Coach.transform.position.z);
        this.Spot3.transform.position = new Vector3(this.Coach.transform.position.x - 7, this.Spot3.transform.position.y, this.Coach.transform.position.z);
        this.EnemyTraining.transform.position = Spot3.transform.position;
        this.EnemyTraining.GetComponent<DontMoveRobotTutorial>().origin = Spot3.transform.position;
        this.Coach.GetComponent<Couch>().stage3 = true;
        this.Swipe.GetComponent<Swipe>().BlockGrab = false;
        try
        {
            this.Swipe.GetComponent<Swipe>().UpdateSensibility();
        }
        catch
        {
            this.Swipe.GetComponent<SwipeControls>().distanceToGrab = 1.5f;
        }
        this.GetComponent<ShowDojoPointer>().enabled = false;
        this.Swipe.GetComponent<Swipe>().BlockForTutorial = true;
        this.Swipe.GetComponent<Swipe>().BlockPunchTap = true;
        StartCoroutine(WaitToStartStage3());
    }

    IEnumerator WaitToStartStage3()
    {
        yield return new WaitForSeconds(1.5f);
        this.Coach.GetComponent<Couch>().speaking = true;
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

    }
    
}