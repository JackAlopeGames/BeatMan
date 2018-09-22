using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperCut3DBar : MonoBehaviour {


    public GameObject SwipeControls;
    public GameObject Player;

    void OnEnable()
    {
        this.SwipeControls = GameObject.FindGameObjectWithTag("SwipeControls");
        this.Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame

    // Update is called once per frame
    void Update()
    {
        if (SwipeControls != null )
        {
            if (SwipeControls.GetComponent<Swipe>().Player != null && !SwipeControls.GetComponent<Swipe>().grabing && !SwipeControls.GetComponent<Swipe>().runningMad && !SwipeControls.GetComponent<SwipeControls>().RunningToPunch)
            {
                try
                {
                    this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(SwipeControls.GetComponent<Swipe>().Player.transform.position.x, this.transform.position.y, SwipeControls.GetComponent<Swipe>().Player.transform.position.z), Time.deltaTime * 20);


                    this.transform.GetChild(0).transform.localScale = new Vector3(((SwipeControls.GetComponent<Swipe>().holdTimer) / 10) * 3.7f, this.transform.GetChild(0).localScale.y, this.transform.GetChild(0).localScale.z);
                    if (SwipeControls.GetComponent<Swipe>().holdTimer <= SwipeControls.GetComponent<Swipe>().UpperCutTime/3 || Player.GetComponent<HealthSystem>().CurrentHp<=0)
                    {
                        this.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                        this.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
                    }
                    else if(Player.GetComponent<HealthSystem>().CurrentHp >0)
                    {
                        this.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                        this.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
                    }

                    this.transform.GetChild(0).GetComponent<Material>().color = Color.Lerp(Color.white, Color.green, SwipeControls.GetComponent<Swipe>().holdTimer);

                }
                catch { }

            }
        }
        else
        {
            this.SwipeControls = GameObject.FindGameObjectWithTag("SwipeControls");
        }
    }
}
