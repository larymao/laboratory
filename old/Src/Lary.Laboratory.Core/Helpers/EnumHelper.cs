using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Lary.Laboratory.Core.Helpers
{
    /// <summary>
    ///     Provides methods for facilitating the use of enumeration.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        ///     Get description of an eunmeration object.
        /// </summary>
        /// <typeparam name="T">
        ///     The enumeration type to which to get description.
        /// </typeparam>
        /// <param name="value">
        ///     The string representation of the enumeration name.
        /// </param>
        /// <returns>
        ///     The description of value.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     Throw if param enumerationValue is not of Enum type.
        /// </exception>
        public static string GetDescription<T>(this T value)
            where T : struct
        {
            Type type = value.GetType();

            return GetDescription(type, value.ToString());
        }

        /// <summary>
        ///     <para/>Get the description of an eunmeration object by its string representation of the name.
        ///     <para/>Notice: the method is case sensitive.
        /// </summary>
        /// <param name="type">
        ///     The enumeration type to which to get description.
        /// </param>
        /// <param name="value">
        ///     The string representation of the enumeration name.
        /// </param>
        /// <returns>
        ///     The description of value.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     Throw if param enumerationValue is not of Enum type.
        /// </exception>
        public static string GetDescription(Type type, string value)
        {
            if (!type.IsEnum(true))
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            if (type.IsNullableEnum())
            {
                type = Nullable.GetUnderlyingType(type);
            }

            // Tries to find a DescriptionAttribute for a potential friendly name for the enum.
            MemberInfo[] memberInfos = type.GetMember(value);

            if (memberInfos != null && memberInfos.Length > 0)
            {
                object[] attrs = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    // Pull out the description value.
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            // If we have no description attribute, just return the ToString of the enum.
            return value;
        }

        /// <summary>
        ///     Gets the name of an enumeration object by its description.
        /// </summary>
        /// <typeparam name="T">
        ///     The enumeration type to which to convert value.
        /// </typeparam>
        /// <param name="description">
        ///     The value of description.
        /// </param>
        /// <param name="comparisonType">
        ///     One of the enumeration values that specifies the rules for the comparison.
        /// </param>
        /// <returns>
        ///     An object of type T whose description is represented by description.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     Throw if target type is not of Enum type.
        /// </exception>
        /// <exception cref="Exception">
        ///     Throw if the description is not found.
        /// </exception>
        public static T GetValueFromDescription<T>(string description, StringComparison comparisonType)
            where T : struct
        {
            Type type = typeof(T);

            if (!type.IsEnum(true))
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            if (type.IsNullableEnum())
            {
                type = Nullable.GetUnderlyingType(type);
            }

            MemberInfo[] memberInfos = type.GetMembers();

            if (memberInfos != null)
            {
                foreach (var memberInfo in memberInfos)
                {
                    object[] attrs = memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (attrs != null && attrs.Length > 0)
                    {
                        if (String.Equals(((DescriptionAttribute)attrs[0]).Description, description, comparisonType))
                        {
                            return (T)Enum.Parse(typeof(T), memberInfo.Name);
                        }
                    }
                }
            }

            throw new Exception($"Description \"{description}\" was not found.");
        }

        /// <summary>
        ///     Converts the string representation of the name, description attribute or numeric value of one or more
        ///     enumerated constants to an equivalent enumerated object. A parameter specifies whether the operation 
        ///     is case-sensitive. The return value indicates whether the conversion succeeded.
        /// </summary>
        /// <typeparam name="T">
        ///     The enumeration type to which to convert value.
        /// </typeparam>
        /// <param name="value">
        ///     The string representation of the enumeration name, description or underlying value to convert.
        /// </param>
        /// <param name="ignoreCase">
        ///     True to ignore case; false to consider case.
        /// </param>
        /// <param name="result">
        ///     When this method returns, contains an object of type TEnum whose value is represented
        ///     by value. This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        ///     True if the value parameter was converted successfully; otherwise, false.
        /// </returns>
        public static bool TryMatch<T>(string value, bool ignoreCase, out T result)
            where T : struct
        {
            if (!typeof(T).IsEnum(true))
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            var success = false;
            result = default(T);

            try
            {
                if (!Enum.TryParse(value, ignoreCase, out result))
                {
                    var comparisonType = ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture;
                    result = GetValueFromDescription<T>(value, comparisonType);
                }

                success = true;
            }
            catch
            {
            }

            return success;
        }
    }
}
