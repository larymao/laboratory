using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     Description of the type of a status update.
    /// </summary>
    public enum PostStatusType
    {
        /// <summary>
        ///     Approved friend.
        /// </summary>
        [Description("approved_friend")]
        ApprovedFriend,

        /// <summary>
        ///     Tagged in photo.
        /// </summary>
        [Description("tagged_in_photo")]
        TaggedInPhoto,

        /// <summary>
        ///     Published story.
        /// </summary>
        [Description("published_story")]
        PublishedStory,

        /// <summary>
        ///     App created story.
        /// </summary>
        [Description("app_created_story")]
        AppCreatedStory,

        /// <summary>
        ///     Wall post.
        /// </summary>
        [Description("wall_post")]
        WallPost,

        /// <summary>
        ///     Created event.
        /// </summary>
        [Description("created_event")]
        CreatedEvent,

        /// <summary>
        ///     Created group.
        /// </summary>
        [Description("created_group")]
        CreatedGroup,

        /// <summary>
        ///     Shared story.
        /// </summary>
        [Description("shared_story")]
        SharedStory,

        /// <summary>
        ///     Added video.
        /// </summary>
        [Description("added_video")]
        AddedVideo,

        /// <summary>
        ///     Added photos.
        /// </summary>
        [Description("added_photos")]
        AddedPhotos,

        /// <summary>
        ///     Created note.
        /// </summary>
        [Description("created_note")]
        CreatedNote,

        /// <summary>
        ///     Mobile status update.
        /// </summary>
        [Description("mobile_status_update")]
        MobileStatusUpdate
    }
}
