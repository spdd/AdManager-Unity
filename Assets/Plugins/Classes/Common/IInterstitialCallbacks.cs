using System;

namespace If3games.Core.Internal.Ads.Common
{
    // Interface for the methods to be invoked by the native plugin.
    public interface IInterstitialCallbacks
    {
        void onInterstitialLoaded();
        void onInterstitialFailedToLoad();
        void onInterstitialOpened();
        void onInterstitialClosed();
        void onInterstitialClicked();
    }
}
