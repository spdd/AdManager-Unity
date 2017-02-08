using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Collections.Generic;
using UnityEditor.iOS.Xcode;
using If3games.Core.Internal.Ads;
using System.Text.RegularExpressions;

namespace If3games.Core.Editor
{
    public class PostprocessBuildPlayer 
    {
		public static AdsSettings mySettings = Resources.Load("AdManager/myAdsSettings") as AdsSettings;

        [PostProcessBuild(1080)]
        public static void OnPostProcessBuild (BuildTarget target, string path)
        {
            #if UNITY_5
            if (target == BuildTarget.iOS) {
            #else
            if (target == BuildTarget.iPhone) {
            #endif
                PostProcessBuild_iOS (path);
            }

            else if(target == BuildTarget.Android) {
                PostProcessBuild_Android(path);
            }
        }

		#region Android

        private static void PostProcessBuild_Android(string path)
        {
			//CopyAndroidDependencies();
			//ProcessAndroidManifest(path);
        }

		private static void CopyAndroidDependencies()
		{
			string androidPluginsPath = Path.Combine(Application.dataPath, "Plugins/Android");
			if (!Directory.Exists(androidPluginsPath))
			{
				Directory.CreateDirectory(androidPluginsPath);
			}

			if (mySettings.CHARTBOOST_ENABLED)
			{
				CopyAndroidLibs(createFullSource("chartboost.jar"), createFullDest("chartboost.jar"), false);
			}

			if (mySettings.ADS_ENABLED)
			{
				CopyAndroidLibs(createFullSource("google-play-services_lib"), createFullDest("google-play-services_lib"), true);
			}

			if (mySettings.ADCOLONY_ENABLED)
			{
				CopyAndroidLibs(createFullSource("adcolony.jar"), createFullDest("adcolony.jar"), false);
			}

			if (mySettings.UNITYADS_ENABLED)
			{
				CopyAndroidLibs(createFullSource("unity-ads"), createFullDest("unity-ads"), true);
			}
		}

		private static string createFullSource(string libName)
		{
			return string.Format("AdManager/Android/{0}", libName);
		}

		private static string createFullDest(string libName)
		{
			return string.Format("Plugins/Android/{0}", libName);
		}

		private static void CopyAndroidLibs(string source, string destination, bool isDirectory)
		{
			string sourcePath = Path.Combine(Application.dataPath, source);
			string destPath = Path.Combine(Application.dataPath, destination);

			if (isDirectory)
			{
				DirectoryCopy(sourcePath, destPath, true);
			}
			else 
			{
				if (!File.Exists(destPath))
				{
					File.Copy(sourcePath, destPath);
				}
			}
		}

		private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
		{
			// Get the subdirectories for the specified directory.
			DirectoryInfo dir = new DirectoryInfo(sourceDirName);

			if (!dir.Exists)
			{
				throw new DirectoryNotFoundException(
					"Source directory does not exist or could not be found: "
					+ sourceDirName);
			}

			DirectoryInfo[] dirs = dir.GetDirectories();
			// If the destination directory doesn't exist, create it.
			if (!Directory.Exists(destDirName))
			{
				Directory.CreateDirectory(destDirName);
			}

			// Get the files in the directory and copy them to the new location.
			FileInfo[] files = dir.GetFiles();
			foreach (FileInfo file in files)
			{
				string temppath = Path.Combine(destDirName, file.Name);
				file.CopyTo(temppath, false);
			}

			// If copying subdirectories, copy them and their contents to new location.
			if (copySubDirs)
			{
				foreach (DirectoryInfo subdir in dirs)
				{
					string temppath = Path.Combine(destDirName, subdir.Name);
					DirectoryCopy(subdir.FullName, temppath, copySubDirs);
				}
			}
		}

		private static void ProcessAndroidManifest(string path)
		{
			Debug.Log("path is:" + path);

			bool isCustomManifestUsed = false;
			string androidPluginsPath = Path.Combine(Application.dataPath, "Plugins/Android");
			string customManifestPath = Path.Combine(Application.dataPath, "AdManager/Android/AdManagerAndroidManifest.xml");
			string appManifestPath = Path.Combine(Application.dataPath, "Plugins/Android/AndroidManifest.xml");

			// Check if user has already created AndroidManifest.xml file in its location.
			// If not, use already predefined AdjustAndroidManifest.xml as default one.
			if (!File.Exists(appManifestPath))
			{
				if (!Directory.Exists(androidPluginsPath))
				{
					Directory.CreateDirectory(androidPluginsPath);
				}

				isCustomManifestUsed = false;
				File.Copy(customManifestPath, appManifestPath);

				Debug.Log("User defined AndroidManifest.xml file not found in Plugins/Android folder.");
				Debug.Log("Creating default app's AndroidManifest.xml from AdManager/Android/AndroidManifest.xmlfile.");
			}
			else {
				Debug.Log("User defined AndroidManifest.xml file located in Plugins/Android folder.");
			}

			if (!isCustomManifestUsed)
			{
				// However, if you already had your own AndroidManifest.xml, we'll now run
				// some checks on it and tweak it a bit if needed to add some stuff which
				// our native Android SDK needs so that it can run properly.

				// Let's open the app's AndroidManifest.xml file.
				XmlDocument manifestFile = new XmlDocument();
				manifestFile.Load(appManifestPath);

				// Add needed permissions if they are missing.
				AddPermissions(manifestFile);

				// Add Activities
				AddActivity(manifestFile);

				// Add intent filter to main activity if it is missing.
				AddBroadcastReceiver(manifestFile);

				// Save the changes.
				manifestFile.Save(appManifestPath);

				// Clean the manifest file.
				CleanManifestFile(appManifestPath);

				Debug.Log("App's AndroidManifest.xml file check and potential modification completed.");
			}

		}

		private static void AddPermissions(XmlDocument manifest)
		{
			// The adjust SDK needs two permissions to be added to you app's manifest file:
			// <uses-permission android:name="android.permission.INTERNET" />
			// <uses-permission android:name="android.permission.ACCESS_WIFI_STATE" />
			// <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
			// <uses-permission android:name="android.permission.READ_PHONE_STATE" />
			// <uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
			// 

			Debug.Log("Checking if all permissions needed for the adjust SDK are present in the app's AndroidManifest.xml file.");

			string INTERNET = "android.permission.INTERNET";
			string ACCESS_WIFI_STATE = "android.permission.ACCESS_WIFI_STATE";
			string ACCESS_NETWORK_STATE = "android.permission.ACCESS_NETWORK_STATE";
			string READ_PHONE_STATE = "android.permission.READ_PHONE_STATE";
			string WRITE_EXTERNAL_STORAGE = "android.permission.WRITE_EXTERNAL_STORAGE";

			bool hasInternet = false;
			bool hasAccessWiFi = false;
			bool hasAccessNetState = false;
			bool hasReadPhone = false;
			bool hasWrExtStorage = false;

			XmlElement manifestRoot = manifest.DocumentElement;

			// Check if permissions are already there.
			foreach (XmlNode node in manifestRoot.ChildNodes)
			{
				if (node.Name == "uses-permission")
				{
					foreach (XmlAttribute attribute in node.Attributes)
					{
						if (attribute.Value.Contains(INTERNET))
						{
							hasInternet = true;
						}
						else if (attribute.Value.Contains(ACCESS_WIFI_STATE))
						{
							hasAccessWiFi = true;
						}
						else if (attribute.Value.Contains(ACCESS_NETWORK_STATE))
						{
							hasAccessNetState = true;
						}
						else if (attribute.Value.Contains(READ_PHONE_STATE))
						{
							hasReadPhone = true;
						}
						else if (attribute.Value.Contains(WRITE_EXTERNAL_STORAGE))
						{
							hasWrExtStorage = true;
						}
					}
				}
			}

			if(!hasInternet)
				AppendPermission(manifest, INTERNET);
			if(!hasAccessWiFi)
				AppendPermission(manifest, ACCESS_WIFI_STATE);
			if(!hasAccessNetState)
				AppendPermission(manifest, ACCESS_NETWORK_STATE);
			if(!hasReadPhone)
				AppendPermission(manifest, READ_PHONE_STATE);
			if(!hasWrExtStorage)
				AppendPermission(manifest, WRITE_EXTERNAL_STORAGE);
			
		}

		private static void AppendPermission(XmlDocument manifest, string permission)
		{
			XmlElement manifestRoot = manifest.DocumentElement;
			XmlElement element = manifest.CreateElement("uses-permission");
			element.SetAttribute("android__name", permission);
			manifestRoot.AppendChild(element);
			Debug.Log(string.Format("{0} successfully added to your app's AndroidManifest.xml file.", permission));
		}

		private static void AddActivity(XmlDocument manifest)
		{
			XmlElement manifestRoot = manifest.DocumentElement;
			XmlNode applicationNode = null;

			// Let's find the application node.
			foreach (XmlNode node in manifestRoot.ChildNodes)
			{
				if (node.Name == "application")
				{
					applicationNode = node;
					break;
				}
			}

			if (applicationNode == null)
			{
				Debug.LogError("Your app's AndroidManifest.xml file does not contain \"<application>\" node.");
				Debug.LogError("Unable to add the activity to AndroidManifest.xml.");
				return;
			}

			bool hasChartboost = false;
			bool hasAdMob = false;
			bool hasUnityAds = false;
			bool hasAdColony = false;

			Debug.Log(AdsAtributes.chartboostAttrs["name"]);

			foreach (XmlNode node in applicationNode.ChildNodes)
			{
				if (node.Name == "activity")
				{
					foreach (XmlAttribute attribute in node.Attributes)
					{
						if (attribute.Value.Contains(AdsAtributes.chartboostAttrs["name"]))
						{
							hasChartboost = true;
							break;
						}
						else if (attribute.Value.Contains(AdsAtributes.admobAttrs["name"])) {
							hasAdMob = true;
							break;
						}
						else if (attribute.Value.Contains(AdsAtributes.unityAdsAttrs["name"]))
						{
							hasUnityAds = true;
							break;
						}
						else if (attribute.Value.Contains(AdsAtributes.adColonyAttrs1["name"]))
						{
							hasAdColony = true;
							break;
						}
					}
				}
			}

			if (!hasChartboost)
			{
				if (mySettings.CHARTBOOST_ENABLED)
					AppendActivityAndMetaData("activity", manifest, applicationNode, AdsAtributes.chartboostAttrs);
			}
			if (!hasAdMob)
			{
				if (mySettings.ADMOB_ENABLED)
				{
					AppendActivityAndMetaData("activity", manifest, applicationNode, AdsAtributes.admobAttrs);
					AppendActivityAndMetaData("meta-data", manifest, applicationNode, AdsAtributes.admobMetaDataAttrs);
				}
			}
			if (!hasUnityAds)
			{
				if (mySettings.UNITYADS_ENABLED)
				{
					AppendActivityAndMetaData("activity", manifest, applicationNode, AdsAtributes.unityAdsAttrs);
				}
			}
			if (!hasAdColony)
			{
				if (mySettings.ADCOLONY_ENABLED)
				{
					AppendActivityAndMetaData("activity", manifest, applicationNode, AdsAtributes.adColonyAttrs1);
					AppendActivityAndMetaData("activity", manifest, applicationNode, AdsAtributes.adColonyAttrs2);
					AppendActivityAndMetaData("activity", manifest, applicationNode, AdsAtributes.adColonyAttrs3);
				}
			}
			
		}

		private static void AppendActivityAndMetaData(string element, XmlDocument manifest, XmlNode applicationNode, Dictionary<string, string> attributes)
		{
			// Generate activity entry and add it to the application node.
			XmlElement activityElement = manifest.CreateElement(element);
			foreach(string key in attributes.Keys) 
			{
				activityElement.SetAttribute(string.Format("android__{0}", key), attributes[key]);
			}
			applicationNode.AppendChild(activityElement);
			Debug.Log(string.Format("{0} successfully added to your app's AndroidManifest.xml file.", attributes["name"]));
		}

		private static void AddBroadcastReceiver(XmlDocument manifest)
		{
		}

		private static void CleanManifestFile(String manifestPath)
		{
			// Due to XML writing issue with XmlElement methods which are unable
        	// to write "android:[param]" string, we have wrote "android__[param]" string instead.
        	// Now make the replacement: "android:[param]" -> "android__[param]"

        	TextReader manifestReader = new StreamReader(manifestPath);
			string manifestContent = manifestReader.ReadToEnd();
			manifestReader.Close();

			Regex regex_tools = new Regex("android__tools_");
			manifestContent = regex_tools.Replace(manifestContent, "tools:");

			Regex regex = new Regex("android__");
			manifestContent = regex.Replace(manifestContent, "android:");

			TextWriter manifestWriter = new StreamWriter(manifestPath);
			manifestWriter.Write(manifestContent);
			manifestWriter.Close();
		}

		#endregion

		#region iOS 

		private static void PostProcessBuild_iOS (string path)
        {
			CreateXCodeDependencies(path);
            ProcessXCodeProject (path);
        }

        private static void ProcessXCodeProject (string path)
        {
			UpdateXCodeLinkerFlags(path);
			UpdateXCodeCompilerFlag(path);
			RemoveFilesFromXCode(path);
        }

		private static void UpdateXCodeCompilerFlag(string path)
		{
			string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";

			PBXProject proj = new PBXProject();
			proj.ReadFromString(File.ReadAllText(projPath));

			string target = proj.TargetGuidByName("Unity-iPhone");
			string file = proj.FindFileGuidByProjectPath("Libraries/AdManager/Plugins/iOS/GoogleToolboxForMac/Foundation/GTMLogger.m");

			var flags = proj.GetCompileFlagsForFile(target, file);
			flags.Add("-fno-objc-arc");
			proj.SetCompileFlagsForFile(target, file, flags);

			File.WriteAllText(projPath, proj.WriteToString());
		}

		private static void UpdateXCodeLinkerFlags(string path)
		{
			string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
			PBXProject proj = new PBXProject();
			proj.ReadFromString(File.ReadAllText(projPath));
			string targetGUID = proj.TargetGuidByName("Unity-iPhone");
			proj.AddBuildProperty(targetGUID, "OTHER_LDFLAGS", "-ObjC");
			File.WriteAllText(projPath, proj.WriteToString());
		}

		private static void RemoveFilesFromXCode(string path) 
		{
			string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
			PBXProject proj = new PBXProject();
			proj.ReadFromString(File.ReadAllText(projPath));

			if (!mySettings.FIREBASE_CONFIG_ENABLED)
			{
				RemoveFileFromXCode(proj, "Libraries/AdManager/Plugins/iOS/AdManager/Loaders/WGFireConfigLoader");
			}

			if (!mySettings.ADMOB_ENABLED)
			{
				
				RemoveFileFromXCode(proj, "Libraries/AdManager/Plugins/iOS/AdManager/Adapters/admob/interstitial/WGADMOBAdapter");
			}

			if (!mySettings.CHARTBOOST_ENABLED)
			{
				RemoveFileFromXCode(proj, "Libraries/AdManager/Plugins/iOS/AdManager/Adapters/chartboost/video/WGCHARTBOOSTAdapter");
			}

			if (!mySettings.UNITYADS_ENABLED)
			{
				RemoveFileFromXCode(proj, "Libraries/AdManager/Plugins/iOS/AdManager/Adapters/unity/video/WGUNITYADSAdapter");
			}

			if (!mySettings.ADCOLONY_ENABLED)
			{
				RemoveFileFromXCode(proj, "Libraries/AdManager/Plugins/iOS/AdManager/Adapters/adcolony/video/WGADCOLONYAdapter");
			}

			if (!mySettings.APPLOVIN_ENABLED)
			{
				RemoveFileFromXCode(proj, "Libraries/AdManager/Plugins/iOS/AdManager/Adapters/applovin/video/WGAPPLOVINAdapter");
			}

			File.WriteAllText(projPath, proj.WriteToString());
		}

		private static void RemoveFileFromXCode(PBXProject proj, string path) 
		{
			string[] exts = new string[] { "h", "m" };
			foreach (var ext in exts) {
				string fileGUID = proj.FindFileGuidByProjectPath(string.Format(@"{0}.{1}", path, ext));
				proj.RemoveFile(fileGUID);
			}
		}

		private static void AddUsrLib(PBXProject proj, string targetGuid, string framework)
		{
			string fileGuid = proj.AddFile("usr/lib/" + framework, "Frameworks/" + framework, PBXSourceTree.Sdk);
			proj.AddFileToBuild(targetGuid, fileGuid);
		}

		private static void CreateXCodeDependencies(string path) 
		{ 
			string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
			PBXProject proj = new PBXProject();
			proj.ReadFromString(File.ReadAllText(projPath));

			string target = proj.TargetGuidByName("Unity-iPhone");

			proj.AddFrameworkToProject(target, "AddressBook.framework", false);
			proj.AddFrameworkToProject(target, "AdSupport.framework", false);
			proj.AddFrameworkToProject(target, "StoreKit.framework", false);
			proj.AddFrameworkToProject(target, "Social.framework", false);
			proj.AddFrameworkToProject(target, "CoreTelephony.framework", false);
			proj.AddFrameworkToProject(target, "AVFoundation.framework", false);
			proj.AddFrameworkToProject(target, "CoreData.framework", false);
			proj.AddFrameworkToProject(target, "CoreGraphics.framework", false);
			proj.AddFrameworkToProject(target, "CoreMedia.framework", false);
			proj.AddFrameworkToProject(target, "CoreTelephony.framework", false);
			proj.AddFrameworkToProject(target, "EventKit.framework", false);
			proj.AddFrameworkToProject(target, "EventKitUI.framework", false);
			proj.AddFrameworkToProject(target, "MediaPlayer.framework", false);
			proj.AddFrameworkToProject(target, "MessageUI.framework", false);
			proj.AddFrameworkToProject(target, "SystemConfiguration.framework", false);
			proj.AddFrameworkToProject(target, "Foundation.framework", false);
			proj.AddFrameworkToProject(target, "UIKit.framework", false);
			proj.AddFrameworkToProject(target, "QuartzCore.framework", false);
			proj.AddFrameworkToProject(target, "WebKit.framework", false);
			proj.AddFrameworkToProject(target, "MobileCoreServices.framework", false);
			proj.AddFrameworkToProject(target, "GLKit.framework", false);

			AddUsrLib(proj, target, "libc++.tbd");
			AddUsrLib(proj, target, "libz.tbd");
			AddUsrLib(proj, target, "libsqlite3.0.tbd");

			File.WriteAllText(projPath, proj.WriteToString());
		}

		#endregion
    }
}
