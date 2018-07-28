using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Twitter.Basic
{
    /// <summary>
    ///     Twitter apis.
    /// </summary>
    internal static class Apis
    {
        /// <summary>
        ///     Regular host of twitter api.
        /// </summary>
        public const string UploadHost = "upload.twitter.com";

        /// <summary>
        ///     The latest version of twitter api.
        /// </summary>
        public const string LatestVersion = "1.1";


        /// <summary>
        ///     Get the twitter uploading api.
        /// </summary>
        /// <param name="apiVersion">
        ///     The version of api.
        /// </param>
        /// <returns>
        ///     The twitter uploading api.
        /// </returns>
        public static string MediaUploading(string apiVersion = LatestVersion)
        {
            return $"https://{UploadHost}/{apiVersion}/media/upload.json";
        }
    }
}
