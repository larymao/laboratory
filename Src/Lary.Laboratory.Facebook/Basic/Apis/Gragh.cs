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
        ///     Basic host of facebook gragh api.
        /// </summary>
        internal const string BasicHost = "graph.facebook.com";

        /// <summary>
        ///     The latest version of facebook gragh api.
        /// </summary>
        internal const string LatestVersion = "v3.0";


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
    }
}
