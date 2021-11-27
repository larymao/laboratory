using Lary.Laboratory.Core.Extensions;
using Lary.Laboratory.Core.Helpers;
using Lary.Laboratory.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lary.Laboratory.Facebook.Uploaders
{
    /// <summary>
    ///     Facebook photo uploader.
    /// </summary>
    public static class PhotoUploader
    {
        /// <summary>
        ///     Uploads a photo to facebook with a special target id as an asynchronous operation.
        /// </summary>
        /// <param name="filename">
        ///     The bytes data of photo.
        /// </param>
        /// <param name="targetId">
        ///     The target of photo uploading. Can be a value of {page_id, user_id, album_id, event_id, group_id, official_event_id}.
        /// </param>
        /// <param name="message">
        ///     The message to show about the photo.
        /// </param>
        /// <param name="accessToken">
        ///     Access token.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="FileNotFoundException">
        ///     Throw if the target file doesn't exists.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Throw if the parameter targetId or accessToken is null or empty string.
        /// </exception>
        public static async Task<ResponseMessage<string>> UploadAsync(string filename, string targetId, string message, string accessToken)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"Cannot find file {filename}");
            }

            if (String.IsNullOrEmpty(targetId) || String.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException();
            }

            var endpoint = new Uri(Basic.Apis.Gragh.PhotoCreating(targetId), UriKind.Absolute);
            var queries = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("access_token", accessToken),
                new KeyValuePair<string, string>("message", message)
            };

            return await UploadAsync(filename, endpoint, queries);
        }

        /// <summary>
        ///     Uploads an ad photo to facebook with the ad account id of user as an asynchronous operation.
        /// </summary>
        /// <param name="filename">
        ///     The full file path of the photo.
        /// </param>
        /// <param name="adAccountId">
        ///     The ad account id of user.
        /// </param>
        /// <param name="accessToken">
        ///     Access token.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="FileNotFoundException">
        ///     Throw if the target file doesn't exists.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Throw if the parameter targetId or accessToken is null or empty string.
        /// </exception>
        public static async Task<ResponseMessage<string>> UploadAsAdPhotoAsync(string filename, string adAccountId, string accessToken)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"Cannot find file {filename}");
            }

            if (String.IsNullOrEmpty(adAccountId) || String.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException();
            }

            var endpoint = new Uri(Basic.Apis.Gragh.AdPhotoUploading(adAccountId), UriKind.Absolute);
            var queries = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("access_token", accessToken)
            };

            return await UploadAsync(filename, endpoint, queries);
        }


        /// <summary>
        ///     Uploads a photo to a facebook api as an asynchronous operation.
        /// </summary>
        /// <param name="filename">
        ///     The full file path of the photo.
        /// </param>
        /// <param name="endpoint">
        ///     The target api to request.
        /// </param>
        /// <param name="queries">
        ///     The query data to append to the endpoint.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        private static async Task<ResponseMessage<string>> UploadAsync(string filename, Uri endpoint, params KeyValuePair<string, string>[] queries)
        {
            // Queries.
            endpoint = endpoint.AppendQueries(queries);

            // Content.
            var bytes = await File.ReadAllBytesAsync(filename);
            var photoContent = new ByteArrayContent(bytes, 0, bytes.Length);
            photoContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            photoContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("filename", $"{Guid.NewGuid().ToString("N")}{Path.GetExtension(filename)}"));

            var content = new MultipartFormDataContent
            {
                photoContent
            };

            // Request.
            var request = new HttpRequestMessage
            {
                RequestUri = endpoint,
                Method = HttpMethod.Post,
                Content = content
            };

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request);

                return await response.ToResponseMessageAsync();
            }
        }
    }
}
