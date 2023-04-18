using System;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class YandexMobileAdsTest
{
    private const string BannerId = "demo-banner-yandex";
    private const string InterstitialId = "demo-interstitial-yandex";
    private const string RewardedId = "demo-rewarded-yandex";
    
    private Banner _banner;
    private Interstitial _interstitial;
    private RewardedAd _rewardedAd;

    private bool _interstitialLoading;
    private bool _rewardedLoading;
    
    public void ShowBanner()
    {
        if (_banner != null)
            return;
        
        MobileAds.SetAgeRestrictedUser(true); //Sets COPPA restriction for user age under 13

        AdSize bannerMaxSize = AdSize.FlexibleSize(GetScreenWidthDp(), 100);
        _banner = new Banner(BannerId, bannerMaxSize, AdPosition.BottomCenter);

        _banner.OnAdLoaded += HandleBannerAdLoaded;
        _banner.LoadAd(CreateAdRequest());
    }
    
    public void ShowInterstitial()
    {
        if (_interstitialLoading)
            return;
        
        MobileAds.SetAgeRestrictedUser(true); //Sets COPPA restriction for user age under 13

        _interstitial ??= new Interstitial(InterstitialId);
        
        _interstitial.OnInterstitialLoaded += HandleInterstitialLoaded;
        _interstitial.LoadAd(CreateAdRequest());
        _interstitialLoading = true;
    }
    
    public void ShowRewardedAd()
    {
        if (_rewardedLoading)
            return;
        
        MobileAds.SetAgeRestrictedUser(true); //Sets COPPA restriction for user age under 13

        _rewardedAd ??= new RewardedAd(RewardedId);

        _rewardedAd.OnRewardedAdLoaded += HandleRewardedAdLoaded;
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