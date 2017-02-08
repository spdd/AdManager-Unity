//
//  AMUObjectCache.h
//  Unity-iPhone
//
//  Created by Dmitry B on 19.05.15.
//
//

#import <Foundation/Foundation.h>

/// A cache to hold onto objects while Unity is still referencing them.
@interface AMUObjectCache : NSObject

+ (instancetype)sharedInstance;

/// References to objects AdsManager ads objects created from Unity.
@property(nonatomic, strong) NSMutableDictionary *references;

@end

@interface NSObject (AMUOwnershipAdditions)

/// Returns a key used to lookup a AdsManager Ads object. This method is intended to only be used
/// by AdsManager Ads objects.
- (NSString *)IOSu_referenceKey;

@end
