using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using If3games.Core.Internal.Ads.Common;

namespace If3games.Core.Internal.Ads.iOS
{
    // Externs used by the iOS component.
    public class IOSVideoClient : IVideoClient
    {
        #region Video callback types
        internal delegate void IOSUVideoAdShouldRewardUserCallback(IntPtr videoClient, int amount);
        internal delegate void IOSUVideoDidReceiveAdCallback(IntPtr videoClient);
        internal delegate void IOSUVideoDidFailToReceiveAdWithErrorCallback(
            IntPtr videolClient, string error);
        internal delegate void IOSUVideoWillPresentScreenCallback(IntPtr videoClient);
        internal delegate void IOSUVideoDidDismissScreenCallback(IntPtr videoClient);
        internal delegate void IOSUVideoWillLeaveApplicationCallback(
            IntPtr videoClient);

        #endregion

        private IAdListener listener;
        private IntPtr videoPtr;

        // This property should be used when setting the interstitialPtr.
        private IntPtr VideoPtr
        {
            get
            {
                return videoPtr;
            }
            set
            {
                Externs.IOSURelease(videoPtr);
                videoPtr = value;
            }
        }

        #region IVideoClient implementation

        public void CreateVideo() {
            IntPtr videoClientPtr = (IntPtr) GCHandle.Alloc(this);
            VideoPtr = Externs.IOSUCreateVideo(videoClientPtr);
            Externs.IOSUSetVideoCallbacks(
                VideoPtr,
                VideoDidReceiveAdCallback,
                VideoDidFailToReceiveAdWithErrorCallback,
                VideoWillPresentScreenCallback,
                VideoDidDismissScreenCallback,
                VideoWillLeaveApplicationCallback,
                VideoAdShouldRewardUserCallback);
        }

        // set IAdListener
        public void SetAdListener(IAdListener listener) {
            this.listener = listener;
        }

        public void LoadAd() {
            IntPtr requestPtr = Externs.IOSUCreateRequest();

            Externs.IOSURequestVideo(VideoPtr, requestPtr);
            Externs.IOSURelease(requestPtr);
        }

        public bool IsLoaded() {
            return Externs.IOSUVideoReady(VideoPtr);
        }

        public void Show() {
            Externs.IOSUShowVideo(VideoPtr);
        }

        public void DestroyVideoAd() {
            VideoPtr = IntPtr.Zero;
        }

        public void Cache ()  {
            Externs.IOSUCacheVideo (VideoPtr);
        }

        public bool IsPrecache () {
            return false;
        }

        public void SetAutoCache (bool autoCache) {
            Externs.IOSUSetAutoCacheVideo (VideoPtr, autoCache);
        }

        public void DisableNetwork (string adName) {
            Externs.IOSUDisableNetworkVideo (VideoPtr, adName);
        }

        public void ShowAdWithAdName (string adName) {
            Externs.IOSUShowVideoWithAdName (VideoPtr, adName);
        }

        #endregion

        #region Banner callback methods

        [MonoPInvokeCallback(typeof(IOSUVideoDidReceiveAdCallback))]
        private static void VideoDidReceiveAdCallback(IntPtr videoClient)
        {
            if(IntPtrToVideoClient(videoClient).listener != null)
                IntPtrToVideoClient(videoClient).listener.FireAdLoaded();
        }

        [MonoPInvokeCallback(typeof(IOSUVideoDidFailToReceiveAdWithErrorCallback))]
        private static void VideoDidFailToReceiveAdWithErrorCallback(
            IntPtr videoClient, string error)
        {
            if(IntPtrToVideoClient(videoClient).listener != null)
                IntPtrToVideoClient(videoClient).listener.FireAdFailedToLoad(error);
        }

        [MonoPInvokeCallback(typeof(IOSUVideoWillPresentScreenCallback))]
        private static void VideoWillPresentScreenCallback(IntPtr videoClient)
        {
            if(IntPtrToVideoClient(videoClient).listener != null)
                IntPtrToVideoClient(videoClient).listener.FireAdOpened();
        }

        [MonoPInvokeCallback(typeof(IOSUVideoDidDismissScreenCallback))]
        private static void VideoDidDismissScreenCallback(IntPtr videoClient)
        {
            if(IntPtrToVideoClient(videoClient).listener != null)
                IntPtrToVideoClient(videoClient).listener.FireAdClosed();
        }

        [MonoPInvokeCallback(typeof(IOSUVideoWillLeaveApplicationCallback))]
        private static void VideoWillLeaveApplicationCallback(IntPtr videoClient)
        {
            if(IntPtrToVideoClient(videoClient).listener != null)
                IntPtrToVideoClient(videoClient).listener.FireAdLeftApplication();
        }

        [MonoPInvokeCallback(typeof(IOSUVideoAdShouldRewardUserCallback))]
        private static void VideoAdShouldRewardUserCallback(IntPtr videoClient, int amount)
        {
            if(IntPtrToVideoClient(videoClient).listener != null)
                IntPtrToVideoClient(videoClient).listener.FireRewardUser(amount);
        }

        private static IOSVideoClient IntPtrToVideoClient(IntPtr videoClient)
        {
            GCHandle handle = (GCHandle) videoClient;
            return handle.Target as IOSVideoClient;
        }

        #endregion
    }
}