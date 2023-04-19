using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private Button _playInterButton;
    [SerializeField] private Button _playRewardButton;
    [SerializeField] private Button _showBannerButton;
    [SerializeField] private TMP_InputField _inputField;

    private YandexMobileAdsTest _yandexMobileAds;

    private void Awake()
    {
        _yandexMobileAds = new YandexMobileAdsTest();
    }

    private void OnEnable()
    {
        _playInterButton.onClick.AddListener(OnPlayInterButton);
        _playRewardButton.onClick.AddListener(OnPlayRewardButton);
        _showBannerButton.onClick.AddListener(OnShowBannerButton);
    }

    private void OnDisable()
    {
        _playInterButton.onClick.RemoveListener(OnPlayInterButton);
        _playRewardButton.onClick.RemoveListener(OnPlayRewardButton);
        _showBannerButton.onClick.RemoveListener(OnShowBannerButton);
    }

    private void OnPlayRewardButton() => _yandexMobileAds.ShowRewardedAd(_inputField.text);
    private void OnShowBannerButton() => _yandexMobileAds.ShowBanner(_inputField.text);
    private void OnPlayInterButton() => _yandexMobileAds.ShowInterstitial(_inputField.text);
}