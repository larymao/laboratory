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
    ///     Provides unit test methods for testing <see cref="Post"/>.
    /// </summary>
    [TestClass]
    public class PostTest
    {
        /// <summary>
        ///     Test for publishing a text only post to facebook as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        [TestMethod]
        public async Task PublishTextPostAsync()
        {
            var post = new Post
            {
                Message = Guid.NewGuid().ToString("N")
            };

            var response = await post.PublishAsync(TestsBase.PageId, TestsBase.PageAccessToken);
            Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);

            var jobj = JObject.Parse(response.Data);
            Assert.IsTrue(jobj["id"] != null && !String.IsNullOrEmpty(jobj["id"].ToString()));
        }

        /// <summary>
        ///     Test for drafting a post that attached with a link to facebook as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        [TestMethod]
        public async Task DraftPostWithLinkAsync()
        {
            var post = new Post
            {
                Message = Guid.NewGuid().ToString("N"),
                Link = TestsBase.Link,
                IsPublished = false
            };

            var response = await post.PublishAsync(TestsBase.PageId, TestsBase.PageAccessToken);
            Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);

            var jobj = JObject.Parse(response.Data);
            Assert.IsTrue(jobj["id"] != null && !String.IsNullOrEmpty(jobj["id"].ToString()));
        }
    }
}
