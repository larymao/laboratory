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
        ///     Get description for eunmeration value.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of enumeration.
        /// </typeparam>
        /// <param name="enumerationValue">
        ///     The value of enumeration.
        /// </param>
        /// <returns>
        ///     The description of enumeration value.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     Throw if param enumerationValue is not of Enum type.
        /// </exception>
        public static string GetDescription<T>(this T enumerationValue)
            where T : struct
        {
            Type type = enumerationValue.GetType();

            return GetDescription(type, enumerationValue.ToString());
        }

        /// <summary>
        ///     Get description for eunmeration value.
        /// </summary>
        /// <param name="type">
        ///     The type of enumeration.
        /// </param>
        /// <param name="enumerationValue">
        ///     The string value of enumeration.
        /// </param>
        /// <returns>
        ///     The description of enumeration value.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     Throw if param enumerationValue is not of Enum type.
        /// </exception>
        public static string GetDescription(Type type, string enumerationValue)
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
            MemberInfo[] memberInfos = type.GetMember(enumerationValue);

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
            return enumerationValue;
        }

        /// <summary>
        ///     Get eunmeration value its description.
        /// </summary>
        /// <typeparam name="T">
        ///     Target enum type.
        /// </typeparam>
        /// <param name="description">
        ///     The value of description.
        /// </param>
        /// <returns>
        ///     Enumeration value.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     Throw if target type is not of Enum type.
        /// </exception>
        public static T GetValueFromDescription<T>(string description)
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
                        if (((DescriptionAttribute)attrs[0]).Description == description)
                        {
                            return Enum.Parse<T>(memberInfo.Name);
                        }
                    }
                }
            }
            
            return default(T);
        }
    }
}
