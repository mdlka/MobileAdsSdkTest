using System;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class YandexMobileAdsTest
{
    private const string BannerId = "R-M-2304865-1";
    private const string InterstitialId = "R-M-2304865-2";
    private const string RewardedId = "R-M-2304865-3";
    
    private Banner _banner;
    private Interstitial _interstitial;
    private RewardedAd _rewardedAd;

    private bool _interstitialLoading;
    private bool _rewardedLoading;
    
    public void ShowBanner(string id)
    {
        if (_banner != null)
            return;
        
        MobileAds.SetAgeRestrictedUser(true); //Sets COPPA restriction for user age under 13

        AdSize bannerMaxSize = AdSize.FlexibleSize(GetScreenWidthDp(), 100);
        _banner = new Banner(id, bannerMaxSize, AdPosition.BottomCenter);
        _banner.OnAdFailedToLoad += (sender, args) => Debug.Log("Banner failed to load: " + args.Message);

        _banner.OnAdLoaded += HandleBannerAdLoaded;
        _banner.LoadAd(CreateAdRequest());
    }
    
    public void ShowInterstitial(string id)
    {
        if (_interstitialLoading)
            return;
        
        MobileAds.SetAgeRestrictedUser(true); //Sets COPPA restriction for user age under 13

        _interstitial ??= new Interstitial(id);
        
        _interstitial.OnInterstitialLoaded += HandleInterstitialLoaded;
        _interstitial.OnInterstitialFailedToShow += (sender, args) => Debug.Log("Interstitial failed to show: " + args.Message);
        _interstitial.OnInterstitialDismissed += (sender, args) => Debug.Log("Interstitial dismissed");
        _interstitial.OnInterstitialFailedToLoad += (sender, args) => Debug.Log("Interstitial failed to load: " + args.Message);
        _interstitial.LoadAd(CreateAdRequest());
        _interstitialLoading = true;
    }
    
    public void ShowRewardedAd(string id)
    {
        if (_rewardedLoading)
            return;
        
        MobileAds.SetAgeRestrictedUser(true); //Sets COPPA restriction for user age under 13

        _rewardedAd ??= new RewardedAd(id);

        _rewardedAd.OnRewardedAdLoaded += HandleRewardedAdLoaded;
        _rewardedAd.OnRewardedAdFailedToShow += (sender, args) => Debug.Log("Rewarded failed to show: " + args.Message);
        _rewardedAd.OnRewardedAdDismissed += (sender, args) => Debug.Log("Rewarded dismissed");
        _rewardedAd.OnRewardedAdFailedToLoad += (sender, args) => Debug.Log("Rewarded failed to load: " + args.Message);
        _rewardedAd.LoadAd(CreateAdRequest());
        _rewardedLoading = true;
    }

    private void HandleBannerAdLoaded(object sender, EventArgs args)
    {
        _banner.Show();
        _banner.OnAdLoaded -= HandleBannerAdLoaded;
    }

    private void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        _interstitial.Show();
        _interstitial.OnInterstitialLoaded -= HandleInterstitialLoaded;
        _interstitialLoading = false;
    }

    private void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        _rewardedAd.Show();
        _rewardedAd.OnRewardedAdLoaded -= HandleRewardedAdLoaded;
        _rewardedLoading = false;
    }

    private int GetScreenWidthDp()
    {
        int screenWidth = (int)Screen.safeArea.width;
        return ScreenUtils.ConvertPixelsToDp(screenWidth);
    }
    
    private AdRequest CreateAdRequest() => new AdRequest.Builder().Build();
}