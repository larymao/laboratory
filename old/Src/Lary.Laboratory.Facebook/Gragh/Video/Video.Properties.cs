using Lary.Laboratory.Facebook.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     <para/>Represents an individual video on Facebook.
    ///     <para/>Any valid access token can read videos on a public Page.
    ///     <para/>A page access token can read all videos posted to or posted by that Page.
    ///     <para/>A user access token can read any video your application created on behalf of that user.
    ///     <para/>A user's videos can be read if the owner has granted the user_videos or user_posts permission.
    ///     <para/>A user access token may read a video that user is tagged in if they user granted the user_videos 
    ///     or user_posts permission. However, in some cases the video's owner's privacy settings may not allow your 
    ///     application to access it.
    ///     <para/>The source field will not be returned for Page-owned videos unless the User making the request has 
    ///     a role on the owning Page.
    /// </summary>
    public partial class Video
    {
        /// <summary>
        ///     Time offsets of ad breaks in milliseconds. Ad breaks are short ads that play within a video.
        /// </summary>
        [FacebookProperty("ad_breaks")]
        public List<int> AdBreaks { get; set; }

        /// <summary>
        ///     The time when the video post was created.
        /// </summary>
        [FacebookProperty("backdated_time")]
        public DateTime? BackdatedTime { get; set; }

        /// <summary>
        ///     Accuracy of the backdated time.
        /// </summary>
        [FacebookProperty("backdated_time_granularity")]
        public BackdatedTimeGranularity? GetBackdatedTimeGranularity { get; set; }

        /// <summary>
        ///     The content category of this video.
        /// </summary>
        [FacebookProperty("content_category")]
        public VideoContentCategory? ContentCategory { get; set; }

        /// <summary>
        ///     Tags that describe the contents of the video.
        /// </summary>
        [FacebookProperty("content_tags")]
        public List<string> ContentTags { get; set; }

        /// <summary>
        ///     The description of the video.
        /// </summary>
        [FacebookProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     The video ID.
        /// </summary>
        [FacebookProperty("id")]
        public string Id { get; set; }

        /// <summary>
        ///     The time the video was initially published.
        /// </summary>
        [FacebookProperty("created_time")]
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        ///     Labels used to describe the video. Unlike content tags, custom labels are not published and 
        ///     only appear in insights data.
        /// </summary>
        [FacebookProperty("custom_labels")]
        public List<string> CustomLabels { get; set; }

        /// <summary>
        ///     The HTML element that may be embedded in a Web page to play the video.
        /// </summary>
        [FacebookProperty("embed_html")]
        public string EmbedHtml { get; set; }

        /// <summary>
        ///     Whether the video is embeddable.
        /// </summary>
        [FacebookProperty("embeddable")]
        public bool? Embeddable { get; set; }

        /// <summary>
        ///     Event.
        /// </summary>
        [FacebookProperty("event")]
        public object Event { get; set; }

        /// <summary>
        ///     The different formats of the video.
        /// </summary>
        [FacebookProperty("format")]
        public List<VideoFormat> Format { get; set; }

        /// <summary>
        ///     The profile that created the video.
        /// </summary>
        [FacebookProperty("form")]
        public object From { get; set; }

        /// <summary>
        ///     The icon that Facebook displays when videos are published to the feed.
        /// </summary>
        [FacebookProperty("icon")]
        public string Icon { get; set; }

        /// <summary>
        ///     Whether the video is crossposted from other page.
        /// </summary>
        [FacebookProperty("is_crosspost_video")]
        public bool? IsCrosspostVideo { get; set; }

        /// <summary>
        ///     Specifies if the video is eligible for crossposting. Page access-token is required to query this field.
        /// </summary>
        [FacebookProperty("is_crossposting_eligible")]
        public bool? IsCrosspostingEligible { get; set; }

        /// <summary>
        ///     Whether this video is episodie or not.
        /// </summary>
        [FacebookProperty("is_episode")]
        public bool? IsEpisode { get; set; }

        /// <summary>
        ///     Whether the video is eligible to be promoted on Instagram.
        /// </summary>
        [FacebookProperty("is_instagram_eligible")]
        public bool? IsInstagramEligible { get; set; }

        /// <summary>
        ///     Whether the video is exclusively used for copyright monitoring.
        /// </summary>
        [FacebookProperty("is_reference_only")]
        public bool? IsReferenceOnly { get; set; }

        /// <summary>
        ///     Duration of this video in seconds.
        /// </summary>
        [FacebookProperty("length")]
        public double? Length { get; set; }

        /// <summary>
        ///     The live status of the video.
        /// </summary>
        [FacebookProperty("live_status")]
        public LiveVideoStatus? LiveStatus { get; set; }

        /// <summary>
        ///     The music video copyright of this video.
        /// </summary>
        [FacebookProperty("music_video_copyright")]
        public object MusicVideoCopyright { get; set; }

        /// <summary>
        ///     URL to the permalink page of the video.
        /// </summary>
        [FacebookProperty("permalink_url")]
        public string PermalinkUrl { get; set; }

        /// <summary>
        ///     The URL for the thumbnail picture of the video.
        /// </summary>
        [FacebookProperty("picture")]
        public string Picture { get; set; }

        /// <summary>
        ///     Location associated with the video, if any.
        /// </summary>
        [FacebookProperty("place")]
        public Place Place { get; set; }

        /// <summary>
        ///     Privacy setting for the video.
        /// </summary>
        [FacebookProperty("privacy")]
        public object Privacy { get; set; }

        /// <summary>
        ///     Whether a post about this video is published. The post is not scheduled, draft, or ads_post.
        /// </summary>
        [FacebookProperty("published")]
        public bool? Published { get; set; }

        /// <summary>
        ///     The time that the video is scheduled to publish.
        /// </summary>
        [FacebookProperty("scheduled_publish_time")]
        public DateTime? ScheduledPublishTime { get; set; }

        /// <summary>
        ///     A URL to the raw, playable video file.
        /// </summary>
        [FacebookProperty("source")]
        public string Source { get; set; }

        /// <summary>
        ///     Object describing the status attributes of a video.
        /// </summary>
        [FacebookProperty("status")]
        public VideoStatus Status { get; set; }

        /// <summary>
        ///     The video title or caption.
        /// </summary>
        [FacebookProperty("title")]
        public string Title { get; set; }

        /// <summary>
        ///     The publishers asset management code for this video.
        /// </summary>
        [FacebookProperty("universal_video_id")]
        public string UniversalVideoId { get; set; }

        /// <summary>
        ///     The last time the video or its caption was updated.
        /// </summary>
        [FacebookProperty("updated_time")]
        public DateTime? UpdatedTime { get; set; }
    }
}
