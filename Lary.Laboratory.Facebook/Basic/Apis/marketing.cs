using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Basic.Apis
{
    /// <summary>
    ///     Provides facebook marketing apis.
    /// </summary>
    internal static class marketing
    {
        private const string NormalHost = "graph.facebook.com";

        private const string VideoUploadHost = "graph-video.facebook.com";

        private const string DefaultApiVersion = "v3.0";


        /// <summary>
        ///     Ad video upload api endpoint.
        /// </summary>
        /// <param name="adAccountId">
        ///     The Ad account id of user.
        /// </param>
        /// <param name="apiVersion">
        ///     The target api version.
        /// </param>
        /// <returns>
        ///     The url of Ad video upload api.
        /// </returns>
        public static string AdVideoUpload(string adAccountId, string apiVersion = DefaultApiVersion)
        {
            return $"https://{VideoUploadHost}/{apiVersion}/act_{adAccountId}/advideos";
        }

        /// <summary>
        ///     Ad images api endpoint.
        /// </summary>
        /// <param name="adAccountId">
        ///     The Ad account id of user.
        /// </param>
        /// <param name="apiVersion">
        ///     The target api version.
        /// </param>
        /// <returns>
        ///     The url of Ad images api.
        /// </returns>
        public static string AdImages(string adAccountId, string apiVersion = DefaultApiVersion)
        {
            return $"https://{NormalHost}/{apiVersion}/act_{adAccountId}/adimages";
        }

        /// <summary>
        ///     Ad video upload api endpoint.
        /// </summary>
        /// <param name="videoId">
        ///     The video to gets/sets thumbnails.
        /// </param>
        /// <param name="apiVersion">
        ///     The target api version.
        /// </param>
        /// <returns>
        ///     The url of thumbnails api.
        /// </returns>
        public static string Thumbnails(string videoId, string apiVersion = DefaultApiVersion)
        {
            return $"https://{NormalHost}/{apiVersion}/{videoId}/thumbnails";
        }

        /// <summary>
        ///     Ad creative api endpoint.
        /// </summary>
        /// <param name="adAccountId">
        ///     The Ad account id of user.
        /// </param>
        /// <param name="apiVersion">
        ///     The target api version.
        /// </param>
        /// <returns>
        ///     The url of Ad creative api.
        /// </returns>
        public static string AdCreatives(string adAccountId, string apiVersion = DefaultApiVersion)
        {
            return $"https://{NormalHost}/{apiVersion}/act_{adAccountId}/adcreatives";
        }
    }
}
