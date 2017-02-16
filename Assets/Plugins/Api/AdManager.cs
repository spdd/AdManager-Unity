using System;
using System.Collections;
using If3games.Core.Internal.Ads.Common;

namespace If3games.Core.Internal.Ads
{
    public class AdManager 
    {
        private static IAdsClient client;

        public static IAdsClient getInstance() {
            if (client == null) {
                client = PlatformClientFactory.GetAdsClient();
            }
            return client;
        }

		public static void initialize()
		{
		#if !UNITY_EDITOR
			string config = ConfigBuilder.BuildConfig();
            getInstance().initialize(config, ConfigBuilder.isDebug());
		#endif
		}

		private static void initialize(AdsSettings settings)
        {   
            #if !UNITY_EDITOR
			string config = ConfigBuilder.BuildConfig(settings);
            getInstance().initialize(config,true);
            #endif
        }

        private static void initialize(string adIds)
        {
            #if !UNITY_EDITOR
            getInstance().initialize(adIds,false);
            #endif
        }

        private static void initialize(string adIds, Boolean isDebug)
        {
            #if !UNITY_EDITOR
            getInstance().initialize(adIds,isDebug);
            #endif
        }
            
        public static void setInterstitialCallbacks(IInterstitialCallbacks listener)
        {
            #if !UNITY_EDITOR
            getInstance().setInterstitialCallbacks (listener);
            #endif
        }

        public static void setVideoCallbacks(IVideoCallbacks listener)
        {
            #if !UNITY_EDITOR
            getInstance().setVideoCallbacks (listener);
            #endif
        }

        public static void setBannerCallbacks(IBannerCallbacks listener)
        {
            #if !UNITY_EDITOR
            getInstance().setBannerCallbacks (listener);
            #endif
        }

        public static void cacheBanner()
        {
            #if !UNITY_EDITOR
            getInstance().cacheBanner ();
            #endif
        }

        public static void cacheInterstitial()
        {
            #if !UNITY_EDITOR
            getInstance().cacheInterstitial ();
            #endif
        }

        public static void cacheVideo()
        {
            #if !UNITY_EDITOR
            getInstance().cacheVideo ();
            #endif
        }

        public static Boolean isBannerLoaded() 
        {
            Boolean isLoaded = false;
            #if !UNITY_EDITOR
            isLoaded = getInstance().isBannerLoaded ();
            #endif
            return isLoaded;
        }

        public static Boolean isInterstitialLoaded() 
        {
            Boolean isLoaded = false;
            #if !UNITY_EDITOR
            isLoaded = getInstance().isInterstitialLoaded ();
            #endif
            return isLoaded;
        }

        public static Boolean isVideoLoaded() 
        {
            Boolean isLoaded = false;
            #if !UNITY_EDITOR
            isLoaded = getInstance().isVideoLoaded ();
            #endif
            return isLoaded;
        }

        public static void showBanner()
        {
            #if !UNITY_EDITOR
            getInstance().showBanner();
            #endif
        }

        public static void showInterstitial()
        {
            #if !UNITY_EDITOR
            getInstance().showInterstitial ();
            #endif
        }

        public static void showRewardedVideo()
        {
            #if !UNITY_EDITOR
            getInstance().showVideo ();
            #endif
        }
            
        public static void hideBanner()
        {
            #if !UNITY_EDITOR
            getInstance().hideBanner ();
            #endif
        }

        public static void setAutoCache(Boolean autoCache) 
        {
            #if !UNITY_EDITOR
            getInstance().setAutoCache (autoCache);
            #endif
        }           

        public static void disableNetwork(String network) 
        {
            #if !UNITY_EDITOR
            getInstance().disableNetwork (network);
            #endif
        }
    }
}