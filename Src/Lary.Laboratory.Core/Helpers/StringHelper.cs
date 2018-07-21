using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Lary.Laboratory.Core.Helpers
{
    /// <summary>
    ///     Provides methods for facilitating the use of <see cref="string"/>.
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        ///     Join multiple lines into single line.
        /// </summary>
        /// <param name="source">
        ///     The <see cref="string"/> to process.
        /// </param>
        /// <returns>
        ///     A single line <see cref="string"/> joined from the source.
        /// </returns>
        public static string JoinLines(this string source)
        {
            var sb = new StringBuilder();
            var lines = source.Replace("\r\n", "\n").Split('\n');

            foreach (var line in lines)
            {
                sb.Append(line.Trim()).Append(" ");
            }

            return sb.ToString().TrimEnd();
        }

        /// <summary>
        ///     Indicates whether the specified pattern finds a match in the specified src string.
        /// </summary>
        /// <param name="src">
        ///     The string to search for a match.
        /// </param>
        /// <param name="pattern">
        ///     The pattern to match.
        /// </param>
        /// <returns>
        ///     True if the pattern finds a match; otherwise, false.
        /// </returns>
        public static bool Likes(this string src, string pattern)
        {
            bool isMatch = false;

            if (pattern.StartsWith(@"\%"))
            {
                if (pattern.EndsWith(@"\%"))
                {
                    // \%{content}\%
                    isMatch = src == $"{pattern.Substring(1, pattern.Length - 3)}%";
                }
                else if (pattern.EndsWith("%"))
                {
                    // \%{content}%
                    isMatch = src.StartsWith(pattern.Substring(1, pattern.Length - 2).ToString());
                }
                else
                {
                    // \%{content}
                    isMatch = src == pattern.Substring(1, pattern.Length - 1).ToString();
                }
            }
            else if (pattern.StartsWith("%"))
            {
                if (pattern.EndsWith(@"\%"))
                {
                    // %{content}\%
                    isMatch = src.EndsWith($"{pattern.Substring(1, pattern.Length - 3).ToString()}%");
                }
                else if (pattern.EndsWith("%"))
                {
                    // %{content}%
                    isMatch = src.Contains(pattern.Substring(1, pattern.Length - 2).ToString());
                }
                else
                {
                    // %{content}
                    isMatch = src.EndsWith(pattern.Substring(1, pattern.Length - 1).ToString());
                }
            }
            else
            {
                if (pattern.EndsWith(@"\%"))
                {
                    // {content}\%
                    isMatch = src == $"{pattern.Remove(pattern.Length - 2).ToString()}%";
                }
                else if (pattern.EndsWith("%"))
                {
                    // {content}%
                    isMatch = src.StartsWith(pattern.Remove(pattern.Length - 1).ToString());
                }
                else
                {
                    // {content}
                    isMatch = src == pattern;
                }
            }

            return isMatch;
        }

        /// <summary>
        ///     Checks if a string is in valid JSON format.
        /// </summary>
        /// <param name="str">
        ///     The string to check.
        /// </param>
        /// <returns>
        ///     True if str is in valid JSON format; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Throw if the parameter str is null.
        /// </exception>
        public static bool IsValidJson(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(str);
            }

            str = str.Trim();

            if ((str.StartsWith("{") && str.EndsWith("}")) // For object
                || (str.StartsWith("[") && str.EndsWith("]")) // For array
               )
            {
                try
                {
                    var obj = JToken.Parse(str);
                    return true;
                }
                catch (JsonReaderException)
                {
                    // Exception in parsing json
                    return false;
                }
                catch (Exception)
                {
                    // some other exception
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///     Checks if a string is in valid XML format.
        /// </summary>
        /// <param name="str">
        ///     The string to check.
        /// </param>
        /// <returns>
        ///     True if str is in valid XML format; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Throw if the parameter str is null.
        /// </exception>
        public static bool IsValidXml(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(str);
            }

            str = str.Trim();

            if (str.StartsWith("<"))
            {
                try
                {
                    var doc = XDocument.Parse(str);
                    return true;
    }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
