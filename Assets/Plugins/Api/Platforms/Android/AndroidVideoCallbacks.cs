using UnityEngine;
using System;
using If3games.Core.Internal.Ads.Common;

namespace If3games.Core.Internal.Ads.Android
{
    public class AndroidVideoCallbacks 
    #if UNITY_ANDROID
        : AndroidJavaProxy
    {
        IVideoCallbacks listener;

        public AndroidVideoCallbacks(IVideoCallbacks listener) : base("com.if3games.admanager.ads.VideoCallbacks") {
            this.listener = listener;
        }

        void onVideoLoaded() {
            //Debug.Log("Android onVideoLoaded");
            listener.onVideoLoaded();
        }

        void onVideoFailedToLoad() {
            //Debug.Log("Android onVideoFailedToLoad");
            listener.onVideoFailedToLoad();
        }

        void onVideoOpened() {
            //Debug.Log("Android onVideoShown");
            listener.onVideoOpened();
        }

        void onVideoFinished() {
            //Debug.Log("Android onVideoFinished");
            listener.onVideoFinished();
        }

        void onVideoClosed() {
            //Debug.Log("Android onVideoClosed");
            listener.onVideoClosed();
        }

        void onVideoClicked() {
            //Debug.Log("Android onVideoClosed");
            listener.onVideoClicked();
        }
    }
    #else
    {
    public AndroidVideoCallbacks(IVideoCallbacks listener) { }
    }
    #endif
}
