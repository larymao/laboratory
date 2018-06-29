using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Models
{
    /// <summary>
    ///     Facebook ad post.
    /// </summary>
    public partial class AdPost
    {
        /// <summary>
        ///     The text message of ad post.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Call to action link address.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        ///     The picture link address of ad post.
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        ///     The thumbnail of ad post video.
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        ///     The video id of ad post.
        /// </summary>
        public string Video { get; set; }

        /// <summary>
        ///     The user access token.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        ///     The page access token.
        /// </summary>
        public string PageAccessToken { get; set; }

        /// <summary>
        ///     The page to publish ad post.
        /// </summary>
        public string PageId { get; set; }

        /// <summary>
        ///     The <see cref="AdAttachments"/> of ad post.
        /// </summary>
        public AdAttachments Attachments { get; set; }
    }
}
