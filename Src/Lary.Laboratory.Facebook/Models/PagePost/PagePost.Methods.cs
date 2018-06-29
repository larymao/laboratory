using Lary.Laboratory.Core.Extensions;
using Lary.Laboratory.Core.Models;
using Lary.Laboratory.Core.Utils;
using Lary.Laboratory.Facebook.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lary.Laboratory.Facebook.Models
{
    /// <summary>
    /// <see cref="PagePost"/>
    /// </summary>
    public partial class PagePost
    {
        /// <summary>
        ///     Publishes current page post as an asynchronous operation.
        /// </summary>
        /// <param name="config">
        ///     <see cref="Config"/>
        /// </param>
        /// <returns>
        ///     <see cref="ResponseMessage{TResult}"/>
        /// </returns>
        public async Task<ResponseMessage<string>> PostAsync(Config config)
        {
            var result = new ResponseMessage<string>();

            var url = $"{Basic.Apis.Gragh.Feed(config.PageId)}?access_token={config.PageAccessToken}";

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url, UriKind.Absolute),
                Method = HttpMethod.Post,
                Content = this.ToFormUrlEncodedContent()
            };

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request);

                result = await response.ToResponseMessageAsync();
            }

            return result;
        }

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
                var name = AttributeHelper.GetFacebookPropertyName(prop);

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
                var name = AttributeHelper.GetFacebookPropertyName(prop);

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
    }
}
