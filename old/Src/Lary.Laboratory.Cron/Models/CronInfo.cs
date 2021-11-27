using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Cron.Models
{
    /// <summary>
    ///     The information of cron expression.
    /// </summary>
    public class CronInfo
    {
        /// <summary>
        ///     <see cref="Minutes"/> values can be from 0 to 59.
        /// </summary>
        public ushort[] Minutes { get; set; }

        /// <summary>
        ///     <see cref="Hours"/> values can be from 0 to 23.
        /// </summary>
        public ushort[] Hours { get; set; }

        /// <summary>
        ///     <see cref="DaysOfMonth"/>  values can be from 1 to 31.
        /// </summary>
        public ushort[] DaysOfMonth { get; set; }

        /// <summary>
        ///     <see cref="Months"/> values can be from 1 to 12.
        /// </summary>
        public ushort[] Months { get; set; }

        /// <summary>
        ///     <see cref="DaysOfWeek"/> values can be from 0 to 6, with 0 denoting Sunday.
        /// </summary>
        public ushort[] DaysOfWeek { get; set; }

        /// <summary>
        ///     The cron expression of minute.
        /// </summary>
        public string ExprMinute { get; set; }

        /// <summary>
        ///     The cron expression of hour.
        /// </summary>
        public string ExprHour { get; set; }

        /// <summary>
        ///     The cron expression of day_of_month.
        /// </summary>
        public string ExprDayOfMonth { get; set; }

        /// <summary>
        ///     The cron expression of month.
        /// </summary>
        public string ExprMonth { get; set; }

        /// <summary>
        ///     The cron expression of day_of_week.
        /// </summary>
        public string ExprDayOfWeek { get; set; }
    }
}
