using Lary.Laboratory.Core.Utils;
using Lary.Laboratory.Facebook.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace Lary.Laboratory.Facebook.Models
{
    /// <summary>
    ///     Facebook normal page post.
    /// </summary>
    public class PagePost
    {
        /// <summary>
        ///     The text message of normal page post.
        /// </summary>
        [FacebookProperty("message")]
        public string Message { get; set; }

        /// <summary>
        ///     Call to action link address.
        /// </summary>
        [FacebookProperty("link")]
        public string Link { get; set; }

        /// <summary>
        ///     The picture link address of normal page post.
        /// </summary>
        [FacebookProperty("picture")]
        public string Picture { get; set; }

        /// <summary>
        ///     The thumbnail of normal page post.
        /// </summary>
        [FacebookProperty("thumbnail")]
        public string Thumbnail { get; set; }

        /// <summary>
        ///     The video id of normal page post.
        /// </summary>
        [FacebookProperty("source")]
        public string Video { get; set; }

        /// <summary>
        ///     The Headline of normal page post.
        /// </summary>
        [FacebookProperty("name")]
        public string Headline { get; set; }

        /// <summary>
        ///     The description of normal page post.
        /// </summary>
        [FacebookProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     The caption of normal page post.
        /// </summary>
        [FacebookProperty("caption")]
        public string Caption { get; set; }

        /// <summary>
        ///     The call to action of normal page post.
        /// </summary>
        [FacebookProperty("call_to_action")]
        public Call2Action Action { get; set; }

        /// <summary>
        ///     Indicate whether post should be published.
        /// </summary>
        [FacebookProperty("published")]
        public bool? Published { get; set; }

        /// <summary>
        ///     The schedule publish time of post.
        /// </summary>
        [FacebookProperty("scheduled_publish_time")]
        public DateTime ScheduledTime { get; set; }


        /// <summary>
        ///     Convert current post to its equivalent <see cref="FormUrlEncodedContent"/> object.
        /// </summary>
        /// <returns>
        ///     A <see cref="FormUrlEncodedContent"/> object that represents the current post.
        /// </returns>
        public FormUrlEncodedContent ToFormUrlEncodedContent()
        {
            var dic = new Dictionary<string, string>();

            var properties = typeof(PagePost).GetProperties();
            foreach (var prop in properties)
            {
                var name = GetFacebookPropertyName(prop);

                if (prop.Name == nameof(this.Thumbnail) || prop.Name == nameof(this.Video))
                {
                    continue;
                }
                else if (prop.PropertyType == typeof(String))
                {
                    var value = prop.GetValue(this)?.ToString();

                    if (!String.IsNullOrEmpty(value))
                    {
                        if (!dic.ContainsKey(name))
                        {
                            dic.Add(name, value);
                        }
                    }
                }
                else if (this.Action != null && prop.Name == nameof(this.Action))
                {
                    if (!dic.ContainsKey(name))
                    {
                        dic.Add(name, JsonConvert.SerializeObject(this.Action));
                    }
                }
                else if (this.Published != null && prop.Name == nameof(this.Published))
                {
                    if (!dic.ContainsKey(name))
                    {
                        dic.Add(name, this.Published.ToString());
                    }
                }
                else if (this.ScheduledTime != default(DateTime) && prop.Name == nameof(this.ScheduledTime))
                {
                    if (!dic.ContainsKey(name))
                    {
                        dic.Add(name, DateTimeUtil.CountTimeStamp(this.ScheduledTime).ToString());
                    }
                }
            }

            return new FormUrlEncodedContent(dic);
        }

        /// <summary>
        ///     Convert current post to its equivalent <see cref="MultipartFormDataContent"/> object.
        /// </summary>
        /// <returns>
        ///     A <see cref="MultipartFormDataContent"/> object that represents the current post.
        /// </returns>
        public MultipartFormDataContent ToMultipartFormDataContent()
        {
            var content = new MultipartFormDataContent();

            var properties = typeof(PagePost).GetProperties();
            foreach (var prop in properties)
            {
                var name = GetFacebookPropertyName(prop);

                if (prop.GetType() == typeof(String))
                {
                    var value = prop.GetValue(this)?.ToString();

                    if (!String.IsNullOrEmpty(value))
                    {
                        if (prop.Name == nameof(this.Thumbnail))
                        {
                            continue;
                        }
                        else if (prop.Name == nameof(this.Video))
                        {
                            var fileInfo = new FileInfo(this.Video);
                            var videoContent = new StreamContent(fileInfo.OpenRead());
                            videoContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                            videoContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("filename", Guid.NewGuid().ToString("N") + fileInfo.Extension)); // 文件名若存在特殊字符，会导致此处抛出异常
                            content.Add(videoContent, name);
                        }
                        else
                        {
                            content.Add(new StringContent(value), name);
                        }
                    }
                }
                else if (this.Action != null && prop.Name == nameof(this.Action))
                {
                    content.Add(new StringContent(JsonConvert.SerializeObject(this.Action)), name);
                }
                else if (this.Published != null && prop.Name == nameof(this.Published))
                {
                    content.Add(new StringContent(this.Published.Value ? "1" : "0"), name);
                }
                else if (this.ScheduledTime != default(DateTime) && prop.Name == nameof(this.ScheduledTime))
                {
                    content.Add(new StringContent(DateTimeUtil.CountTimeStamp(this.ScheduledTime).ToString()), name);
                }
            }

            return content;
        }


        private string GetFacebookPropertyName(MemberInfo element)
        {
            var attr = (FacebookPropertyAttribute)Attribute.GetCustomAttribute(element, typeof(FacebookPropertyAttribute));
            return attr.Name;
        }
    }
}
