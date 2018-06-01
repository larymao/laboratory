using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
