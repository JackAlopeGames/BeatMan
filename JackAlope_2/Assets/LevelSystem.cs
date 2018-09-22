using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{

    // Use this for initialization
    private GameObject SavingSystem;
    public GameObject Text, LevelUpNotify;
    private GameObject player;
    public int CurrentLevel;
    bool change;
    private bool firstTime;
    void OnEnable()
    {
        this.SavingSystem = GameObject.FindGameObjectWithTag("SavingSystem");
        this.SavingSystem.GetComponent<SavingSystem>().Load();
        this.CurrentLevel = SavingSystem.GetComponent<SavingSystem>().Level;
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.firstTime = true;
        MakePlayerStronger();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (this.Text.gameObject.GetComponent<Text>().text != "Lvl: " + this.CurrentLevel)
            {
                this.Text.gameObject.GetComponent<Text>().text = "Lvl: " + this.CurrentLevel;
                MakePlayerStronger();
            }
        }
        catch { }
    }

    public void MakePlayerStronger()
    {
        try
        {
            for (int i = 0; i < player.GetComponent<PlayerCombat>().PunchCombo.Length; i++)
            {
                player.GetComponent<PlayerCombat>().PunchCombo[i].damage += (this.CurrentLevel / 2);
            }
            for (int i = 0; i < player.GetComponent<PlayerCombat>().KickCombo.Length; i++)
            {
                player.GetComponent<PlayerCombat>().KickCombo[i].damage += (this.CurrentLevel / 2);
            }
            player.GetComponent<PlayerCombat>().JumpKickData.damage += (this.CurrentLevel / 2);
            player.GetComponent<PlayerCombat>().GroundPunchData.damage += (this.CurrentLevel / 2);
            player.GetComponent<PlayerCombat>().GroundKickData.damage += (this.CurrentLevel / 2);

            player.GetComponent<HealthSystem>().MaxHp += (this.CurrentLevel * 2);
            player.GetComponent<HealthSystem>().CurrentHp = player.GetComponent<HealthSystem>().MaxHp;
        }
        catch { }
    }

    public void ShowCongratLevelUp()
    {
        StartCoroutine(LevelUp());
    }
    IEnumerator LevelUp()
    {
        this.LevelUpNotify.SetActive(true);
        yield return new WaitForSeconds(2);
        this.LevelUpNotify.SetActive(false);
    }
}