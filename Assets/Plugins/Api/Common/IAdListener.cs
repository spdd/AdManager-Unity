using System;

namespace If3games.Core.Internal.Ads.Common
{
    // Interface for the methods to be invoked by the native plugin.
    public interface IAdListener
    {
        void FireRewardUser(int amount);
        void FireAdLoaded();
        void FireAdFailedToLoad(string message);
        void FireAdOpened();
        void FireAdClosing();
        void FireAdClosed();
        void FireAdLeftApplication();
    }
}
