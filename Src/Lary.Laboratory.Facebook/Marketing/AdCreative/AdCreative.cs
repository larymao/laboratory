using Lary.Laboratory.Core.Extensions;
using Lary.Laboratory.Core.Models;
using Lary.Laboratory.Facebook.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lary.Laboratory.Facebook.Marketing
{
    /// <summary>
    ///     An ad creative is an object that contains all the data for visually rendering the ad itself. 
    /// </summary>
    public partial class AdCreative
    {
        /// <summary>
        ///     Post current ad creative to facebook page as an asynchronous operation.
        ///     <see cref="AdCreative.Id"/> is required.
        /// </summary>
        /// <param name="accessToken">
        ///     User access token.
        /// </param>
        /// <param name="pageAccessToken">
        ///     Page access token.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        public async Task<ResponseMessage<string>> PublishAsync(string accessToken, string pageAccessToken)
        {
            var storyId = await GetEffectiveObjectStoryIdAsync(accessToken);

            if (!String.IsNullOrEmpty(storyId))
            {
                var xref = $"f{Guid.NewGuid().ToString("N").Remove(14).ToLower()}"; // It seems that facebook doesn't check this argument.
                var dic = new Dictionary<string, string>
                {
                    { "_reqName", "object:post" },
                    { "include_headers", "false" },
                    { "is_published", "true" },
                    { "locale", "en_US" },
                    { "method", "post" },
                    { "pretty", "0" },
                    { "suppress_http_code", "1" },
                    { "xref", xref },
                    { "access_token", pageAccessToken }
                };

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(Basic.Apis.Marketing.AdCreativePublishing(storyId), UriKind.Absolute),
                    Method = HttpMethod.Post,
                    Content = HttpContentHelper.CreateFormUrlEncodedContentFrom<object>(null, dic.ToArray())
                };

                using (var client = new HttpClient())
                {
                    var response = await client.SendAsync(request);

                    return await response.ToResponseMessageAsync();
                }
            }
            else
            {
                return new ResponseMessage<string>
                {
                    Code = ResponseCode.UNKNOWN_ERROR,
                    ReasonPhrase = "Failed to publish current ad creative to facebook."
                };
            }
        }


        /// <summary>
        ///     Get effective_object_story_id by the <see cref="AdCreative"/> as an asynchronous operation.
        /// </summary>
        /// <param name="accessToken">
        ///     User access token.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="Exception">
        ///     Throw if failed to get effective_object_story_id.
        /// </exception>
        private async Task<string> GetEffectiveObjectStoryIdAsync(string accessToken)
        {
            var dic = new Dictionary<string, string>
            {
                { "fields", "effective_object_story_id" },
                { "access_token", accessToken }
            };

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(Basic.Apis.Marketing.AdCreativeInfo(this.Id), UriKind.Absolute),
                Method = HttpMethod.Post,
                Content = HttpContentHelper.CreateFormUrlEncodedContentFrom<object>(null, dic.ToArray())
            };

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                var responseMsg = await response.ToResponseMessageAsync();

                if (responseMsg.Code == ResponseCode.SUCCESS)
                {
                    var jobj = JObject.Parse(responseMsg.Data);

                    if (jobj["effective_object_story_id"] != null)
                    {
                        return jobj["effective_object_story_id"].ToString();
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    throw new Exception(responseMsg.ReasonPhrase);
                }
            }
        }
    }
}
