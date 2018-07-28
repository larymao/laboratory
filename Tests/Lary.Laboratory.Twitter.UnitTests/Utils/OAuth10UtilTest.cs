using Lary.Laboratory.Twitter.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Lary.Laboratory.Twitter.UnitTests.Utils
{
    /// <summary>
    ///     Provides unit test methods for testing <see cref="OAuth10UtilTest"/>.
    /// </summary>
    [TestClass]
    public class OAuth10UtilTest
    {
        private Uri _uri;
        private Dictionary<string, string> _queries;
        private OAuth10Util _oauth;

        /// <summary>
        ///     Initializes basic data for <see cref="OAuth10UtilTest"/>.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _uri = new Uri("https://lary.me", UriKind.Absolute);
            _queries = new Dictionary<string, string>
            {
                { Guid.NewGuid().ToString("N").Remove(4), Guid.NewGuid().ToString("N").Remove(8) },
                { Guid.NewGuid().ToString("N").Remove(4), Guid.NewGuid().ToString("N").Remove(8) },
                { Guid.NewGuid().ToString("N").Remove(4), Guid.NewGuid().ToString("N").Remove(8) }
            };

            _oauth = new OAuth10Util(TestsBase.ConsumerKey, TestsBase.ConsumerSecret, TestsBase.AccessToken, TestsBase.AccessTokenSecret);
        }

        /// <summary>
        ///     The test for generating authorization string.
        /// </summary>
        [TestMethod]
        public void GenerateAuthorizationString()
        {
            var result = _oauth.GenerateAuthorizationString(_uri, HttpMethod.Get, _queries);
            Assert.IsFalse(String.IsNullOrEmpty(result));

            Console.WriteLine(result);
        }

        /// <summary>
        ///     The test for generating authorization headers.
        /// </summary>
        [TestMethod]
        public void GenerateAuthorizationHeaders()
        {
            var result = _oauth.GenerateAuthorizationHeaders(_uri, HttpMethod.Post, _queries);
            Assert.IsTrue(result != null || result.Count() > 0);

            Console.WriteLine(JsonConvert.SerializeObject(result));
        }
    }
}
