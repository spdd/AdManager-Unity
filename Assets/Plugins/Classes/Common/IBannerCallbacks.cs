using System;

namespace If3games.Core.Internal.Ads.Common
{
    // Interface for the methods to be invoked by the native plugin.
    public interface IBannerCallbacks
    {
        void onBannerLoaded();
        void onBannerFailedToLoad();
        void onBannerOpened();
        void onBannerClicked();
    }
}