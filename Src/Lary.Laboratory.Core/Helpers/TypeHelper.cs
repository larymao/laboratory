using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Core.Helpers
{
    /// <summary>
    ///     Provides methods for facilitating the use of <see cref="Type"/>.
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        ///     Checks whether the current <see cref="Type"/> is a simple type.
        /// </summary>
        /// <param name="type">
        ///     The type to check.
        /// </param>
        /// <returns>
        ///     True if the current <see cref="Type"/> is a simple type.
        /// </returns>
        public static bool IsSimple(this Type type)
        {
            return type.IsPrimitive
                || type.Equals(typeof(string));
        }

        /// <summary>
        ///     Checks whether the current <see cref="Type"/> is a simple type.
        /// </summary>
        /// <param name="type">
        ///     The type to check.
        /// </param>
        /// <param name="nullable">
        ///     If true, this method will checks whether the current <see cref="Type"/> is a nullable simple type.
        /// </param>
        /// <returns>
        ///     True if the current <see cref="Type"/> is a simple type.
        /// </returns>
        public static bool IsSimple(this Type type, bool nullable)
        {
            if (nullable)
            {
                return type.IsSimple() || type.IsNullableSimple();
            }
            else
            {
                return type.IsSimple();
            }
        }

        /// <summary>
        ///     Checks whether the current <see cref="Type"/> is a nullable simple type.
        /// </summary>
        /// <param name="type">
        ///     The type to check.
        /// </param>
        /// <returns>
        ///     True if the current <see cref="Type"/> is a nullable simple type.
        /// </returns>
        public static bool IsNullableSimple(this Type type)
        {
            var underType = Nullable.GetUnderlyingType(type);

            return IsSimple(underType);
        }

        /// <summary>
        ///     Checks whether the current <see cref="Type"/> represents an enumeration.
        /// </summary>
        /// <param name="type">
        ///     The type to check.
        /// </param>
        /// <param name="nullable">
        ///     If true, this method will checks whether the current <see cref="Type"/> is a nullable enumeration.
        /// </param>
        /// <returns>
        ///     True if the current <see cref="Type"/> represents an enumeration.
        /// </returns>
        public static bool IsEnum(this Type type, bool nullable)
        {
            if (nullable)
            {
                return type.IsEnum || type.IsNullableEnum();
            }
            else
            {
                return type.IsEnum;
            }
        }

        /// <summary>
        ///     Checks whether the current <see cref="Type"/> represents a nullable enumeration.
        /// </summary>
        /// <param name="type">
        ///     The type to check.
        /// </param>
        /// <returns>
        ///     True if the current <see cref="Type"/> represents a nullable enumeration.
        /// </returns>
        public static bool IsNullableEnum(this Type type)
        {
            var underType = Nullable.GetUnderlyingType(type);

            return underType.IsEnum;
        }
    }
}
