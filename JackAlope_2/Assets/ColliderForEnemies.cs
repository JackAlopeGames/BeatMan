using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderForEnemies : MonoBehaviour
{

    public GameObject[] Enemies;
    public int count, countColi;
    // Use this for initialization
    void OnEnable()
    {
        StartCoroutine(CheckEnemies());
    }

    IEnumerator CheckEnemies()
    {
        this.count = 0;
        for (int i = 0; i < Enemies.Length; i++)
        {
            try
            {
                if (Enemies[i].transform.position.x > this.gameObject.transform.position.x)
                {
                    this.count++;
                }
                else
                {
                    i = Enemies.Length + 1;
                    break;
                }
            }
            catch { }
        }

        if (count >= Enemies.Length)
        {
            this.gameObject.layer = 11;
        }
        else
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(CheckEnemies());
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            countColi++;
            collision.gameObject.transform.position = new Vector3(this.transform.position.x + 1, 0, collision.transform.position.z);
        }
        if (countColi > 3)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.transform.position = new Vector3(this.transform.position.x + 1.5f, 0, collision.transform.position.z);
            }
            countColi = 0;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            countColi++;
            other.gameObject.transform.position = new Vector3(this.transform.position.x + 1, 0, other.transform.position.z);
        }
        if (countColi > 3)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.transform.position = new Vector3(this.transform.position.x + 1.5f, 0, other.transform.position.z);
            }
            countColi = 0;
        }
    }
}


