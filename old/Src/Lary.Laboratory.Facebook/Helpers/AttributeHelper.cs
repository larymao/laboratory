using Lary.Laboratory.Facebook.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Lary.Laboratory.Facebook.Helpers
{
    /// <summary>
    ///     Provides methods for facilitating the use of <see cref="Attribute"/>.
    /// </summary>
    internal static class AttributeHelper
    {
        /// <summary>
        ///     Get <see cref="FacebookPropertyAttribute.Name"/> of current <see cref="MemberInfo"/>.
        /// </summary>
        /// <param name="element">
        ///     The <see cref="MemberInfo"/> object.
        /// </param>
        /// <returns>
        ///     The <see cref="FacebookPropertyAttribute.Name"/> of current <see cref="MemberInfo"/>.
        /// </returns>
        internal static string GetFacebookPropertyName(MemberInfo element)
        {
            var attr = (FacebookPropertyAttribute)Attribute.GetCustomAttribute(element, typeof(FacebookPropertyAttribute));

            if (attr != null)
            {
                return attr.Name;
            }
            else
            {
                return element.Name;
            }
        }

        /// <summary>
        ///     Get <see cref="DescriptionAttribute"/> of current <see cref="MemberInfo"/>.
        /// </summary>
        /// <param name="element">
        ///     The <see cref="MemberInfo"/> object.
        /// </param>
        /// <returns>
        ///     The <see cref="DescriptionAttribute.Description"/> of current <see cref="MemberInfo"/>.
        /// </returns>
        internal static string GetDescription(MemberInfo element)
        {
            var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(element, typeof(DescriptionAttribute));

            if (attr != null)
            {
                return attr.Description;
            }
            else
            {
                return element.Name;
            }
        }
    }
}
