using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Models
{
    /// <summary>
    ///     Facebook picture post.
    /// </summary>
    public partial class ImagePost
    {
        /// <summary>
        ///     The text message of post.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     The link address the page post links to.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        ///     The picture link address of post.
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        ///     The schedule publish time of post.
        /// </summary>
        public DateTime ScheduledTime { get; set; }
    }
}
