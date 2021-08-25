using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;

public class AdManager : MonoBehaviour
{
    private RewardedAd _rewardedAd;
    private bool _isPressed;

    public Button RewardButton;


    private void Update()
    {
        if (_isPressed) // I separate this statement from line 18 to improve robustness. (To avoid null reference exception.)
        {
            if (this._rewardedAd.IsLoaded())
            {
                _isPressed = false;
                RewardButton.interactable = false;
                this._rewardedAd.Show();
            }
        }
    }

    public void RewardEvent()
    {
        _isPressed = true;
        Debug.Log("Rewarded button pressed.");
        this._rewardedAd = new RewardedAd("ca-app-pub-3940256099942544/5224354917");
        AdRequest request = new AdRequest.Builder().Build();

        this._rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this._rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        this._rewardedAd.LoadAd(request);
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
