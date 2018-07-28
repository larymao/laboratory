using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lary.Laboratory.Twitter.Models
{
    /// <summary>
    ///     Process status of media upload.
    /// </summary>
    internal enum ProcessState
    {
        /// <summary>
        ///     Indicates none.
        /// </summary>
        [Description("none")]
        None,

        /// <summary>
        ///     Indicates pending.
        /// </summary>
        [Description("pending")]
        Pending,

        /// <summary>
        ///     Indicates in progress.
        /// </summary>
        [Description("in_progress")]
        InProgress,

        /// <summary>
        ///     Indicates succeeded
        /// </summary>
        [Description("succeeded")]
        Succeeded,

        /// <summary>
        ///     Indicates failed.
        /// </summary>
        [Description("failed")]
        Failed
    }
}
