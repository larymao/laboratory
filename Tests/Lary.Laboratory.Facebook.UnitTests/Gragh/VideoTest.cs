using Lary.Laboratory.Core.Models;
using Lary.Laboratory.Core.Utils;
using Lary.Laboratory.Facebook.Gragh;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Lary.Laboratory.Facebook.UnitTests.Gragh
{
    /// <summary>
    ///     Provides unit test methods for testing <see cref="Video"/>.
    /// </summary>
    [TestClass]
    public class VideoTest
    {
        /// <summary>
        ///     Test for publishing a post that attached with a video to facebook as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        [TestMethod]
        public async Task PublishPostWithVideoAsync()
        {
            var videoType = Path.GetExtension(TestsBase.TestVideoPath);
            var bytes = File.ReadAllBytes(TestsBase.TestVideoPath);

            var post = new VideoCreatingRequest(videoType)
            {
                Source = bytes,
                FileSize = bytes.Length,
                Description = $"Description_{TestsBase.Context.TestName}_{Guid.NewGuid().ToString("N")}",
                Name = $"Name_{TestsBase.Context.TestName}_{Guid.NewGuid().ToString("N").Remove(16)}",
                Title = $"Title_{TestsBase.Context.TestName}_{Guid.NewGuid().ToString("N").Remove(8)}"
            };

            var response = await post.PostAsync(TestsBase.PageId, TestsBase.PageAccessToken);
            Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);

            var jobj = JObject.Parse(response.Data);
            Assert.IsTrue(jobj["id"] != null && !String.IsNullOrEmpty(jobj["id"].ToString()));
        }

        /// <summary>
        ///     Test for drafting a post that attached with a video to facebook as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        [TestMethod]
        public async Task DraftPostWithVideoAsync()
        {
            var videoType = Path.GetExtension(TestsBase.TestVideoPath);
            var bytes = File.ReadAllBytes(TestsBase.TestVideoPath);

            var post = new VideoCreatingRequest(videoType)
            {
                Source = bytes,
                FileSize = bytes.Length,
                Description = $"Description_{TestsBase.Context.TestName}_{Guid.NewGuid().ToString("N")}",
                Name = $"Name_{TestsBase.Context.TestName}_{Guid.NewGuid().ToString("N").Remove(16)}",
                Title = $"Title_{TestsBase.Context.TestName}_{Guid.NewGuid().ToString("N").Remove(8)}",
                Published = false,
                UnpublishedContentType = UnpublishedContentType.Draft
            };

            var response = await post.PostAsync(TestsBase.PageId, TestsBase.PageAccessToken);
            Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);

            var jobj = JObject.Parse(response.Data);
            Assert.IsTrue(jobj["id"] != null && !String.IsNullOrEmpty(jobj["id"].ToString()));
        }

        /// <summary>
        ///     Test for scheduling a post that attached with a video to facebook as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        [TestMethod]
        public async Task SchedulePostWithVideoAsync()
        {
            var videoType = Path.GetExtension(TestsBase.TestVideoPath);
            var bytes = File.ReadAllBytes(TestsBase.TestVideoPath);

            var post = new VideoCreatingRequest(videoType)
            {
                Source = bytes,
                FileSize = bytes.Length,
                Description = $"Description_{TestsBase.Context.TestName}_{Guid.NewGuid().ToString("N")}",
                Name = $"Name_{TestsBase.Context.TestName}_{Guid.NewGuid().ToString("N").Remove(16)}",
                Title = $"Title_{TestsBase.Context.TestName}_{Guid.NewGuid().ToString("N").Remove(8)}",
                Published = false,
                UnpublishedContentType = UnpublishedContentType.Scheduled,
                ScheduledPublishTime = DateTimeUtil.CountTimeStamp(DateTime.Now.AddHours(1))
            };

            var response = await post.PostAsync(TestsBase.PageId, TestsBase.PageAccessToken);
            Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);

            var jobj = JObject.Parse(response.Data);
            Assert.IsTrue(jobj["id"] != null && !String.IsNullOrEmpty(jobj["id"].ToString()));
        }
    }
}
