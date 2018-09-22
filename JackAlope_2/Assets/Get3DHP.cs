using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Get3DHP : MonoBehaviour {

    // Use this for initialization
    public GameObject Enemy;

    private int punches;
    void OnEnable()
    {
    }

    // Update is called once per frame

    // Update is called once per frame
    void Update () {
        if (Enemy != null)
        {
            if (punches > 2)
            {
                Destroy(this.gameObject);
            }

            try
            {
                this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.Enemy.transform.position.x, this.transform.position.y, this.Enemy.transform.position.z), Time.deltaTime * 20);

                if (this.GetComponent<TextMesh>().text != this.Enemy.GetComponent<HealthSystem>().CurrentHp + "")
                {
                    punches++;
                    this.GetComponent<TextMesh>().text = this.Enemy.GetComponent<HealthSystem>().CurrentHp + "";
                }
            }
            catch
            {
                    this.GetComponent<TextMesh>().text = -1 + "";

            }
            if (this.GetComponent<TextMesh>().text == "0")
            {
                Destroy(this.gameObject);
            }
        }

        if(this.gameObject.tag == "ShadowHP")
        {
            this.GetComponent<TextMesh>().text = this.transform.parent.GetComponent<TextMesh>().text;
        }
	}
}
