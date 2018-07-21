using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lary.Laboratory.Cron.Models
{
    /// <summary>
    ///     Specifies the day of the week.
    /// </summary>
    internal enum DayOfWeek
    {
        /// <summary>
        ///     Indicates Sunday.
        /// </summary>
        [Description("Sunday")]
        SUN = 0,

        /// <summary>
        ///     Indicates Monday.
        /// </summary>
        [Description("Monday")]
        MON = 1,

        /// <summary>
        ///     Indicates Tuesday.
        /// </summary>
        [Description("Tuesday")]
        TUE = 2,

        /// <summary>
        ///     Indicates Wednesday.
        /// </summary>
        [Description("Wednesday")]
        WED = 3,

        /// <summary>
        ///     Indicates Thursday.
        /// </summary>
        [Description("Thursday")]
        THU = 4,

        /// <summary>
        ///     Indicates Friday.
        /// </summary>
        [Description("Friday")]
        FRI = 5,

        /// <summary>
        ///     Indicates Saturday.
        /// </summary>
        [Description("Saturday")]
        SAT = 6
    }
}
