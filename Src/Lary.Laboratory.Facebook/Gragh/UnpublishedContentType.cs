using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     Content type of the unpublished content type.
    /// </summary>
    public enum UnpublishedContentType
    {
        /// <summary>
        ///     Scheduled.
        /// </summary>
        [Description("SCHEDULED")]
        Scheduled,

        /// <summary>
        ///     Draft.
        /// </summary>
        [Description("DRAFT")]
        Draft,

        /// <summary>
        ///     Ads post.
        /// </summary>
        [Description("ADS_POST")]
        AdsPost,

        /// <summary>
        ///     Inline created.
        /// </summary>
        [Description("INLINE_CREATED")]
        InlineCreated,

        /// <summary>
        ///     Published.
        /// </summary>
        [Description("PUBLISHED")]
        Published
    }
}
