using Lary.Laboratory.Core.Models;
using Lary.Laboratory.Core.Utils;
using Lary.Laboratory.Facebook.Gragh;
using Lary.Laboratory.Facebook.Marketing;
using Lary.Laboratory.Facebook.Uploaders;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lary.Laboratory.Facebook.Publishers
{
    /// <summary>
    ///     Facebook ad creative publisher.
    /// </summary>
    public static class AdCreativePublisher
    {
        /// <summary>
        ///     Publishes photo ad to facebook as an asynchronous operation.
        /// </summary>
        /// <param name="photo">
        ///     The url or filepath of the ad photo.
        /// </param>
        /// <param name="request">
        ///     An object of <see cref="AdCreativeCreatingRequest"/>, which contains most of publish infomation.
        /// </param>
        /// <param name="adAccountId">
        ///     The ad account id of user.
        /// </param>
        /// <param name="accessToken">
        ///     User access token.
        /// </param>
        /// <param name="pageAccessToken">
        ///     Page access token.
        /// </param>
        /// <param name="publish">
        ///     If true, the photo ad will be published to a facebook page immediately.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="FileNotFoundException">
        ///     Throw if the target photo file doesn't exist.
        /// </exception>
        public static async Task<ResponseMessage<string>> PublishPhotoAdAsync(string photo, AdCreativeCreatingRequest request, string adAccountId, string accessToken, string pageAccessToken, bool publish)
        {
            ResponseMessage<string> response = null;

            var imgUrl = String.Empty;
            var adCreative = new AdCreative();

            // 1. Gets the url of image.
#pragma warning disable SA1008
            var (AdPhotoUrl, AdPhotoResponse) = await GetAdPhotoUrlAsync(photo, adAccountId, accessToken);
#pragma warning restore SA1008

            if (String.IsNullOrEmpty(AdPhotoUrl))
            {
                return AdPhotoResponse;
            }

            imgUrl = AdPhotoUrl;

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"Got image_url: {imgUrl}");
#endif

            // 2. Posts ad creative creating request.
            request.ObjectStorySpec.LinkData.Picture = imgUrl;

#pragma warning disable SA1008
            var (AdCreativeId, AdCreativeResponse) = await PostAdCreativeAsync(request, adAccountId, accessToken);
#pragma warning restore SA1008

            if (String.IsNullOrEmpty(AdCreativeId))
            {
                return AdCreativeResponse;
            }

            adCreative.Id = AdCreativeId;

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"Got ad_creative_id: {AdCreativeId}");
#endif

            if (publish)
            {
                // 3. Publishes ad creative.
                response = await adCreative.PublishAsync(accessToken, pageAccessToken);
            }

            return response;
        }

        /// <summary>
        ///     Publishes video ad to facebook with a specified cover as an asynchronous operation.
        /// </summary>
        /// <param name="video">
        ///     The filepath of the ad video.
        /// </param>
        /// <param name="request">
        ///     An object of <see cref="AdCreativeCreatingRequest"/>, which contains most of publish infomation.
        /// </param>
        /// <param name="adAccountId">
        ///     The ad account id of user.
        /// </param>
        /// <param name="accessToken">
        ///     User access token.
        /// </param>
        /// <param name="pageAccessToken">
        ///     Page access token.
        /// </param>
        /// <param name="publish">
        ///     If true, the photo ad will be published to a facebook page immediately.
        /// </param>
        /// <param name="threshold">
        ///     If the filesize (in MByte) of video exceeded this value, the video will be uploaded in resumable upload; 
        ///     otherwise, non-resumable upload. The default threshold is set to 50MB.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="FileNotFoundException">
        ///     Throw if the target video or cover file doesn't exist.
        /// </exception>
        public static async Task<ResponseMessage<string>> PublishVideoAdAsync(string video, AdCreativeCreatingRequest request, string adAccountId, string accessToken, string pageAccessToken, bool publish, int threshold = 50)
        {
            return await PublishVideoAdAsync(video, null, request, adAccountId, accessToken, pageAccessToken, publish, threshold);
        }

        /// <summary>
        ///     Publishes video ad to facebook with a specified cover as an asynchronous operation.
        /// </summary>
        /// <param name="video">
        ///     The filepath of the ad video.
        /// </param>
        /// <param name="cover">
        ///     The url or filepath of the video cover. Sets to null or an empty string to use the thumbnail that 
        ///     generated by facebook as the cover.
        /// </param>
        /// <param name="request">
        ///     An object of <see cref="AdCreativeCreatingRequest"/>, which contains most of publish infomation.
        /// </param>
        /// <param name="adAccountId">
        ///     The ad account id of user.
        /// </param>
        /// <param name="accessToken">
        ///     User access token.
        /// </param>
        /// <param name="pageAccessToken">
        ///     Page access token.
        /// </param>
        /// <param name="publish">
        ///     If true, the photo ad will be published to a facebook page immediately.
        /// </param>
        /// <param name="threshold">
        ///     If the filesize (in MByte) of video exceeded this value, the video will be uploaded in resumable upload; 
        ///     otherwise, non-resumable upload. The default threshold is set to 50MB.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="FileNotFoundException">
        ///     Throw if the target video or cover file doesn't exist.
        /// </exception>
        public static async Task<ResponseMessage<string>> PublishVideoAdAsync(string video, string cover, AdCreativeCreatingRequest request, string adAccountId, string accessToken, string pageAccessToken, bool publish, int threshold = 50)
        {
            ResponseMessage<string> response = null;

            JObject jobj = null;

            var videoId = String.Empty;
            var coverUrl = String.Empty;
            var adCreative = new AdCreative();

            // 1. Gets the video_id.
            #region Uploads video to facebook.
            if (!File.Exists(video))
            {
                throw new FileNotFoundException($"Cannot find file {video}");
            }

            var videoInfo = new FileInfo(video);
            var videoSize = videoInfo.Length;
            var enableChunkedUpload = videoSize > (50L * 1024 * 1024);
            var videoCreatingRequest = new VideoCreatingRequest(videoInfo.Extension)
            {
                Name = videoInfo.Name,
                Title = Path.GetFileNameWithoutExtension(video)
            };

            response = enableChunkedUpload ?
                await VideoUploader.UploadAsAdVideoInChunksAsync(video, adAccountId, accessToken, videoCreatingRequest) :
                await VideoUploader.UploadAsAdVideoAsync(video, adAccountId, accessToken, videoCreatingRequest);

            if (response.Code == ResponseCode.SUCCESS)
            {
                jobj = JObject.Parse(response.Data);

                if (enableChunkedUpload)
                {
                    // Parses resumable upload result.
                    if (jobj["video_id"] != null && !String.IsNullOrEmpty(jobj["video_id"].ToString()))
                    {
                        videoId = jobj["video_id"].ToString();
                    }
                }
                else
                {
                    // Parses non-resumable upload result.
                    if (jobj["id"] != null && !String.IsNullOrEmpty(jobj["id"].ToString()))
                    {
                        videoId = jobj["id"].ToString();
                    }
                }

                if (String.IsNullOrEmpty(videoId))
                {
                    return new ResponseMessage<string>
                    {
                        Code = ResponseCode.UNKNOWN_ERROR,
                        ReasonPhrase = $"Failed to upload video {video} to facebook."
                    };
                }
            }
            else
            {
                return response;
            }
            #endregion

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"Got video_id: {videoId}");
#endif

            // 2. Gets the url of cover.
            if (!String.IsNullOrEmpty(cover))
            {
                // Customizes cover.
#pragma warning disable SA1008
                var (AdPhotoUrl, AdPhotoResponse) = await GetAdPhotoUrlAsync(cover, adAccountId, accessToken);
#pragma warning restore SA1008

                if (String.IsNullOrEmpty(AdPhotoUrl))
                {
                    return AdPhotoResponse;
                }

                coverUrl = AdPhotoUrl;
            }

            /* The thumbnail won't be generated until the video has been decoded by facebook successfully, 
             * just wait for a while.
             * */
            await Task.Delay(10_000);

#pragma warning disable SA1008
            var (ThumbnailUrl, ThumbnailResponse) = await GetVideoThumbnailAsync(videoId, accessToken, 20);
#pragma warning restore SA1008

            if (String.IsNullOrEmpty(cover))
            {
                if (String.IsNullOrEmpty(ThumbnailUrl))
                {
                    return ThumbnailResponse;
                }

                coverUrl = ThumbnailUrl;
            }

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"Got cover_url: {coverUrl}");
#endif

            // 3. Posts ad creative creating request.
            request.ObjectStorySpec.VideoData.VideoId = videoId;
            request.ObjectStorySpec.VideoData.Cover = coverUrl;

#pragma warning disable SA1008
            var (AdCreativeId, AdCreativeResponse) = await PostAdCreativeAsync(request, adAccountId, accessToken);
#pragma warning restore SA1008

            if (String.IsNullOrEmpty(AdCreativeId))
            {
                return AdCreativeResponse;
            }

            adCreative.Id = AdCreativeId;

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"Got ad_creative_id: {AdCreativeId}");
#endif

            if (publish)
            {
                // 4. Publishes ad creative.
                response = await adCreative.PublishAsync(accessToken, pageAccessToken);
            }

            return response;
        }


        /// <summary>
        ///     Gets the url of specified facebook ad photo as an asynchronous operation.
        /// </summary>
        /// <param name="photo">
        ///     The url or filepath of the ad photo.
        /// </param>
        /// <param name="adAccountId">
        ///     The ad account id of user.
        /// </param>
        /// <param name="accessToken">
        ///     User access token.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="FileNotFoundException">
        ///     Throw if the target photo file doesn't exist.
        /// </exception>
        private static async Task<(string AdPhotoUrl, ResponseMessage<string> AdPhotoResponse)> GetAdPhotoUrlAsync(string photo, string adAccountId, string accessToken)
        {
            var imgUrl = String.Empty;
            ResponseMessage<string> response = null;
            JObject jobj = null;

            if (RegexUtil.IsUrl(photo))
            {
                imgUrl = photo;

                response = new ResponseMessage<string>
                {
                    Code = ResponseCode.SUCCESS
                };
            }
            else
            {
                // Uploads photo to facebook.
                response = await PhotoUploader.UploadAsAdPhotoAsync(photo, adAccountId, accessToken);

                if (response.Code == ResponseCode.SUCCESS)
                {
                    jobj = JObject.Parse(response.Data);

                    var urlToken = jobj.Descendants()
                        .Where(pp => pp.Type == JTokenType.Property && ((JProperty)pp).Name == "url")
                        .Select(pp => ((JProperty)pp).Value)
                        .FirstOrDefault();

                    if (urlToken == null)
                    {
                        response = new ResponseMessage<string>
                        {
                            Code = ResponseCode.UNKNOWN_ERROR,
                            ReasonPhrase = $"Failed to upload photo {photo} to facebook."
                        };
                    }
                    else
                    {
                        imgUrl = urlToken.ToString();
                    }
                }
            }

            return (imgUrl, response);
        }

        /// <summary>
        ///     Posts an ad creative creating request to facebook with user's ad account id as an asynchronous 
        ///     operation. 
        /// </summary>
        /// <param name="request">
        ///     An object of <see cref="AdCreativeCreatingRequest"/>.
        /// </param>
        /// <param name="adAccountId">
        ///     The ad account id of user.
        /// </param>
        /// <param name="accessToken">
        ///     User access token.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        private static async Task<(string AdCreativeId, ResponseMessage<string> AdCreativeResponse)> PostAdCreativeAsync(AdCreativeCreatingRequest request, string adAccountId, string accessToken)
        {
            var adCreativeId = String.Empty;
            ResponseMessage<string> response = null;
            JObject jobj = null;

            response = await request.PostAsync(adAccountId, accessToken);

            if (response.Code == ResponseCode.SUCCESS)
            {
                jobj = JObject.Parse(response.Data);

                if (jobj["id"] != null && !String.IsNullOrEmpty(jobj["id"].ToString()))
                {
                    adCreativeId = jobj["id"].ToString();
                }
                else
                {
                    response = new ResponseMessage<string>
                    {
                        Code = ResponseCode.UNKNOWN_ERROR,
                        ReasonPhrase = $"Failed to post ad creative creating request."
                    };
                }
            }

            return (adCreativeId, response);
        }

        /// <summary>
        ///     Gets the thumbnail url of specified facebook video as an asynchronous operation.
        /// </summary>
        /// <param name="videoId">
        ///     The video id.
        /// </param>
        /// <param name="accessToken">
        ///     Access token.
        /// </param>
        /// <param name="retries">
        ///     Chances of retry. The retry operation will be triggered on failed to upload chunk file.
        /// </param>
        /// <param name="interval">
        ///     The milliseconds time interval between request attempts.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        private static async Task<(string ThumbnailUrl, ResponseMessage<string> ThumbnailResponse)> GetVideoThumbnailAsync(string videoId, string accessToken, int retries = 2, int interval = 3_000)
        {
            var thumbnailUrl = String.Empty;
            ResponseMessage<string> response = null;
            JObject jobj = null;

            List<VideoThumbnail> thumbnails = null;
            var counter = 0;
            var video = new Video
            {
                Id = videoId
            };

            do
            {
                if (counter > 0)
                {
                    await Task.Delay(interval);
                }

                response = await video.GetThumbnailsAsync(accessToken);

                if (response.Code == ResponseCode.SUCCESS)
                {
                    jobj = JObject.Parse(response.Data);

                    if (jobj["data"] != null && jobj["data"].Count() > 0)
                    {
                        thumbnails = jobj["data"].ToObject<List<VideoThumbnail>>();
                        break;
                    }
                }
            }
            while (retries < 0 ? true : counter++ < retries);

            if (thumbnails != null)
            {
                thumbnailUrl = thumbnails.First().Uri;
            }

            return (thumbnailUrl, response);
        }
    }
}
