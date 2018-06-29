using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Basic.Apis
{
    /// <summary>
    ///     Provides facebook gragh apis.
    /// </summary>
    internal static class Gragh
    {
        private const string NormalHost = "graph.facebook.com";

        private const string VideoUploadHost = "graph-video.facebook.com";

        private const string DefaultApiVersion = "v3.0";


        /// <summary>
        ///     Base gragh api endpoint.
        /// </summary>
        /// <param name="targetId">
        ///     The destination id of gragh api request. The value can be 
        ///     ["page_id", "user_id", "event_id", "group_id"]
        /// </param>
        /// <param name="apiVersion">
        ///     The target api version.
        /// </param>
        /// <returns>
        ///     The url of base gragh api.
        /// </returns>
        internal static string Base(string targetId, string apiVersion = DefaultApiVersion)
        {
            return $"https://{NormalHost}/{apiVersion}/{targetId}";
        }

        /// <summary>
        ///     Feed api endpoint.
        /// </summary>
        /// <param name="targetId">
        ///     The destination id of gragh api request. The value can be 
        ///     ["page_id", "user_id", "event_id", "group_id"]
        /// </param>
        /// <param name="apiVersion">
        ///     The target api version.
        /// </param>
        /// <returns>
        ///     The url of feed api.
        /// </returns>
        internal static string Feed(string targetId, string apiVersion = DefaultApiVersion)
        {
            return $"https://{NormalHost}/{apiVersion}/{targetId}/feed";
        }

        /// <summary>
        ///     Photos api endpoint.
        /// </summary>
        /// <param name="targetId">
        ///     The destination id of gragh api request. The value can be 
        ///     ["page_id", "user_id", "event_id", "group_id"]
        /// </param>
        /// <param name="apiVersion">
        ///     The target api version.
        /// </param>
        /// <returns>
        ///     The url of photos api.
        /// </returns>
        internal static string Photos(string targetId, string apiVersion = DefaultApiVersion)
        {
            return $"https://{NormalHost}/{apiVersion}/{targetId}/photos";
        }
        
        /// <summary>
        ///     Videos api endpoint.
        /// </summary>
        /// <param name="targetId">
        ///     The destination id of gragh api request. The value can be 
        ///     ["page_id", "user_id", "event_id", "group_id"]
        /// </param>
        /// <param name="apiVersion">
        ///     The target api version.
        /// </param>
        /// <returns>
        ///     The url of videos api.
        /// </returns>
        internal static string Videos(string targetId, string apiVersion = DefaultApiVersion)
        {
            return $"https://{NormalHost}/{apiVersion}/{targetId}/videos";
        }

        /// <summary>
        ///     Video upload api endpoint.
        /// </summary>
        /// <param name="targetId">
        ///     The destination id of gragh api request. The value can be 
        ///     ["page_id", "user_id", "event_id", "group_id"]
        /// </param>
        /// <param name="apiVersion">
        ///     The target api version.
        /// </param>
        /// <returns>
        ///     The url of video upload api.
        /// </returns>
        internal static string VideoUpload(string targetId, string apiVersion = DefaultApiVersion)
        {
            return $"https://{VideoUploadHost}/{apiVersion}/{targetId}/videos";
        }
    }
}
