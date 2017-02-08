//
//  IOSUInterface.m
//  iosdemo
//
//  Created by Dmitry B on 19.05.15.
//  Copyright (c) 2015 if3games. All rights reserved.
//

#import "AMInterstitialDelegate.h"
#import "AMVideoDelegate.h"
#import "AMUTypes.h"
#import <AdManager/WGAdsManager.h>
#import "UnityAppController.h"
#import "AMUObjectCache.h"

/// Returns an NSString copying the characters from |bytes|, a C array of UTF8-encoded bytes.
/// Returns nil if |bytes| is NULL.
static NSString *IOSUStringFromUTF8String(const char *bytes) {
    if (bytes) {
        return @(bytes);
    } else {
        return nil;
    }
}

void IOSUInit(const char *adIds, BOOL autocache) {
    //NSLog(@"%@", IOSUStringFromUTF8String(adIds));
    [WGAdsManager initWithAdsConfig:IOSUStringFromUTF8String(adIds) autocache:autocache];
}

/// Creates a IOSBannerView with the specified width, height, and position. Returns a reference to
/// the IOSUBannerView.
IOSUTypeBannerRef IOSUCreateBannerView(IOSUTypeBannerClientRef *bannerClient, const char *appKey,
                                       int adPosition) {
    /*
    IOSUBanner *banner =
    [[IOSUBanner alloc] initWithBannerClientReference:bannerClient
                                             appKey:IOSUStringFromUTF8String(appKey)
                                           adPosition:adPosition];
    AMUObjectCache *cache = [AMUObjectCache sharedInstance];
    [cache.references setObject:banner forKey:[banner IOSu_referenceKey]];
    return (__bridge IOSUTypeBannerRef)banner;
     */
    return nil;
}

/// Creates a full-width IOSBannerView in the current orientation. Returns a reference to the
/// IOSUBannerView.
IOSUTypeBannerRef IOSUCreateSmartBannerView(IOSUTypeBannerClientRef *bannerClient,
                                            const char *appKey, int adPosition) {
    /*
    IOSUBanner *banner =
            [[IOSUBanner alloc] initWithBannerClientReference:bannerClient
                                               appKey:IOSUStringFromUTF8String(appKey)
                                           adPosition:adPosition];
    AMUObjectCache *cache = [AMUObjectCache sharedInstance];
    [cache.references setObject:banner forKey:[banner IOSu_referenceKey]];
    return (__bridge IOSUTypeBannerRef)banner;
     */
    return nil;
}

/// Creates a IOSUInterstitial and returns its reference.
IOSUTypeInterstitialRef IOSUCreateInterstitial(IOSUTypeInterstitialClientRef *interstitialClient) {
    AMInterstitialDelegate *interstitial = [[AMInterstitialDelegate alloc]
                                      initWithClientReference:interstitialClient];
    AMUObjectCache *cache = [AMUObjectCache sharedInstance];
    [cache.references setObject:interstitial forKey:[interstitial IOSu_referenceKey]];
    return (__bridge IOSUTypeInterstitialRef)interstitial;
}

/// Creates a IOSUInterstitial and returns its reference.
IOSUTypeVideoRef IOSUCreateVideo(IOSUTypeVideoClientRef *videoClient) {
    AMVideoDelegate *video = [[AMVideoDelegate alloc] initWithClientReference:videoClient];
    AMUObjectCache *cache = [AMUObjectCache sharedInstance];
    [cache.references setObject:video forKey:[video IOSu_referenceKey]];
    return (__bridge IOSUTypeVideoRef)video;
}

/// Sets the banner callback methods to be invoked during banner ad events.
void IOSUSetBannerCallbacks(IOSUTypeBannerRef banner,
                            IOSUAdViewDidReceiveAdCallback adReceivedCallback,
                            IOSUAdViewDidFailToReceiveAdWithErrorCallback adFailedCallback,
                            IOSUAdViewWillPresentScreenCallback willPresentCallback,
                            IOSUAdViewWillDismissScreenCallback willDismissCallback,
                            IOSUAdViewDidDismissScreenCallback didDismissCallback,
                            IOSUAdViewWillLeaveApplicationCallback willLeaveCallback) {
    /*
    IOSUBanner *internalBanner = (__bridge IOSUBanner *)banner;
    internalBanner.adReceivedCallback = adReceivedCallback;
    internalBanner.adFailedCallback = adFailedCallback;
    internalBanner.willPresentCallback = willPresentCallback;
    internalBanner.willDismissCallback = willDismissCallback;
    internalBanner.didDismissCallback = didDismissCallback;
    internalBanner.willLeaveCallback = willLeaveCallback;
    NSLog(@"set banner callback");
     */
}

/// Sets the interstitial callback methods to be invoked during interstitial ad events.
void IOSUSetInterstitialCallbacks(
                                  IOSUTypeInterstitialRef interstitial, IOSUInterstitialDidReceiveAdCallback adReceivedCallback,
                                  IOSUInterstitialDidFailToReceiveAdWithErrorCallback adFailedCallback,
                                  IOSUInterstitialWillPresentScreenCallback willPresentCallback,
                                  IOSUInterstitialWillDismissScreenCallback willDismissCallback,
                                  IOSUInterstitialDidDismissScreenCallback didDismissCallback,
                                  IOSUInterstitialWillLeaveApplicationCallback willLeaveCallback) {
    AMInterstitialDelegate *internalInterstitial = (__bridge AMInterstitialDelegate *)interstitial;
    internalInterstitial.adReceivedCallback = adReceivedCallback;
    internalInterstitial.adFailedCallback = adFailedCallback;
    internalInterstitial.willPresentCallback = willPresentCallback;
    internalInterstitial.willDismissCallback = willDismissCallback;
    internalInterstitial.didDismissCallback = didDismissCallback;
    internalInterstitial.willLeaveCallback = willLeaveCallback;
    NSLog(@"set interstitial callback");
}

/// Sets the video callback methods to be invoked during video ad events.
void IOSUSetVideoCallbacks(
                                  IOSUTypeVideoRef video, IOSUVideoDidReceiveAdCallback adReceivedCallback,
                                  IOSUVideoDidFailToReceiveAdWithErrorCallback adFailedCallback,
                                  IOSUVideoWillPresentScreenCallback willPresentCallback,
                                  IOSUVideoDidDismissScreenCallback didDismissCallback,
                                  IOSUVideoWillLeaveApplicationCallback willLeaveCallback,
                                    IOSUVideoAdShouldRewardUserCallback rewardUserCallback) {
    AMVideoDelegate *internalVideo = (__bridge AMVideoDelegate *)video;
    internalVideo.adReceivedCallback = adReceivedCallback;
    internalVideo.adFailedCallback = adFailedCallback;
    internalVideo.willPresentCallback = willPresentCallback;
    internalVideo.didDismissCallback = didDismissCallback;
    internalVideo.willLeaveCallback = willLeaveCallback;
    internalVideo.rewardUserCallback = rewardUserCallback;
    NSLog(@"set video callback");
}

////////////////////////////////
//////// Banners ///////////////
////////////////////////////////


/// Sets the IOSBannerView's hidden property to YES.
void IOSUHideBannerView(IOSUTypeBannerRef banner) {
}

/// Sets the IOSBannerView's hidden property to NO.
void IOSUShowBannerView(IOSUTypeBannerRef banner, int adTypes) {
}

BOOL IOSUBannerReady(IOSUTypeBannerRef banner) {
    return NO;
}

/// Removes the IOSURemoveBannerView from the view hierarchy.
void IOSURemoveBannerView(IOSUTypeBannerRef banner) {
}

/// Makes a banner ad request.
void IOSURequestBannerAd(IOSUTypeBannerRef banner, IOSUTypeRequestRef request) {
}

/// Cache banner
void IOSUCacheBanner(IOSUTypeBannerRef banner) {
}

/// Set auto cache
void IOSUSetAutoCacheBanner(IOSUTypeBannerRef banner, BOOL autoCache) {
}

/// Disable ad network
void IOSUDisableNetworkBanner(IOSUTypeBannerRef banner, const char *adName) {
}

/// Show banner on ad name
void IOSUShowBannerWithAdName(IOSUTypeBannerRef banner, const char *adName) {
}

////////////////////////////////
//////// Interstitial //////////
////////////////////////////////

/// Returns YES if the IOSInterstitial is ready to be shown.
BOOL IOSUInterstitialReady(IOSUTypeInterstitialRef interstitial) {
    return [WGAdsManager isInterstitialLoaded];
}

/// Returns YES if the IOSInterstitial is ready to be shown.
BOOL IOSUInterstitialIsPrecache(IOSUTypeInterstitialRef interstitial) {
    return NO;
}

/// Shows the IOSInterstitial.
void IOSUShowInterstitial(IOSUTypeInterstitialRef interstitial) {
    AMInterstitialDelegate *internalInterstitial = (__bridge AMInterstitialDelegate *)interstitial;
    [internalInterstitial show];
}

/// Creates an empty IOSRequest and returns its reference.
IOSUTypeRequestRef IOSUCreateRequest() {
    //IOSURequest *request = [[IOSURequest alloc] init];
    //AMUObjectCache *cache = [AMUObjectCache sharedInstance];
    //[cache.references setObject:request forKey:[request IOSu_referenceKey]];
    return nil;
}

/// Makes an interstitial ad request.
void IOSURequestInterstitial(IOSUTypeInterstitialRef interstitial, IOSUTypeRequestRef request) {
}

/// Cache interstitial
void IOSUCacheInterstitial(IOSUTypeInterstitialRef interstitial) {
    [WGAdsManager cacheInterstitial];
}

/// Set auto cache
void IOSUSetAutoCacheInterstitial(IOSUTypeInterstitialRef interstitial, BOOL autoCache) {
    // TODO:
}

/// Disable ad network
void IOSUDisableNetworkInterstitial(IOSUTypeInterstitialRef interstitial, const char *adName) {
    
}

/// Show interstitial on ad name
void IOSUShowInterstitialWithAdName(IOSUTypeInterstitialRef interstitial, const char *adName) {
    
}

/////////////////////////
//////// Video //////////
/////////////////////////


/// Shows the IOSVideo.
void IOSUShowVideo(IOSUTypeVideoRef video) {
    AMVideoDelegate *internalVideo = (__bridge AMVideoDelegate *)video;
    [internalVideo show];
}

/// Makes an video ad request.
void IOSURequestVideo(IOSUTypeVideoRef video, IOSUTypeRequestRef request) {
}

/// Returns YES if the IOSVideo is ready to be shown.
BOOL IOSUVideoReady(IOSUTypeVideoRef video) {
    return [WGAdsManager isVideoLoaded];
}

/// Cache video
void IOSUCacheVideo(IOSUTypeVideoRef video) {
    [WGAdsManager cacheVideo];
}

/// Set auto cache
void IOSUSetAutoCacheVideo(IOSUTypeVideoRef video, BOOL autoCache) {

}

/// Disable ad network
void IOSUDisableNetworkVideo(IOSUTypeVideoRef video, const char *adName) {

}

/// Show video on ad name
void IOSUShowVideoWithAdName(IOSUTypeVideoRef video, const char *adName) {

}

/// Removes an object from the cache.
void IOSURelease(IOSUTypeRef ref) {
    if (ref) {
        AMUObjectCache *cache = [AMUObjectCache sharedInstance];
        [cache.references removeObjectForKey:[(__bridge NSObject *)ref IOSu_referenceKey]];
    }
}
