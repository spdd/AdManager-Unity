using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using If3games.Core.Internal.Ads.Common;

namespace If3games.Core.Internal.Ads.iOS
{
    // Externs used by the iOS component.
    public class IOSInterstitialClient : IInterstitialClient
    {
        #region Interstitial callback types

        internal delegate void IOSUInterstitialDidReceiveAdCallback(IntPtr interstitialClient);
        internal delegate void IOSUInterstitialDidFailToReceiveAdWithErrorCallback(
            IntPtr interstitialClient, string error);
        internal delegate void IOSUInterstitialWillPresentScreenCallback(IntPtr interstitialClient);
        internal delegate void IOSUInterstitialWillDismissScreenCallback(IntPtr interstitialClient);
        internal delegate void IOSUInterstitialDidDismissScreenCallback(IntPtr interstitialClient);
        internal delegate void IOSUInterstitialWillLeaveApplicationCallback(
            IntPtr interstitialClient);

        #endregion

        private IAdListener listener;
        private IntPtr interstitialPtr;

        // This property should be used when setting the interstitialPtr.
        private IntPtr InterstitialPtr
        {
            get
            {
                return interstitialPtr;
            }
            set
            {
                Externs.IOSURelease(interstitialPtr);
                interstitialPtr = value;
            }
        }

        #region IInterstitialClient implementation

        // Create a banner view and add it into the view hierarchy.
        public void CreateInterstitial() {
            IntPtr interstitialClientPtr = (IntPtr) GCHandle.Alloc(this);
            InterstitialPtr = Externs.IOSUCreateInterstitial(interstitialClientPtr);
            Externs.IOSUSetInterstitialCallbacks(
                InterstitialPtr,
                InterstitialDidReceiveAdCallback,
                InterstitialDidFailToReceiveAdWithErrorCallback,
                InterstitialWillPresentScreenCallback,
                InterstitialWillDismissScreenCallback,
                InterstitialDidDismissScreenCallback,
                InterstitialWillLeaveApplicationCallback);
        }

        // set IAdListener
        public void SetAdListener(IAdListener listener) {
            this.listener = listener;
        }

        // Request a new ad for the banner view.
        public void LoadAd() {
            IntPtr requestPtr = Externs.IOSUCreateRequest();

            Externs.IOSURequestInterstitial(InterstitialPtr, requestPtr);
            Externs.IOSURelease(requestPtr);
        }

        // Determines whether the banner has loaded.
        public bool IsLoaded() {
            return Externs.IOSUInterstitialReady(InterstitialPtr);
        }

        // Show the banner view on the screen.
        public void Show() {
            Externs.IOSUShowInterstitial(InterstitialPtr);
        }

        // Destroys a banner view and to free up memory.
        public void DestroyInterstitial() {
            InterstitialPtr = IntPtr.Zero;
        }

        public void Cache () {
            Externs.IOSUCacheInterstitial (InterstitialPtr);
        }

        public bool IsPrecache () {
            return false;
        }

        public void SetAutoCache (bool autoCache) {
            Externs.IOSUSetAutoCacheInterstitial(InterstitialPtr, autoCache);
        }

        public void DisableNetwork (string adName) {
            Externs.IOSUDisableNetworkInterstitial (InterstitialPtr, adName);
        }

        public void ShowAdWithAdName (string adName) {
            Externs.IOSUShowInterstitialWithAdName (InterstitialPtr, adName);
        }

        #endregion

        #region Banner callback methods

        [MonoPInvokeCallback(typeof(IOSUInterstitialDidReceiveAdCallback))]
        private static void InterstitialDidReceiveAdCallback(IntPtr interstitialClient)
        {
            if(IntPtrToInterstitialClient(interstitialClient).listener != null)
                IntPtrToInterstitialClient(interstitialClient).listener.FireAdLoaded();
        }

        [MonoPInvokeCallback(typeof(IOSUInterstitialDidFailToReceiveAdWithErrorCallback))]
        private static void InterstitialDidFailToReceiveAdWithErrorCallback(
            IntPtr interstitialClient, string error)
        {
            if(IntPtrToInterstitialClient(interstitialClient).listener != null)
                IntPtrToInterstitialClient(interstitialClient).listener.FireAdFailedToLoad(error);
        }

        [MonoPInvokeCallback(typeof(IOSUInterstitialWillPresentScreenCallback))]
        private static void InterstitialWillPresentScreenCallback(IntPtr interstitialClient)
        {
            if(IntPtrToInterstitialClient(interstitialClient).listener != null)
                IntPtrToInterstitialClient(interstitialClient).listener.FireAdOpened();
        }

        [MonoPInvokeCallback(typeof(IOSUInterstitialWillDismissScreenCallback))]
        private static void InterstitialWillDismissScreenCallback(IntPtr interstitialClient)
        {
            if(IntPtrToInterstitialClient(interstitialClient).listener != null)
                IntPtrToInterstitialClient(interstitialClient).listener.FireAdClosing();
        }

        [MonoPInvokeCallback(typeof(IOSUInterstitialDidDismissScreenCallback))]
        private static void InterstitialDidDismissScreenCallback(IntPtr interstitialClient)
        {
            if(IntPtrToInterstitialClient(interstitialClient).listener != null)
                IntPtrToInterstitialClient(interstitialClient).listener.FireAdClosed();
        }

        [MonoPInvokeCallback(typeof(IOSUInterstitialWillLeaveApplicationCallback))]
        private static void InterstitialWillLeaveApplicationCallback(IntPtr interstitialClient)
        {
            if(IntPtrToInterstitialClient(interstitialClient).listener != null)
                IntPtrToInterstitialClient(interstitialClient).listener.FireAdLeftApplication();
        }

        private static IOSInterstitialClient IntPtrToInterstitialClient(IntPtr interstitialClient)
        {
            GCHandle handle = (GCHandle) interstitialClient;
            return handle.Target as IOSInterstitialClient;
        }

        #endregion
    }
}
