using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;

public class AdManager : MonoBehaviour
{
    private RewardedAd _rewardedAd;
    public Button RewardButton;


    public void RewardEvent()
    {
        Debug.Log("Rewarded button pressed.");
        this._rewardedAd = new RewardedAd("ca-app-pub-3940256099942544/5224354917");
        AdRequest request = new AdRequest.Builder().Build();

        this._rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this._rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        this._rewardedAd.LoadAd(request);
        if (this._rewardedAd.IsLoaded())
        {
            this._rewardedAd.Show();
            RewardButton.interactable = false;
        }
    }


    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        if (PlayerPrefs.GetInt("Lives") < 5)
        {
            PlayerPrefs.SetInt("Lives", PlayerPrefs.GetInt("Lives") + 1);
        }
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        if (PlayerPrefs.GetInt("Lives") < 5)
        {
            PlayerPrefs.SetInt("Lives", PlayerPrefs.GetInt("Lives") + 1);
        }
    }
}
