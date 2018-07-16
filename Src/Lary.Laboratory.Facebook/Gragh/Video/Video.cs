using Lary.Laboratory.Core.Extensions;
using Lary.Laboratory.Core.Models;
using Lary.Laboratory.Facebook.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     <para/>Represents an individual video on Facebook.
    ///     <para/>Any valid access token can read videos on a public Page.
    ///     <para/>A page access token can read all videos posted to or posted by that Page.
    ///     <para/>A user access token can read any video your application created on behalf of that user.
    ///     <para/>A user's videos can be read if the owner has granted the user_videos or user_posts permission.
    ///     <para/>A user access token may read a video that user is tagged in if they user granted the user_videos 
    ///     or user_posts permission. However, in some cases the video's owner's privacy settings may not allow your 
    ///     application to access it.
    ///     <para/>The source field will not be returned for Page-owned videos unless the User making the request has 
    ///     a role on the owning Page.
    /// </summary>
    public partial class Video
    {
        /// <summary>
        ///     Get the thumbnails of current video by its id as an asynchronous operation.
        /// </summary>
        /// <param name="accessToken">
        ///     Access token.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        public async Task<ResponseMessage<string>> GetThumbnailsAsync(string accessToken)
        {
            var dic = new Dictionary<string, string>
            {
                { "access_token", accessToken }
            };

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(Basic.Apis.Gragh.VideoThumbnails(this.Id), UriKind.Absolute),
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
