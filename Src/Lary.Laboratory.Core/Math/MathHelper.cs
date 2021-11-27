using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lary.Laboratory.Core.Math
{
    /// <summary>
    /// Provides methods for math using.
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        /// Rounds all fractional properties of the given object to a specified number of fractional digits.
        /// </summary>
        /// <param name="obj">The object to be rounded.</param>
        /// <param name="digits">The number of decimal places in the return value.</param>
        /// <returns>An object with all decimal properties been rounded.</returns>
        public static object? Round(object? obj, int digits = 2)
        {
            return Round(obj, FractionTypes.All, digits);
        }

        /// <summary>
        /// Rounds all fractional properties of the given object to a specified number of fractional digits.
        /// </summary>
        /// <param name="obj">The object to be rounded.</param>
        /// <param name="fractionTypes">The type of fractions that should be rounded.</param>
        /// <param name="digits">The number of decimal places in the return value.</param>
        /// <returns>An object with all decimal properties been rounded.</returns>
        public static object? Round(object? obj, FractionTypes fractionTypes, int digits = 2)
        {
            if (obj == null)
            {
                return null;
            }

            var type = obj.GetType();

            if (type == typeof(object)
                || type.FullName!.Contains("IGrouping"))
            {
                return obj;
            }

            // round straight
            if (obj is decimal m)
            {
                return fractionTypes.HasFlag(FractionTypes.Decimal)
                    ? Convert.ChangeType(System.Math.Round(m, digits), type)
                    : obj;
            }

            if (obj is double d)
            {
                return fractionTypes.HasFlag(FractionTypes.Double)
                    ? Convert.ChangeType(System.Math.Round(d, digits), type)
                    : obj;
            }

            if (obj is float f)
            {
                return fractionTypes.HasFlag(FractionTypes.Single)
                    ? Convert.ChangeType(System.Math.Round(f, digits), type)
                    : obj;
            }

            if (obj is string)
            {
                // string is derived from IEnumerable
                return obj;
            }

            // list
            if (obj is IEnumerable ie)
            {
                var list = new List<object?>();

                foreach (var x in ie)
                {
                    list.Add(Round(x, fractionTypes, digits));
                }

                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(list), type);
            }

            // complex object
            var props = type.GetProperties();

            foreach (var prop in props)
            {
                if (!prop.CanWrite)
                {
                    continue;
                }

                var currentData = prop.GetValue(obj);

                if (currentData == null)
                {
                    continue;
                }

                var fixedDigits = GetPropDigits(prop, digits);

                if (prop.PropertyType == typeof(decimal)
                    || prop.PropertyType == typeof(decimal?))
                {
                    // decimal
                    var decimalData = currentData as decimal?;

                    if (!fractionTypes.HasFlag(FractionTypes.Decimal)
                        || !decimalData.HasValue)
                    {
                        continue;
                    }

                    prop.SetValue(obj, System.Math.Round(decimalData!.Value, fixedDigits));
                }
                else if (prop.PropertyType == typeof(double)
                    || prop.PropertyType == typeof(double?))
                {
                    // double
                    var doubleData = currentData as double?;

                    if (!fractionTypes.HasFlag(FractionTypes.Double)
                        || !doubleData.HasValue)
                    {
                        continue;
                    }

                    prop.SetValue(obj, System.Math.Round(doubleData!.Value, fixedDigits));
                }
                else if (prop.PropertyType == typeof(float)
                    || prop.PropertyType == typeof(float?))
                {
                    // float
                    var floatData = currentData as float?;

                    if (!fractionTypes.HasFlag(FractionTypes.Single)
                        || !floatData.HasValue)
                    {
                        continue;
                    }

                    prop.SetValue(obj, (float)System.Math.Round(floatData!.Value, fixedDigits));
                }
                else if (!currentData.GetType().IsPrimitive
                    && currentData.GetType() != typeof(string)
                    && currentData.GetType() != type)
                {
                    currentData = Round(currentData, fractionTypes, fixedDigits);
                    prop.SetValue(obj, currentData);
                }
                else
                {
                    continue;
                }
            }

            return obj;
        }

        private static int GetPropDigits(PropertyInfo prop, int defaultDigits)
        {
            var decimalAttr = prop.GetCustomAttributes(typeof(FractionAttribute), false).SingleOrDefault();

            if (decimalAttr == null)
            {
                return defaultDigits;
            }

            var attr = decimalAttr as FractionAttribute;

            return attr!.Digits;
        }
    }
}
