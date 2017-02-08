using System;

namespace If3games.Core.Internal.Ads.Common
{
    public interface IVideoClient 
    {
        void CreateVideo();

        // set IAdListener
        void SetAdListener(IAdListener listener);

        // Loads a new video request.
        void LoadAd();

        // Determines whether the video has loaded.
        bool IsLoaded();

        // Shows the Videod.
        void Show();

        // Destroys an Video to free up memory.
        void DestroyVideoAd();

        void Cache ();

        bool IsPrecache ();

        void SetAutoCache (bool autoCache);

        void DisableNetwork (string adName);

        void ShowAdWithAdName (string adName);
    }
}
