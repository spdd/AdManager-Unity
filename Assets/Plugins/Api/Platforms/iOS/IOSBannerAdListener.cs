using UnityEngine;
using System;
using If3games.Core.Internal.Ads.Common;

namespace If3games.Core.Internal.Ads.iOS
{
    public class IOSBannerAdListener : IAdListener 
    {
        private IBannerCallbacks listener;

        public IOSBannerAdListener(IBannerCallbacks callbacks)
        {
            listener = callbacks;
        }

        #region IAdListener implementation

        // The following methods are invoked from an IAdsClient. Forward these calls
        // to the developer.

        void IAdListener.FireRewardUser(int amount)
        {
        }

        void IAdListener.FireAdLoaded()
        {
            listener.onBannerLoaded ();
        }

        void IAdListener.FireAdFailedToLoad(string message)
        {
            listener.onBannerFailedToLoad();
        }

        void IAdListener.FireAdOpened()
        {
            listener.onBannerOpened ();
        }

        void IAdListener.FireAdClosing()
        {
        }

        void IAdListener.FireAdClosed()
        {
        }

        void IAdListener.FireAdLeftApplication()
        {
            listener.onBannerClicked ();
        }

        #endregion
    }
}

