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
        ///     Uploads a video to facebook by non-resumable upload as an asynchronous operation.
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
        ///     The task object representing the asynchronous operation.
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
        ///     Uploads an ad video to facebook by non-resumable upload as an asynchronous operation.
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
        ///     The task object representing the asynchronous operation.
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
        ///     Uploads a video to facebook by resumable upload as an asynchronous operation.
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
        /// <param name="retries">
        ///     Chances of retry. The retry operation will be triggered on failed to upload chunk file.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="FileNotFoundException">
        ///     Throw if the target file doesn't exists.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Throw if the parameter targetId or accessToken is null or empty string, or request equals to null.
        /// </exception>
        public static async Task<ResponseMessage<string>> UploadInChunksAsync(string filename, string targetId, string accessToken, VideoCreatingRequest request, int retries = 2)
        {
            #region Data check.
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"Cannot find file {filename}");
            }

            if (String.IsNullOrEmpty(targetId) || String.IsNullOrEmpty(accessToken) || request == null)
            {
                throw new ArgumentNullException();
            }
            #endregion
            
            return await UploadInChunksAsync(filename, request, retries, async (req) =>
            {
                return await req.PostAsync(targetId, accessToken);
            });
        }

        /// <summary>
        ///     Uploads an ad video to facebook by resumable upload as an asynchronous operation.
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
        /// <param name="retries">
        ///     Chances of retry. The retry operation will be triggered on failed to upload chunk file.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="FileNotFoundException">
        ///     Throw if the target file doesn't exists.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Throw if the parameter targetId or accessToken is null or empty string, or request equals to null.
        /// </exception>
        public static async Task<ResponseMessage<string>> UploadAsAdVideoInChunksAsync(string filename, string adAccountId, string accessToken, VideoCreatingRequest request, int retries = 2)
        {
            #region Data check.
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"Cannot find file {filename}");
            }

            if (String.IsNullOrEmpty(adAccountId) || String.IsNullOrEmpty(accessToken) || request == null)
            {
                throw new ArgumentNullException();
            }
            #endregion

            return await UploadInChunksAsync(filename, request, retries, async (req) =>
            {
                return await req.PostAsAdVideoAsync(adAccountId, accessToken);
            });
        }


        /// <summary>
        ///     Uploads a video to facebook by resumable upload as an asynchronous operation.
        /// </summary>
        /// <param name="filename">
        ///     The full file path of the video.
        /// </param>
        /// <param name="request">
        ///     A <see cref="VideoCreatingRequest"/> object, of which basic information will be in use.
        /// </param>
        /// <param name="retries">
        ///     Chances of retry. The retry operation will be triggered on failed to upload chunk file.
        /// </param>
        /// <param name="asyncRequestOperation">
        ///     A function to post <see cref="VideoCreatingRequest"/> asynchronously, a 
        ///     <see cref="ResponseMessage{TResult}"/> object should be returns.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        private static async Task<ResponseMessage<string>> UploadInChunksAsync(string filename, VideoCreatingRequest request, int retries, Func<VideoCreatingRequest, Task<ResponseMessage<string>>> asyncRequestOperation)
        {
            var response = new ResponseMessage<string>();

            var fileExtension = Path.GetExtension(filename);
            var fileInfo = new FileInfo(filename);
            var videoId = String.Empty;
            var uploadSessionId = String.Empty;
            var allUploaded = false;
            JObject jobj = null;
            Exception exception = null;

            // To init chunked upload.
            var initRequest = new VideoCreatingRequest(fileExtension)
            {
                UploadPhase = UploadPhase.Start,
                FileSize = fileInfo.Length
            };

            response = await asyncRequestOperation(initRequest);

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"Initializes chunked upload returns {response.Data}");
#endif

            if (response.Code == ResponseCode.SUCCESS)
            {
                jobj = JObject.Parse(response.Data);

                if (jobj["video_id"] != null &&
                    jobj["upload_session_id"] != null &&
                    !String.IsNullOrEmpty(jobj["video_id"].ToString()) &&
                    !String.IsNullOrEmpty(jobj["upload_session_id"].ToString()))
                {
                    videoId = jobj["video_id"].ToString();
                    uploadSessionId = jobj["upload_session_id"].ToString();

                    var transferRequest = new VideoCreatingRequest(fileExtension)
                    {
                        UploadSessionId = uploadSessionId,
                        StartOffset = Int64.Parse(jobj["start_offset"].ToString()),
                        EndOffset = Int64.Parse(jobj["end_offset"].ToString()),
                        UploadPhase = UploadPhase.Transfer
                    };

                    // To open file and transfer data.
                    using (var fs = fileInfo.OpenRead())
                    {
                        do
                        {
                            var bufferLength = (Int32)(transferRequest.EndOffset - transferRequest.StartOffset).Value;
                            var buffer = new byte[bufferLength];
                            fs.Position = transferRequest.StartOffset.Value;
                            fs.Read(buffer, 0, bufferLength);
                            transferRequest.VideoFileChunk = buffer;

                            var uploaded = false; // Indicates if current chunk file uploaded successfully.
                            var uploadCounter = 0; // Indicates the uploading attempts of current chunk file.

                            #region Transfers single chunk file.
                            do
                            {
                                try
                                {
                                    exception = null; // Reset exception.
#if DEBUG
                                    var startTime = DateTime.Now;
#endif
                                    response = await asyncRequestOperation(transferRequest);

#if DEBUG
                                    var duration = DateTime.Now - startTime;
                                    System.Diagnostics.Debug.WriteLine($"Transfering {transferRequest.StartOffset}-{transferRequest.EndOffset} returns {response.Code}. Duration: {duration.TotalSeconds.ToString("N2")}s.");
#endif

                                    if (response.Code == ResponseCode.SUCCESS)
                                    {
                                        jobj = JObject.Parse(response.Data);

                                        if (jobj["start_offset"] != null &&
                                            jobj["end_offset"] != null &&
                                            !String.IsNullOrEmpty(jobj["start_offset"].ToString()) &&
                                            !String.IsNullOrEmpty(jobj["end_offset"].ToString()))
                                        {
                                            transferRequest.StartOffset = Int64.Parse(jobj["start_offset"].ToString());
                                            transferRequest.EndOffset = Int64.Parse(jobj["end_offset"].ToString());

                                            uploaded = true;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    exception = ex;

                                    await Task.Delay(1000);
                                }
                            }
                            while (!uploaded && uploadCounter++ < retries);
                            #endregion

                            if (!uploaded)
                            {
                                if (exception != null)
                                {
                                    throw exception; // Throw exception if it occured during the last attempt.
                                }
                                else
                                {
                                    break; // None successful attempt, break.
                                }
                            }
                            else if (transferRequest.StartOffset >= transferRequest.EndOffset)
                            {
                                allUploaded = true;
                            }
                        }
                        while (!allUploaded);
                    }

                    if (allUploaded)
                    {
                        // To complete chunked upload.
                        SimplifyVideoCreatingRequest(request);

                        request.UploadSessionId = uploadSessionId;
                        request.UploadPhase = UploadPhase.Finish;

                        response = await asyncRequestOperation(request);

                        jobj = JObject.Parse(response.Data);
                        jobj["video_id"] = videoId;
                        response.Data = jobj.ToString();
                    }
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
