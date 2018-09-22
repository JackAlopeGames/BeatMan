using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using Heyzap;
using UnityEngine.SceneManagement;

public class UnityAdsInitializer : MonoBehaviour
{
    [SerializeField]
    private string
        androidGameId = "2665359";

    [SerializeField]
    private bool testMode;

    void Awake()
    {
#if UNITY_ANDROID
        /* HeyzapAds.Start("1cfd278f7ac1867f34afbb636aadb0bb", HeyzapAds.FLAG_NO_OPTIONS);
         Heyzap.HeyzapAds.ShowDebugLogs();
         HZIncentivizedAd.Fetch();*/

     
        if (Application.platform == RuntimePlatform.Android ||
             Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string gameId = null;


            gameId = androidGameId;

            if (Advertisement.isSupported && !Advertisement.isInitialized)
            {
                Advertisement.Initialize(gameId, testMode);
            }

            
            Debug.Log(Advertisement.isInitialized);
            
            /*
            AppLovin.InitializeSdk();
            AppLovin.PreloadInterstitial();
            AppLovin.LoadRewardedInterstitial("4bcd90e276831a9d");
            */

            AdColony.AppOptions appOptions = new AdColony.AppOptions();
            appOptions.UserId = "JackAlope Games";
            appOptions.AdOrientation = AdColony.AdOrientationType.AdColonyOrientationPortrait;

            string[] zoneIds = new string[] { "vz10b22995e30f4fe09a", "vz09fc3a34e6ef465c80" };

            AdColony.Ads.Configure("appa6589f5bfeab426d91", appOptions, zoneIds);
        }
#endif
    }
  
}
