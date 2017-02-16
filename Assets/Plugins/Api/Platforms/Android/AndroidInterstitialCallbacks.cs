using UnityEngine;
using System;
using If3games.Core.Internal.Ads.Common;

namespace If3games.Core.Internal.Ads.Android
{
    public class AndroidInterstitialCallbacks
    #if UNITY_ANDROID
        : AndroidJavaProxy
    {
        IInterstitialCallbacks listener;   

        public AndroidInterstitialCallbacks(IInterstitialCallbacks listener) : base("com.if3games.admanager.ads.InterstitialCallbacks") {
            this.listener = listener;
        }

        void onInterstitialLoaded() {
            //Debug.Log("Android onInterstitialLoaded");
            listener.onInterstitialLoaded();
        }

        void onInterstitialFailedToLoad() {
            //Debug.Log("Android onInterstitialFailedToLoad");
            listener.onInterstitialFailedToLoad();
        }

        void onInterstitialOpened() {
            //Debug.Log("Android onInterstitialShown");
            listener.onInterstitialOpened();
        }

        void onInterstitialClicked() {
            //Debug.Log("Android onInterstitialClicked");
            listener.onInterstitialClicked();
        }

        void onInterstitialClosed() {
            //Debug.Log("Android onInterstitialClosed");
            listener.onInterstitialClosed();
        }
    }
    #else
    {
    public AndroidInterstitialCallbacks(IInterstitialCallbacks listener) { }
    }
    #endif
}
