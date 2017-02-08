using System;

namespace If3games.Core.Internal.Ads.Common
{
    public interface IAdsClient
    {
        void initialize(string adIds, Boolean autocache);

        void orientationChange ();
        void disableNetwork (string network);

        void setInterstitialCallbacks (IInterstitialCallbacks listener);
        void setVideoCallbacks (IVideoCallbacks listener);
        void setBannerCallbacks (IBannerCallbacks listener);

        void showBanner();
        void showInterstitial();
        void showVideo();

        void cacheBanner();
        void cacheInterstitial();
        void cacheVideo();

        Boolean isBannerLoaded ();
        Boolean isInterstitialLoaded ();
        Boolean isVideoLoaded ();

        void hideBanner ();
        void setAutoCache (Boolean autoCache);
        void setTesting(Boolean test);

        string getVersion();
    }
}