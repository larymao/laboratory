using Lary.Laboratory.Core.Extensions;
using Lary.Laboratory.Core.Models;
using Lary.Laboratory.Facebook.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lary.Laboratory.Facebook.Marketing
{
    /// <summary>
    ///     Facebook ad creative creating request.
    /// </summary>
    public partial class AdCreativeCreatingRequest
    {
        /// <summary>
        ///      Posts an ad creative creating request to facebook with user's ad account id as an asynchronous 
        ///      operation. 
        /// </summary>
        /// <param name="adAccountId">
        ///     The ad account id of user.
        /// </param>
        /// <param name="accessToken">
        ///     User access token.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        public async Task<ResponseMessage<string>> PostAsync(string adAccountId, string accessToken)
        {
            var dic = new Dictionary<string, string>
            {
                { "access_token", accessToken }
            };

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(Basic.Apis.Marketing.AdCreative(adAccountId), UriKind.Absolute),
                Method = HttpMethod.Post,
                Content = HttpContentHelper.CreateMultipartFormDataContentFrom(this, dic.ToArray())
            };

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request);

                return await response.ToResponseMessageAsync();
            }
        }
    }
}
