using Lary.Laboratory.Core.Extensions;
using Lary.Laboratory.Core.Helpers;
using Lary.Laboratory.Core.Models;
using Lary.Laboratory.Facebook.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     <para/>Facebook video creating request.
    ///     <para/>The aspect ratio of the video must be between 9x16 and 16x9. We support the following formats 
    ///     for uploaded videos:
    ///     <para/>3g2, 3gp, 3gpp, asf, avi, dat, divx, dv, f4v, flv, gif, m2ts, m4v, mkv, mod, mov, mp4, mpe, 
    ///     mpeg, mpeg4, mpg, mts, nsv, ogm, ogv, qt, tod, ts, vob, and wmv.
    /// </summary>
    public partial class VideoCreatingRequest
    {
        /// <summary>
        ///     <para/>A simple wrap for uploading a video to facebook by non-resumable upload.
        ///     <para/>Supports video uploads that are up to 1GB and 20 minutes long.
        /// </summary>
        /// <param name="targetId">
        ///     The target of video uploading. Can be a value of {user_id, event_id, page_id, group_id}.
        /// </param>
        /// <param name="accessToken">
        ///     Access token.
        /// </param>
        /// <returns>
        ///     Video uploading result.
        /// </returns>
        public async Task<ResponseMessage<string>> PostAsync(string targetId, string accessToken)
        {
            var endpoint = new Uri(Basic.Apis.Gragh.VideoUploading(targetId), UriKind.Absolute);

            return await UploadVideoAsync(endpoint, accessToken);
        }

        /// <summary>
        ///     <para/>A simple wrap for uploading an ad video to facebook by non-resumable upload.
        ///     <para/>Supports video uploads that are up to 1GB and 20 minutes long.
        /// </summary>
        /// <param name="adAccountId">
        ///     The ad account id of user.
        /// </param>
        /// <param name="accessToken">
        ///     Access token.
        /// </param>
        /// <returns>
        ///     Ad video uploading result.
        /// </returns>
        public async Task<ResponseMessage<string>> PostAsAdVideoAsync(string adAccountId, string accessToken)
        {
            var endpoint = new Uri(Basic.Apis.Gragh.AdVideoUploading(adAccountId), UriKind.Absolute);

            return await UploadVideoAsync(endpoint, accessToken);
        }


        /// <summary>
        ///     Converts current object to its equivalent <see cref="MultipartFormDataContent"/> object.
        /// </summary>
        /// <param name="attachments">
        ///     An array of <see cref="KeyValuePair{TKey, TValue}"/> to attch to the <see cref="MultipartFormDataContent"/> object.
        /// </param>
        /// <returns>
        ///     A <see cref="MultipartFormDataContent"/> object.
        /// </returns>
        private MultipartFormDataContent ToMultipartFormDataContent(params KeyValuePair<string, string>[] attachments)
        {
            return ToMultipartFormDataContent(0, -1, attachments);
        }

        /// <summary>
        ///     Converts current object to its equivalent <see cref="MultipartFormDataContent"/> object.
        /// </summary>
        /// <param name="offset">
        ///     The offset of video file to upload.
        /// </param>
        /// <param name="count">
        ///     The length of video file to upload.
        /// </param>
        /// <param name="attachments">
        ///     An array of <see cref="KeyValuePair{TKey, TValue}"/> to attch to the <see cref="MultipartFormDataContent"/> object.
        /// </param>
        /// <returns>
        ///     A <see cref="MultipartFormDataContent"/> object.
        /// </returns>
        private MultipartFormDataContent ToMultipartFormDataContent(int offset, int count, params KeyValuePair<string, string>[] attachments)
        {
            var result = new MultipartFormDataContent();
            var type = this.GetType();
            var props = type.GetProperties();

            foreach (var prop in props)
            {
                var originalValue = prop.GetValue(this);

                if (originalValue != null)
                {
                    var name = AttributeHelper.GetFacebookPropertyName(prop);
                    HttpContent content;

                    if (prop.Name == nameof(this.Source))
                    {
                        using (var fs = File.OpenRead(this.Source))
                        {
                            count = count == -1 ? (int)fs.Length : count;

                            var buffer = new byte[count];
                            var length = fs.Read(buffer, offset, count);

                            content = new ByteArrayContent(buffer, 0, length);
                        }

                        result.Add(content, name, new FileInfo(this.Source).Name);
                    }
                    else
                    {
                        if (TypeHelper.IsSimple(prop.PropertyType))
                        {
                            content = new StringContent(originalValue.ToString());
                        }
                        else if (prop.PropertyType.IsEnum)
                        {
                            content = new StringContent(EnumHelper.GetDescription(prop.PropertyType, originalValue.ToString()));
                        }
                        else
                        {
                            content = new StringContent(JsonConvert.SerializeObject(originalValue));
                        }

                        result.Add(content, name);
                    }
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

        /// <summary>
        ///     Uploads a video to facebook in non-resumable upload.
        /// </summary>
        /// <param name="endpoint">
        ///     The uri of target endpoint.
        /// </param>
        /// <param name="accessToken">
        ///     Access token.
        /// </param>
        /// <returns>
        ///     Video uploading result.
        /// </returns>
        private async Task<ResponseMessage<string>> UploadVideoAsync(Uri endpoint, string accessToken)
        {
            var dic = new Dictionary<string, string>
            {
                { "access_token", accessToken }
            };

            var request = new HttpRequestMessage
            {
                RequestUri = endpoint,
                Method = HttpMethod.Post,
                Content = ToMultipartFormDataContent(dic.ToArray())
            };

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request);

                return await response.ToResponseMessageAsync();
            }
        }
    }
}
