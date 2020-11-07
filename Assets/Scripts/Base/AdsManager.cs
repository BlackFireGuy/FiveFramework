using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : BaseSingleton<AdsManager>
{
    string placementID = "rewardedVideo";
    public void ShowRewardAds()
    {
        Advertisement.Show(placementID);
    }
}