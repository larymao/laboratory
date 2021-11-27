using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Basic.Apis
{
    /// <summary>
    ///     Facebook gragh api endpoints.
    /// </summary>
    internal static class Gragh
    {
        /// <summary>
        ///     Regular host of facebook gragh api.
        /// </summary>
        internal const string BasicHost = "graph.facebook.com";

        /// <summary>
        ///     Video uploading host of facebook gragh api.
        /// </summary>
        internal const string VideoUploadingHost = "graph-video.facebook.com";

        /// <summary>
        ///     The latest version of facebook gragh api.
        /// </summary>
        internal const string LatestVersion = "v3.0";


        /// <summary>
        ///     Get the facebook ad photo uploading api.
        /// </summary>
        /// <param name="adAccountId">
        ///     The ad account id of user.
        /// </param>
        /// <param name="apiVersion">
        ///     The version of api.
        /// </param>
        /// <returns>
        ///     Facebook ad photo uploading api.
        /// </returns>
        internal static string AdPhotoUploading(string adAccountId, string apiVersion = LatestVersion)
        {
            return $"https://{BasicHost}/{apiVersion}/act_{adAccountId}/adimages";
        }

        /// <summary>
        ///     Get the facebook ad video uploading api.
        /// </summary>
        /// <param name="adAccountId">
        ///     The ad account id of user.
        /// </param>
        /// <param name="apiVersion">
        ///     The version of api.
        /// </param>
        /// <returns>
        ///     Facebook ad video uploading api.
        /// </returns>
        internal static string AdVideoUploading(string adAccountId, string apiVersion = LatestVersion)
        {
            return $"https://{VideoUploadingHost}/{apiVersion}/act_{adAccountId}/advideos";
        }

        /// <summary>
        ///     The base edge of facebook gragh api.
        /// </summary>
        /// <param name="target">
        ///     Target endpoint.
        /// </param>
        /// <param name="apiVersion">
        ///     The version of api.
        /// </param>
        /// <returns>
        ///     Facebook base gragh api.
        /// </returns>
        internal static string Base(string target, string apiVersion = LatestVersion)
        {
            return $"https://{BasicHost}/{apiVersion}/{target}";
        }

        /// <summary>
        ///     Returns api for querying the comments of a post by its id.
        /// </summary>
        /// <param name="postId">
        ///     The id of a post to query.
        /// </param>
        /// <param name="apiVersion">
        ///     The version of api.
        /// </param>
        /// <returns>
        ///     Api for querying the comments of a post by its id.
        /// </returns>
        internal static string Comments(string postId, string apiVersion = LatestVersion)
        {
            return $"https://{BasicHost}/{apiVersion}/{postId}/comments";
        }

        /// <summary>
        ///     Returns api for posting a like to a post by its id.
        /// </summary>
        /// <param name="postId">
        ///     The id of a post to like.
        /// </param>
        /// <param name="apiVersion">
        ///     The version of api.
        /// </param>
        /// <returns>
        ///     Api for posting a like to a post by its id.
        /// </returns>
        internal static string Likes(string postId, string apiVersion = LatestVersion)
        {
            return $"https://{BasicHost}/{apiVersion}/{postId}/likes";
        }

        /// <summary>
        ///     <para/>When posting to this edge, a Photo will be created.
        ///     <para/>Note: the post_id value is not returned for photos added to Albums.
        /// </summary>
        /// <param name="targetId">
        ///     The target of photo creating. Can be a value of {page_id, user_id, album_id, event_id, group_id, official_event_id}
        /// </param>
        /// <param name="apiVersion">
        ///     The version of api.
        /// </param>
        /// <returns>
        ///     Facebook photo creating api.
        /// </returns>
        internal static string PhotoCreating(string targetId, string apiVersion = LatestVersion)
        {
            return $"https://{BasicHost}/{apiVersion}/{targetId}/photos";
        }

        /// <summary>
        ///     Get the facebook post publishing api.
        /// </summary>
        /// <param name="targetId">
        ///     The target of post publishing.
        /// </param>
        /// <param name="apiVersion">
        ///     The version of api.
        /// </param>
        /// <returns>
        ///     Facebook post publishing api.
        /// </returns>
        internal static string PostPublishing(string targetId, string apiVersion = LatestVersion)
        {
            return $"https://{BasicHost}/{apiVersion}/{targetId}/feed";
        }

        /// <summary>
        ///     Returns api for getting the reactions of a post by its id.
        /// </summary>
        /// <param name="postId">
        ///     The id of a post to query.
        /// </param>
        /// <param name="apiVersion">
        ///     The version of api.
        /// </param>
        /// <returns>
        ///     Api for getting the reactions of a post by its id.
        /// </returns>
        internal static string Reactions(string postId, string apiVersion = LatestVersion)
        {
            return $"https://{BasicHost}/{apiVersion}/{postId}/reactions";
        }

        /// <summary>
        ///     Get the facebook video publishing api.
        /// </summary>
        /// <param name="targetId">
        ///     The target of video publishing. Can be a value of {user_id, event_id, page_id, group_id}
        /// </param>
        /// <param name="apiVersion">
        ///     The version of api.
        /// </param>
        /// <returns>
        ///     Facebook video publishing api.
        /// </returns>
        internal static string VideoPublishing(string targetId, string apiVersion = LatestVersion)
        {
            return $"https://{BasicHost}/{apiVersion}/{targetId}/videos";
        }

        /// <summary>
        ///     Get the facebook video uploading api.
        /// </summary>
        /// <param name="targetId">
        ///     The target of video uploading. Can be a value of {user_id, event_id, page_id, group_id}
        /// </param>
        /// <param name="apiVersion">
        ///     The version of api.
        /// </param>
        /// <returns>
        ///     Facebook video uploading api.
        /// </returns>
        internal static string VideoUploading(string targetId, string apiVersion = LatestVersion)
        {
            return $"https://{VideoUploadingHost}/{apiVersion}/{targetId}/videos";
        }

        /// <summary>
        ///     Facebook gragh api, to get thumbnails of video by its id.
        /// </summary>
        /// <param name="videoId">
        ///     The video to get thumbnails.
        /// </param>
        /// <param name="apiVersion">
        ///     The version of api.
        /// </param>
        /// <returns>
        ///     Facebook video thumbnails api.
        /// </returns>
        internal static string VideoThumbnails(string videoId, string apiVersion = LatestVersion)
        {
            return $"https://{BasicHost}/{apiVersion}/{videoId}/thumbnails";
        }
    }
}
