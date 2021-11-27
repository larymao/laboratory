using Lary.Laboratory.Core.Models;
using Lary.Laboratory.Twitter.Uploaders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lary.Laboratory.Twitter.UnitTests.Uploaders
{
    /// <summary>
    ///     Provides unit test methods for testing <see cref="MediaUploader"/>.
    /// </summary>
    [TestClass]
    public class MediaUploaderTest
    {
        private static MediaUploader _uploader;
        private static TestContext _testContext;

        /// <summary>
        ///     Initializes basic data for <see cref="MediaUploaderTest"/>.
        /// </summary>
        /// <param name="context">
        ///     An instance of <see cref="TestContext"/>.
        /// </param>
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            _testContext = context;
            _uploader = new MediaUploader(TestsBase.ConsumerKey, TestsBase.ConsumerSecret, TestsBase.AccessToken, TestsBase.AccessTokenSecret);
        }

        /// <summary>
        ///     The test for uploading a media to twitter in chunked upload as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        [TestMethod]
        public async Task UploadMediaInChunksAsync()
        {
            var response = await _uploader.UploadMediaInChunksAsync(TestsBase.MediaFilepath);
            Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);

            var jobj = JObject.Parse(response.Data);
            Assert.IsTrue(true);

            Console.WriteLine($"{_testContext.TestName} returns: {response.Data}");
        }
    }
}
