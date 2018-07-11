using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Lary.Laboratory.Core.Helpers
{
    /// <summary>
    ///     Provides methods for facilitating the use of <see cref="Uri"/>.
    /// </summary>
    public static class UriHelper
    {
        /// <summary>
        ///     Appends a query to a specified uri. If the target query already exists, it is overwritten.
        /// </summary>
        /// <param name="uri">
        ///     The target uri.
        /// </param>
        /// <param name="query">
        ///     The query to append.
        /// </param>
        /// <returns>
        ///     An <see cref="Uri"/> object.
        /// </returns>
        public static Uri AppendQuery(this Uri uri, KeyValuePair<string, string> query)
        {
            return uri.AppendQueries(new List<KeyValuePair<string, string>>
            {
                query
            });
        }

        /// <summary>
        ///     Appends queries to a specified uri. If the target query already exists, it is overwritten.
        /// </summary>
        /// <param name="uri">
        ///     The target uri.
        /// </param>
        /// <param name="queries">
        ///     The queries to append.
        /// </param>
        /// <returns>
        ///     An <see cref="Uri"/> object.
        /// </returns>
        public static Uri AppendQueries(this Uri uri, IEnumerable<KeyValuePair<string, string>> queries)
        {
            if (queries == null)
            {
                return uri;
            }

            var uriBuilder = new UriBuilder(uri);
            var queryCollection = HttpUtility.ParseQueryString(uriBuilder.Query);

            foreach (var query in queries)
            {
                queryCollection[query.Key] = query.Value;
            }

            uriBuilder.Query = queryCollection.ToString();
            return uriBuilder.Uri;
        }
    }
}
