using Lary.Laboratory.Core.Models;
using Lary.Laboratory.Facebook.Gragh;
using Lary.Laboratory.Facebook.Uploaders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Lary.Laboratory.Facebook.UnitTests.Uploaders
{
    /// <summary>
    ///     Provides unit test methods for testing <see cref="VideoUploader"/>.
    /// </summary>
    [TestClass]
    public class VideoUploaderTest
    {
        private VideoCreatingRequest _request;

        /// <summary>
        ///     Initializes basic data for <see cref="VideoUploaderTest"/>.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            var videoType = Path.GetExtension(TestsBase.TestVideoPath);

            _request = new VideoCreatingRequest(videoType)
            {
                Description = $"Description_{TestsBase.Context.TestName}_{Guid.NewGuid().ToString("N")}",
                Name = $"Name_{TestsBase.Context.TestName}_{Guid.NewGuid().ToString("N").Remove(16)}",
                Title = $"Title_{TestsBase.Context.TestName}_{Guid.NewGuid().ToString("N").Remove(8)}"
            };
        }

        /// <summary>
        ///     The test for uploading a video to facebook by non-resumable upload as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        [TestMethod]
        public async Task UploadAsync()
        {
            var response = await VideoUploader.UploadAsync(TestsBase.TestVideoPath, TestsBase.PageId, TestsBase.PageAccessToken, _request);
            Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);

            var jobj = JObject.Parse(response.Data);
            Assert.IsTrue(jobj["id"] != null && !String.IsNullOrEmpty(jobj["id"].ToString()));
        }

        /// <summary>
        ///     The test for uploading an ad video to facebook by non-resumable upload as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        [TestMethod]
        public async Task UploadAsAdVideoAsync()
        {
            var response = await VideoUploader.UploadAsAdVideoAsync(TestsBase.TestVideoPath, TestsBase.AdAccountId, TestsBase.AccessToken, _request);
            Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);

            var jobj = JObject.Parse(response.Data);
            Assert.IsTrue(jobj["id"] != null && !String.IsNullOrEmpty(jobj["id"].ToString()));
        }

        /// <summary>
        ///     The test for uploading a video to facebook by resumable upload as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        [TestMethod]
        public async Task UploadInChunksAsync()
        {
            var response = await VideoUploader.UploadInChunksAsync(TestsBase.TestVideoPath, TestsBase.PageId, TestsBase.PageAccessToken, _request);
            Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);

            var jobj = JObject.Parse(response.Data);
            Assert.IsTrue((jobj["success"] != null && Boolean.Parse(jobj["success"].ToString()) == true)
                && (jobj["video_id"] != null && !String.IsNullOrEmpty(jobj["video_id"].ToString())));
        }

        /// <summary>
        ///     The test for uploading an ad video to facebook by resumable upload as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        [TestMethod]
        public async Task UploadAsAdVideoInChunksAsync()
        {
            var response = await VideoUploader.UploadAsAdVideoInChunksAsync(TestsBase.TestVideoPath, TestsBase.AdAccountId, TestsBase.AccessToken, _request);
            Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);

            var jobj = JObject.Parse(response.Data);
            Assert.IsTrue((jobj["success"] != null && Boolean.Parse(jobj["success"].ToString()) == true)
                && (jobj["video_id"] != null && !String.IsNullOrEmpty(jobj["video_id"].ToString())));
        }
    }
}
