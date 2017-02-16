using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using If3games.Core.Internal.Ads.Common;
using If3games.Core.Internal.Ads;
using System;

// Example script showing how to invoke the AdManager Unity plugin.
public class TestAds : MonoBehaviour, IInterstitialCallbacks, IVideoCallbacks
{
	void OnGUI()
	{
		// Puts some basic buttons onto the screen.
		GUI.skin.button.fontSize = (int)(0.05f * Screen.height);

		Rect requestBannerRect = new Rect(0.1f * Screen.width, 0.05f * Screen.height,
										  0.8f * Screen.width, 0.1f * Screen.height);
		if (GUI.Button(requestBannerRect, "Initialize"))
		{
			Debug.Log("AdManager initialization");

			AdManager.initialize();
			AdManager.setInterstitialCallbacks(this);
			AdManager.setVideoCallbacks(this);
		}

		Rect showInterstitialRect = new Rect(0.1f * Screen.width, 0.175f * Screen.height,
									   0.8f * Screen.width, 0.1f * Screen.height); 
		if (GUI.Button(showInterstitialRect, "Show Interstitial"))
		{
			AdManager.showInterstitial();
		}

		Rect showVideoRect = new Rect(0.1f * Screen.width, 0.3f * Screen.height,
									  0.8f * Screen.width, 0.1f * Screen.height);
		if (GUI.Button(showVideoRect, "Show Video"))
		{
			AdManager.showRewardedVideo();
		}
	}

	#region Interstitial callback handlers

	public void onInterstitialLoaded() { print("plugin: Interstitial loaded"); }
	public void onInterstitialFailedToLoad() { print("plugin: Interstitial failed"); }
	public void onInterstitialOpened() { print("plugin: Interstitial opened"); }
	public void onInterstitialClosed() { print("plugin: Interstitial closed"); }
	public void onInterstitialClicked() { print("plugin: Interstitial clicked"); }

	#endregion

	#region Video callback handlers

	public void onVideoLoaded() { print("plugin: Video loaded"); }
	public void onVideoFailedToLoad() { print("plugin: Video failed"); }
	public void onVideoOpened() { print("plugin: Video opened"); }
	public void onVideoClosed() { print("plugin: Video closed"); }
	public void onVideoClicked() { print("plugin: Video clicked"); }
	public void onVideoFinished() { print("plugin: Video finished"); }

	#endregion
}
