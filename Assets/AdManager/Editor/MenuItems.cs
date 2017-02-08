using UnityEngine;
using UnityEditor;
using If3games.Core.Editor;

namespace If3games.Core.Editor
{
    public class MenuItems  
    {
        [MenuItem ("Tools/AdManager/Show Ads Settings")]
        private static void ShowGameSettings () {
            AdManagerSettings.ShowAdSettings ();
        }
	}
}
