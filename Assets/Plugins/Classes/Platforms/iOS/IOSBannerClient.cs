using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using If3games.Core.Internal.Ads.Common;

namespace If3games.Core.Internal.Ads.iOS
{
    // Externs used by the iOS component.
    public class IOSBannerClient : IBannerClient
    {
        #region Banner callback types

        internal delegate void IOSUAdViewDidReceiveAdCallback(IntPtr bannerClient);
        internal delegate void IOSUAdViewDidFailToReceiveAdWithErrorCallback(
            IntPtr bannerClient, string error);
        internal delegate void IOSUAdViewWillPresentScreenCallback(IntPtr bannerClient);
        internal delegate void IOSUAdViewWillDismissScreenCallback(IntPtr bannerClient);
        internal delegate void IOSUAdViewDidDismissScreenCallback(IntPtr bannerClient);
        internal delegate void IOSUAdViewWillLeaveApplicationCallback(IntPtr bannerClient);

        #endregion

        private IntPtr bannerViewPtr;
        private IAdListener listener;
       
        // This property should be used when setting the bannerViewPtr.
        private IntPtr BannerViewPtr
        {
            get
            {
                return bannerViewPtr;
            }
            set
            {
                //Externs.IOSURelease(bannerViewPtr);
                bannerViewPtr = value;
            }
        }

        #region IBannerClient implementation

        // Create a banner view and add it into the view hierarchy.
        public void CreateBanner(AdPosition position) {
            IntPtr bannerClientPtr = (IntPtr) GCHandle.Alloc(this);

            BannerViewPtr = Externs.IOSUCreateBannerView(
                bannerClientPtr, (int)position);

            Externs.IOSUSetBannerCallbacks(
                BannerViewPtr,
                AdViewDidReceiveAdCallback,
                AdViewDidFailToReceiveAdWithErrorCallback,
                AdViewWillPresentScreenCallback,
                AdViewWillDismissScreenCallback,
                AdViewDidDismissScreenCallback,
                AdViewWillLeaveApplicationCallback);
        }

        // set IAdListener
        public void SetAdListener(IAdListener listener) {
            this.listener = listener;
        }

        // Request a new ad for the banner view.
        public void LoadAd() {
            IntPtr requestPtr = Externs.IOSUCreateRequest();

            Externs.IOSURequestBannerAd(BannerViewPtr, requestPtr);
            Externs.IOSURelease(requestPtr);
        }

        // Determines whether the banner has loaded.
        public bool IsLoaded() {
            return Externs.IOSUBannerReady (BannerViewPtr);
        }

        // Show the banner view on the screen.
        public void Show() {
            Externs.IOSUShowBannerView(BannerViewPtr);
        }

        // Hide the banner view from the screen.
        public void Hide() {
            Externs.IOSUHideBannerView(BannerViewPtr);
        }

        // Destroys a banner view and to free up memory.
        public void DestroyBannerView() {
            Externs.IOSURemoveBannerView(BannerViewPtr);
            BannerViewPtr = IntPtr.Zero;
        }

        public void Cache () {
            Externs.IOSUCacheBanner (BannerViewPtr);
        }

        public bool IsPrecache () {
            return false;
        }

        public void SetAutoCache (bool autoCache) {
            Externs.IOSUSetAutoCacheBanner (BannerViewPtr, autoCache);
        }

        public void DisableNetwork (string adName) {
            Externs.IOSUDisableNetworkBanner (BannerViewPtr, adName);
        }

        public void ShowAdWithAdName (string adName) {
            Externs.IOSUShowBannerWithAdName (BannerViewPtr, adName);
        }

        #endregion

        #region Banner callback methods

        [MonoPInvokeCallback(typeof(IOSUAdViewDidReceiveAdCallback))]
        private static void AdViewDidReceiveAdCallback(IntPtr bannerClient)
        {
            if(IntPtrToBannerClient(bannerClient).listener != null)
                IntPtrToBannerClient(bannerClient).listener.FireAdLoaded();
        }

        [MonoPInvokeCallback(typeof(IOSUAdViewDidFailToReceiveAdWithErrorCallback))]
        private static void AdViewDidFailToReceiveAdWithErrorCallback(
            IntPtr bannerClient, string error)
        {
            if(IntPtrToBannerClient(bannerClient).listener != null)
                IntPtrToBannerClient(bannerClient).listener.FireAdFailedToLoad(error);
        }

        [MonoPInvokeCallback(typeof(IOSUAdViewWillPresentScreenCallback))]
        private static void AdViewWillPresentScreenCallback(IntPtr bannerClient)
        {
            if(IntPtrToBannerClient(bannerClient).listener != null)
                IntPtrToBannerClient(bannerClient).listener.FireAdOpened();
        }

        [MonoPInvokeCallback(typeof(IOSUAdViewWillDismissScreenCallback))]
        private static void AdViewWillDismissScreenCallback(IntPtr bannerClient)
        {
            if(IntPtrToBannerClient(bannerClient).listener != null)
                IntPtrToBannerClient(bannerClient).listener.FireAdClosing();
        }

        [MonoPInvokeCallback(typeof(IOSUAdViewDidDismissScreenCallback))]
        private static void AdViewDidDismissScreenCallback(IntPtr bannerClient)
        {
            if(IntPtrToBannerClient(bannerClient).listener != null)
                IntPtrToBannerClient(bannerClient).listener.FireAdClosed();
        }

        [MonoPInvokeCallback(typeof(IOSUAdViewWillLeaveApplicationCallback))]
        private static void AdViewWillLeaveApplicationCallback(IntPtr bannerClient)
        {
            if(IntPtrToBannerClient(bannerClient).listener != null)
                IntPtrToBannerClient(bannerClient).listener.FireAdLeftApplication();
        }

        private static IOSBannerClient IntPtrToBannerClient(IntPtr bannerClient)
        {
            GCHandle handle = (GCHandle) bannerClient;
            return handle.Target as IOSBannerClient;
        }

        #endregion
    }
}

