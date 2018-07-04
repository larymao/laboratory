using Lary.Laboratory.Facebook.Attributes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     Facebook photo creating request. The photo must be less than 10MB in size.
    /// </summary>
    public partial class PhotoCreatingRequest
    {
        /// <summary>
        ///     Legacy album ID. Deprecated.
        /// </summary>
        [Obsolete]
        [FacebookProperty("aid")]
        public string Aid { get; set; }

        /// <summary>
        ///     <para/>Default value: false
        ///     <para/>Indicates that we should allow this photo to be treated as a spherical photo. This will not change the 
        ///     behavior unless the server is able to interpret the photo as spherical, such as via Photosphere XMP 
        ///     metadata. Regular non-spherical photos will still be treated as regular photos even if this parameter 
        ///     is true.
        /// </summary>
        [FacebookProperty("allow_spherical_photo")]
        public bool AllowSphericalPhoto { get; set; } = false;

        /// <summary>
        ///     iTunes App ID. This is used by the native Share dialog that's part of iOS.
        /// </summary>
        [FacebookProperty("application_id")]
        public string ApplicationId { get; set; }

        /// <summary>
        ///     <para/>Default value: 0
        ///     <para/>Number of attempts that have been made to upload this photo
        /// </summary>
        [FacebookProperty("attempt")]
        public long Attempt { get; set; } = 0;

        /// <summary>
        ///     <para/>Default value: false
        ///     <para/>Audience exp
        /// </summary>
        [FacebookProperty("audience_exp")]
        public bool AudienceExp { get; set; } = false;

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
        ///     The description of the photo.
        /// </summary>
        [FacebookProperty("caption")]
        public string Caption { get; set; }

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
        ///     Object that controls News Feed targeting for this post. Anyone in these groups will be more likely to 
        ///     see this post. People not in these groups will be less likely to see this post, but may still see it 
        ///     anyway. Any of the targeting fields shown here can be used, but none are required. feed_targeting 
        ///     applies to Pages only.
        /// </summary>
        [FacebookProperty("feed_targeting")]
        public object FeedTargeting { get; set; }

        /// <summary>
        ///     <para/>Default value: -1
        ///     <para/>Unused?
        /// </summary>
        [FacebookProperty("filter_type")]
        public long FilterType { get; set; } = -1L;

        /// <summary>
        ///     <para/>Default value: false
        ///     <para/>Full res is coming later.
        /// </summary>
        [FacebookProperty("full_res_is_coming_later")]
        public bool FullResIsComingLater { get; set; } = false;

        /// <summary>
        ///     Manually specify the initial view heading in degrees from 0 to 360. This overrides any value present 
        ///     in the photo embedded metadata or provided in the spherical_metadata parameter.
        /// </summary>
        [FacebookProperty("initial_view_heading_override_degrees")]
        public long? InitialViewHeadingOverrideDegrees { get; set; }

        /// <summary>
        ///     Manually specify the initial view pitch in degrees from -90 to 90. This overrides any value present in 
        ///     the photo embedded metadata or provided in the spherical_metadata parameter.
        /// </summary>
        [FacebookProperty("initial_view_pitch_override_degrees")]
        public long? InitialViewPitchOverrideDegrees { get; set; }

        /// <summary>
        ///     Manually specify the initial view vertical FOV in degrees from 60 to 120. This overrides any value 
        ///     present in the photo embedded metadata or provided in the spherical_metadata parameter.
        /// </summary>
        [FacebookProperty("initial_view_vertical_fov_override_degrees")]
        public long? InitialViewVerticalFovOverrideDegrees { get; set; }

        /// <summary>
        ///     Is this an explicit location?
        /// </summary>
        [FacebookProperty("is_explicit_location")]
        public bool? IsExplicitLocation { get; set; }

        /// <summary>
        ///     If set to true, the tag is a place, not a person.
        /// </summary>
        [FacebookProperty("is_explicit_place")]
        public bool? IsExplicitPlace { get; set; }

        /// <summary>
        ///     ID of a page or a page set that provides location informationto enable Local Extensions.
        /// </summary>
        [FacebookProperty("location_source_id")]
        public string LocationSourceId { get; set; }

        /// <summary>
        ///     <para/>Default value: false
        ///     <para/>Manual privacy
        /// </summary>
        [FacebookProperty("manual_privacy")]
        public bool ManualPrivacy { get; set; } = false;

        /// <summary>
        ///     Deprecated. Please use the caption param instead.
        /// </summary>
        [Obsolete("Please use the caption param instead.")]
        [FacebookProperty("message")]
        public string Message { get; set; }

        /// <summary>
        ///     Deprecated. Please use the caption param instead.
        /// </summary>
        [Obsolete("Please use the caption param instead.")]
        [FacebookProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Nectar module. Internal apps only.
        /// </summary>
        [FacebookProperty("nectar_module")]
        public string NectarModule { get; set; }

        /// <summary>
        ///     If set to true, this will suppress the News Feed story that is automatically generated on a profile 
        ///     when people upload a photo using your app. Useful for adding old photos where you may not want to 
        ///     generate a story.
        /// </summary>
        [FacebookProperty("no_story")]
        public bool? NoStory { get; set; }

        /// <summary>
        ///     Default value: 0
        ///     Offline ID.
        /// </summary>
        [FacebookProperty("offline_id")]
        public long OfflineId { get; set; } = 0;

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
        ///     <para/>Flag to set if the post should create a profile badge
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
        ///     Deprecated. Use target_id instead.
        /// </summary>
        [Obsolete("Use target_id instead.")]
        [FacebookProperty("profile_id")]
        public int? ProfileId { get; set; }

        /// <summary>
        ///     <para/>Default value: true
        ///     <para/>Set to false if you don't want the photo to be published immediately.
        /// </summary>
        [FacebookProperty("published")]
        public bool? Published { get; set; }

        /// <summary>
        ///     Photos waterfall ID.
        /// </summary>
        [FacebookProperty("qn")]
        public string Qn { get; set; }

        /// <summary>
        ///     Time at which an unpublished post should be published (Unix ISO-8601 format). Applies to Pages only.
        /// </summary>
        [FacebookProperty("scheduled_publish_time")]
        public long? ScheduledPublishTime { get; set; }

        /// <summary>
        ///     A set of params describing an uploaded spherical photo. This field is not required; if it is not 
        ///     present we will try to generate spherical metadata from the metadata embedded in the image. If it is 
        ///     present, it takes precedence over any embedded metadata. Please click to the left to expand this list 
        ///     and see more information on each parameter. See also the Google Photo Sphere spec for more info on 
        ///     the meaning of the params: https://developers.google.com/streetview/spherical-metadata.
        /// </summary>
        [FacebookProperty("spherical_metadata")]
        public JObject SpghericalMetadata { get; set; }

        /// <summary>
        ///     Facebook Page id that is tagged as sponsor in the photo post.
        /// </summary>
        [FacebookProperty("sponsor_id")]
        public string SponsorId { get; set; }

        /// <summary>
        ///     Sponsor Relationship, such as Presented By or Paid PartnershipWith.
        /// </summary>
        [FacebookProperty("sponsor_relationship")]
        public long? SponserRelationship { get; set; }

        /// <summary>
        ///     <para/>Default value: Array
        ///     <para/>Tags on this photo.
        /// </summary>
        [FacebookProperty("tags")]
        public List<object> Tags { get; set; } = new List<object>();

        /// <summary>
        ///     Don't use this. Specifying a target_id allows you to post the photo to an object that's not the user 
        ///     in the access token. It only works when posting directly to the /photos endpoint. Instead of using 
        ///     this parameter you should be using the edge on an object directly, like /page/photos.
        /// </summary>
        [FacebookProperty("target_id")]
        public int? TargetId { get; set; }

        /// <summary>
        ///     Allows you to target posts to specific audiences. Applies to Pages only.
        /// </summary>
        [FacebookProperty("targeting")]
        public object Targeting { get; set; }

        /// <summary>
        ///     <para/>Default value: false
        ///     <para/>This is a temporary photo. published must be false, and you can't set scheduled_publish_time.
        /// </summary>
        [FacebookProperty("temporary")]
        public bool Temporary { get; set; } = false;

        /// <summary>
        ///     Content type of the unpublished content type.
        /// </summary>
        [FacebookProperty("unpublished_content_type")]
        public UnpublishedContentType? UnpublishedContentType { get; set; }

        /// <summary>
        ///     The URL of a photo that is already uploaded to the Internet. You must specify this or a file attachment.
        /// </summary>
        [FacebookProperty("url")]
        public string Url { get; set; }

        /// <summary>
        ///     A vault image ID to use for a photo. You can use only one of url, a file attachment, vault_image_id, 
        ///     or sync_object_uuid.
        /// </summary>
        [FacebookProperty("vault_image_id")]
        public string VaultImageId { get; set; }
    }
}
