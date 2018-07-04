using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     The status of the live video.
    /// </summary>
    public enum LiveVideoStatus
    {
        /// <summary>
        ///     Schedule canceled.
        /// </summary>
        [Description("SCHEDULED_CANCELED")]
        ScheduleCanceled,

        /// <summary>
        ///     Scheduled live.
        /// </summary>
        [Description("SCHEDULED_LIVE")]
        ScheduledLive,

        /// <summary>
        ///     Scheduled unpublished.
        /// </summary>
        [Description("SCHEDULED_UNPUBLISHED")]
        ScheduledUnpublished,

        /// <summary>
        ///     Live now.
        /// </summary>
        [Description("LIVE_NOW")]
        LiveNow,

        /// <summary>
        ///     Unpublished.
        /// </summary>
        [Description("UNPUBLISHED")]
        Unpublished
    }
}
