using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Lary.Laboratory.Twitter.UnitTests
{
    /// <summary>
    ///     Provides basic data for all tests of Lary.Laboratory.Twitter.UnitTests.
    /// </summary>
    [TestClass]
    public static class TestsBase
    {
        /// <summary>
        ///     Indicates current test context;
        /// </summary>
        public static TestContext Context { get; private set; }

        /// <summary>
        ///     Indicates consumer_key.
        /// </summary>
        public static string ConsumerKey { get; private set; }

        /// <summary>
        ///     Indicates consumer_secret.
        /// </summary>
        public static string ConsumerSecret { get; private set; }

        /// <summary>
        ///     Indicates access_token.
        /// </summary>
        public static string AccessToken { get; private set; }

        /// <summary>
        ///     Indicates access_token_secret.
        /// </summary>
        public static string AccessTokenSecret { get; private set; }

        /// <summary>
        ///     Indicates the filepath of the test media.
        /// </summary>
        public static string MediaFilepath { get; private set; }


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
            var srcConfigPath = "Lary.Laboratory.Twitter.UnitTests.Assets.Documents.Configs.json";
            var streamConfig = Assembly.GetExecutingAssembly().GetManifestResourceStream(srcConfigPath);

            using (var sr = new StreamReader(streamConfig))
            {
                var strConfig = sr.ReadToEnd();
                var jobj = JObject.Parse(strConfig);

                // Initializes variables.
                Context = context;
                ConsumerKey = jobj["consumer_key"].ToString();
                ConsumerSecret = jobj["consumer_secret"].ToString();
                AccessToken = jobj["access_token"].ToString();
                AccessTokenSecret = jobj["access_token_secret"].ToString();
                MediaFilepath = jobj["media_filepath"].ToString();
            }

            context.WriteLine($"TestsBase initialized.");
        }
    }
}
