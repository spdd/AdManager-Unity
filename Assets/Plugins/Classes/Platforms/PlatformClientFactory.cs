using UnityEngine;
using System.Collections;
using If3games.Core.Internal.Ads.Common;

namespace If3games.Core.Internal.Ads
{
    public class PlatformClientFactory 
    {
        public static IAdsClient GetAdsClient()
        {
            #if UNITY_EDITOR
            // Testing UNITY_EDITOR first because the editor also responds to the currently
            // selected platform.
            return null;
            #elif UNITY_ANDROID
            return new If3games.Core.Internal.Ads.Android.AndroidAdsClient();
            #elif UNITY_IPHONE
            return new If3games.Core.Internal.Ads.iOS.IOSAdsClient();
            #elif WINDOWS_PHONE
            return new If3games.Core.Internal.Ads.WP8.WPAdsClient();
            #else
            return null;
            #endif
            }

            internal static IBannerClient GetBannerClient()
            {
            #if UNITY_EDITOR
            // Testing UNITY_EDITOR first because the editor also responds to the currently
            // selected platform.
            return new EmptyClient();
            #elif UNITY_ANDROID
            return new EmptyClient();
            #elif UNITY_IPHONE
            return new If3games.Core.Internal.Ads.iOS.IOSBannerClient();
            #else
            return new EmptyClient();
            #endif
            }

            internal static IInterstitialClient GetInterstitialClient()
            {
            #if UNITY_EDITOR
            // Testing UNITY_EDITOR first because the editor also responds to the currently
            // selected platform.
            return new EmptyClient();
            #elif UNITY_ANDROID
            return new EmptyClient();
            #elif UNITY_IPHONE
            return new If3games.Core.Internal.Ads.iOS.IOSInterstitialClient();
            #else
            return new EmptyClient();
            #endif
            }

            internal static IVideoClient GetVideoClient()
            {
            #if UNITY_EDITOR
            // Testing UNITY_EDITOR first because the editor also responds to the currently
            // selected platform.
            return new EmptyClient();
            #elif UNITY_ANDROID
            return new EmptyClient();
            #elif UNITY_IPHONE
            return new If3games.Core.Internal.Ads.iOS.IOSVideoClient();
            #else
            return new EmptyClient();
            #endif
            }
    }
}
