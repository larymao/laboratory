using Lary.Laboratory.Facebook.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     <para/>Facebook gragh api photo node. Represents a photo album.
    ///     <para/>For Albums on Users and Pages
    ///     <para/>Any valid access token if the album is public.
    ///     <para/>A user access token with user_photos permission to retrieve any albums that the session user has uploaded.
    ///     <para/>For Albums on Groups
    ///     <para/>A User access token of an Admin of the Group with the user_photos permission.
    /// </summary>
    public partial class Album
    {
        /// <summary>
        ///     The album ID.
        /// </summary>
        [FacebookProperty("id")]
        public string Id { get; set; }

        /// <summary>
        ///     Whether the viewer can upload photos to this album.
        /// </summary>
        [FacebookProperty("can_upload")]
        public bool? CanUpload { get; set; }

        /// <summary>
        ///     The approximate number of photos in the album. This is not necessarily an exact count.
        /// </summary>
        [FacebookProperty("count")]
        public int? Count { get; set; }

        /// <summary>
        ///     The ID of the album's cover photo.
        /// </summary>
        [FacebookProperty("cover_photo")]
        public string CoverPhoto { get; set; }

        /// <summary>
        ///     The time the album was initially created.
        /// </summary>
        [FacebookProperty("created_time")]
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        ///     The description of the album.
        /// </summary>
        [FacebookProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     The event associated with this album.
        /// </summary>
        [FacebookProperty("event")]
        public object Event { get; set; }

        /// <summary>
        ///     The current user, if the current user created the album.
        /// </summary>
        [FacebookProperty("from")]
        public object From { get; set; }

        /// <summary>
        ///     A link to this album on Facebook.
        /// </summary>
        [FacebookProperty("link")]
        public string Link { get; set; }

        /// <summary>
        ///     The textual location of the album.
        /// </summary>
        [FacebookProperty("location")]
        public string Location { get; set; }

        /// <summary>
        ///     The title of the album.
        /// </summary>
        [FacebookProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     The place associated with this album.
        /// </summary>
        [FacebookProperty("place")]
        public object Place { get; set; }

        /// <summary>
        ///     The privacy settings for the album.
        /// </summary>
        [FacebookProperty("privacy")]
        public string Privacy { get; set; }

        /// <summary>
        ///     The type of the album.
        /// </summary>
        [FacebookProperty("type")]
        public AlbumType? Type { get; set; }

        /// <summary>
        ///     The last time the album was updated.
        /// </summary>
        [FacebookProperty("updated_time")]
        public DateTime? updated_time { get; set; }
    }
}
