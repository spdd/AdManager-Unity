using System;

namespace If3games.Core.Internal.Ads.Common
{
    public interface IInterstitialClient 
    {
        void CreateInterstitial();

        // set IAdListener
        void SetAdListener(IAdListener listener);

        // Loads a new interstitial request.
        void LoadAd();

        // Determines whether the interstitial has loaded.
        bool IsLoaded();

        // Shows the InterstitialAd.
        void Show();

        // Destroys an InterstitialAd to free up memory.
        void DestroyInterstitial();

        void Cache ();

        bool IsPrecache ();

        void SetAutoCache (bool autoCache);

        void DisableNetwork (string adName);

        void ShowAdWithAdName (string adName);
    }
}
