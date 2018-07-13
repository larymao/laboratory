using Lary.Laboratory.Core.Extensions;
using Lary.Laboratory.Core.Helpers;
using Lary.Laboratory.Core.Models;
using Lary.Laboratory.Facebook.Helpers;
using Lary.Laboratory.Facebook.Uploaders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private readonly string _videoType = String.Empty;

        /// <summary>
        ///     Initializes a new instance of the <see cref="VideoCreatingRequest"/> class.
        /// </summary>
        /// <param name="videoType">
        ///     <para/>The type of video.
        ///     <para/>The aspect ratio of the video must be between 9x16 and 16x9. We support the following formats 
        ///     for uploaded videos:
        ///     <para/>3g2, 3gp, 3gpp, asf, avi, dat, divx, dv, f4v, flv, gif, m2ts, m4v, mkv, mod, mov, mp4, mpe, 
        ///     mpeg, mpeg4, mpg, mts, nsv, ogm, ogv, qt, tod, ts, vob, and wmv.
        /// </param>
        public VideoCreatingRequest(string videoType)
        {
            this._videoType = String.IsNullOrEmpty(videoType) ? String.Empty : $".{videoType.TrimStart('.')}";
        }

        /// <summary>
        ///     Posts a video creating request to facebook with a special target id as an asynchronous operation. 
        ///     Manually uploading control is required. See <see cref="VideoUploader"/> to get an easy way to upload.
        /// </summary>
        /// <param name="targetId">
        ///     The target of video creating. Can be a value of {user_id, event_id, page_id, group_id}.
        /// </param>
        /// <param name="accessToken">
        ///     Access token.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        public async Task<ResponseMessage<string>> PostAsync(string targetId, string accessToken)
        {
            var endpoint = new Uri(Basic.Apis.Gragh.VideoUploading(targetId), UriKind.Absolute);

            return await this.PostAsync(endpoint, accessToken);
        }

        /// <summary>
        ///     Posts a video creating request to facebook with the ad account id of user as an asynchronous operation. 
        ///     Manually uploading control is required. See <see cref="VideoUploader"/> to get an easy way to upload.
        /// </summary>
        /// <param name="adAccountId">
        ///     The ad account id of user.
        /// </param>
        /// <param name="accessToken">
        ///     Access token.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        public async Task<ResponseMessage<string>> PostAsAdVideoAsync(string adAccountId, string accessToken)
        {
            var endpoint = new Uri(Basic.Apis.Gragh.AdVideoUploading(adAccountId), UriKind.Absolute);

            return await this.PostAsync(endpoint, accessToken);
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
                        content = new ByteArrayContent(this.Source, 0, this.Source.Length);
                    
                        result.Add(content, name, $"{Guid.NewGuid().ToString("N")}{this._videoType}");
                    }
                    else if (prop.Name == nameof(this.VideoFileChunk))
                    {
                        content = new ByteArrayContent(this.VideoFileChunk, 0, this.VideoFileChunk.Length);

                        result.Add(content, name, $"{Guid.NewGuid().ToString("N")}{this._videoType}");
                    }
                    else
                    {
                        if (prop.PropertyType.IsSimple(true))
                        {
                            content = new StringContent(originalValue.ToString());
                        }
                        else if (prop.PropertyType.IsEnum(true))
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
        ///     Posts a video creating request to a facebook api as an asynchronous operation.
        /// </summary>
        /// <param name="endpoint">
        ///     The target api to request.
        /// </param>
        /// <param name="accessToken">
        ///     Access token.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        private async Task<ResponseMessage<string>> PostAsync(Uri endpoint, string accessToken)
        {
            var dic = new Dictionary<string, string>
            {
                { "access_token", accessToken }
            };

            var request = new HttpRequestMessage
            {
                RequestUri = endpoint,
                Method = HttpMethod.Post,
                Content = this.ToMultipartFormDataContent(dic.ToArray())
            };

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request);

                return await response.ToResponseMessageAsync();
            }
        }
    }
}
