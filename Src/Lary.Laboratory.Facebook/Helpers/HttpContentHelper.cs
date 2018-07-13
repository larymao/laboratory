using Lary.Laboratory.Core.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

                    if (prop.PropertyType.IsSimple(true))
                    {
                        value = originalValue.ToString();
                    }
                    else if (prop.PropertyType.IsEnum(true))
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

        /// <summary>
        ///     Converts a item to its equivalent <see cref="MultipartFormDataContent"/> object.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of item.
        /// </typeparam>
        /// <param name="item">
        ///     The item to convert.
        /// </param>
        /// <param name="attachments">
        ///     An array of <see cref="KeyValuePair{TKey, TValue}"/> to attch to the <see cref="MultipartFormDataContent"/> object.
        /// </param>
        /// <returns>
        ///     A <see cref="MultipartFormDataContent"/> object.
        /// </returns>
        internal static MultipartFormDataContent CreateMultipartFormDataContentFrom<T>(T item, params KeyValuePair<string, string>[] attachments)
        {
            var result = new MultipartFormDataContent();
            var type = item.GetType();
            var props = type.GetProperties();

            foreach (var prop in props)
            {
                var originalValue = prop.GetValue(item);

                if (originalValue != null)
                {
                    var name = AttributeHelper.GetFacebookPropertyName(prop);
                    HttpContent content;

                    if (prop.PropertyType.IsSimple(true))
                    {
                        content = new StringContent(originalValue.ToString());
                    }
                    else if (prop.PropertyType.IsEnum(true))
                    {
                        content = new StringContent(EnumHelper.GetDescription(prop.PropertyType, originalValue.ToString()));
                    }
                    else if (prop.PropertyType == typeof(byte[]))
                    {
                        var bytes = originalValue as byte[];
                        content = new ByteArrayContent(bytes, 0, bytes.Length);
                    }
                    else
                    {
                        content = new StringContent(JsonConvert.SerializeObject(originalValue));
                    }

                    result.Add(content, name);
                }
            }

            if (attachments != null && attachments.Length > 0)
            {
                foreach (var attach in attachments)
                {
                    result.Add(new StringContent(attach.Value), attach.Key);
                }
            }

            return result;
        }
    }
}
