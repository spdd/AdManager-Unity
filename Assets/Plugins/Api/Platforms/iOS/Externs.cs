using System;
using System.Runtime.InteropServices;

namespace If3games.Core.Internal.Ads.iOS
{
    // Externs used by the iOS component.
    public class Externs
    {
        #region Common externs

        [DllImport("__Internal")]
        internal static extern void IOSUInit(string adIds, Boolean autocache);

        [DllImport("__Internal")]
        internal static extern IntPtr IOSUCreateRequest();

        [DllImport("__Internal")]
        internal static extern void IOSURelease(IntPtr obj);

        #endregion

        #region Banner externs

        [DllImport("__Internal")]
        internal static extern IntPtr IOSUCreateBannerView(
            IntPtr bannerClient, int position);

        [DllImport("__Internal")]
        internal static extern void IOSUSetBannerCallbacks(
            IntPtr bannerView,
            IOSBannerClient.IOSUAdViewDidReceiveAdCallback adReceivedCallback,
            IOSBannerClient.IOSUAdViewDidFailToReceiveAdWithErrorCallback adFailedCallback,
            IOSBannerClient.IOSUAdViewWillPresentScreenCallback willPresentCallback,
            IOSBannerClient.IOSUAdViewWillDismissScreenCallback willDismissCallback,
            IOSBannerClient.IOSUAdViewDidDismissScreenCallback didDismissCallback,
            IOSBannerClient.IOSUAdViewWillLeaveApplicationCallback willLeaveCallback);

        [DllImport("__Internal")]
        internal static extern void IOSUHideBannerView(IntPtr bannerView);

        [DllImport("__Internal")]
        internal static extern void IOSUShowBannerView(IntPtr bannerView);

        [DllImport("__Internal")]
        internal static extern bool IOSUBannerReady(IntPtr bannerView);

        [DllImport("__Internal")]
        internal static extern void IOSURemoveBannerView(IntPtr bannerView);

        [DllImport("__Internal")]
        internal static extern void IOSURequestBannerAd(IntPtr bannerView, IntPtr request);

        [DllImport("__Internal")]
        internal static extern void IOSUCacheBanner(IntPtr bannerView);

        [DllImport("__Internal")]
        internal static extern void IOSUSetAutoCacheBanner(IntPtr bannerView, bool autoCache);

        [DllImport("__Internal")]
        internal static extern void IOSUDisableNetworkBanner(IntPtr bannerView, string adName);

        [DllImport("__Internal")]
        internal static extern void IOSUShowBannerWithAdName(IntPtr bannerView, string adName);

        #endregion

        #region Interstitial externs

        [DllImport("__Internal")]
        internal static extern IntPtr IOSUCreateInterstitial(IntPtr interstitialClient);

        [DllImport("__Internal")]
        internal static extern void IOSUSetInterstitialCallbacks(
            IntPtr interstitial,
            IOSInterstitialClient.IOSUInterstitialDidReceiveAdCallback interstitialReceivedCallback,
            IOSInterstitialClient.IOSUInterstitialDidFailToReceiveAdWithErrorCallback
            interstitialFailedCallback,
            IOSInterstitialClient.IOSUInterstitialWillPresentScreenCallback interstitialWillPresentCallback,
            IOSInterstitialClient.IOSUInterstitialWillDismissScreenCallback interstitialWillDismissCallback,
            IOSInterstitialClient.IOSUInterstitialDidDismissScreenCallback interstitialDidDismissCallback,
            IOSInterstitialClient.IOSUInterstitialWillLeaveApplicationCallback
            interstitialWillLeaveCallback);

        [DllImport("__Internal")]
        internal static extern bool IOSUInterstitialReady(IntPtr interstitial);

        [DllImport("__Internal")]
        internal static extern bool IOSUInterstitialIsPrecache(IntPtr interstitial);

        [DllImport("__Internal")]
        internal static extern void IOSUShowInterstitial(IntPtr interstitial);

        [DllImport("__Internal")]
        internal static extern void IOSURequestInterstitial(IntPtr interstitial, IntPtr request);

        [DllImport("__Internal")]
        internal static extern void IOSUCacheInterstitial(IntPtr interstitial);

        [DllImport("__Internal")]
        internal static extern void IOSUSetAutoCacheInterstitial(IntPtr interstitial, bool autoCache);

        [DllImport("__Internal")]
        internal static extern void IOSUDisableNetworkInterstitial(IntPtr interstitial, string adName);

        [DllImport("__Internal")]
        internal static extern void IOSUShowInterstitialWithAdName(IntPtr interstitial, string adName);

        #endregion

        #region Video externs

        [DllImport("__Internal")]
        internal static extern IntPtr IOSUCreateVideo(
            IntPtr videoClient);

        [DllImport("__Internal")]
        internal static extern void IOSUSetVideoCallbacks(
            IntPtr video,
            IOSVideoClient.IOSUVideoDidReceiveAdCallback videoReceivedCallback,
            IOSVideoClient.IOSUVideoDidFailToReceiveAdWithErrorCallback
            videoFailedCallback,
            IOSVideoClient.IOSUVideoWillPresentScreenCallback videoDidPresentCallback,
            IOSVideoClient.IOSUVideoDidDismissScreenCallback videoDidDismissCallback,
            IOSVideoClient.IOSUVideoWillLeaveApplicationCallback
            videoWillLeaveCallback,
            IOSVideoClient.IOSUVideoAdShouldRewardUserCallback videoRewardCallback);

        [DllImport("__Internal")]
        internal static extern bool IOSUVideoReady(IntPtr video);

        [DllImport("__Internal")]
        internal static extern void IOSUShowVideo(IntPtr video);

        [DllImport("__Internal")]
        internal static extern void IOSURequestVideo(IntPtr video, IntPtr request);

        [DllImport("__Internal")]
        internal static extern void IOSUCacheVideo(IntPtr video);

        [DllImport("__Internal")]
        internal static extern void IOSUSetAutoCacheVideo(IntPtr video, bool autoCache);

        [DllImport("__Internal")]
        internal static extern void IOSUDisableNetworkVideo(IntPtr video, string adName);

        [DllImport("__Internal")]
        internal static extern void IOSUShowVideoWithAdName(IntPtr video, string adName);

        #endregion
    }
}
