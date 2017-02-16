using UnityEngine;
using System;
using If3games.Core.Internal.Ads.Common;

namespace If3games.Core.Internal.Ads.iOS
{
    public class IOSAdsClient : IAdsClient 
    {
        private IBannerClient bannerClient;
        private IInterstitialClient interstitialClient;
        private IVideoClient videoClient;

        public IBannerClient getBannerClient() {
            if (bannerClient == null) {
                bannerClient = PlatformClientFactory.GetBannerClient ();
                bannerClient.CreateBanner (AdPosition.BottomPortrait);
                bannerClient.LoadAd ();
            }
            return bannerClient;
        }

        public IInterstitialClient getInterstitialClient() {
            if (interstitialClient == null) {
                interstitialClient = PlatformClientFactory.GetInterstitialClient ();
                interstitialClient.CreateInterstitial ();
            }
            return interstitialClient;
        }

        public IVideoClient getVideoClient() {
            if (videoClient == null) {
                videoClient = PlatformClientFactory.GetVideoClient ();
                videoClient.CreateVideo ();
            }
            return videoClient;
        }

        public void initialize(string adIds, Boolean autocache) {
            Externs.IOSUInit(adIds, autocache);
        }

        public void setInterstitialCallbacks(IInterstitialCallbacks userCallbacks) 
        {
            getInterstitialClient ().SetAdListener (new IOSInterstitialAdListener(userCallbacks));
        }

        public void setVideoCallbacks(IVideoCallbacks userCallbacks)
        {
            getVideoClient ().SetAdListener (new IOSVideoAdListener(userCallbacks));
        }

        public void setBannerCallbacks(IBannerCallbacks userCallbacks)
        {
            getBannerClient ().SetAdListener (new IOSBannerAdListener(userCallbacks));
        }

        public void showBanner() {
            getBannerClient ().Show ();
        }

        public void showInterstitial() {
            getInterstitialClient ().Show ();
        }

        public void showVideo() {
            getVideoClient ().Show ();
        }

        public void cacheBanner() {
            getBannerClient ().Cache ();
        }

        public void cacheInterstitial() {
            getInterstitialClient ().Cache ();
        }

        public void cacheVideo() {
            getVideoClient ().Cache ();
        }

        public Boolean isBannerLoaded () {
            return getBannerClient ().IsLoaded();
        }

        public Boolean isInterstitialLoaded () {
            return getInterstitialClient ().IsLoaded();
        }

        public Boolean isVideoLoaded () {
            return getVideoClient ().IsLoaded();
        }

        public void hideBanner () {
            getBannerClient ().Hide();
        }

        public void setAutoCache (Boolean autoCache) {
            getBannerClient ().SetAutoCache (autoCache);
            getInterstitialClient ().SetAutoCache (autoCache);
            getVideoClient ().SetAutoCache (autoCache);
        }

        public void setTesting(Boolean test) {}
        public string getVersion() { return "0.0.1"; }
        public void orientationChange () {}
        public void disableNetwork (string network) {}
	}
}
