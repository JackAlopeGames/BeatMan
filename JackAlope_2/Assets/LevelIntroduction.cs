using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIntroduction : MonoBehaviour {

    // Use this for initialization
    public GameObject LevelButton;
    public string Difficulty;
    public GameObject EasyButton,NormalButton,HardButton;
    public GameObject Item1, Item2;
    public Sprite uimask;

	void OnEnable () {
        EasyButton.GetComponent<Button>().onClick.Invoke();
        Item1.GetComponent<Image>().sprite = uimask;
        Item2.GetComponent<Image>().sprite = uimask;
    }
	
    public void changeItem(int ItemBox, Sprite ItemSprite)
    {
        if(ItemBox == 1)
        {
            Item1.GetComponent<Image>().sprite = ItemSprite;
        }
        if(ItemBox == 2)
        {
            Item2.GetComponent<Image>().sprite = ItemSprite;
        }
    }

    public void Easy()
    {
        Difficulty = "Easy";
        ChangeSpriteButton(EasyButton);
    }

    public void Normal()
    {
        Difficulty = "Normal";
        ChangeSpriteButton(NormalButton);
    }

    public void Hard()
    {
        Difficulty = "Hard";
        ChangeSpriteButton(HardButton);
    }

    public void ChangeSpriteButton(GameObject button)
    {
        EasyButton.GetComponent<Image>().sprite = EasyButton.GetComponent<Button>().spriteState.disabledSprite;
        NormalButton.GetComponent<Image>().sprite = NormalButton.GetComponent<Button>().spriteState.disabledSprite;
        HardButton.GetComponent<Image>().sprite = HardButton.GetComponent<Button>().spriteState.disabledSprite;
        button.GetComponent<Image>().sprite = button.GetComponent<Button>().spriteState.highlightedSprite;
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void PlayInExchangeOfThunders()
    {
        GameObject BC = GameObject.FindGameObjectWithTag("BannerController");
        GameObject SS = GameObject.FindGameObjectWithTag("SavingSystem");
        if (BC.GetComponent<ThunderLoading>().ThunderCount >= 2)
        {
            BC.GetComponent<ThunderLoading>().ThunderCount -= 2;
            SS.GetComponent<SavingSystem>().Thunders -= 2;
            SS.GetComponent<SavingSystem>().Save();
            LevelButton.GetComponent<Button>().onClick.Invoke();
        }
        else
        {
            Debug.Log("You need lightings");
        }
    }
}
