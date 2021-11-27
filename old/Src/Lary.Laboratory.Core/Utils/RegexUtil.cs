using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Lary.Laboratory.Core.Utils
{
    /// <summary>
    ///     Provides methods for those most used reglar expression matches.
    /// </summary>
    public static class RegexUtil
    {
        /// <summary>
        ///     Matches the first IPv4 address from the source string.
        /// </summary>
        /// <param name="source">
        ///     The text to match.
        /// </param>
        /// <returns>
        ///     An IPv4 address if succeeding in matching.
        /// </returns>
        public static string MatchIPv4Address(string source)
        {
            if (String.IsNullOrEmpty(source))
            {
                return null;
            }

            var pattern = @"((?:(?:25[0-5]\.)|(?:2[0-4]\d\.)|(?:1\d\d\.)|(?:[1-9]\d\.)|(?:\d\.)){3}(?:(?:25[0-5])|(?:2[0-4]\d)|(?:1\d\d)|(?:[1-9]\d)|(?:\d)))"; // 从255.匹配到0.三次，从255匹配到0一次
            var ipMatch = Regex.Match(source, pattern);

            if (ipMatch != null)
            {
                return ipMatch.Groups[1].Value;
            }

            return null;
        }

        /// <summary>
        ///     Checks whether the source string is an IPv4 address.
        /// </summary>
        /// <param name="source">
        ///     The text to check.
        /// </param>
        /// <returns>
        ///     True if the source string is an IPv4 address.
        /// </returns>
        public static bool IsIPv4Address(string source)
        {
            var ip = MatchIPv4Address(source);

            if (String.IsNullOrEmpty(ip) || ip != source)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Matches the first url of the source string.
        /// </summary>
        /// <param name="source">
        ///     The text to match.
        /// </param>
        /// <returns>
        ///     A url address if succeeding in matching.
        /// </returns>
        public static string MatchUrl(string source)
        {
            if (String.IsNullOrEmpty(source))
            {
                return null;
            }

            var pattern = @"((?:https?|ftp|file)://[-\w\+&@#/%\?=~\|!:,.;]+[-\w\+&@#/%=~\|])";
            var urlMatch = Regex.Match(source, pattern);

            if (urlMatch.Success)
            {
                return urlMatch.Groups[1].Value;
            }

            return null;
        }

        /// <summary>
        ///     Checks whether the source string is a valid url.
        /// </summary>
        /// <param name="source">
        ///     The text to check.
        /// </param>
        /// <returns>
        ///     True if the source string is a valid url.
        /// </returns>
        public static bool IsUrl(string source)
        {
            var url = MatchUrl(source);

            if (String.IsNullOrEmpty(url) || url != source)
            {
                return false;
            }

            return true;
        }
    }
}
