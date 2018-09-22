using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using Heyzap;
using AdColony;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JustPlayAd : MonoBehaviour
{

    int rand;
    public string[] zoneIDs = new string[] { "vz10b22995e30f4fe09a", "vz09fc3a34e6ef465c80" };
    public string AppID = "appa6589f5bfeab426d91";
    AdColony.InterstitialAd _ad = null;
    public GameObject NotReady,getCoinsMenu, adForCoinsMenu;
    float currencyPopupTimer = 0.0f;

    public void Start()
    {

        ConfigureAds();
        RegisterForAdsCallbacks();
        RequestAd();
     
    }


    public void WatchADs()
    {
        rand = Random.Range(0, 2);
       
#if UNITY_ANDROID
        if (rand == 0)
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
            }
            else if (_ad != null)
            {
                AdColony.Ads.ShowAd(_ad);
                GiveCoins();
                ConfigureAds();
                RegisterForAdsCallbacks();
                RequestAd();
            }
            else {
                NotReady.SetActive(true);
            }
        }
        
        if (rand == 1)
        {
            ConfigureAds();
            RegisterForAdsCallbacks();
            RequestAd();
            PlayAdv();
        }
#endif

    }

    IEnumerator restarWait()
    {
        yield return new WaitForSeconds(1f);
        GiveCoins();
        /*Debug.Log("Player gain 5 GEMS");
        go = GameObject.FindGameObjectWithTag("GameOver");
        go.GetComponent<GameOverScrn>().RestartLevel();*/
    }
    

    private void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                GiveCoins();

                break;
            case ShowResult.Skipped:
                Debug.Log("The player skiped");
                break;
            case ShowResult.Failed:
                Debug.Log("NOT INTERNET MAYBE");
                break;
        }
    }


    public string taginput = "Beat Man HeyZap";

    private string adTag()
    {
        string tag = this.taginput;
        if (tag == null || tag.Trim().Length == 0)
        {
            return "default";
        }
        else
        {
            return tag;
        }
    }


    void ConfigureAds()
    {
        Debug.Log("**** Configure ADC SDK ****");
        // AppOptions are optional
        AdColony.AppOptions appOptions = new AdColony.AppOptions();

        appOptions.UserId = "JackAlope Games";
        appOptions.AdOrientation = AdColony.AdOrientationType.AdColonyOrientationPortrait;
        if (Application.platform == RuntimePlatform.Android ||
        Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Ads.Configure(this.AppID, appOptions, this.zoneIDs);
        }


        
        
    }

    void RegisterForAdsCallbacks()
    {
        AdColony.Ads.OnRequestInterstitial += (AdColony.InterstitialAd ad) =>
        {
            if (_ad == null)
            {
                _ad = ad;
            }
        };

        AdColony.Ads.OnExpiring += (AdColony.InterstitialAd ad) =>
        {
            AdColony.Ads.RequestInterstitialAd(ad.ZoneId, null);
        };

        AdColony.Ads.OnRewardGranted += (string zoneId, bool success, string name, int amount) =>
        {
            GiveCoins();
            //gameoverfunctions.GetComponent<GameOverScrn>().RestartLevel();
        };
    }

    void RequestAd()
    {
        Debug.Log("**** Request Ad ****");
        AdColony.AdOptions adOptions = new AdColony.AdOptions();
        adOptions.ShowPrePopup = true;
        adOptions.ShowPostPopup = true;
        if (Application.platform == RuntimePlatform.Android ||
        Application.platform == RuntimePlatform.IPhonePlayer)
        {
            AdColony.Ads.RequestInterstitialAd(zoneIDs[0], adOptions);
        }
        
    }

    void PlayAdv()
    {
        if (_ad != null)
        {
                AdColony.Ads.ShowAd(_ad);
                GiveCoins();
        }
        else if (Advertisement.IsReady())
        {
                Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
        }
        else
        {
            NotReady.SetActive(true);
        }

        ConfigureAds();
        RegisterForAdsCallbacks();
        RequestAd();
    }

    void RequestAdReward()
    {
        AdColony.AdOptions adOptions = new AdColony.AdOptions();
        adOptions.ShowPrePopup = false;
        adOptions.ShowPostPopup = false;

        AdColony.Ads.RequestInterstitialAd(zoneIDs[1], adOptions);
    }

    void RegisterForAdsCallbacksReward()
    {

        // Other event registrations...

        AdColony.Ads.OnRewardGranted += (string zoneId, bool success, string name, int amount) =>
        {
            GiveCoins();
        };
    }
    private IEnumerator PlayStreamingVideo(string url)
    {
        Handheld.PlayFullScreenMovie(url, Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFill);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Debug.Log("Video playback completed.");
    }

    public void GiveCoins()
    {
        try
        {
            GlobalAudioPlayer.PlaySFX("RewardCoins");
            getCoinsMenu.GetComponent<GetCoinsMenu>().RewardWithCoins();
            GameObject.FindGameObjectWithTag("SavingSystem").GetComponent<SavingSystem>().Save();
            if (SceneManager.GetSceneByName("MainMenu").isLoaded)
            {
                adForCoinsMenu.GetComponent<AdForCoinsMenu>().WantCoins();
                GameObject Notify = GameObject.FindGameObjectWithTag("RewardCoinsNotify");
                for (int i = 0; i < 3; i++)
                {
                    Notify.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            else
            {
                Destroy(GameObject.FindGameObjectWithTag("UI"));
                Destroy(GameObject.FindGameObjectWithTag("SavingSystem"));
                Destroy(GameObject.FindGameObjectWithTag("SaveWhenPaused"));
                SceneManager.LoadScene("MainMenu");
            }
        }
        catch
        {
            Destroy(GameObject.FindGameObjectWithTag("UI"));
            Destroy(GameObject.FindGameObjectWithTag("SavingSystem"));
            try
            {
                Destroy(GameObject.FindGameObjectWithTag("SaveWhenPaused"));
            }
            catch { }
            SceneManager.LoadScene("MainMenu");
        }
    }
   
}
