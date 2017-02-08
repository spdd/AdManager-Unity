using System;

namespace If3games.Core.Internal.Ads.Common
{
    // Interface for the methods to be invoked by the native plugin.
    public interface IVideoCallbacks
    {
        void onVideoLoaded();
        void onVideoFailedToLoad();
        void onVideoOpened();
        void onVideoFinished();
        void onVideoClosed();
        void onVideoClicked();
    }
}
