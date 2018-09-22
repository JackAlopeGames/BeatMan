using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using Heyzap;
using AdColony;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayAd : MonoBehaviour
{

    int rand;
    public Text Counter;
    public GameObject go;
    public string[] zoneIDs = new string[] { "vz10b22995e30f4fe09a", "vz09fc3a34e6ef465c80" };
    public string AppID  = "appa6589f5bfeab426d91";
    AdColony.InterstitialAd _ad = null;
    public GameObject oldplayer, hud, healthbar, controls, gameoverscreen, enemies, NotReady;

    private void Start()
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
                StartCoroutine(restarWait());
                ConfigureAds();
                RegisterForAdsCallbacks();
                RequestAd();
            }
            else
            {
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



    IEnumerator waitMe()
    {
        yield return new WaitForSeconds(1f);
    }
    IEnumerator restarWait()
    {
        yield return new WaitForSeconds(1f);
        ContinuePlaying();
    }
    

     private void HandleAdResult(ShowResult result)
     {
         switch (result)
         {
             case ShowResult.Finished:
                ContinuePlaying();

                break;
             case ShowResult.Skipped:
                 Debug.Log("The player skiped");
                 break;
             case ShowResult.Failed:
                 Debug.Log("NOT INTERNET MAYBE");
                 break;
         }
     }


    public string taginput = "BeatMan HeyZap";

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

         // AppOptions are optional
         AdColony.AppOptions appOptions = new AdColony.AppOptions();

         appOptions.UserId = "JackAlope Games";
         appOptions.AdOrientation = AdColony.AdOrientationType.AdColonyOrientationPortrait;
         if (Application.platform == RuntimePlatform.Android ||
         Application.platform == RuntimePlatform.IPhonePlayer )
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
             AdColony.Ads.RequestInterstitialAd(ad.ZoneId,null );
         };

        AdColony.Ads.OnRewardGranted += (string zoneId, bool success, string name, int amount) =>
        {
            ContinuePlaying();
        };
    }
     
     void RequestAd()
     {
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
                StartCoroutine(restarWait());
          
        }
        else if (Advertisement.IsReady())
        {
                Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
            
        }else
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
         adOptions.ShowPrePopup = true;
         adOptions.ShowPostPopup = true;
         

         AdColony.Ads.RequestInterstitialAd(zoneIDs[1], adOptions);
     }

     void RegisterForAdsCallbacksReward()
     {

         // Other event registrations...

         AdColony.Ads.OnRewardGranted += (string zoneId, bool success, string name, int amount) =>
         {
             
         };
     }
     private IEnumerator PlayStreamingVideo(string url)
     {
         Handheld.PlayFullScreenMovie(url, Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFill);
         yield return new WaitForEndOfFrame();
         yield return new WaitForEndOfFrame();
         Debug.Log("Video playback completed.");
     }

    public void ContinuePlaying()
    {
        try
        {
            StartCoroutine(WaitSeconds());
            this.oldplayer = GameObject.FindGameObjectWithTag("Player");
            this.healthbar.GetComponent<Slider>().value = 1.0f;
            this.oldplayer.GetComponent<HealthSystem>().AddHealth(20);
            this.oldplayer.GetComponent<HealthSystem>().invulnerable = true;
            this.oldplayer.GetComponent<PlayerMovement>().revived();
            this.oldplayer.GetComponent<PlayerCombat>().Revived();
            this.gameoverscreen.SetActive(false);
            this.controls.SetActive(true);
            this.hud.SetActive(true);
            this.enemies = GameObject.FindGameObjectWithTag("Enemies");
            StartCoroutine(WaitToMove());
        }
        catch {
            Destroy(GameObject.FindGameObjectWithTag("UI"));
            SceneManager.LoadScene("MainMenu");
        }
       
    }
    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(1);
    }
    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(EnemiesShow());
    }
    IEnumerator EnemiesShow()
    {
        this.Counter.gameObject.SetActive(true);
        this.Counter.GetComponent<isCounterActive>().isActive = true;

       this.Counter.text = "3";
        yield return null;
        if (enemies != null)
        {
            foreach (Transform child in this.enemies.gameObject.transform)
            {
                try
                {
                    child.gameObject.GetComponent<EnemyAI>().enableAI = false;
                    child.gameObject.GetComponent<HealthSystem>().invulnerable = true;
                }
                catch { }
            }
            yield return new WaitForSeconds(1);
            this.Counter.text = "2";
            yield return new WaitForSeconds(1);
            this.Counter.text = "1";
            yield return new WaitForSeconds(1);
            this.Counter.text = "¡GO!";
            yield return new WaitForSeconds(1);
            this.Counter.text = "";
            foreach (Transform child in this.enemies.gameObject.transform)
            {
                try
                {
                    child.gameObject.GetComponent<EnemyAI>().enableAI = true;
                    child.gameObject.GetComponent<HealthSystem>().invulnerable = false;
                    this.oldplayer.GetComponent<HealthSystem>().invulnerable = false;
                }
                catch { }
            }
        }
        this.Counter.GetComponent<isCounterActive>().isActive = false;
        this.Counter.gameObject.SetActive(false);
      
    }
}
