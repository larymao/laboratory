using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Basic.Apis
{
    internal static class Marketing
    {
        /// <summary>
        ///     Regular host of facebook gragh api, also popular among marketing apis.
        /// </summary>
        internal const string GraghHost = "graph.facebook.com";

        /// <summary>
        ///     The latest version of facebook marketing api.
        /// </summary>
        internal const string LatestVersion = "v3.0";


        /// <summary>
        ///     Get the facebook ad creative api.
        /// </summary>
        /// <param name="adAccountId">
        ///     The ad account id of user.
        /// </param>
        /// <param name="apiVersion">
        ///     The version of api.
        /// </param>
        /// <returns>
        ///     Facebook ad creative api.
        /// </returns>
        internal static string AdCreative(string adAccountId, string apiVersion = LatestVersion)
        {
            return $"https://{GraghHost}/{apiVersion}/act_{adAccountId}/adcreatives";
        }

        /// <summary>
        ///     Get the facebook ad creative information api.
        /// </summary>
        /// <param name="creativeId">
        ///     The id of ad creative.
        /// </param>
        /// <param name="apiVersion">
        ///     The version of api.
        /// </param>
        /// <returns>
        ///     Facebook ad creative information api.
        /// </returns>
        internal static string AdCreativeInfo(string creativeId, string apiVersion = LatestVersion)
        {
            return $"https://{GraghHost}/{apiVersion}/{creativeId}";
        }

        /// <summary>
        ///     Get the facebook ad creative publishing api.
        /// </summary>
        /// <param name="storyId">
        ///     A string object of effective_object_story_id.
        /// </param>
        /// <param name="apiVersion">
        ///     The version of api.
        /// </param>
        /// <returns>
        ///     Facebook ad creative publishing api.
        /// </returns>
        /// <remarks>
        ///     This is a hidden api.
        /// </remarks>
        internal static string AdCreativePublishing(string storyId, string apiVersion = LatestVersion)
        {
            return $"https://{GraghHost}/{apiVersion}/{storyId}";
        }
    }
}
