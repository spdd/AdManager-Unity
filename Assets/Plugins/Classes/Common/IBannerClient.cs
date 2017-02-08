using System;

namespace If3games.Core.Internal.Ads.Common
{
    public interface IBannerClient 
    {
        // Create a banner view and add it into the view hierarchy.
        void CreateBanner(AdPosition position);

        // set IAdListener
        void SetAdListener(IAdListener listener);

        // Request a new ad for the banner view.
        void LoadAd();

        // Determines whether the banner has loaded.
        bool IsLoaded();

        // Show the banner view on the screen.
        void Show();

        // Hide the banner view from the screen.
        void Hide();

        // Destroys a banner view and to free up memory.
        void DestroyBannerView();

        void Cache ();

        bool IsPrecache ();

        void SetAutoCache (bool autoCache);

        void DisableNetwork (string adName);

        void ShowAdWithAdName (string adName);
    }
}
