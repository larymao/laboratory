using Lary.Laboratory.Facebook.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     <para/>An individual entry in a profile's feed. The profile could be a user, page, app, or group.
    ///     <para/>For Posts on a User:
    ///     <para/>That User's access token with the user_posts permission, or
    ///     <para/>That User's access token, if that User used the app to create the Post, or
    ///     <para/>The app's access token, if that User has previously granted the app the user_posts permission, or
    ///     <para/>The access token for a friend of that User, with the user_friends permission, if that User has
    ///     already granted the app both the user_posts and the user_friends permission.
    ///     <para/>For Posts on a Page:
    ///     <para/>Any valid access token can read posts on a public Page, but responses will not include User information.
    ///     <para/>A Page access token can read all Posts posted to or posted by that Page, and responses will include 
    ///     User information.
    ///     <para/>For Posts on a Group:
    ///     <para/>As of April 4th, 2018, an access token for an Admin of the Group, if the app has passed App Review. 
    ///     Please refer to the Breaking Changes changelog for more details.
    ///     <para/>For Posts that tag Users:
    ///     <para/>As of April 4th, 2018, apps can no longer read Posts owned by non-app User who have been tagged by 
    ///     an app User. Please refer to the Breaking Changes changelog for more details.
    ///     <para/>For Posts on an Event:
    ///     <para/>Any valid access token of an Admin of the Event for Event-owned Posts is required after April 30, 2018.
    /// </summary>
    public partial class Post
    {
        /// <summary>
        ///     The post ID.
        /// </summary>
        [FacebookProperty("id")]
        public string Id { get; set; }

        /// <summary>
        ///     The admin creator of a Page post. If the Page has only one admin, no data will be returned. Requires 
        ///     a Page Access Token and the business_management permission.
        /// </summary>
        [FacebookProperty("admin_creator")]
        public List<object> AdminCreator { get; set; }

        /// <summary>
        ///     Information about the app this post was published by.
        /// </summary>
        [FacebookProperty("application")]
        public object Application { get; set; }

        /// <summary>
        ///     The call to action type used in any Page posts for mobile app engagement ads.
        /// </summary>
        [FacebookProperty("call_to_action")]
        public object Call2Action { get; set; }

        /// <summary>
        ///     Whether the Page viewer can send a private reply to this Post. Requires the read_page_mailboxes permission.
        /// </summary>
        [FacebookProperty("can_reply_privately")]
        public bool? CanReplyPrivately { get; set; }

        /// <summary>
        ///     Link caption in post that appears below name. The caption must be an actual URLs and should accurately 
        ///     reflect the URL and associated advertiser or business someone visits when they click on it.
        /// </summary>
        [FacebookProperty("caption")]
        public string Caption { get; set; }

        /// <summary>
        ///     The time the post was initially published. For a post about a life event, this will be the date and 
        ///     time of the life event.
        /// </summary>
        [FacebookProperty("created_time")]
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        ///     A description of a link in the post (appears beneath the caption).
        /// </summary>
        [FacebookProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     Object that controls news feed targeting for this post.Anyone in these groups will be more likely to 
        ///     see this post, others will be less likely, but may still see it anyway. Any of the targeting fields 
        ///     shown here can be used, none are required (applies to Pages only).
        /// </summary>
        [FacebookProperty("feed_targeting")]
        public FeedTargeting FeedTargeting { get; set; }

        /// <summary>
        ///     Information (name and id) about the Profile that created the Post. If you read this field with a user 
        ///     access token, it returns only the current user.
        /// </summary>
        [FacebookProperty("from")]
        public object From { get; set; }

        /// <summary>
        ///     URL to a full-sized version of the Photo published in the Post or scraped from a link in the Post. 
        ///     If the photo's largest dimension exceeds 720 pixels, it will be resized, with the largest dimension 
        ///     set to 720.
        /// </summary>
        [FacebookProperty("full_picture")]
        public string FullPicture { get; set; }

        /// <summary>
        ///     A link to an icon representing the type of this post.
        /// </summary>
        [FacebookProperty("icon")]
        public string Icon { get; set; }

        /// <summary>
        ///     Whether the post can be promoted on Instagram. It returns the enum "eligible" if it can be promoted. 
        ///     Otherwise it returns an enum for why it cannot be promoted.
        /// </summary>
        [FacebookProperty("instagram_eligibility")]
        public string InstagramEligibility { get; set; }

        /// <summary>
        ///     If this post is marked as hidden (Applies to Pages only).
        /// </summary>
        [FacebookProperty("is_hidden")]
        public bool? IsHidden { get; set; }

        /// <summary>
        ///     Whether this post can be promoted in Instagram.
        /// </summary>
        [FacebookProperty("is_instagram_eligible")]
        public string IsInstagramEligible { get; set; }

        /// <summary>
        ///     Indicates whether a scheduled post was published (applies to scheduled Page Post only, for users post 
        ///     and instantly published posts this value is always true ). Note that this value is always false for 
        ///     page posts created as part of the Ad Creation process.
        /// </summary>
        [FacebookProperty("is_published")]
        public bool? IsPublished { get; set; }

        /// <summary>
        ///     The link attached to this post.
        /// </summary>
        [FacebookProperty("link")]
        public string Link { get; set; }

        /// <summary>
        ///     The status message in the post.
        /// </summary>
        [FacebookProperty("message")]
        public string Message { get; set; }

        /// <summary>
        ///     An array of profiles tagged in the message text. If you read this field with a user user access token, 
        ///     it returns only the current user.
        /// </summary>
        [FacebookProperty("message_tags")]
        public List<object> MessageTags { get; set; }

        /// <summary>
        ///     The name of the link.
        /// </summary>
        [FacebookProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     The ID of any uploaded photo or video attached to the post.
        /// </summary>
        [FacebookProperty("object_id")]
        public string ObjectId { get; set; }

        /// <summary>
        ///     The ID of a parent post for this post, if it exists. For example, if this story is a 'Your Page was 
        ///     mentioned in a post' story, the parent_id will be the original post where the mention happened.
        /// </summary>
        [FacebookProperty("parent_id")]
        public string ParentId { get; set; }

        /// <summary>
        ///     URL to the permalink page of the post.
        /// </summary>
        [FacebookProperty("permalink_url")]
        public string PermalinkUrl { get; set; }

        /// <summary>
        ///     URL to a resized version of the Photo published in the Post or scraped from a link in the Post. 
        ///     If the photo's largest dimension exceeds 130 pixels, it will be resized, with the largest dimension 
        ///     set to 130.
        /// </summary>
        [FacebookProperty("picture")]
        public string Picture { get; set; }

        /// <summary>
        ///     Any location information attached to the post.
        /// </summary>
        [FacebookProperty("place")]
        public object Place { get; set; }

        /// <summary>
        ///     The privacy settings of the post.
        /// </summary>
        [FacebookProperty("privacy")]
        public object Privacy { get; set; }

        /// <summary>
        ///     ID of post to use for promotion for stories that cannot be promoted directly.
        /// </summary>
        [FacebookProperty("promotable_id")]
        public string PromotableId { get; set; }

        /// <summary>
        ///     <para/>Status of the promotion. Requires Page admin privileges. Possible values:
        ///     <para/>active — Promotion is currently running.
        ///     <para/>draft — Promotion is still in draft mode.
        ///     <para/>extendable — Promotion's campaign has ended but can be restarted.
        ///     <para/>finished — Promotion has ended.
        ///     <para/>inactive — No active promotion.
        ///     <para/>ineligible — Post is ineligible for boosting.
        ///     <para/>paused — Promotion is currently paused.
        ///     <para/>pending — Promotion is still under review.
        ///     <para/>rejected — Promotion was rejected by the review process.
        /// </summary>
        [FacebookProperty("promotion_status")]
        public string PromotionStatus { get; set; }

        /// <summary>
        ///     A list of properties for any attached video, for example, the length of the video.
        /// </summary>
        [FacebookProperty("properties")]
        public List<object> Properties { get; set; }

        /// <summary>
        ///     The shares count of this post. The share count may include deleted posts and posts you cannot see 
        ///     for privacy reasons.
        /// </summary>
        [FacebookProperty("shares")]
        public object Shares { get; set; }

        /// <summary>
        ///     A URL to any Flash movie or video file attached to the post.
        /// </summary>
        [FacebookProperty("source")]
        public string Source { get; set; }

        /// <summary>
        ///     Description of the type of a status update.
        /// </summary>
        [FacebookProperty("status_type")]
        public PostStatusType? StatusType { get; set; }

        /// <summary>
        ///     Deprecated. Story.
        /// </summary>
        [Obsolete]
        [FacebookProperty("story")]
        public string Story { get; set; }

        /// <summary>
        ///     Deprecated field, same as <see cref="MessageTags"/>.
        /// </summary>
        [Obsolete]
        [FacebookProperty("story_tags")]
        public List<object> StoryTags { get; set; }

        /// <summary>
        ///     Object that limits the audience for this content.Only audiences in the specified demographics can view 
        ///     this content.The demographics are additive. Each additional value adds its audience to the cumulative 
        ///     targeted audience.These values do not override any Page-level demographic restrictions that may be in 
        ///     place.
        /// </summary>
        [FacebookProperty("targeting")]
        public object Targeting { get; set; }

        /// <summary>
        ///     Profiles mentioned or targeted in this post. If you read this field with a user access token, it 
        ///     returns only the current user.
        /// </summary>
        [FacebookProperty("to")]
        public List<object> To { get; set; }

        /// <summary>
        ///     A string indicating the object type of this post.
        /// </summary>
        [FacebookProperty("type")]
        public PostType? @Type { get; set; }

        /// <summary>
        ///     <para/>This field's behavior depends on the type of object the Post is on:
        ///     <para/>Group Posts — Time when the Post was created or commented upon.
        ///     <para/>Page Posts — Time when the Post was created, edited, or commented upon.
        ///     <para/>User Posts — Time when the Post was created, edited, or commented upon.
        /// </summary>
        [FacebookProperty("updated_time")]
        public DateTime? UpdatedTime { get; set; }

        /// <summary>
        ///     Profiles tagged as being 'with' the publisher of the post. If you read this field with a user 
        ///     access token, it returns only the current user.
        /// </summary>
        [FacebookProperty("with_tags")]
        public object WithTags { get; set; }
    }
}
