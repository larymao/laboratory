using Lary.Laboratory.Core.Extensions;
using Lary.Laboratory.Core.Models;
using Lary.Laboratory.Facebook.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     Facebook photo creating request. The photo must be less than 10MB in size.
    /// </summary>
    public partial class PhotoCreatingRequest
    {
        /// <summary>
        ///     Creates a photo on facebook.
        /// </summary>
        /// <param name="targetId">
        ///     The target of photo creating. Can be a value of {page_id, user_id, album_id, event_id, group_id, official_event_id}.
        /// </param>
        /// <param name="accessToken">
        ///     Access token.
        /// </param>
        /// <returns>
        ///     Photo creating result.
        /// </returns>
        public async Task<ResponseMessage<string>> PublishAsync(string targetId, string accessToken)
        {
            var dic = new Dictionary<string, string>
            {
                { "access_token", accessToken }
            };

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(Basic.Apis.Gragh.PhotoCreating(targetId), UriKind.Absolute),
                Method = HttpMethod.Post,
                Content = HttpContentHelper.CreateFormUrlEncodedContentFrom(this, dic.ToArray())
            };

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request);

                return await response.ToResponseMessageAsync();
            }
        }
    }
}
