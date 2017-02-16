using UnityEngine;
using System;
using If3games.Core.Internal.Ads.Common;

namespace If3games.Core.Internal.Ads.Android
{
    public class AndroidAdsClient : IAdsClient 
    {
        AndroidJavaClass adsManagerClass;
        AndroidJavaObject activity;

        public AndroidJavaClass getAdsMamagerClass() {
            if (adsManagerClass == null) {
                adsManagerClass = new AndroidJavaClass("com.if3games.admanager.ads.AdsManager");
            }
            return adsManagerClass;
        }

        public AndroidJavaObject getActivity() {
            if (activity == null) {
                AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            }
            return activity;
        }

        public void initialize(string adIds, Boolean autocache) 
        {
            getAdsMamagerClass().CallStatic("initialize", getActivity(), adIds, autocache);
        }

        public void initialize(string adIds, int adTypes) 
        {
            getAdsMamagerClass().CallStatic("initialize", getActivity(), adIds, adTypes);
        }

        public void setInterstitialCallbacks(IInterstitialCallbacks listener) 
        {
            getAdsMamagerClass().CallStatic("setInterstitialCallbacks", new AndroidInterstitialCallbacks(listener));
        }

        public void setVideoCallbacks(IVideoCallbacks listener)
        {
            getAdsMamagerClass().CallStatic("setVideoCallbacks", new AndroidVideoCallbacks(listener));
        }

        public void setBannerCallbacks(IBannerCallbacks listener)
        {
            getAdsMamagerClass().CallStatic("setBannerCallbacks", new AndroidBannerCallbacks(listener));
        }

        public void cacheBanner()
        {
            getAdsMamagerClass().CallStatic("cacheBanner", getActivity());
        }

        public void cacheInterstitial()
        {
            getAdsMamagerClass().CallStatic("cacheInterstitial", getActivity());
        }

        public void cacheVideo()
        {
            getAdsMamagerClass().CallStatic("cacheVideo", getActivity());
        }

        public Boolean isBannerLoaded() 
        {
            return getAdsMamagerClass().CallStatic<Boolean>("isBannerLoaded");
        }

        public Boolean isInterstitialLoaded() 
        {
            return getAdsMamagerClass().CallStatic<Boolean>("isInterstitialLoaded");
        }

        public Boolean isVideoLoaded() 
        {
            return getAdsMamagerClass().CallStatic<Boolean>("isVideoLoaded");
        }

        public void showBanner()
        {
            //getAdsMamagerClass().CallStatic("showBanner", getActivity());
        }

        public void showInterstitial()
        {
            getAdsMamagerClass().CallStatic("showInterstitial", getActivity());
        }

        public void showVideo()
        {
            getAdsMamagerClass().CallStatic("showVideo", getActivity());
        }

        public void hideBanner()
        {
            //getAdsMamagerClass().CallStatic("hideBanner", getActivity());
        }

        public void setAutoCache(Boolean autoCache) 
        {
            //getAdsMamagerClass().CallStatic("setBannerAutoCache", autoCache);  
        }

        public void setTesting(Boolean test) {}
        public string getVersion() { return "0.0.1"; }
        public void orientationChange () {}
        public void disableNetwork (string network) {}
    }
}
