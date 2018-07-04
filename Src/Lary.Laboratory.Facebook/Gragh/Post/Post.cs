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
    ///     /// <summary>
    ///     <para/>An individual entry in a profile's feed. The profile could be a user, page, app, or group.
    ///     <para/>For Posts on a User:
    ///     <para/>That User's access token with the user_posts permission, or
    ///     <para/>That User's access token, if that User used the app to create the Post, or
    ///     <para/>The app's access token, if that User has previously granted the app the user_posts permission, or
    ///     <para/>The access token for a friend of that User, with the user_friends permission, if that User has
    ///     already granted the app both the user_posts and the user_friends permission.
    ///     <para/>For Posts on a Page:
    ///     <para/>Any valid access token can read posts on a public Page, but responses will not include User information.
    ///     <para/>A Page access token can read all Posts posted to or posted by that Page, and responses will include 
    ///     User information.
    ///     <para/>For Posts on a Group:
    ///     <para/>As of April 4th, 2018, an access token for an Admin of the Group, if the app has passed App Review. 
    ///     Please refer to the Breaking Changes changelog for more details.
    ///     <para/>For Posts that tag Users:
    ///     <para/>As of April 4th, 2018, apps can no longer read Posts owned by non-app User who have been tagged by 
    ///     an app User. Please refer to the Breaking Changes changelog for more details.
    ///     <para/>For Posts on an Event:
    ///     <para/>Any valid access token of an Admin of the Event for Event-owned Posts is required after April 30, 2018.
    /// </summary>
    /// </summary>
    public partial class Post
    {
        /// <summary>
        ///     Publishes a post to facebook.
        /// </summary>
        /// <param name="targetId">
        ///     The target of post publishing.
        /// </param>
        /// <param name="accessToken">
        ///     Access token.
        /// </param>
        /// <returns>
        ///     Post publishing result.
        /// </returns>
        public async Task<ResponseMessage<string>> PublishAsync(string targetId, string accessToken)
        {
            var dic = new Dictionary<string, string>
            {
                { "access_token", accessToken }
            };

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(Basic.Apis.Gragh.PostPublishing(targetId), UriKind.Absolute),
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
