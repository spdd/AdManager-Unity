using UnityEngine;
using System;
using System.Collections.Generic;

namespace If3games.Core.Internal.Ads
{
    [Serializable]
    public class AdsSettings : ScriptableObject
    {
        public bool GAMEVIEW_HAS_BANNER = true;

        public bool FIREBASE_CONFIG_ENABLED = false;

        public bool ADS_ENABLED = true;
		public bool ADS_DEBUG = false;

        public bool ADS_CONFIG_FROM_URL = false;

        public bool ADMOB_ENABLED = true;
        public bool CHARTBOOST_ENABLED = true;
        public bool UNITYADS_ENABLED = true;
        public bool APPLOVIN_ENABLED = true;
        public bool ADCOLONY_ENABLED = true;

        public int ANDROID_MARKET_TYPE = 0; // 0 - Google Play, 1 - Amazon

        public int SHOW_ADS_EVERY_LEVEL = 6;

        public string SERVER_URL = "";

        public int inter_admob_order = 0;
        public int inter_cb_order = 1;

        public int video_unity_ads_order = 0;
        public int video_cb_order = 1;
        public int video_ac_order = 2;

        //public string[] intersList = new string[] {"AdMob", "Chartboost"};

        // AdMob
        public string ADMOB_ANDROID_INTER_APPID = "ca-app-pub-9692610173697432/7598306674";
        public string ADMOB_ANDROID_BANNER_APPID = "ca-app-pub-9692610173697432/1978100676";

        public string ADMOB_IOS_INTER_APPID = "ca-app-pub-9692610173697432/7598306674";
        public string ADMOB_IOS_BANNER_APPID = "ca-app-pub-9692610173697432/1978100676";

        public string ADMOB_WP_INTER_APPID = "ca-app-pub-9692610173697432/7598306674";
        public string ADMOB_WP_BANNER_APPID = "ca-app-pub-9692610173697432/1978100676";

        // Charboost
        // For Google Play
        public string CHARBOOST_PLAY_APPID = "543ddba6c26ee43759168948";
        public string CHARBOOST_PLAY_APPSIGH = "0602fa5275d280fc9e2cc3bfe29cb2f930a8fdd8";
        // For iOS
        public string CHARBOOST_IOS_APPID = "54052409c26ee42f095424ae";
        public string CHARBOOST_IOS_APPSIGH = "c4fdf2a2ac9efb7e99d6225c3f6f66d3fc485809";
        // For Amazon App Store
        public string CHARBOOST_AMAZON_APPID = "54052409c26ee42f095424ae";
        public string CHARBOOST_AMAZON_APPSIGH = "c4fdf2a2ac9efb7e99d6225c3f6f66d3fc485809";

        // AdColony android
        public string ADCOLONY_ANDROID_APP_ID = "app000c69c7bbd8452ba0";
        public string ADCOLONY_ANDROID_ZONE_ID = "vz692700c8578041fb84";
        public string ADCOLONY_ANDROID_MARKET_TYPE = "version:1.0,store:google";
        // Adcolony iOS
        public string ADCOLONY_IOS_APP_ID = "app000c69c7bbd8452ba0";
        public string ADCOLONY_IOS_ZONE_ID = "vz692700c8578041fb84";
        public string ADCOLONY_IOS_MARKET_TYPE = "version:1.0,store:apple";

        // UnityAds
        public string UNITYADS_APPID_IOS = "1008062";
        public string UNITYADS_APPID_ANDROID = "1008062";

        // Google Analytics
        public string GA_PROPERTY_ID = "UA-54433314-15";

        // Parse // config from fourpicsrus
        public string PARSE_APP_ID = "M7A87m0f9oqME8zKLkEeGV5WKGbyJ2GpniLjAA93";
        public string PARSE_REST_API_KEY = "zpxrOv3Tu9j2FusRsYsIJ3WhZoxkRe0RSQDjGEgr";

        // rest api url for config
        public string REST_API_URL = "";
    }

}