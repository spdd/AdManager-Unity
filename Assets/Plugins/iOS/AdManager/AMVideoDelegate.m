//
//  AMVideoDelegate.m
//  
//
//  Created by Dmitry B on 18.04.16.
//
//

#import "AMVideoDelegate.h"
#import <AdManager/WGAdsManager.h>
#import "UnityAppController.h"

@interface AMVideoDelegate () <WGVideoDelegate>

@end

@implementation AMVideoDelegate

+ (UIViewController *)unityGLViewController {
    return ((UnityAppController *)[UIApplication sharedApplication].delegate).rootViewController;
}

- (id)initWithClientReference:(IOSUTypeVideoClientRef *)videoClient {
    self = [super init];
    if (self) {
        _videoClient = videoClient;
        [WGAdsManager setVideoAdDelegate:self];
    }
    return self;
}

- (void) show {
    UIViewController *unityController = [AMVideoDelegate unityGLViewController];
    [WGAdsManager showVideo:unityController];
}

#pragma mark - video ads delegate

- (void)onVideoLoaded:(NSString*)adName {
    NSLog(@"video loaded");
    if (self.adReceivedCallback) {
        self.adReceivedCallback(self.videoClient);
    }
}

- (void)onVideoFailedToLoad:(NSString*)adName {
    NSLog(@"video failed to load");
    if(self.adFailedCallback) {
        self.adFailedCallback(self.videoClient, [@"error video" cStringUsingEncoding:NSUTF8StringEncoding]);
    }
}

- (void)onVideoOpened:(NSString*)adName {
    NSLog(@"video opened");
    if (self.willPresentCallback) {
        self.willPresentCallback(self.videoClient);
    }
}

- (void)onVideoClosed:(NSString*)adName {
    NSLog(@"video closed");
    if (self.didDismissCallback) {
        self.didDismissCallback(self.videoClient);
    }
}

- (void)onVideoClicked:(NSString*)adName {
    NSLog(@"video clicked");
}

- (void)onVideoFinished:(NSString*)adName {
    NSLog(@"video finished");
    if (self.rewardUserCallback) {
        self.rewardUserCallback(self.videoClient, 0);
    }
}

@end
