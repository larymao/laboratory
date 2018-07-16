using Lary.Laboratory.Core.Models;
using Lary.Laboratory.Core.Utils;
using Lary.Laboratory.Facebook.Marketing;
using Lary.Laboratory.Facebook.Uploaders;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
        ///     Publish photo ad to facebook as an asynchronous operation.
        /// </summary>
        /// <param name="photo">
        ///     The url or filepath of the photo.
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
        ///     Throw if the target photo file doesn't exists.
        /// </exception>
        public static async Task<ResponseMessage<string>> PublishPhotoAdAsync(string photo, AdCreativeCreatingRequest request, string adAccountId, string accessToken, string pageAccessToken, bool publish)
        {
            ResponseMessage<string> response = null;

            JObject jobj = null;

            var imgUrl = String.Empty;
            var adCreative = new AdCreative();

            // 1. Gets the url of image.
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
                #region Uploads photo to facebook.
                if (!File.Exists(photo))
                {
                    throw new FileNotFoundException($"Cannot find file {photo}");
                }

                response = await PhotoUploader.UploadAsAdPhotoAsync(photo, adAccountId, accessToken);

                if (response.Code == ResponseCode.SUCCESS)
                {
                    jobj = JObject.Parse(response.Data);

                    if (jobj["image_url"] != null && !String.IsNullOrEmpty(jobj["image_url"].ToString()))
                    {
                        imgUrl = jobj["image_url"].ToString();
                    }
                    else
                    {
                        return new ResponseMessage<string>
                        {
                            Code = ResponseCode.UNKNOWN_ERROR,
                            ReasonPhrase = $"Failed to upload photo {photo} to facebook."
                        };
                    }
                }
                else
                {
                    return response;
                } 
                #endregion
            }

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"Got image_url: {imgUrl}");
#endif

            // 2. Posts ad creative creating request.
            #region Posts ad creative creating request.
            response = await request.PostAsync(adAccountId, accessToken);

            if (response.Code == ResponseCode.SUCCESS)
            {
                jobj = JObject.Parse(response.Data);

                if (jobj["id"] != null && !String.IsNullOrEmpty(jobj["id"].ToString()))
                {
                    adCreative.Id = jobj["id"].ToString();
                }
                else
                {
                    return new ResponseMessage<string>
                    {
                        Code = ResponseCode.UNKNOWN_ERROR,
                        ReasonPhrase = $"Failed to post ad creative creating request."
                    };
                }
            }
            else
            {
                return response;
            } 
            #endregion

            if (publish)
            {
                // 3. Publishes ad creative.
                response = await adCreative.PublishAsync(accessToken, pageAccessToken);
            }

            return response;
        }
    }
}
