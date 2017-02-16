using System;
using If3games.Core.Internal.Ads.Common;

namespace If3games.Core.Internal.Ads.iOS
{
    public class IOSInterstitialAdListener : IAdListener
    {
        private IInterstitialCallbacks listener;

        // Creates an InsterstitialAd.
        public IOSInterstitialAdListener(IInterstitialCallbacks callbacks)
        {
            listener = callbacks;
        }
            
        #region IAdListener implementation

        // The following methods are invoked from an IAppodealAdsInterstitialClient. Forward
        // these calls to the developer.
        void IAdListener.FireAdLoaded()
        {
            listener.onInterstitialLoaded ();
        }

        void IAdListener.FireAdFailedToLoad(string message)
        {
            listener.onInterstitialFailedToLoad();
        }

        void IAdListener.FireAdOpened()
        {
            listener.onInterstitialOpened();
        }

        void IAdListener.FireAdClosing()
        {
            listener.onInterstitialClosed ();
        }

        void IAdListener.FireAdClosed()
        {
            listener.onInterstitialClosed ();
        }

        void IAdListener.FireAdLeftApplication()
        {
            listener.onInterstitialClicked ();
        }

        void IAdListener.FireRewardUser(int amount)
        {

        }

        #endregion
    }
}
