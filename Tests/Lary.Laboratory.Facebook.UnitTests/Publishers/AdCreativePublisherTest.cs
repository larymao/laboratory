using Lary.Laboratory.Core.Models;
using Lary.Laboratory.Facebook.Marketing;
using Lary.Laboratory.Facebook.Publishers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lary.Laboratory.Facebook.UnitTests.Publishers
{
    /// <summary>
    ///     Provides unit test methods for testing <see cref="AdCreativePublisher"/>.
    /// </summary>
    [TestClass]
    public class AdCreativePublisherTest
    {
        /// <summary>
        ///     Tests the publish of facebook photo ad as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        [TestMethod]
        public async Task PublishPhotoAdAsync()
        {
            var request = new AdCreativeCreatingRequest
            {
                ObjectStorySpec = new ObjectStorySpec
                {
                    PageId = TestsBase.PageId,
                    LinkData = new LinkData
                    {
                        Call2Action = new Call2Action
                        {
                            Type = Call2ActionType.LEARN_MORE
                        },
                        Caption = "LARY.ME",
                        Description = "Description of photo ad.",
                        Headline = "Headline of Photo Ad",
                        Link = TestsBase.Link,
                        Message = Guid.NewGuid().ToString("N")
                    }
                }
            };

            var response = await AdCreativePublisher.PublishPhotoAdAsync(TestsBase.TestPicturePath, request, TestsBase.AdAccountId, TestsBase.AccessToken, TestsBase.PageAccessToken, true);
            Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);

            var jobj = JObject.Parse(response.Data);
            Assert.IsTrue(jobj["success"] != null && Boolean.Parse(jobj["success"].ToString()) == true);
        }

        /// <summary>
        ///     Tests the publish of facebook video ad with custom video cover as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        [TestMethod]
        public async Task PublishVieoAdWithCustomCoverAsync()
        {
            var request = new AdCreativeCreatingRequest
            {
                ObjectStorySpec = new ObjectStorySpec
                {
                    PageId = TestsBase.PageId,
                    VideoData = new VideoData
                    {
                        Call2Action = new Call2Action
                        {
                            Type = Call2ActionType.LEARN_MORE,
                            Value = new Call2ActionValue
                            {
                                Link = TestsBase.Link
                            }
                        },
                        Headline = "Headline of Video Ad With Custom Cover",
                        Message = Guid.NewGuid().ToString("N")
                    }
                }
            };

            var response = await AdCreativePublisher.PublishVideoAdAsync(TestsBase.TestVideoPath, TestsBase.TestPicturePath, request, TestsBase.AdAccountId, TestsBase.AccessToken, TestsBase.PageAccessToken, true);
            Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);

            var jobj = JObject.Parse(response.Data);
            Assert.IsTrue(jobj["success"] != null && Boolean.Parse(jobj["success"].ToString()) == true);
        }

        /// <summary>
        ///     Tests the draft of video ad as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        [TestMethod]
        public async Task DraftVideoAdAsync()
        {
            var request = new AdCreativeCreatingRequest
            {
                ObjectStorySpec = new ObjectStorySpec
                {
                    PageId = TestsBase.PageId,
                    VideoData = new VideoData
                    {
                        Call2Action = new Call2Action
                        {
                            Type = Call2ActionType.LEARN_MORE,
                            Value = new Call2ActionValue
                            {
                                Link = TestsBase.Link
                            }
                        },
                        Headline = "Headline of Video Ad With default Cover",
                        Message = Guid.NewGuid().ToString("N")
                    }
                }
            };

            var response = await AdCreativePublisher.PublishVideoAdAsync(TestsBase.TestVideoPath, request, TestsBase.AdAccountId, TestsBase.AccessToken, TestsBase.PageAccessToken, false);
            Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);

            var jobj = JObject.Parse(response.Data);
            Assert.IsTrue((jobj["video_id"] != null && !String.IsNullOrEmpty(jobj["video_id"].ToString()))
                || (jobj["id"] != null && !String.IsNullOrEmpty(jobj["id"].ToString())));
        }
    }
}
