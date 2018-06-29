using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Models
{
    /// <summary>
    ///     The base configs of facebook.
    /// </summary>
    public class Config
    {
        /// <summary>
        ///     User access token.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        ///     Facebook page access token.
        /// </summary>
        public string PageAccessToken { get; set; }

        /// <summary>
        ///     User ID.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        ///     User's ad account ID.
        /// </summary>
        public string AdAccountId { get; set; }

        /// <summary>
        ///     The page to publish posts.
        /// </summary>
        public string PageId { get; set; }
    }
}
