using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     Indicates the object type of post.
    /// </summary>
    public enum PostType
    {
        /// <summary>
        ///     Offer.
        /// </summary>
        [Description("offer")]
        Offer,

        /// <summary>
        ///     Video.
        /// </summary>
        [Description("video")]
        Video,

        /// <summary>
        ///     Photo.
        /// </summary>
        [Description("photo")]
        Photo,

        /// <summary>
        ///     Status.
        /// </summary>
        [Description("status")]
        Status,

        /// <summary>
        ///     Link.
        /// </summary>
        [Description("link")]
        Link
    }
}
