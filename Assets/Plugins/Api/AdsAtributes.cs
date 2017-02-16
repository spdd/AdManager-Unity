using System.Collections;
using System.Collections.Generic;

namespace If3games.Core.Internal.Ads
{
	public static class AdsAtributes
	{
		public static Dictionary<string, string> chartboostAttrs = new Dictionary<string, string>
			{
				{ "name", "com.chartboost.sdk.CBImpressionActivity" },
				{ "excludeFromRecents", "true" },
				{ "theme", "@android:style/Theme.Translucent.NoTitleBar" }
			};

		#region AdMob
		public static Dictionary<string, string> admobAttrs = new Dictionary<string, string>
			{
				{ "name", "com.google.android.gms.ads.AdActivity" },
				{ "configChanges", "keyboard|keyboardHidden|orientation|screenLayout|uiMode|screenSize|smallestScreenSize"},
				{ "theme", "@android:style/Theme.Translucent"}
			};
		public static Dictionary<string, string> admobMetaDataAttrs = new Dictionary<string, string>
			{
				{ "name", "com.google.android.gms.version" },
				{ "value", "@integer/google_play_services_version" }
			};

		#endregion

		public static Dictionary<string, string> unityAdsAttrs = new Dictionary<string, string>
			{
				{ "name", "com.unity3d.ads.android.view.UnityAdsFullscreenActivity" },
				{ "configChanges", "fontScale|keyboard|keyboardHidden|locale|mnc|mcc|navigation|orientation|screenLayout|screenSize|smallestScreenSize|uiMode|touchscreen" },
				{ "theme", "@android:style/Theme.NoTitleBar.Fullscreen"},
				{ "hardwareAccelerated", "true" },
				{ "tools_ignore", "UnusedAttribute"}
			};

		#region AdColony
		public static Dictionary<string, string> adColonyAttrs1 = new Dictionary<string, string>
			{
				{ "name", "com.jirbo.adcolony.AdColonyOverlay" },
				{ "configChanges", "keyboardHidden|orientation|screenSize" },
				{ "theme", "@android:style/Theme.Translucent.NoTitleBar.Fullscreen" }
			};
		public static Dictionary<string, string> adColonyAttrs2 = new Dictionary<string, string>
			{
				{ "name", "com.jirbo.adcolony.AdColonyFullscreen" },
				{ "configChanges", "keyboardHidden|orientation|screenSize" },
				{ "theme", "@android:style/Theme.Translucent.NoTitleBar.Fullscreen" }
			};
		public static Dictionary<string, string> adColonyAttrs3 = new Dictionary<string, string>
			{
				{ "name", "com.jirbo.adcolony.AdColonyBrowser" },
				{ "configChanges", "keyboardHidden|orientation|screenSize" },
				{ "theme", "@android:style/Theme.Translucent.NoTitleBar.Fullscreen" }
			};
		#endregion

		public static Dictionary<string, string> appLovinAttrs = new Dictionary<string, string>
			{
				{ "name", "com.unity3d.ads.android.view.UnityAdsFullscreenActivity" },
				{ "configChanges", "fontScale|keyboard|keyboardHidden|locale|mnc|mcc|navigation|orientation|screenLayout|screenSize|smallestScreenSize|uiMode|touchscreen" },
				{ "theme", "@android:style/Theme.NoTitleBar.Fullscreen"}
			};
	}
}
