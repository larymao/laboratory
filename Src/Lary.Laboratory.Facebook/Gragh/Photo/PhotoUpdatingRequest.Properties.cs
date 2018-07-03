using Lary.Laboratory.Facebook.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     Facebook photo updating request.
    /// </summary>
    public partial class PhotoUpdatingRequest
    {
        /// <summary>
        ///     Android key hash.
        /// </summary>
        [FacebookProperty("android_key_hash")]
        public string AndroidKeyHash { get; set; }

        /// <summary>
        ///     This feature adds support for uploading 3D model on posts.
        /// </summary>
        [FacebookProperty("asset3d_id")]
        public string Asset3dId { get; set; }

        /// <summary>
        ///     A user-specified creation time for this photo.
        /// </summary>
        [FacebookProperty("backdated_time")]
        public DateTime? BackdatedTime { get; set; }

        /// <summary>
        ///     <para/>Default value: none
        ///     <para/>Use only the part of the backdated_time parameter to the specified granularity.
        /// </summary>
        [FacebookProperty("backdated_time_granularity")]
        public BackdatedTimeGranularity BackdatedTimeGranularity { get; set; } = BackdatedTimeGranularity.None;

        /// <summary>
        ///     <para/>Default value: BRANDING_OTHER
        ///     <para/>The method that the user used to add a place tag to their story.
        /// </summary>
        [FacebookProperty("checkin_entry_point")]
        public CheckinEntryPoint CheckinEntryPoint { get; set; } = CheckinEntryPoint.BrandingOther;

        /// <summary>
        ///     Composer session ID.
        /// </summary>
        [FacebookProperty("composer_session_id")]
        public string ComposerSessionId { get; set; }

        /// <summary>
        ///     The status to allow sponsor directly boost the post.
        /// </summary>
        [FacebookProperty("direct_share_status")]
        public long? DirectShareStatus { get; set; }

        /// <summary>
        ///     IOS Bundle ID.
        /// </summary>
        [FacebookProperty("ios_bundle_id")]
        public string IosBundleId { get; set; }

        /// <summary>
        ///     <para/>Default value: false
        ///     <para/>Is this photo a checkin?
        /// </summary>
        [FacebookProperty("is_checkin")]
        public bool IsCheckin { get; set; } = false;

        /// <summary>
        ///     Is this photo cropped?
        /// </summary>
        [FacebookProperty("is_cropped")]
        public bool? IsCropped { get; set; }

        /// <summary>
        ///     <para/>Default value: false
        ///     <para/>Is this an explicit location?
        /// </summary>
        [FacebookProperty("is_explicit_location")]
        public bool IsExplicitLocation { get; set; } = false;

        /// <summary>
        ///     The Open Graph action type.
        /// </summary>
        [FacebookProperty("og_action_type_id")]
        public string OgActionTypeId { get; set; }

        /// <summary>
        ///     The Open Graph icon.
        /// </summary>
        [FacebookProperty("og_icon_id")]
        public string OgIconId { get; set; }

        /// <summary>
        ///     The Open Graph object ID.
        /// </summary>
        [FacebookProperty("og_object_id")]
        public string OgObjectId { get; set; }

        /// <summary>
        ///     The Open Graph phrase.
        /// </summary>
        [FacebookProperty("og_phrase")]
        public string OgPhrase { get; set; }

        /// <summary>
        ///     <para/>Default value: false
        ///     <para/>Flag to set if the post should create a profile badge.
        /// </summary>
        [FacebookProperty("og_set_profile_badge")]
        public bool OgSetProfileBadge { get; set; } = false;

        /// <summary>
        ///     The Open Graph suggestion.
        /// </summary>
        [FacebookProperty("og_suggestion_mechanism")]
        public string OgSuggestionMechanism { get; set; }

        /// <summary>
        ///     Page ID of a place associated with the photo.
        /// </summary>
        [FacebookProperty("place")]
        public string Place { get; set; }

        /// <summary>
        ///     Determines the privacy settings of the photo. If not supplied, this defaults to the privacy level 
        ///     granted to the app in the Login dialog. This field cannot be used to set a more open privacy setting 
        ///     than the one granted.
        /// </summary>
        [FacebookProperty("privacy")]
        public object Privacy { get; set; }

        /// <summary>
        ///     The prompt id if the post is generated from prompt.
        /// </summary>
        [FacebookProperty("prompt_id")]
        public string PromptId { get; set; }

        /// <summary>
        ///     The prompt tracking string if the post is generated from prompt.
        /// </summary>
        [FacebookProperty("prompt_tracking_string")]
        public string PromptTrackingString { get; set; }

        /// <summary>
        ///     Proxied app ID.
        /// </summary>
        [FacebookProperty("proxied_app_id")]
        public string ProxiedAppId { get; set; }

        /// <summary>
        ///     Set to false if you don't want the photo to be published immediately.
        /// </summary>
        [FacebookProperty("published")]
        public bool? Published { get; set; }

        /// <summary>
        ///     Sticker id of the sticker in the post.
        /// </summary>
        [FacebookProperty("referenced_sticker_id")]
        public string ReferencedStickerId { get; set; }

        /// <summary>
        ///     Facebook Page id that is tagged as sponsor in the photo post.
        /// </summary>
        [FacebookProperty("sponsor_id")]
        public string SponsorId { get; set; }

        /// <summary>
        ///     Sponsor Relationship, such as Presented By or Paid PartnershipWith.
        /// </summary>
        [FacebookProperty("sponsor_relationship")]
        public long? SponsorRelationship { get; set; }

        /// <summary>
        ///     Tags on this photo.
        /// </summary>
        [FacebookProperty("tags")]
        public List<int> Tags { get; set; }

        /// <summary>
        ///     Sets the object that this photo is attached to. Only relavant when publishing an unpublished photo.
        /// </summary>
        [FacebookProperty("target")]
        public object Target { get; set; }

        /// <summary>
        /// Sets the post this photo is attached to. Only relavant when publishing an unpublished photo.
        /// </summary>
        [FacebookProperty("target_post")]
        public string TargetPost { get; set; }

        /// <summary>
        ///     Same as backdated_time but with a time delta instead of absolute time.
        /// </summary>
        [FacebookProperty("time_since_original_post")]
        public long? TimeSinceOriginalPost { get; set; }
    }
}
