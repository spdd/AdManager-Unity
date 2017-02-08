//
//  AMInterstitialDelegate.h
//  
//
//  Created by Dmitry B on 18.04.16.
//
//

#import <Foundation/Foundation.h>
#import "AMUTypes.h"

@interface AMInterstitialDelegate : NSObject

/// Initializes a IOSUInterstitial.
- (id)initWithClientReference:(IOSUTypeInterstitialClientRef *)interstitialClient;
- (void) show;

/// The interstitial ad.
@property(nonatomic, strong) AMInterstitialDelegate *interstitial;

/// A reference to the Unity interstitial client.
@property(nonatomic, assign) IOSUTypeInterstitialClientRef *interstitialClient;

/// The ad received callback into Unity.
@property(nonatomic, assign) IOSUInterstitialDidReceiveAdCallback adReceivedCallback;

/// The ad failed callback into Unity.
@property(nonatomic, assign) IOSUInterstitialDidFailToReceiveAdWithErrorCallback adFailedCallback;

/// The will present screen callback into Unity.
@property(nonatomic, assign) IOSUInterstitialWillPresentScreenCallback willPresentCallback;

/// The will dismiss screen callback into Unity.
@property(nonatomic, assign) IOSUInterstitialWillDismissScreenCallback willDismissCallback;

/// The did dismiss screen callback into Unity.
@property(nonatomic, assign) IOSUInterstitialDidDismissScreenCallback didDismissCallback;

/// The will leave application callback into Unity.
@property(nonatomic, assign) IOSUInterstitialWillLeaveApplicationCallback willLeaveCallback;

@end
