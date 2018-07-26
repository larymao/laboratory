using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Lary.Laboratory.Facebook.UnitTests
{
    /// <summary>
    ///     Provides basic data for all tests of Lary.Laboratory.Facebook.UnitTests.
    /// </summary>
    [TestClass]
    public static class TestsBase
    {
        private static readonly string _testPicturePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Pictures", "Test Picture.jpg");
        private static readonly string _testVideoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Videos", "Test Video.mp4");

        /// <summary>
        ///     Indicates current test context;
        /// </summary>
        public static TestContext Context { get; private set; }

        /// <summary>
        ///     Indicates user's facebook id.
        /// </summary>
        public static string UserId { get; private set; }

        /// <summary>
        ///     Indicates facebook page id.
        /// </summary>
        public static string PageId { get; private set; }

        /// <summary>
        ///     Indicates user's ad account id.
        /// </summary>
        public static string AdAccountId { get; private set; }

        /// <summary>
        ///     Indicates user access token.
        /// </summary>
        public static string AccessToken { get; private set; }

        /// <summary>
        ///     Indicates page access token.
        /// </summary>
        public static string PageAccessToken { get; private set; }

        /// <summary>
        ///     Local file path of test picture.
        /// </summary>
        public static string TestPicturePath => _testPicturePath;

        /// <summary>
        ///     Local file path of test video.
        /// </summary>
        public static string TestVideoPath => _testVideoPath;

        /// <summary>
        ///     The link address for facebook post.
        /// </summary>
        public static string Link => "https://lary.me";

        /// <summary>
        ///     The url of an online picture.
        /// </summary>
        public static string OnlinePicture => "http://image.cdn.lary.me/wallpapers/rabbit.jpg";


        /// <summary>
        ///     Initializes global variables.
        /// </summary>
        /// <param name="context">
        ///     Indicates the test context.
        /// </param>
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            // Reads configs from embedded resource.
            var srcConfigPath = "Lary.Laboratory.Facebook.UnitTests.Assets.Documents.Configs.json";
            var streamConfig = Assembly.GetExecutingAssembly().GetManifestResourceStream(srcConfigPath);

            using (var sr = new StreamReader(streamConfig))
            {
                var strConfig = sr.ReadToEnd();
                var jobj = JObject.Parse(strConfig);

                // Initializes variables.
                Context = context;
                UserId = jobj["user_id"].ToString();
                PageId = jobj["page_id"].ToString();
                AdAccountId = jobj["ad_account_id"].ToString();
                AccessToken = jobj["access_token"].ToString();
                PageAccessToken = jobj["page_access_token"].ToString();
            }

            context.WriteLine($"TestsBase initialized.");
        }
    }
}
