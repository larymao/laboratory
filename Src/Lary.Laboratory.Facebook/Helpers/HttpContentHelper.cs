using Lary.Laboratory.Core.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Lary.Laboratory.Facebook.Helpers
{
    /// <summary>
    ///     Provides methods for facilitating the use of <see cref="HttpContent"/>.
    /// </summary>
    internal static class HttpContentHelper
    {
        /// <summary>
        ///     Converts a item to its equivalent <see cref="FormUrlEncodedContent"/> object.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of item.
        /// </typeparam>
        /// <param name="item">
        ///     The item to convert.
        /// </param>
        /// <param name="attachments">
        ///     An array of <see cref="KeyValuePair{TKey, TValue}"/> to attch to the <see cref="FormUrlEncodedContent"/> object.
        /// </param>
        /// <returns>
        ///     A <see cref="FormUrlEncodedContent"/> object.
        /// </returns>
        internal static FormUrlEncodedContent CreateFormUrlEncodedContentFrom<T>(T item, params KeyValuePair<string, string>[] attachments)
        {
            var dic = new Dictionary<string, string>();
            var type = item.GetType();
            var props = type.GetProperties();

            foreach (var prop in props)
            {
                var originalValue = prop.GetValue(item);

                if (originalValue != null)
                {
                    var key = AttributeHelper.GetFacebookPropertyName(prop);
                    var value = String.Empty;

                    if (TypeHelper.IsSimple(prop.PropertyType))
                    {
                        value = originalValue.ToString();
                    }
                    else if (prop.PropertyType.IsEnum)
                    {
                        value = EnumHelper.GetDescription(prop.PropertyType, originalValue.ToString());
                    }
                    else
                    {
                        value = JsonConvert.SerializeObject(originalValue);
                    }

                    dic.Add(key, value);
                }
            }

            if (attachments != null && attachments.Length > 0)
            {
                foreach (var attach in attachments)
                {
                    dic.Add(attach.Key, attach.Value);
                }
            }

            return new FormUrlEncodedContent(dic);
        }
    }
}
