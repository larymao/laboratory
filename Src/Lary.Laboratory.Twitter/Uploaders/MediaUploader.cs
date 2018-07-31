using Lary.Laboratory.Core.Helpers;
using Lary.Laboratory.Core.Models;
using Lary.Laboratory.Twitter.Basic;
using Lary.Laboratory.Twitter.Helpers;
using Lary.Laboratory.Twitter.Models;
using Lary.Laboratory.Twitter.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lary.Laboratory.Twitter.Uploaders
{
    /// <summary>
    ///     Twitter media uploader.
    /// </summary>
    public class MediaUploader
    {
        private static readonly Uri _mediaUploadingApi = new Uri(Apis.MediaUploading(), UriKind.Absolute);
        private readonly OAuth10Util _oauth;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MediaUploader"/> class with consumerKey, consumerSecret, 
        ///     accessToken and accessTokenSecret.
        /// </summary>
        /// <param name="consumerKey">
        ///     Indicates consumer_key.
        /// </param>
        /// <param name="consumerSecret">
        ///     Indicates consumer_secret.
        /// </param>
        /// <param name="accessToken">
        ///     Indicates access_token.
        /// </param>
        /// <param name="accessTokenSecret">
        ///     Indicates access_token_secret.
        /// </param>
        public MediaUploader(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            _oauth = new OAuth10Util(consumerKey, consumerSecret, accessToken, accessTokenSecret);
        }

        /// <summary>
        ///     Uploads a video to twitter by chunked upload as an asynchronous operation.
        /// </summary>
        /// <param name="filename">
        ///     The full file path of the video.
        /// </param>
        /// <param name="chunkSize">
        ///     The length of a single chunk file, in bytes. And the length should not be set to more than 5MB.
        /// </param>
        /// <param name="wait4Available">
        ///     If set to true, this method won't return until the process of the uploaded media has done.
        /// </param>
        /// <param name="retries">
        ///     Chances of retry. The retry operation will be triggered on failed to upload chunk file.
        /// </param>
        /// <param name="mediaType">
        ///     Indicates the type of the media.
        /// </param>
        /// <param name="mediaCategory">
        ///     Indicates the category that the media will be uploaded to.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        public async Task<ResponseMessage<string>> UploadMediaInChunksAsync(string filename, int chunkSize = 2 * 1024 * 1024, bool wait4Available = true, int retries = 2, string mediaType = "video/mp4", string mediaCategory = "tweet_video")
        {
            ResponseMessage<string> response;
            JObject jobj;

            // 1. Initializes chunked upload.   
            var mediaId = String.Empty;
            var mediaKey = String.Empty;

            response = await InitChunkedUploadAsync(filename);

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"Initializes chunked upload returns {response.Data}");
#endif

            if (response.Code == ResponseCode.SUCCESS)
            {
                jobj = JObject.Parse(response.Data);

                if (jobj["media_id_string"] != null
                    && jobj["media_key"] != null
                    && !String.IsNullOrEmpty(jobj["media_id_string"].ToString())
                    && !String.IsNullOrEmpty(jobj["media_key"].ToString()))
                {
                    mediaId = jobj["media_id_string"].ToString();
                    mediaKey = jobj["media_key"].ToString();
                }
                else
                {
                    return new ResponseMessage<string>
                    {
                        Code = ResponseCode.UNKNOWN_ERROR,
                        ReasonPhrase = $"Failed to initialize chunked upload. Message: {response.ToString(true)}"
                    };
                }
            }
            else
            {
                return response;
            }

            // 2. Transfers data.
            var allTransfered = false;
            var fileInfo = new FileInfo(filename);
            var filesize = (int)fileInfo.Length;
            var chunkCount = filesize % chunkSize == 0 ? filesize / chunkSize : (filesize / chunkSize) + 1;

            using (var fs = File.OpenRead(filename))
            {
                var bytes = new byte[chunkSize];

                for (int i = 0; i < chunkCount; i++)
                {
                    var success = false;
                    Exception exception = null;

                    var count = 0;
                    var length = i == chunkCount - 1 ? filesize % chunkSize : chunkSize; // Length of the chunk file.

                    if (length < chunkSize)
                    {
                        bytes = new byte[length];
                    }

                    fs.Position = chunkSize * i;
                    fs.Read(bytes, 0, length);

                    do
                    {
                        try
                        {
                            exception = null;
#if DEBUG
                            var startTime = DateTime.Now;
#endif
                            response = await AppendChunkedUploadAsync(bytes, mediaId, i);
                            success = response.Code == ResponseCode.SUCCESS ? true : false;
#if DEBUG
                            var duration = DateTime.Now - startTime;
                            System.Diagnostics.Debug.WriteLine($"Transfering chunk_{i} with media_id {mediaId} returns {response.Code}. Duration: {duration.TotalSeconds.ToString("N2")}s.");
#endif
                        }
                        catch (Exception ex)
                        {
                            exception = ex;

                            success = false;
                        }
                    }
                    while (!success && count++ < retries);

                    if (!success)
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
                    else if (i == chunkCount - 1)
                    {
                        allTransfered = true;
                    }
                }
            }

            if (!allTransfered)
            {
                return new ResponseMessage<string>
                {
                    Code = ResponseCode.UNKNOWN_ERROR,
                    ReasonPhrase = $"Failed to transfer data. Message: {response.ToString(true)}"
                };
            }

            // 3. Finalizes chunked upload.
            response = await FinalizeChunkedUploadAsync(mediaId);

            if (wait4Available)
            {
                if (response.Code == ResponseCode.SUCCESS)
                {
                    jobj = JObject.Parse(response.Data);

                    if (jobj["processing_info"] != null
                        && jobj["processing_info"]["check_after_secs"] != null)
                    {
                        var success = Int32.TryParse(jobj["processing_info"]["check_after_secs"].ToString(), out int check_after_secs);

                        if (!success)
                        {
                            check_after_secs = 5;
                        }

                        await Task.Delay(check_after_secs * 1000);
                    }
                    else
                    {
                        return new ResponseMessage<string>
                        {
                            Code = ResponseCode.UNKNOWN_ERROR,
                            ReasonPhrase = $"Failed to finalize chunked upload. Message: {response.ToString(true)}"
                        };
                    }

                    response = await GetMediaStatusAsync(mediaId);
                }
            }

            return response;
        }


        /// <summary>
        ///     Initializes chunked upload as an asynchronous operation.
        /// </summary>
        /// <param name="filename">
        ///     The full file path of the media to upload.
        /// </param>
        /// <param name="mediaType">
        ///     Indicates the type of the media.
        /// </param>
        /// <param name="mediaCategory">
        ///     Indicates the category that the media will be uploaded to.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        private async Task<ResponseMessage<string>> InitChunkedUploadAsync(string filename, string mediaType = "video/mp4", string mediaCategory = "tweet_video")
        {
            var response = new HttpResponseMessage();

            var fileInfo = new FileInfo(filename);
            var dic = new Dictionary<string, string>
            {
                ["command"] = "INIT",
                ["total_bytes"] = fileInfo.Length.ToString(),
                ["media_type"] = mediaType,
                ["media_category"] = mediaCategory
            };

            do
            {
                response = await _oauth.PostAsync(_mediaUploadingApi, dic);
            }
            while ((int)response.StatusCode < 200 || (int)response.StatusCode >= 300); // Sometimes failed to authorization.

            return await response.ToResponseMessageAsync();
        }

        /// <summary>
        ///     Transfers a chunk file to twitter as an asynchronous operation.
        /// </summary>
        /// <param name="media">
        ///     A byte array that contains the chunked data to transfer.
        /// </param>
        /// <param name="mediaId">
        ///     Indicates the media id.
        /// </param>
        /// <param name="segmentIndex">
        ///     Indicates the segment index.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        private async Task<ResponseMessage<string>> AppendChunkedUploadAsync(byte[] media, string mediaId, int segmentIndex)
        {
            var response = new HttpResponseMessage();

            var dic = new Dictionary<string, string>
            {
                ["command"] = "APPEND",
                ["media_id"] = mediaId,
                ["segment_index"] = segmentIndex.ToString()
            };

            response = await _oauth.PostAsync(_mediaUploadingApi, dic, media);

            return await response.ToResponseMessageAsync();
        }

        /// <summary>
        ///     Finalizes the chunked upload as an asynchronous operation.
        /// </summary>
        /// <param name="mediaId">
        ///     Indicates the media id.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        private async Task<ResponseMessage<string>> FinalizeChunkedUploadAsync(string mediaId)
        {
            var response = new HttpResponseMessage();

            var dic = new Dictionary<string, string>
            {
                ["command"] = "FINALIZE",
                ["media_id"] = mediaId
            };

            response = await _oauth.PostAsync(_mediaUploadingApi, dic);

            return await response.ToResponseMessageAsync();
        }

        /// <summary>
        ///     Gets the status of uploaded media as an asynchronous operation.
        /// </summary>
        /// <param name="mediaId">
        ///     Indicates the media id.
        /// </param>
        /// <param name="wait4Available">
        ///     If set to true, this method won't return until the process of the uploaded media has done.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        private async Task<ResponseMessage<string>> GetMediaStatusAsync(string mediaId, bool wait4Available = true)
        {
            var response = new HttpResponseMessage();
            var content = String.Empty;
            JObject jobj;

            var dic = new Dictionary<string, string>
            {
                ["command"] = "STATUS",
                ["media_id"] = mediaId
            };

            var shouldRetry = false;

            do
            {
                shouldRetry = false;
                response = await _oauth.GetAsync(_mediaUploadingApi, dic);
                content = await response.Content.ReadAsStringAsync();
                jobj = JObject.Parse(content);

                if ((int)response.StatusCode >= 200 && (int)response.StatusCode < 300 && jobj["processing_info"] != null && jobj["processing_info"]["state"] != null)
                {
                    if (EnumHelper.TryMatch(jobj["processing_info"]["state"].ToString(), true, out ProcessState state))
                    {
                        if (state == ProcessState.Pending || state == ProcessState.InProgress)
                        {
                            if (!Int32.TryParse(jobj["processing_info"]["check_after_secs"].ToString(), out int check_after_secs))
                            {
                                check_after_secs = 5;
                            }

                            shouldRetry = true;

                            await Task.Delay(check_after_secs * 1000);
                        }
                    }
                }
            }
            while (shouldRetry);

            return await response.ToResponseMessageAsync();
        }
    }
}
