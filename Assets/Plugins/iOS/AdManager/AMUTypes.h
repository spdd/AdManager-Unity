//
//  IOSUTypes.h
//
//  Created by Dmitry B on 18.05.15.
//  Copyright (c) 2015 if3. All rights reserved.
//

/// Base type representing a IOSU* pointer.
typedef const void *IOSUTypeRef;

/// Type representing a Unity banner client.
typedef const void *IOSUTypeBannerClientRef;

/// Type representing a Unity interstitial client.
typedef const void *IOSUTypeInterstitialClientRef;

/// Type representing a Unity Video client.
typedef const void *IOSUTypeVideoClientRef;

/// Type representing a IOSUBanner.
typedef const void *IOSUTypeBannerRef;

/// Type representing a IOSUInterstitial.
typedef const void *IOSUTypeInterstitialRef;

/// Type representing a IOSUInterstitial.
typedef const void *IOSUTypeVideoRef;

/// Type representing a IOSURequest.
typedef const void *IOSUTypeRequestRef;

/// Callback for when a banner ad request was successfully loaded.
typedef void (*IOSUAdViewDidReceiveAdCallback)(IOSUTypeBannerClientRef *bannerClient);

/// Callback for when a banner ad request failed.
typedef void (*IOSUAdViewDidFailToReceiveAdWithErrorCallback)(IOSUTypeBannerClientRef *bannerClient,
const char *error);

/// Callback for when a full screen view is about to be presented as a result of a banner click.
typedef void (*IOSUAdViewWillPresentScreenCallback)(IOSUTypeBannerClientRef *bannerClient);

/// Callback for when a full screen view is about to be dismissed.
typedef void (*IOSUAdViewWillDismissScreenCallback)(IOSUTypeBannerClientRef *bannerClient);

/// Callback for when a full screen view has just been dismissed.
typedef void (*IOSUAdViewDidDismissScreenCallback)(IOSUTypeBannerClientRef *bannerClient);

/// Callback for when an application will background or terminate as a result of a banner click.
typedef void (*IOSUAdViewWillLeaveApplicationCallback)(IOSUTypeBannerClientRef *bannerClient);


// Interstitial

/// Callback for when a interstitial ad request was successfully loaded.
typedef void (*IOSUInterstitialDidReceiveAdCallback)(
IOSUTypeInterstitialClientRef *interstitialClient);

/// Callback for when an interstitial ad request failed.
typedef void (*IOSUInterstitialDidFailToReceiveAdWithErrorCallback)(
IOSUTypeInterstitialClientRef *interstitialClient, const char *error);

/// Callback for when an interstitial is about to be presented.
typedef void (*IOSUInterstitialWillPresentScreenCallback)(
IOSUTypeInterstitialClientRef *interstitialClient);

/// Callback for when an interstitial is about to be dismissed.
typedef void (*IOSUInterstitialWillDismissScreenCallback)(
IOSUTypeInterstitialClientRef *interstitialClient);

/// Callback for when an interstitial has just been dismissed.
typedef void (*IOSUInterstitialDidDismissScreenCallback)(
IOSUTypeInterstitialClientRef *interstitialClient);

/// Callback for when an application will background or terminate because of an interstitial click.
typedef void (*IOSUInterstitialWillLeaveApplicationCallback)(
IOSUTypeInterstitialClientRef *interstitialClient);

// Video

/// Callback for when video ad reward user
typedef void (*IOSUVideoAdShouldRewardUserCallback)(
IOSUTypeVideoClientRef *videoClient, const int amount);

/// Callback for when a video ad request was successfully loaded.
typedef void (*IOSUVideoDidReceiveAdCallback)(
IOSUTypeVideoClientRef *videoClient);

/// Callback for when an video ad request failed.
typedef void (*IOSUVideoDidFailToReceiveAdWithErrorCallback)(
IOSUTypeVideoClientRef *videoClient, const char *error);

/// Callback for when an video is about to be presented.
typedef void (*IOSUVideoWillPresentScreenCallback)(
IOSUTypeVideoClientRef *videoClient);

/// Callback for when an video has just been dismissed.
typedef void (*IOSUVideoDidDismissScreenCallback)(
IOSUTypeVideoClientRef *videoClient);

/// Callback for when an application will background or terminate because of an video click.
typedef void (*IOSUVideoWillLeaveApplicationCallback)(
IOSUTypeVideoClientRef *videoClient);
