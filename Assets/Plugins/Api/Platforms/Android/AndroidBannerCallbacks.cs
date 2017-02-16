using UnityEngine;
using System.Collections;
using If3games.Core.Internal.Ads.Common;

namespace If3games.Core.Internal.Ads.Android
{
    public class AndroidBannerCallbacks
    #if UNITY_ANDROID
        : AndroidJavaProxy
    {
        IBannerCallbacks listener; 

        public AndroidBannerCallbacks(IBannerCallbacks listener) : base("com.if3games.admanager.ads.BannerCallbacks") {
            this.listener = listener;
        }

        void onBannerLoaded() {
            //Debug.Log("Android onBannerLoaded");
            listener.onBannerLoaded();
        }

        void onBannerFailedToLoad() {
            //Debug.Log("Android onBannerFailedToLoad");
            listener.onBannerFailedToLoad();
        }

        void onBannerOpened() {
            //Debug.Log("Android onBannerShown");
            listener.onBannerOpened();
        }

        void onBannerClicked() {
            //Debug.Log("Android onBannerClicked");
            listener.onBannerClicked();
        }
    }
    #else
    {
    public AndroidBannerCallbacks(IBannerCallbacks listener) { }
    }
    #endif
}
