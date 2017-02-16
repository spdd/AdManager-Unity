using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using If3games.Core.Internal.Ads.Utils.SimpleJSON;
using System.Linq;

namespace If3games.Core.Internal.Ads
{
    public static class ConfigBuilder
    {
        private static AdsSettings mySettings = Resources.Load("AdManager/myAdsSettings") as AdsSettings;

        private enum ANDROID_MARKET { PLAY, AMAZON }
        private static ANDROID_MARKET mMARKET   // This set market type
        {
            get
            {
                if (mySettings.ANDROID_MARKET_TYPE == 1)
                    return ANDROID_MARKET.AMAZON;
                return ANDROID_MARKET.PLAY;
            }
        }

        private static Dictionary<string, int> adsInterDict = new Dictionary<string, int>
        {
            { "admob", 0 },
            { "chartboost", 1 }
        };

        private static Dictionary<string, int> adsVideoDict = new Dictionary<string, int>
        {
            { "unity_ads", 0 },
            { "chartboost", 1 },
            { "adcolony", 2 }
        };

        private static string ADMOB_INTER_APPID
        {
            get
            {
                #if UNITY_ANDROID
                return mySettings.ADMOB_ANDROID_INTER_APPID;
                #elif UNITY_IOS
                return mySettings.ADMOB_IOS_INTER_APPID;
                #else
                return "";
                #endif
            }
        }

        private static string ADMOB_BANNER_APPID
        {
            get
            {
                #if UNITY_ANDROID
                return mySettings.ADMOB_ANDROID_BANNER_APPID;
                #elif UNITY_IOS
                return mySettings.ADMOB_IOS_BANNER_APPID;
                #else
                return "";
                #endif
            }
        }

        private static string CHARBOOST_APPID
        {
            get
            {
                #if UNITY_ANDROID
                return mMARKET == ANDROID_MARKET.PLAY ? mySettings.CHARBOOST_PLAY_APPID : mySettings.CHARBOOST_AMAZON_APPID;
                #elif UNITY_IOS
                return mySettings.CHARBOOST_IOS_APPID;
                #else
                return null;
                #endif
            }
        }

        private static string CHARBOOST_APPSIGH
        {
            get
            {
                #if UNITY_ANDROID
                return mMARKET == ANDROID_MARKET.PLAY ? mySettings.CHARBOOST_PLAY_APPSIGH : mySettings.CHARBOOST_AMAZON_APPSIGH;
                #elif UNITY_IOS
                return mySettings.CHARBOOST_IOS_APPSIGH;
                #else
                return null;
                #endif
            }
        }

        // AdColony android
        //private static string ADCOLONY_ANDROID_MARKET_TYPE = "version:1.0,store:google";
        // Adcolony iOS
        //private static string ADCOLONY_IOS_MARKET_TYPE = "version:1.0,store:apple";

        private static string ADCOLONY_APP_ID
        {
            get
            {
                #if UNITY_ANDROID
                return mySettings.ADCOLONY_ANDROID_APP_ID;
                #elif UNITY_IOS
                return mySettings.ADCOLONY_IOS_APP_ID;
                #else
                return "";
                #endif
            }
        }

        private static string ADCOLONY_ZONE_ID
        {
            get
            {
                #if UNITY_ANDROID
                return mySettings.ADCOLONY_ANDROID_ZONE_ID;
                #elif UNITY_IOS
                return mySettings.ADCOLONY_IOS_ZONE_ID;
                #else
                return "";
                #endif
            }
        }

        // UnityAds
        private static string UNITYADS_APPID
        {
            get
            {
                #if UNITY_ANDROID
                return mySettings.UNITYADS_APPID_ANDROID;
                #elif UNITY_IOS
                return mySettings.UNITYADS_APPID_IOS;
                #else
                return "";
                #endif
            }
        }

		private static void setupOrder()
		{
			adsInterDict["admob"] = mySettings.inter_admob_order;
			Debug.Log("admob order: " + mySettings.inter_admob_order.ToString());
			adsInterDict["chartboost"] = mySettings.inter_cb_order;
			Debug.Log("chartboost inter order: " + mySettings.inter_cb_order.ToString());
			Debug.Log("chartboost appid = " + mySettings.CHARBOOST_PLAY_APPID);

			adsVideoDict["unity_ads"] = mySettings.video_unity_ads_order;
			adsVideoDict["chartboost"] = mySettings.video_cb_order;
			adsVideoDict["adcolony"] = mySettings.video_ac_order;
		}

        private static string adsIdsToJson()
        {
            if (mySettings == null)
                Debug.Log("mySettings is null");
			
			setupOrder();

            JSONNode node = new JSONClass();
            node["status"] = "ok";
			node["show_freq"] = mySettings.SHOW_ADS_EVERY_LEVEL.ToString();
            node["config_from_url"] = mySettings.ADS_CONFIG_FROM_URL ? "1" : "0";
            node["config_url"] = mySettings.SERVER_URL;

            List<KeyValuePair<string, int>> sortedInterstitial = adsInterDict.ToList();

            sortedInterstitial.Sort((firstPair, nextPair) =>
                {
                    return firstPair.Value.CompareTo(nextPair.Value);
                }
            );
            JSONNode interArray = new JSONArray();
            {
                foreach (KeyValuePair<string, int> kv in sortedInterstitial)
                {
                    JSONNode adNode = new JSONClass();
                    if (kv.Key.Equals("admob"))
                    {
                        adNode["adname"] = "admob";
                        adNode["admob_inter_id"] = ADMOB_INTER_APPID;
                        adNode["admob_banner_id"] = ADMOB_BANNER_APPID;
                    }
                    else if (kv.Key.Equals("chartboost"))
                    {
                        adNode["adname"] = "chartboost";
                        adNode["cb_appId"] = CHARBOOST_APPID;
                        adNode["cb_appSigh"] = CHARBOOST_APPSIGH;
                    }
                    interArray.Add(adNode);

                }
            }
            node["ads_interstitial"] = interArray;

            List<KeyValuePair<string, int>> sortedVideo = adsVideoDict.ToList();

            sortedVideo.Sort((firstPair, nextPair) =>
                {
                    return firstPair.Value.CompareTo(nextPair.Value);
                }
            );

            JSONNode videoArray = new JSONArray();
            {
                foreach (KeyValuePair<string, int> kv in sortedVideo)
                {
                    if (kv.Value < 0)
                        continue;
                    JSONNode adNode = new JSONClass();
                    if (kv.Key.Equals("unity_ads"))
                    {
                        adNode["adname"] = "unity_ads";
                        adNode["unity_ads_id"] = UNITYADS_APPID;
                    }
                    else if (kv.Key.Equals("chartboost"))
                    {
                        adNode["adname"] = "chartboost";
                        adNode["cb_appId"] = CHARBOOST_APPID;
                        adNode["cb_appSigh"] = CHARBOOST_APPSIGH;
                    }
                    else if (kv.Key.Equals("adcolony"))
                    {
                        adNode["adname"] = "adcolony";
                        adNode["ac_appId"] = ADCOLONY_APP_ID;
                        adNode["ac_zoneId"] = ADCOLONY_ZONE_ID;
                    }
                    videoArray.Add(adNode);
                }
            }

            node["ads_video"] = videoArray;


            JSONNode precacheInterArray = new JSONArray();
            {
                JSONNode bannerNode = new JSONClass();
                bannerNode["adname"] = "image";
                bannerNode["banner_url"] = "";
                bannerNode["store_url"] = "";
                bannerNode["app_descr"] = "Our new game!";
                precacheInterArray.Add(bannerNode);
            }

            node["precache_interstitial"] = precacheInterArray;
            node["precache_video"] = precacheInterArray;

            return node.ToString();
        }

        public static string BuildConfig(AdsSettings settings)
        {
            mySettings = settings;
            return adsIdsToJson();
        }

        public static string BuildConfig()
        {
            return adsIdsToJson();
        }

		public static Boolean isDebug()
		{
			if (mySettings != null)
				return mySettings.ADS_DEBUG;
			else
				return false;
		}
    }

}