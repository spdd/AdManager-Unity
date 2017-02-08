//
//  AMVideoDelegate.h
//  
//
//  Created by Dmitry B on 18.04.16.
//
//

#import <Foundation/Foundation.h>
#import "AMUTypes.h"

@interface AMVideoDelegate : NSObject

/// Initializes a IOSUvideo.
- (id)initWithClientReference:(IOSUTypeVideoClientRef *)videoClient;

- (void) show;

/// The video ad.
@property(nonatomic, strong) AMVideoDelegate *video;

/// A reference to the Unity video client.
@property(nonatomic, assign) IOSUTypeVideoClientRef *videoClient;

/// The ad received callback into Unity.
@property(nonatomic, assign) IOSUVideoDidReceiveAdCallback adReceivedCallback;

/// The ad failed callback into Unity.
@property(nonatomic, assign) IOSUVideoDidFailToReceiveAdWithErrorCallback adFailedCallback;

/// The will present screen callback into Unity.
@property(nonatomic, assign) IOSUVideoWillPresentScreenCallback willPresentCallback;

/// The did dismiss screen callback into Unity.
@property(nonatomic, assign) IOSUVideoDidDismissScreenCallback didDismissCallback;

/// The will leave application callback into Unity.
@property(nonatomic, assign) IOSUVideoWillLeaveApplicationCallback willLeaveCallback;

/// when video ad reward user callback into Unity.
@property(nonatomic, assign) IOSUVideoAdShouldRewardUserCallback rewardUserCallback;


@end
