using Lary.Laboratory.Core.Helpers;
using Lary.Laboratory.Cron.Exceptions;
using Lary.Laboratory.Cron.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Lary.Laboratory.Cron
{
    /// <summary>
    ///     The interpreter of cron expression.
    /// </summary>
    public static class CronInterpreter
    {
        private const string CharacterCheckPattern = @"^[ \*/\-,\d\w]+$"; // If failed to match this pattern with regular expression, the cron expression is illegal.

        private const char AnyValueOpr = '*';
        private const char SteppingOpr = '/';
        private const char RangeOpr = '-';
        private const char ListOpr = ',';

        private static readonly ushort[] MinuteRange = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59 };
        private static readonly ushort[] HourRange = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
        private static readonly ushort[] DayOfMonthRange = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };
        private static readonly ushort[] MonthRange = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        private static readonly ushort[] DayOfWeekRange = { 0, 1, 2, 3, 4, 5, 6 };

        /// <summary>
        ///     Converts the string representation of a cron expression to its <see cref="Cron"/> object equivalent.
        /// </summary>
        /// <param name="cronExpr">
        ///     A string that represents the cron expression to convert.
        /// </param>
        /// <returns>
        ///     An object of <see cref="Cron"/>.
        /// </returns>
        /// <exception cref="CronParserException">
        ///     Throw if failed to parse the cron expression.
        /// </exception>
        public static CronInfo Parse(string cronExpr)
        {
            if (String.IsNullOrWhiteSpace(cronExpr) && !Regex.IsMatch(cronExpr, CharacterCheckPattern))
            {
                throw new CronParserException($"Illegal cron expression: {cronExpr}.");
            }

            var constraints = cronExpr.Trim().Split(' ');

            if (constraints.Length != 5)
            {
                throw new CronParserException($"Illegal cron expression: {cronExpr}.");
            }

            var cron = new CronInfo
            {
                ExprMinute = constraints[0],
                ExprHour = constraints[1],
                ExprDayOfMonth = constraints[2],
                ExprMonth = constraints[3],
                ExprDayOfWeek = constraints[4]
            };

            cron.Minutes = ParseSingleNode(cron.ExprMinute, MinuteRange);
            cron.Hours = ParseSingleNode(cron.ExprHour, HourRange);
            cron.DaysOfMonth = ParseSingleNode(cron.ExprDayOfMonth, DayOfMonthRange);
            cron.Months = ParseSingleNode<Month>(cron.ExprMonth, MonthRange);
            cron.DaysOfWeek = ParseSingleNode<Models.DayOfWeek>(cron.ExprDayOfWeek, DayOfWeekRange);

            return cron;
        }

        /// <summary>
        ///     Converts the string representation of a cron node expression to its <see cref="ushort"/> array 
        ///     equivalent according to a special range limit.
        /// </summary>
        /// <param name="nodeExpr">
        ///     A string that represents the cron node expression to convert.
        /// </param>
        /// <param name="range">
        ///     An <see cref="ushort"/> array that indicates the range of current cron node.
        /// </param>
        /// <returns>
        ///     An <see cref="ushort"/> array.
        /// </returns>
        /// <exception cref="CronParserException">
        ///     Throw if failed to parse the cron node expression.
        /// </exception>
        private static ushort[] ParseSingleNode(string nodeExpr, ushort[] range)
        {
            return ParseSingleNode<ushort>(nodeExpr, range);
        }

        /// <summary>
        ///     Converts the string representation of a cron node expression to its <see cref="ushort"/> array 
        ///     equivalent according to a special range limit.
        /// </summary>
        /// <typeparam name="T">
        ///     Allowed template type during convertion. Can be a type of <see cref="ushort"/>, 
        ///     <see cref="Models.DayOfWeek"/> or <see cref="Month"/>.
        /// </typeparam>
        /// <param name="nodeExpr">
        ///     A string that represents the cron node expression to convert.
        /// </param>
        /// <param name="range">
        ///     An <see cref="ushort"/> array that indicates the range of current cron node.
        /// </param>
        /// <returns>
        ///     An <see cref="ushort"/> array.
        /// </returns>
        /// <exception cref="CronParserException">
        ///     Throw if failed to parse the cron node expression.
        /// </exception>
        private static ushort[] ParseSingleNode<T>(string nodeExpr, ushort[] range)
        {
            /* Splits by '/', only one '/' is allowed in a single node. For one '/' can archive the goal, 
             * but two or more '/' may cause the total step out of range.
             * */
            var steppingSplits = nodeExpr.Split(SteppingOpr);

            if (steppingSplits.Length > 2)
            {
                throw new CronParserException($"Illegal cron expression {nodeExpr}. More than one '{SteppingOpr}' was detected.");
            }
            else if (steppingSplits.Length == 2)
            {
                if (steppingSplits[1].Contains("*"))
                {
                    throw new CronParserException($"Illegal cron expression {nodeExpr}. '{AnyValueOpr}' was detected in stepping part.");
                }

                var result = new List<ushort>();

                var values = ParseSingleNodeWithoutStepping<T>(steppingSplits[0], range);
                var steppings = ParseSingleNodeWithoutStepping<T>(steppingSplits[1], range);

                for (int i = 0; i < values.Length; i++)
                {
                    foreach (var stepping in steppings)
                    {
                        if (stepping != 0
                            && i % stepping == 0)
                        {
                            result.Add(values[i]);

                            break;
                        }
                    }
                }

                return result.ToArray();
            }
            else
            {
                return ParseSingleNodeWithoutStepping<T>(steppingSplits[0], range);
            }
        }

        /// <summary>
        ///     Converts the string representation of a cron node expression without 
        ///     <see cref="CronInterpreter.SteppingOpr"/> to its <see cref="ushort"/> array equivalent according to 
        ///     a special range limit.
        /// </summary>
        /// <typeparam name="T">
        ///     Allowed template type during convertion. Can be a type of <see cref="ushort"/>, 
        ///     <see cref="Models.DayOfWeek"/> or <see cref="Month"/>.
        /// </typeparam>
        /// <param name="nodeExpr">
        ///     A string that represents the cron node expression to convert.
        /// </param>
        /// <param name="range">
        ///     An <see cref="ushort"/> array that indicates the range of current cron node.
        /// </param>
        /// <returns>
        ///     An <see cref="ushort"/> array.
        /// </returns>
        /// <exception cref="CronParserException">
        ///     Throw if failed to parse the cron node expression.
        /// </exception>
        private static ushort[] ParseSingleNodeWithoutStepping<T>(string nodeExpr, ushort[] range)
        {
            var result = new List<ushort>();

            // Splits by ','.
            var listSplits = nodeExpr.Split(ListOpr);

            if (listSplits.Any(aa => aa == AnyValueOpr.ToString()))
            {
                return range;
            }

            foreach (var item in listSplits)
            {
                // Splits by '-', only one '-' is allowed in a range expression.
                var rangeSplits = item.Split(RangeOpr);

                if (rangeSplits.Length > 2)
                {
                    throw new CronParserException($"Illegal cron expression {nodeExpr}. More than one '{RangeOpr}' was detected.");
                }
                else if (rangeSplits.Length == 2)
                {
                    if (TryParseSingleValue<T>(rangeSplits[0], out ushort rangeStart)
                        && TryParseSingleValue<T>(rangeSplits[1], out ushort rangeEnd)
                        && rangeEnd >= rangeStart)
                    {
                        for (ushort i = rangeStart; i <= rangeEnd; i++)
                        {
                            if (range.Contains(i))
                            {
                                if (!result.Contains(i))
                                {
                                    result.Add(i);
                                }
                            }
                            else
                            {
                                throw new CronParserException($"Illegal cron expression {nodeExpr}. Value {i} out of range.");
                            }
                        }
                    }
                    else
                    {
                        throw new CronParserException($"Illegal cron expression {nodeExpr}.");
                    }
                }
                else
                {
                    if (TryParseSingleValue<T>(rangeSplits[0], out ushort value))
                    {
                        if (range.Contains(value))
                        {
                            if (!result.Contains(value))
                            {
                                result.Add(value);
                            }
                        }
                        else
                        {
                            throw new CronParserException($"Illegal cron expression {nodeExpr}. Value {value} out of range.");
                        }
                    }
                    else
                    {
                        throw new CronParserException($"Illegal cron expression {nodeExpr}.");
                    }
                }
            }

            result.Sort();
            return result.ToArray();
        }

        /// <summary>
        ///     Tries to convert the string representation of a number to its 16-bit unsigned integer equivalent. 
        ///     A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <typeparam name="T">
        ///     Allowed template type during convertion. Can be a type of <see cref="ushort"/>, 
        ///     <see cref="Models.DayOfWeek"/> or <see cref="Month"/>.
        /// </typeparam>
        /// <param name="s">
        ///     A string that represents the value to convert.
        /// </param>
        /// <param name="result">
        ///     When this method returns, contains the 16-bit unsigned integer value that is equivalent to the numeric 
        ///     meaning of s, if the conversion succeeded, or zero if the conversion failed. 
        /// </param>
        /// <returns>
        ///     True if s was converted successfully; otherwise, false.
        /// </returns>
        private static bool TryParseSingleValue<T>(string s, out ushort result)
        {
            var success = false;
            result = 0;

            try
            {
                var type = typeof(T);

                if (type == typeof(ushort))
                {
                    result = UInt16.Parse(s);
                    success = true;
                }
                else if (type == typeof(Models.DayOfWeek))
                {
                    if (EnumHelper.TryMatch(s, true, out Models.DayOfWeek dayOfWeek))
                    {
                        result = (ushort)dayOfWeek;
                        success = true;
                    }
                }
                else if (type == typeof(Month))
                {
                    if (EnumHelper.TryMatch(s, true, out Month month))
                    {
                        result = (ushort)month;
                        success = true;
                    }
                }
            }
            catch
            {
            }

            return success;
        }
    }
}
