using Lary.Laboratory.Core.Models;
using Lary.Laboratory.Facebook.Uploaders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lary.Laboratory.Facebook.UnitTests.Uploaders
{
    /// <summary>
    ///     Provides unit test methods for testing <see cref="PhotoUploader"/>.
    /// </summary>
    [TestClass]
    public class PhotoUploaderTest
    {
        /// <summary>
        ///     The test for uploading a picture to facebook as an asynchronous opertion.
        /// </summary>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        [TestMethod]
        public async Task UploadAsync()
        {
            var message = Guid.NewGuid().ToString("N");

            var response = await PhotoUploader.UploadAsync(TestsBase.TestPicturePath, TestsBase.PageId, message, TestsBase.PageAccessToken);
            Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);

            var jobj = JObject.Parse(response.Data);
            Assert.IsTrue((jobj["post_id"] != null && !String.IsNullOrEmpty(jobj["post_id"].ToString()))
                && (jobj["id"] != null && !String.IsNullOrEmpty(jobj["id"].ToString())));
        }

        /// <summary>
        ///     The test for uploading an ad photo to facebook as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        [TestMethod]
        public async Task UploadAsAdPhotoAsync()
        {
            var response = await PhotoUploader.UploadAsAdPhotoAsync(TestsBase.TestPicturePath, TestsBase.AdAccountId, TestsBase.AccessToken);
            Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);

            var jobj = JObject.Parse(response.Data);
            Assert.IsTrue(jobj["images"] != null);
        }
    }
}
