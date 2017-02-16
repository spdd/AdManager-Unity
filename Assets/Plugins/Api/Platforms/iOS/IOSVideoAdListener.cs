using System;
using If3games.Core.Internal.Ads.Common;

namespace If3games.Core.Internal.Ads.iOS
{
    public class IOSVideoAdListener : IAdListener
    {
        private IVideoCallbacks listener;

        public IOSVideoAdListener(IVideoCallbacks callbacks)
        {
            listener = callbacks;
        }

        #region IAdListener implementation

        // The following methods are invoked from an IAppodealAdsVideoClient. Forward
        // these calls to the developer.
        void IAdListener.FireAdLoaded()
        {
            listener.onVideoLoaded ();
        }

        void IAdListener.FireAdFailedToLoad(string message)
        {
            listener.onVideoFailedToLoad ();
        }

        void IAdListener.FireAdOpened()
        {
            listener.onVideoOpened ();
        }

        void IAdListener.FireAdClosing()
        {
            listener.onVideoClosed();
        }

        void IAdListener.FireAdClosed()
        {
            listener.onVideoClosed();
        }

        void IAdListener.FireAdLeftApplication()
        {
            listener.onVideoClicked ();
        }

        void IAdListener.FireRewardUser(int amount)
        {
            listener.onVideoFinished();
        }

        #endregion
    }
}
