using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using If3games.Core.Internal.Ads;

namespace If3games.Core.Editor
{
    public class AdManagerSettings : EditorWindow 
    {
        public static AdManagerSettings instance;

        public string[] toolbarStrings = new string[] { "Ads"};
        public string[] adsInterstitialStrings = new string[] {"AdMob", "Chartboost"};
        public string[] adsVideoStrings = new string[] {"UnityAds", "Chartboost", "AdColony"};

		public string[] platformsStrings = new string[] { "Android", "iOS" };// "Amazon"};

        private Vector2 scrollViewVector;
        private int selected;
        private AdsSettings mySettings;

        public static void ShowAdSettings () {
            instance = (AdManagerSettings)EditorWindow.GetWindow (typeof(AdManagerSettings));
            instance.titleContent = new GUIContent("AdManager");
            instance.Show ();
        }

        void OnGUI ()
        {
            scrollViewVector = GUI.BeginScrollView (new Rect (25, 45, position.width - 30, position.height), scrollViewVector, new Rect (0, 0, 400, 1600));

			InitStyles();
			GUIAds();

            GUI.EndScrollView (); 
        }

        #region ads_settings

		private string[] setupInterstitialAdsLabels(AdsSettings mySettings)
		{
			List<string> parts = new List<string>(new string[] { "AdMob", "Chartboost" });
			if (!mySettings.ADMOB_ENABLED)
				parts.Remove("AdMob");
			if (!mySettings.CHARTBOOST_ENABLED)
				parts.Remove("Chartboost");
			return parts.ToArray();
		}

		private string[] setupVideoAdsLabels(AdsSettings mySettings)
		{
			List<string> parts = new List<string>(new string[] { "UnityAds", "Chartboost", "AdColony" });
			if (!mySettings.UNITYADS_ENABLED)
				parts.Remove("UnityAds");
			if (!mySettings.CHARTBOOST_ENABLED)
				parts.Remove("Chartboost");
			if (!mySettings.ADCOLONY_ENABLED)
				parts.Remove("AdColony");
			return parts.ToArray();
		}

		private int index_inter1 = 0;
		private int index_inter2 = 1;
        private int index_video1 = 0;
        private int index_video2 = 1;
        private int index_video3 = 2;
            
        private void initVideoValues() {
            mySettings.video_unity_ads_order = -1;
            mySettings.video_cb_order = -1;
            mySettings.video_ac_order = -1;
        }

        private void setOrderIndex(int num, int indx, AdsSettings mySettings) {
			if (indx > adsVideoStrings.Length)
				indx = adsVideoStrings.Length - 1;
            if (adsVideoStrings [indx].Equals ("UnityAds")) {
                mySettings.video_unity_ads_order = num;
            } else if (adsVideoStrings [indx].Equals ("Chartboost")) {
                mySettings.video_cb_order = num;
            } else if (adsVideoStrings [indx].Equals ("AdColony")) {
                mySettings.video_ac_order = num;
            }
        }

		private void setOrderIndexInter(int num, int indx, AdsSettings mySettings)
		{
			if (indx > adsInterstitialStrings.Length)
				indx = adsInterstitialStrings.Length - 1;
			if (adsInterstitialStrings[indx].Equals("AdMob"))
			{
				mySettings.inter_admob_order = num;
			}
			else if (adsInterstitialStrings[indx].Equals("Chartboost"))
			{
				mySettings.inter_cb_order = num;
			}
		}

		public static Color hexToColor(string hex)
		{
			hex = hex.Replace("0x", "");//in case the string is formatted 0xFFFFFF
			hex = hex.Replace("#", "");//in case the string is formatted #FFFFFF
			byte a = 255;//assume fully visible unless specified in hex
			byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
			byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
			byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
			//Only use alpha if the string has enough characters
			if (hex.Length == 8)
			{
				a = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
			}
			return new Color32(r, g, b, a);
		}

		private GUIStyle _titleStyle;
		private GUIStyle _titleAdvStyle;
		private GUIStyle _titlePluginStyle;
		private GUIStyle _titleSectionStyle;
		private GUIStyle _labelStyle;

		private void InitStyles()
		{
			Texture2D texture1 = new Texture2D(1, 1);
			texture1.SetPixel(0, 0, hexToColor("#0091EA"));
			texture1.Apply();

			Texture2D texture2 = new Texture2D(1, 1);
			texture2.SetPixel(0, 0, hexToColor("#009688"));
			texture2.Apply();

			Texture2D texture3 = new Texture2D(1, 1);
			texture3.SetPixel(0, 0, hexToColor("#CDDC39"));
			texture3.Apply();

			Texture2D texture4 = new Texture2D(1, 1);
			texture4.SetPixel(0, 0, hexToColor("#EF6C00"));
			texture4.Apply();

			_titlePluginStyle = new GUIStyle();
			_titlePluginStyle.alignment = TextAnchor.MiddleCenter;
			_titlePluginStyle.fontSize = 22;
			_titlePluginStyle.normal.background = texture3;
			_titlePluginStyle.normal.textColor = hexToColor("#424242");

			_titleSectionStyle = new GUIStyle();
			_titleSectionStyle.alignment = TextAnchor.MiddleCenter;
			_titleSectionStyle.fontSize = 16;
			_titleSectionStyle.normal.background = texture2;
			_titleSectionStyle.normal.textColor = Color.white;

			_titleStyle = new GUIStyle();
			_titleStyle.alignment = TextAnchor.MiddleCenter;
			_titleStyle.fontSize = 16;
			_titleStyle.normal.background = texture1;
			_titleStyle.normal.textColor = Color.white;

			_titleAdvStyle = new GUIStyle();
			_titleAdvStyle.alignment = TextAnchor.MiddleCenter;
			_titleAdvStyle.fontSize = 16;
			_titleAdvStyle.normal.background = texture4;
			_titleAdvStyle.normal.textColor = Color.white;

			_labelStyle = new GUIStyle();
			_labelStyle.fontSize = 11;
			_labelStyle.normal.textColor = Color.yellow;
		}

		private int FIELD_WIDTH = 400;
        private int platfornmIndex = 0;
        void GUIAds ()
        {
			string dirPath = "Assets/Resources/AdManager";
			if (!Directory.Exists(dirPath))
				Directory.CreateDirectory(dirPath);

            mySettings = AssetDatabase.LoadAssetAtPath ("Assets/Resources/AdManager/myAdsSettings.asset", typeof(AdsSettings)) as AdsSettings; 
            if (mySettings == null) {
                mySettings = ScriptableObject.CreateInstance<AdsSettings> ();
                AssetDatabase.CreateAsset (mySettings, "Assets/Resources/AdManager/myAdsSettings.asset");
                AssetDatabase.SaveAssets ();
                AssetDatabase.Refresh ();
            }
            initVideoValues ();

			GUILayout.Space(10);

			GUILayout.Label ("AdManager", _titlePluginStyle);

			GUILayout.Space(15);
			GUILayout.Label("Common", _titleSectionStyle);
			GUILayout.Space(10);

            mySettings.ADS_ENABLED = EditorGUILayout.Toggle ("Enable ads", mySettings.ADS_ENABLED, new GUILayoutOption[] {
                GUILayout.Width (50),
                GUILayout.MaxWidth (200)
            });
			mySettings.ADS_DEBUG = EditorGUILayout.Toggle("Debug ads", mySettings.ADS_DEBUG, new GUILayoutOption[] {
				GUILayout.Width (50),
				GUILayout.MaxWidth (200)
			});
			GUILayout.Space(20);

			mySettings.ADMOB_ENABLED = EditorGUILayout.Toggle("Enable AdMob", mySettings.ADMOB_ENABLED, new GUILayoutOption[] {
				GUILayout.Width (50),
				GUILayout.MaxWidth (200)
			});
			mySettings.CHARTBOOST_ENABLED = EditorGUILayout.Toggle("Enable Chartboost", mySettings.CHARTBOOST_ENABLED, new GUILayoutOption[] {
				GUILayout.Width (50),
				GUILayout.MaxWidth (200)
			});
			mySettings.UNITYADS_ENABLED = EditorGUILayout.Toggle("Enable Unity Ads", mySettings.UNITYADS_ENABLED, new GUILayoutOption[] {
				GUILayout.Width (50),
				GUILayout.MaxWidth (200)
			});

			mySettings.ADCOLONY_ENABLED = EditorGUILayout.Toggle("Enable AdColony", mySettings.ADCOLONY_ENABLED, new GUILayoutOption[] {
				GUILayout.Width (50),
				GUILayout.MaxWidth (200)
			});
			/*
			mySettings.APPLOVIN_ENABLED = EditorGUILayout.Toggle("Enable AppLovin", mySettings.APPLOVIN_ENABLED, new GUILayoutOption[] {
				GUILayout.Width (50),
				GUILayout.MaxWidth (200)
			});
			*/

            GUILayout.Space (10);
            GUILayout.BeginHorizontal ();
            GUILayout.Label ("Select platform: ", new GUILayoutOption[] { GUILayout.Width (150) });
            platfornmIndex = EditorGUILayout.Popup(platfornmIndex, platformsStrings, new GUILayoutOption[] { GUILayout.Width (150) });
            GUILayout.EndHorizontal ();

            if (platfornmIndex == 0 || platfornmIndex == 1) { // android or ios
                // Interstitial Ads showing order
                GUILayout.Space (15);
				GUILayout.Label (adsInterstitialStrings.Length >= 1 ? "Interstitial ads order" : "Interstitial is disabled", _titleSectionStyle);
				GUILayout.Space(15);
				adsInterstitialStrings = setupInterstitialAdsLabels(mySettings);

				if (adsInterstitialStrings.Length > 0)
				{
					GUILayout.BeginHorizontal();
					GUILayout.Label("order 1: ", EditorStyles.label, new GUILayoutOption[] { GUILayout.Width(150) });
					index_inter1 = EditorGUILayout.Popup(index_inter1, adsInterstitialStrings, new GUILayoutOption[] { GUILayout.Width(150) });
					setOrderIndexInter(0, index_inter1, mySettings);
					GUILayout.EndHorizontal();

					if (adsInterstitialStrings.Length > 1)
					{
						GUILayout.BeginHorizontal();
						GUILayout.Label("order 2: ", EditorStyles.label, new GUILayoutOption[] { GUILayout.Width(150) });
						index_inter2 = EditorGUILayout.Popup(index_inter2, adsInterstitialStrings, new GUILayoutOption[] { GUILayout.Width(150) });
						setOrderIndexInter(1, index_inter2, mySettings);
						GUILayout.EndHorizontal();
					}
				}


				adsVideoStrings = setupVideoAdsLabels(mySettings);

                // Video Ads showing order
                GUILayout.Space (10);
                GUILayout.Label (adsVideoStrings.Length >= 1 ? "Video ads order" : "Video ads disabled", _titleSectionStyle);
				GUILayout.Space(15);
				if (adsVideoStrings.Length > 0)
				{
					GUILayout.BeginHorizontal();
					GUILayout.Label("order 1: ", EditorStyles.label, new GUILayoutOption[] { GUILayout.Width(150) });
					index_video1 = EditorGUILayout.Popup(index_video1, adsVideoStrings, new GUILayoutOption[] { GUILayout.Width(150) });
					setOrderIndex(0, index_video1, mySettings);
					GUILayout.EndHorizontal();

					if (adsVideoStrings.Length > 1)
					{
						GUILayout.BeginHorizontal();
						GUILayout.Label("order 2: ", EditorStyles.label, new GUILayoutOption[] { GUILayout.Width(150) });
						index_video2 = EditorGUILayout.Popup(index_video2, adsVideoStrings, new GUILayoutOption[] { GUILayout.Width(150) });
						setOrderIndex(1, index_video2, mySettings);
						GUILayout.EndHorizontal();
					}

					if (adsVideoStrings.Length > 2)
					{
						GUILayout.BeginHorizontal();
						GUILayout.Label("order 3: ", EditorStyles.label, new GUILayoutOption[] { GUILayout.Width(150) });
						index_video3 = EditorGUILayout.Popup(index_video3, adsVideoStrings, new GUILayoutOption[] { GUILayout.Width(150) });
						setOrderIndex(2, index_video3, mySettings);
						GUILayout.EndHorizontal();
					}
				}

            }

			#region ads ids

			if (mySettings.UNITYADS_ENABLED)
			{
				// Unity Ads
				GUILayout.Space(10);
				GUILayout.Label("Unity Ads", _titleStyle);
				EditorGUILayout.BeginVertical("box");
				if (platfornmIndex == 0)
				{
					EditorGUILayout.LabelField("App ID Android:");
					mySettings.UNITYADS_APPID_ANDROID = EditorGUILayout.TextField(mySettings.UNITYADS_APPID_ANDROID, new GUILayoutOption[] {
					GUILayout.Width (FIELD_WIDTH),
					GUILayout.MaxWidth (FIELD_WIDTH)
				});
				}
				if (platfornmIndex == 1)
				{
					EditorGUILayout.LabelField("App ID iOS:");
					mySettings.UNITYADS_APPID_IOS = EditorGUILayout.TextField(mySettings.UNITYADS_APPID_IOS, new GUILayoutOption[] {
					GUILayout.Width (FIELD_WIDTH),
					GUILayout.MaxWidth (FIELD_WIDTH)
				});
				}
				GUILayout.EndVertical();
			}

			if (mySettings.ADMOB_ENABLED)
			{
				// Admob
				GUILayout.Space(10);
				GUILayout.Label("Admob", _titleStyle);
				EditorGUILayout.BeginVertical("box");
				if (platfornmIndex == 0)
				{
					
					EditorGUILayout.LabelField("Interstitial ID Android:");
					mySettings.ADMOB_ANDROID_INTER_APPID = EditorGUILayout.TextField(mySettings.ADMOB_ANDROID_INTER_APPID, new GUILayoutOption[] {
						GUILayout.Width (FIELD_WIDTH),
						GUILayout.MaxWidth (FIELD_WIDTH)
				});
				}
				if (platfornmIndex == 1)
				{
					EditorGUILayout.LabelField("Interstitial ID iOS:");
					mySettings.ADMOB_IOS_INTER_APPID = EditorGUILayout.TextField(mySettings.ADMOB_IOS_INTER_APPID, new GUILayoutOption[] {
						GUILayout.Width (FIELD_WIDTH),
						GUILayout.MaxWidth (FIELD_WIDTH)
				});
				}
				if (platfornmIndex == 2)
				{
					EditorGUILayout.LabelField("Interstitial ID WP");
					mySettings.ADMOB_WP_INTER_APPID = EditorGUILayout.TextField(mySettings.ADMOB_WP_INTER_APPID, new GUILayoutOption[] {
						GUILayout.Width (FIELD_WIDTH),
						GUILayout.MaxWidth (FIELD_WIDTH)
				});
				}
				GUILayout.EndVertical();
			}

			if (mySettings.CHARTBOOST_ENABLED)
			{
				// Chartboost
				GUILayout.Space(10);
				GUILayout.Label("Chartboost", _titleStyle);

				EditorGUILayout.BeginVertical("box");
				if (platfornmIndex == 0)
				{

					EditorGUILayout.LabelField("App ID Google Play:");
					mySettings.CHARBOOST_PLAY_APPID = EditorGUILayout.TextField(mySettings.CHARBOOST_PLAY_APPID, new GUILayoutOption[] {
					GUILayout.Width (FIELD_WIDTH),
					GUILayout.MaxWidth (FIELD_WIDTH)
				});
					GUILayout.Space(10);
					EditorGUILayout.LabelField("App Sigh Google Play:");
					mySettings.CHARBOOST_PLAY_APPSIGH = EditorGUILayout.TextField(mySettings.CHARBOOST_PLAY_APPSIGH, new GUILayoutOption[] {
					GUILayout.Width (FIELD_WIDTH),
					GUILayout.MaxWidth (FIELD_WIDTH)
				});
					GUILayout.Space(10);
					GUILayout.Space(10);
					EditorGUILayout.LabelField("App ID Amazon Store:");
					mySettings.CHARBOOST_AMAZON_APPID = EditorGUILayout.TextField(mySettings.CHARBOOST_AMAZON_APPID, new GUILayoutOption[] {
					GUILayout.Width (FIELD_WIDTH),
					GUILayout.MaxWidth (FIELD_WIDTH)
				});
					GUILayout.Space(10);
					EditorGUILayout.LabelField("App Sigh Amazon Store:");
					mySettings.CHARBOOST_AMAZON_APPSIGH = EditorGUILayout.TextField(mySettings.CHARBOOST_AMAZON_APPSIGH, new GUILayoutOption[] {
					GUILayout.Width (FIELD_WIDTH),
					GUILayout.MaxWidth (FIELD_WIDTH)
				});
				}

				if (platfornmIndex == 1)
				{
					GUILayout.Space(10);
					EditorGUILayout.LabelField("App ID iOS:");
					mySettings.CHARBOOST_IOS_APPID = EditorGUILayout.TextField(mySettings.CHARBOOST_IOS_APPID, new GUILayoutOption[] {
					GUILayout.Width (FIELD_WIDTH),
					GUILayout.MaxWidth (FIELD_WIDTH)
				});
					GUILayout.Space(10);
					EditorGUILayout.LabelField("App Sigh iOS:");
					mySettings.CHARBOOST_IOS_APPSIGH = EditorGUILayout.TextField(mySettings.CHARBOOST_IOS_APPSIGH, new GUILayoutOption[] {
					GUILayout.Width (FIELD_WIDTH),
					GUILayout.MaxWidth (FIELD_WIDTH)
				});
				}
				GUILayout.EndVertical();
			}

			if (mySettings.ADCOLONY_ENABLED)
			{
				GUILayout.Space(10);
				// Adcolony

				GUILayout.Space(10);
				GUILayout.Label("AdColony", _titleStyle);

				EditorGUILayout.BeginVertical("box");
				if (platfornmIndex == 0)
				{
					EditorGUILayout.LabelField("App ID Android:");
					mySettings.ADCOLONY_ANDROID_APP_ID = EditorGUILayout.TextField(mySettings.ADCOLONY_ANDROID_APP_ID, new GUILayoutOption[] {
					GUILayout.Width (FIELD_WIDTH),
					GUILayout.MaxWidth (FIELD_WIDTH)
				});
					GUILayout.Space(10);
					EditorGUILayout.LabelField("Zone ID Android:");
					mySettings.ADCOLONY_ANDROID_ZONE_ID = EditorGUILayout.TextField(mySettings.ADCOLONY_ANDROID_ZONE_ID, new GUILayoutOption[] {
					GUILayout.Width (FIELD_WIDTH),
					GUILayout.MaxWidth (FIELD_WIDTH)
				});
				}

				if (platfornmIndex == 1)
				{
					EditorGUILayout.LabelField("App ID iOS:");
					mySettings.ADCOLONY_IOS_APP_ID = EditorGUILayout.TextField(mySettings.ADCOLONY_IOS_APP_ID, new GUILayoutOption[] {
					GUILayout.Width (FIELD_WIDTH),
					GUILayout.MaxWidth (FIELD_WIDTH)
				});
					EditorGUILayout.LabelField("Zone ID iOS:");
					mySettings.ADCOLONY_IOS_ZONE_ID = EditorGUILayout.TextField(mySettings.ADCOLONY_IOS_ZONE_ID, new GUILayoutOption[] {
					GUILayout.Width (FIELD_WIDTH),
					GUILayout.MaxWidth (FIELD_WIDTH)
				});
				}
				GUILayout.EndVertical();
			}
			#endregion

			#region ad config sources
			GUILayout.Space(10);
			GUILayout.Label("Advanced settings", _titleAdvStyle);

			EditorGUILayout.BeginVertical("box");

            GUILayout.Space(10);

			EditorGUILayout.LabelField("Show interstitial ads every:");
			GUILayout.BeginHorizontal();
			mySettings.SHOW_ADS_EVERY_LEVEL = EditorGUILayout.IntField(mySettings.SHOW_ADS_EVERY_LEVEL, new GUILayoutOption[] {
				GUILayout.Width (FIELD_WIDTH),
				GUILayout.MaxWidth (FIELD_WIDTH)
			});
			GUILayout.Label(" level");
			GUILayout.EndHorizontal();

			// Ads config server url

			GUILayout.Space(10);
			//EditorGUILayout.LabelField("Ads config from URL", _labelStyle);
			mySettings.ADS_CONFIG_FROM_URL = EditorGUILayout.Toggle("Ads config from URL",mySettings.ADS_CONFIG_FROM_URL, new GUILayoutOption[] {
				GUILayout.Width (50),
				GUILayout.MaxWidth (200)
			});

			EditorGUILayout.LabelField("Ads config server URL:");
			mySettings.SERVER_URL = EditorGUILayout.TextField(mySettings.SERVER_URL, new GUILayoutOption[] {
				GUILayout.Width (FIELD_WIDTH),
				GUILayout.MaxWidth (FIELD_WIDTH)
			});

            if (mySettings.SHOW_ADS_EVERY_LEVEL < 0)
                mySettings.SHOW_ADS_EVERY_LEVEL = 1;
            GUILayout.Space (10);

			GUILayout.Space(10);
			if (platfornmIndex == 1) 
			{
				mySettings.FIREBASE_CONFIG_ENABLED = EditorGUILayout.Toggle("Google Firebase Config", mySettings.FIREBASE_CONFIG_ENABLED, new GUILayoutOption[] {
					//GUILayout.Width (50),
					//GUILayout.MaxWidth (200)
				});

				if (mySettings.FIREBASE_CONFIG_ENABLED)
				{
					GUILayout.BeginHorizontal();
					GUILayout.Label("Google Services file", EditorStyles.label, new GUILayoutOption[] { GUILayout.Width(50), GUILayout.MaxWidth(200) });
					if (GUILayout.Button("Load", new GUILayoutOption[] { GUILayout.Width(100) }))
					{
						if (platfornmIndex == 1)
							WriteData(ReadDataFromFile("plist"), "GoogleServices-Info.plist");
						else if (platfornmIndex == 0)
							WriteData(ReadDataFromFile("json"), "google-services.json");
					}
					GUILayout.EndHorizontal();
				}
			}
			GUILayout.EndVertical();

			#endregion

			AssetDatabase.Refresh();
			EditorUtility.SetDirty(mySettings);
            AssetDatabase.SaveAssets ();
        }

        #endregion

        #region write read files

		private void WriteData(string data, string fileName)
		{
			string dirPath = "Assets/StreamingAssets";
			if (!Directory.Exists(dirPath))
			{
				//if it doesn't, create it
				Directory.CreateDirectory(dirPath);
			}
			var path = string.Format(@"{0}/{1}", dirPath, fileName);

			using (FileStream fs = new FileStream(path, FileMode.Create))
			{
				using (StreamWriter writer = new StreamWriter(fs))
				{
					writer.Write(data);
				}
			}
			AssetDatabase.Refresh();
		}

		private string ReadDataFromFile(string ext) 
		{
			var path = EditorUtility.OpenFilePanel("Load Data", "", ext);
			var reader = new WWW("file:///" + path);
			while (!reader.isDone) {
			}
			return reader.text;
		}

        #endregion
    }
}
