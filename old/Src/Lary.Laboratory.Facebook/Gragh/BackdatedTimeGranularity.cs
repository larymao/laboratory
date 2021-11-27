using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     Use only the part of the backdated_time parameter to the specified granularity.
    /// </summary>
    public enum BackdatedTimeGranularity
    {
        /// <summary>
        ///     None.
        /// </summary>
        [Description("none")]
        None,

        /// <summary>
        ///     Minute.
        /// </summary>
        [Description("min")]
        Minute,

        /// <summary>
        ///     Hour.
        /// </summary>
        [Description("hour")]
        Hour,

        /// <summary>
        ///     Month.
        /// </summary>
        [Description("month")]
        Month,

        /// <summary>
        ///     Year.
        /// </summary>
        [Description("year")]
        Year
    }
}
