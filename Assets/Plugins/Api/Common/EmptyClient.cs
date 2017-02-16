using UnityEngine;

namespace If3games.Core.Internal.Ads.Common
{
    public class EmptyClient : IBannerClient, IInterstitialClient, IVideoClient
    {
        public EmptyClient()
        {
            Debug.Log("Created EmptyClient");
        }

        public void CreateBanner(AdPosition position)
        {
            Debug.Log("Empty CreateBanner");
        }

        public void CreateInterstitial()
        {
            Debug.Log("Empty CreateInterstitial");
        }

        public void CreateVideo()
        {
            Debug.Log("Empty CreateVideo");
        }

        public void SetAdListener(IAdListener listener) {
            Debug.Log("Empty SetAdListener");
        }

        public void LoadAd()
        {
            Debug.Log("Empty LoadAd");
        }

        public void Show()
        {
            Debug.Log("Empty ShowBannerView");
        }

        public void Hide()
        {
            Debug.Log("Empty HideBannerView");
        }

        public void DestroyBannerView()
        {
            Debug.Log("Empty DestroyBannerView");
        }

        public bool IsLoaded() {
            Debug.Log("Empty IsLoaded");
            return true;
        }

        public void ShowInterstitial() {
            Debug.Log("Empty ShowInterstitial");
        }

        public void DestroyInterstitial() {
            Debug.Log("Empty DestroyInterstitial");
        }

        public bool IsVideoLoaded() {
            Debug.Log("Empty IsVideoLoaded");
            return true;
        }

        public void ShowVideoAd() {
            Debug.Log("Empty ShowVideoAd");
        }

        public void DestroyVideoAd() {
            Debug.Log("Empty DestroyVideoAd");
        }

        public void Cache () 
        {
            Debug.Log("Empty Cache");
        }

        public bool IsPrecache ()
        {
            Debug.Log("Empty IsPrecache");
            return false;
        }

        public void SetAutoCache (bool autoCache)
        {
            Debug.Log("Empty SetAutoCache");
        }

        public void DisableNetwork (string adName)
        {
            Debug.Log("Empty DisableNetwork");
        }

        public void ShowAdWithAdName (string adName)
        {
            Debug.Log("Empty ShowAdWithAdName");
        }
    }
}
