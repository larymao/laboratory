using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Core.Utils
{
    /// <summary>
    ///     Provides methods for data convert of <see cref="DateTime"/> objects.
    /// </summary>
    public static class DateTimeUtil
    {
        /// <summary>
        ///     Convert a <see cref="DateTime"/> object to time stamp of <see cref="Int64"/> format.
        /// </summary>
        /// <param name="time">
        ///     The date time to convert to time stamp.
        /// </param>
        /// <returns>
        ///     The converted time stamp in <see cref="Int64"/> format.
        /// </returns>
        public static long CountLongTimeStamp(DateTime time)
        {
            var span = CountStampTimeDifference(time);
            return (Int64)span.TotalMilliseconds;
        }

        /// <summary>
        ///     Convert a <see cref="DateTime"/> object to time stamp of <see cref="Int32"/> format.
        /// </summary>
        /// <param name="time">
        ///     The date time to convert to time stamp.
        /// </param>
        /// <returns>
        ///     The converted time stamp in <see cref="Int32"/> format.
        /// </returns>
        public static int CountTimeStamp(DateTime time)
        {
            var span = CountStampTimeDifference(time);
            return (Int32)span.TotalSeconds;
        }

        /// <summary>
        ///     Convert an <see cref="Int32"/> formatted time stamp string to a <see cref="DateTime"/>
        ///     object.
        /// </summary>
        /// <param name="timeStamp">
        ///     The <see cref="Int32"/> formatted time stamp string.
        /// </param>
        /// <returns>
        ///     The converted <see cref="DateTime"/> object.
        /// </returns>
        public static DateTime ParseTimeStamp(int timeStamp)
        {
            var dtStart = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            var lTime = timeStamp * 10_000_000L;
            var toNow = new TimeSpan(lTime);

            return dtStart.Add(toNow);
        }

        /// <summary>
        ///     Convert an <see cref="Int64"/> formatted time stamp string to a <see cref="DateTime"/>
        ///     object.
        /// </summary>
        /// <param name="timeStamp">
        ///     The <see cref="Int64"/> formatted time stamp string.
        /// </param>
        /// <returns>
        ///     The converted <see cref="DateTime"/> object.
        /// </returns>
        public static DateTime ParseTimeStamp(long timeStamp)
        {
            var dtStart = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            var lTime = timeStamp * 10_000;
            var toNow = new TimeSpan(lTime);

            return dtStart.Add(toNow);
        }


        /// <summary>
        ///     Count the time span between the specified time and 1970-01-01.
        /// </summary>
        /// <param name="time">
        ///     The specified date time to count time span.
        /// </param>
        /// <returns>
        ///     The time span between the specified time and 1970-01-01.
        /// </returns>
        private static TimeSpan CountStampTimeDifference(DateTime time)
        {
            var startTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);

            return time - startTime;
        }
    }
}
