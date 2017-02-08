//
//  AMInterstitialDelegate.m
//  
//
//  Created by Dmitry B on 18.04.16.
//
//

#import "AMInterstitialDelegate.h"
#import <AdManager/WGAdsManager.h>
#import "UnityAppController.h"

@interface AMInterstitialDelegate () <WGInterstitialDelegate>

@end

@implementation AMInterstitialDelegate

- (id)initWithClientReference:(IOSUTypeInterstitialClientRef *)interstitialClient {
    self = [super init];
    if (self) {
        _interstitialClient = interstitialClient;
        [WGAdsManager setInterstitialDelegate:self];
    }
    return self;
}

+ (UIViewController *)unityGLViewController {
    return ((UnityAppController *)[UIApplication sharedApplication].delegate).rootViewController;
}

- (void) show {
    UIViewController *unityController = [AMInterstitialDelegate unityGLViewController];
    [WGAdsManager showInterstitial:unityController];
}

#pragma mark - interstitial ads delegate

- (void)onInterstitialLoaded:(NSString*)adName isPrecache:(BOOL)isPrecache {
    NSLog(@"inter %@ loaded", adName);
    if (self.adReceivedCallback) {
        self.adReceivedCallback(self.interstitialClient);
    }
}

- (void)onInterstitialFailedToLoad:(NSString*)adName {
    NSLog(@"inter %@ failed to load", adName);
    if (self.adFailedCallback) {
        self.adFailedCallback(self.interstitialClient, [@"error interstitial" cStringUsingEncoding:NSUTF8StringEncoding]);
    }
}

- (void)onInterstitialOpened:(NSString*)adName {
    NSLog(@"inter %@ opened", adName);
    if (self.willPresentCallback) {
        self.willPresentCallback(self.interstitialClient);
    }
}

- (void)onInterstitialClicked:(NSString*)adName {
    if (self.willLeaveCallback) {
        self.willLeaveCallback(self.interstitialClient);
    }
}

- (void)onInterstitialClosed:(NSString*)adName {
    if (self.didDismissCallback) {
        self.didDismissCallback(self.interstitialClient);
    }
}

@end
