using Lary.Laboratory.Facebook.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     <para/>Facebook gragh api photo node. Represents an individual photo on Facebook.
    ///     <para/>Any valid access token can read photos on a public Page.
    ///     <para/>A page access token can read all photos posted to or posted by that Page.
    ///     <para/>The current user's photos can be read if the user has granted the user_photos or user_posts permission.
    ///     <para/>A user access token may read a photo that the current user is tagged in if they have granted the 
    ///     user_photos or user_posts permission.However, in some cases the photo's owner's privacy settings may not 
    ///     allow your application to access it.
    ///     <para/>A User access token for an Admin of a Group can read Group-owned Photos.
    ///     <para/>A User access token for an Admin of an Event can read Event-owned Photos if required after April 30, 2018.
    /// </summary>
    public partial class Photo
    {
        /// <summary>
        ///     The photo ID.
        /// </summary>
        [FacebookProperty("id")]
        public string Id { get; set; }

        /// <summary>
        ///     The album this photo is in.
        /// </summary>
        [FacebookProperty("album")]
        public Album Album { get; set; }

        /// <summary>
        ///     Backdated time.
        /// </summary>
        [FacebookProperty("backdated_time")]
        public DateTime? BackdatedTime { get; set; }

        /// <summary>
        ///     <see cref="BackdatedTimeGranularity"/>.
        /// </summary>
        [FacebookProperty("backdated_time_granularity")]
        public BackdatedTimeGranularity? BackdatedTimeGranularity { get; set; }

        /// <summary>
        ///     Indicates whether the viewer can backdate the photo.
        /// </summary>
        [FacebookProperty("can_backdate")]
        public bool? CanBackdate { get; set; }

        /// <summary>
        ///     Indicates whether the viewer can delete the photo.
        /// </summary>
        [FacebookProperty("can_delete")]
        public bool? CanDelete { get; set; }

        /// <summary>
        ///     Indicates whether the viewer can tag the photo.
        /// </summary>
        [FacebookProperty("can_tag")]
        public bool? CanTag { get; set; }

        /// <summary>
        ///     The time this photo was published.
        /// </summary>
        [FacebookProperty("created_time")]
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        ///     Event.
        /// </summary>
        [FacebookProperty("event")]
        public object Event { get; set; }

        /// <summary>
        ///     The profile (user or page) that uploaded this photo.
        /// </summary>
        [FacebookProperty("from")]
        public string From { get; set; }

        /// <summary>
        ///     The height of this photo in pixels.
        /// </summary>
        [FacebookProperty("height")]
        public uint? Height { get; set; }

        /// <summary>
        ///     The icon that Facebook displays when photos are published to News Feed.
        /// </summary>
        [FacebookProperty("icon")]
        public string Icon { get; set; }

        /// <summary>
        ///     The different stored representations of the photo. Can vary in number based upon the size of 
        ///     the original photo.
        /// </summary>
        [FacebookProperty("images")]
        public List<PlatformImageSource> Images { get; set; }

        /// <summary>
        ///     A link to the photo on Facebook.
        /// </summary>
        [FacebookProperty("link")]
        public string Link { get; set; }

        /// <summary>
        ///     The user-provided caption given to this photo. Corresponds to caption when creating photos.
        /// </summary>
        [FacebookProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     An array containing an array of objects mentioned in the name field which contain the id, name, 
        ///     and type of each object as well as the offset and length which can be used to match it up with its 
        ///     corresponding string in the name field.
        /// </summary>
        [FacebookProperty("name_tags")]
        public List<EntityAtTextRange> NameTags { get; set; }

        /// <summary>
        ///     ID of the page story this corresponds to. May not be on all photos. Applies only to published photos.
        /// </summary>
        [FacebookProperty("page_story_id")]
        public string PageStoryId { get; set; }

        /// <summary>
        ///     Link to the 100px wide representation of this photo.
        /// </summary>
        [FacebookProperty("picture")]
        public string Picture { get; set; }

        /// <summary>
        ///     Location associated with the photo, if any.
        /// </summary>
        [FacebookProperty("place")]
        public Place Place { get; set; }

        /// <summary>
        /// Deprecated. Returns 0.
        /// </summary>
        [Obsolete("Returns 0.")]
        [FacebookProperty("position")]
        public uint? Position { get; set; }

        /// <summary>
        /// Deprecated. Use images instead.
        /// </summary>
        [Obsolete("Use images instead.")]
        [FacebookProperty("source")]
        public string Source { get; set; }

        /// <summary>
        ///     The target this photo is published to.
        /// </summary>
        [FacebookProperty("target")]
        public object Target { get; set; }

        /// <summary>
        ///     The last time the photo was updated.
        /// </summary>
        [FacebookProperty("updated_time")]
        public DateTime? UpdatedTime { get; set; }

        /// <summary>
        ///     The different stored representations of the photo in webp format. Can vary in number based upon 
        ///     the size of the original photo.
        /// </summary>
        [FacebookProperty("webp_images")]
        public List<PlatformImageSource> WebpImages { get; set; }

        /// <summary>
        ///     The width of this photo in pixels.
        /// </summary>
        [FacebookProperty("width")]
        public uint? Width { get; set; }
    }
}
