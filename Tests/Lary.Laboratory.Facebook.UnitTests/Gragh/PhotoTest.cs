using Lary.Laboratory.Core.Models;
using Lary.Laboratory.Core.Utils;
using Lary.Laboratory.Facebook.Gragh;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lary.Laboratory.Facebook.UnitTests.Gragh
{
    /// <summary>
    ///     Provides unit test methods for testing <see cref="Photo"/>.
    /// </summary>
    [TestClass]
    public class PhotoTest
    {
        /// <summary>
        ///     Test for posting a post that attached with an online picture to facebook as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        [TestMethod]
        public async Task PublishPostWithOnlinePictureAsync()
        {
            var post = new PhotoCreatingRequest
            {
                Caption = Guid.NewGuid().ToString("N"),
                Url = TestsBase.OnlinePicture
            };

            var response = await post.PublishAsync(TestsBase.PageId, TestsBase.PageAccessToken);
            Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);

            var jobj = JObject.Parse(response.Data);
            Assert.IsTrue(jobj["id"] != null && !String.IsNullOrEmpty(jobj["id"].ToString()));
        }

        /// <summary>
        ///     Test for drafting a post that attached with an online picture to facebook as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        [TestMethod]
        public async Task DraftPostWithOnlinePictureAsync()
        {
            var post = new PhotoCreatingRequest
            {
                Caption = Guid.NewGuid().ToString("N"),
                Url = TestsBase.OnlinePicture,
                Published = false,
                UnpublishedContentType = UnpublishedContentType.Draft
            };

            var response = await post.PublishAsync(TestsBase.PageId, TestsBase.PageAccessToken);
            Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);

            var jobj = JObject.Parse(response.Data);
            Assert.IsTrue(jobj["id"] != null && !String.IsNullOrEmpty(jobj["id"].ToString()));
        }

        /// <summary>
        ///     Test for scheduling a post that attached with an online picture to facebook as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        [TestMethod]
        public async Task SchedulePostWithOnlinePictureAsync()
        {
            var post = new PhotoCreatingRequest
            {
                Caption = Guid.NewGuid().ToString("N"),
                Url = TestsBase.OnlinePicture,
                Published = false,
                UnpublishedContentType = UnpublishedContentType.Scheduled,
                ScheduledPublishTime = DateTimeUtil.CountTimeStamp(DateTime.Now.AddHours(1))
            };

            var response = await post.PublishAsync(TestsBase.PageId, TestsBase.PageAccessToken);
            Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);

            var jobj = JObject.Parse(response.Data);
            Assert.IsTrue(jobj["id"] != null && !String.IsNullOrEmpty(jobj["id"].ToString()));
        }
    }
}
