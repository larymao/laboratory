using Lary.Laboratory.Core.Extensions;
using Lary.Laboratory.Core.Helpers;
using Lary.Laboratory.Core.Models;
using Lary.Laboratory.Facebook.Gragh;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lary.Laboratory.Facebook.Uploaders
{
    /// <summary>
    ///     Facebook video uploader, which helps you to upload a video to facebook in an easy way.
    /// </summary>
    public static class VideoUploader
    {
        /// <summary>
        ///     Uploads a video to facebook by non-resumable upload.
        /// </summary>
        /// <param name="filename">
        ///     The full file path of the video.
        /// </param>
        /// <param name="targetId">
        ///     The target of video creating. Can be a value of {user_id, event_id, page_id, group_id}.
        /// </param>
        /// <param name="accessToken">
        ///     Access token.
        /// </param>
        /// <param name="request">
        ///     A <see cref="VideoCreatingRequest"/> object, of which basic information will be in use.
        /// </param>
        /// <returns>
        ///     Video uploading result.
        /// </returns>
        /// <exception cref="FileNotFoundException">
        ///     Throw if the target file doesn't exists.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Throw if the parameter targetId or accessToken is null or empty string, or request equals to null.
        /// </exception>
        public static async Task<ResponseMessage<string>> UploadAsync(string filename, string targetId, string accessToken, VideoCreatingRequest request)
        {
            #region Data check & preprocess.
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"Cannot find file {filename}");
            }

            if (String.IsNullOrEmpty(targetId) || String.IsNullOrEmpty(accessToken) || request == null)
            {
                throw new ArgumentNullException();
            }

            SimplifyVideoCreatingRequest(request); 
            #endregion

            request.Source = await File.ReadAllBytesAsync(filename);
            request.FileSize = request.Source.Length;

            return await request.PostAsync(targetId, accessToken);
        }

        /// <summary>
        ///     Uploads a video to facebook by non-resumable upload.
        /// </summary>
        /// <param name="filename">
        ///     The full file path of the video.
        /// </param>
        /// <param name="adAccountId">
        ///     The ad account id of user.
        /// </param>
        /// <param name="accessToken">
        ///     Access token.
        /// </param>
        /// <param name="request">
        ///     A <see cref="VideoCreatingRequest"/> object, of which basic information will be in use.
        /// </param>
        /// <returns>
        ///     Video uploading result.
        /// </returns>
        /// <exception cref="FileNotFoundException">
        ///     Throw if the target file doesn't exists.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Throw if the parameter targetId or accessToken is null or empty string, or request equals to null.
        /// </exception>
        public static async Task<ResponseMessage<string>> UploadAsAdVideoAsync(string filename, string adAccountId, string accessToken, VideoCreatingRequest request)
        {
            #region Data check & preprocess.
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"Cannot find file {filename}");
            }

            if (String.IsNullOrEmpty(adAccountId) || String.IsNullOrEmpty(accessToken) || request == null)
            {
                throw new ArgumentNullException();
            }

            SimplifyVideoCreatingRequest(request); 
            #endregion

            request.Source = await File.ReadAllBytesAsync(filename);
            request.FileSize = request.Source.Length;

            return await request.PostAsAdVideoAsync(adAccountId, accessToken);
        }

        /// <summary>
        ///     Uploads a video to facebook by resumable upload.
        /// </summary>
        /// <param name="filename">
        ///     The full file path of the video.
        /// </param>
        /// <param name="targetId">
        ///     The target of video creating. Can be a value of {user_id, event_id, page_id, group_id}.
        /// </param>
        /// <param name="accessToken">
        ///     Access token.
        /// </param>
        /// <param name="request">
        ///     A <see cref="VideoCreatingRequest"/> object, of which basic information will be in use.
        /// </param>
        /// <returns>
        ///     Video uploading result.
        /// </returns>
        /// <exception cref="FileNotFoundException">
        ///     Throw if the target file doesn't exists.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Throw if the parameter targetId or accessToken is null or empty string, or request equals to null.
        /// </exception>
        public static async Task<ResponseMessage<string>> UploadInChunksAsync(string filename, string targetId, string accessToken, VideoCreatingRequest request)
        {
            #region Data check & preprocess.
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"Cannot find file {filename}");
            }

            if (String.IsNullOrEmpty(targetId) || String.IsNullOrEmpty(accessToken) || request == null)
            {
                throw new ArgumentNullException();
            }

            SimplifyVideoCreatingRequest(request);
            #endregion

            var response = new ResponseMessage<string>();

            var fileExtension = Path.GetExtension(filename);
            var fileInfo = new FileInfo(filename);
            var videoId = String.Empty;
            JObject jobj = null;

            var initRequest = new VideoCreatingRequest(fileExtension)
            {
                UploadPhase = UploadPhase.Start,
                FileSize = fileInfo.Length
            };

            response = await initRequest.PostAsync(targetId, accessToken);

            if (response.Code == ResponseCode.SUCCESS)
            {
                jobj = JObject.Parse(response.Data);

                if (jobj["video_id"] != null && 
                    jobj["upload_session_id"] != null &&
                    !String.IsNullOrEmpty(jobj["video_id"].ToString()) && 
                    !String.IsNullOrEmpty(jobj["upload_session_id"].ToString()))
                {
                    videoId = jobj["video_id"].ToString();
                    request.UploadSessionId = jobj["upload_session_id"].ToString();
                    request.StartOffset = Int64.Parse(jobj["start_offset"].ToString());
                    request.EndOffset = Int64.Parse(jobj["end_offset"].ToString());

                    // TODO: Transfer chunk file.
                }
            }

            return response;
        }


        /// <summary>
        ///     Simplify the target <see cref="VideoCreatingRequest"/> object.
        /// </summary>
        /// <param name="request">
        ///     A <see cref="VideoCreatingRequest"/> object.
        /// </param>
        private static void SimplifyVideoCreatingRequest(VideoCreatingRequest request)
        {
            request.EndOffset = null;
            request.FileUrl = null;
            request.Source = null;
            request.StartOffset = null;
            request.UploadPhase = null;
            request.UploadSessionId = null;
            request.VideoFileChunk = null;
        }
    }
}
