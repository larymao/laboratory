using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lary.Laboratory.Cron.Models
{
    /// <summary>
    ///     Specifies the month.
    /// </summary>
    internal enum Month
    {
        /// <summary>
        ///     Indicates January.
        /// </summary>
        [Description("January")]
        JAN = 1,

        /// <summary>
        ///     Indicates February.
        /// </summary>
        [Description("February")]
        FEB = 2,

        /// <summary>
        ///     Indicates March.
        /// </summary>
        [Description("March")]
        MAR = 3,

        /// <summary>
        ///     Indicates April.
        /// </summary>
        [Description("April")]
        APR = 4,

        /// <summary>
        ///     Indicates May.
        /// </summary>
        [Description("May")]
        MAY = 5,

        /// <summary>
        ///     Indicates June.
        /// </summary>
        [Description("June")]
        JUN = 6,

        /// <summary>
        ///     Indicates July.
        /// </summary>
        [Description("July")]
        JUL = 7,

        /// <summary>
        ///     Indicates August.
        /// </summary>
        [Description("August")]
        AUG = 8,

        /// <summary>
        ///     Indicates September.
        /// </summary>
        [Description("September")]
        SEP = 9,

        /// <summary>
        ///     Indicates October.
        /// </summary>
        [Description("October")]
        OCT = 10,

        /// <summary>
        ///     Indicates November.
        /// </summary>
        [Description("November")]
        NOV = 11,

        /// <summary>
        ///     Indicates December.
        /// </summary>
        [Description("December")]
        DEC = 12
    }
}
