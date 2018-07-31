using Lary.Laboratory.Core.Helpers;
using Lary.Laboratory.Core.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Lary.Laboratory.Twitter.UnitTests")]
namespace Lary.Laboratory.Twitter.Utils
{
    /// <summary>
    ///     Provides methods for twitter oauth(v1.0).
    /// </summary>
    internal class OAuth10Util
    {
        private readonly string _consumerKey;
        private readonly string _consumerSecret;
        private readonly string _accessToken;
        private readonly string _accessTokenSecret;

        /// <summary>
        ///     Initializes a new instance of the <see cref="OAuth10Util"/> class with consumerKey, consumerSecret, 
        ///     accessToken and accessTokenSecret.
        /// </summary>
        /// <param name="consumerKey">
        ///     Indicates consumer_key.
        /// </param>
        /// <param name="consumerSecret">
        ///     Indicates consumer_secret.
        /// </param>
        /// <param name="accessToken">
        ///     Indicates access_token.
        /// </param>
        /// <param name="accessTokenSecret">
        ///     Indicates access_token_secret.
        /// </param>
        public OAuth10Util(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
            _accessToken = accessToken;
            _accessTokenSecret = accessTokenSecret;
        }

        /// <summary>
        ///     Send an HTTP request with authorization as an asynchronous operation.
        /// </summary>
        /// <param name="request">
        ///     The HTTP request message to send.
        /// </param>
        /// <param name="queries">
        ///     The queries of HTTP request.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        public async Task<HttpResponseMessage> ClientAsync(HttpRequestMessage request, IEnumerable<KeyValuePair<string, string>> queries)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Connection.Add("Keep-Alive");

            var authInfo = GenerateAuthorizationString(request.RequestUri, request.Method, queries);
            request.Headers.Add("Authorization", authInfo);
            //request.Headers.Add("User-Agent", "Twitter/1.0.0.0");
            //request.Headers.ExpectContinue = false;
            //request.Headers.CacheControl = new CacheControlHeaderValue() { NoCache = true };
            //request.Version = new Version("1.1");
            //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return response;
        }

        /// <summary>
        ///     Send a GET request to the specified uri as an asynchronous operation.
        /// </summary>
        /// <param name="uri">
        ///     The uri to client.
        /// </param>
        /// <param name="queries">
        ///     The queries of HTTP request.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        public async Task<HttpResponseMessage> GetAsync(Uri uri, IEnumerable<KeyValuePair<string, string>> queries)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Connection.Add("Keep-Alive");
            
            var sb_query = new StringBuilder();

            foreach (var param in queries)
            {
                sb_query.Append($"&{param.Key}={param.Value}");
            }
            var query = sb_query.ToString().Trim(new[] { '&' });

            uri = new Uri($"{uri.BasicUri()}?{query}", UriKind.Absolute);
            var authInfo = GenerateAuthorizationString(uri, HttpMethod.Get, queries);
            client.DefaultRequestHeaders.Add("Authorization", authInfo);

            return await client.GetAsync(uri);
        }

        /// <summary>
        ///     Send a POST request to the specified uri as an asynchronous operation.
        /// </summary>
        /// <param name="uri">
        ///     The uri to client.
        /// </param>
        /// <param name="queries">
        ///     The queries of HTTP request.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        public async Task<HttpResponseMessage> PostAsync(Uri uri, IEnumerable<KeyValuePair<string, string>> queries)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Connection.Add("Keep-Alive");

            var sb_query = new StringBuilder();

            foreach (var param in queries)
            {
                sb_query.Append($"&{param.Key}={param.Value}");
            }
            var query = sb_query.ToString().Trim(new[] { '&' });

            uri = new Uri($"{uri.BasicUri()}?{query}", UriKind.Absolute);
            var authInfo = GenerateAuthorizationString(uri, HttpMethod.Post, queries);
            client.DefaultRequestHeaders.Add("Authorization", authInfo);

            return await client.PostAsync(uri, null);
        }

        /// <summary>
        ///     Send a POST request to the specified uri as an asynchronous operation.
        /// </summary>
        /// <param name="uri">
        ///     The uri to client.
        /// </param>
        /// <param name="queries">
        ///     The queries of HTTP request.
        /// </param>
        /// <param name="data">
        ///     The data to post.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        public async Task<HttpResponseMessage> PostAsync(Uri uri, IEnumerable<KeyValuePair<string, string>> queries, byte[] data)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Connection.Add("Keep-Alive");

            var sb_query = new StringBuilder();

            foreach (var param in queries)
            {
                sb_query.Append($"&{param.Key}={param.Value}");
            }
            var query = sb_query.ToString().Trim(new[] { '&' });

            uri = new Uri($"{uri.BasicUri()}?{query}", UriKind.Absolute);
            var authInfo = GenerateAuthorizationString(uri, HttpMethod.Post, queries);
            client.DefaultRequestHeaders.Add("Authorization", authInfo);

            var chunkContent = new StreamContent(new MemoryStream(data));
            chunkContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            chunkContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            chunkContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("name", "media"));

            var content = new MultipartFormDataContent
            {
                chunkContent
            };

            return await client.PostAsync(uri, content);
        }

        /// <summary>
        ///     Generates oauth header value for HTTP request.
        /// </summary>
        /// <param name="uri">
        ///     The uri to client.
        /// </param>
        /// <param name="httpMethod">
        ///     An instance of <see cref="HttpMethod"/>.
        /// </param>
        /// <param name="queries">
        ///     The queries of HTTP request.
        /// </param>
        /// <returns>
        ///     A string that represents the authorization string.
        /// </returns>
        public string GenerateAuthorizationString(Uri uri, HttpMethod httpMethod, IEnumerable<KeyValuePair<string, string>> queries)
        {
            var sb = new StringBuilder();
            var additionals = GenerateOrderedAdditionalSignatureParams();
            var signatureParams = MergeOrderedSignatureParams(queries, additionals);
            var authKeyParams = GenerateAuthKeyParams();
            var signature = GenerateSignature(uri, httpMethod, signatureParams, authKeyParams);

            foreach (var item in additionals)
            {
                sb.Append($",{item.Key}=\"{item.Value}\"");
            }
            sb.Append($",oauth_signature=\"{signature}\"");

            var headerStr = $"OAuth {sb.ToString().Trim(new[] { ',' })}";
            return headerStr;
        }

        /// <summary>
        ///     Generates oauth headers for request.
        /// </summary>
        /// <param name="uri">
        ///     The uri to client.
        /// </param>
        /// <param name="httpMethod">
        ///     An instance of <see cref="HttpMethod"/>.
        /// </param>
        /// <param name="queries">
        ///     The queries of HTTP request.
        /// </param>
        /// <returns>
        ///     An <see cref="IEnumerable{T}"/> that contains the oauth headers for request.
        /// </returns>
        public IEnumerable<KeyValuePair<string, string>> GenerateAuthorizationHeaders(Uri uri, HttpMethod httpMethod, IEnumerable<KeyValuePair<string, string>> queries)
        {
            var sb = new StringBuilder();
            var additionals = GenerateOrderedAdditionalSignatureParams().ToList();
            var signatureParams = MergeOrderedSignatureParams(queries, additionals);
            var authKeyParams = GenerateAuthKeyParams();
            var signature = GenerateSignature(uri, httpMethod, signatureParams, authKeyParams);

            additionals.Add(new KeyValuePair<string, string>("oauth_signature", signature));

            return additionals;
        }


        /// <summary>
        ///     Generates oauth signature string.
        /// </summary>
        /// <param name="uri">
        ///     The uri to client.
        /// </param>
        /// <param name="httpMethod">
        ///     An instance of <see cref="HttpMethod"/>.
        /// </param>
        /// <param name="signatureParams">
        ///     Ordered signature parameters for oauth.
        /// </param>
        /// <param name="authKeyParams">
        ///     The oauth keys.
        /// </param>
        /// <returns>
        ///     A string that represents the signature for oauth.
        /// </returns>
        private string GenerateSignature(Uri uri, HttpMethod httpMethod, IEnumerable<KeyValuePair<string, string>> signatureParams, IEnumerable<KeyValuePair<string, string>> authKeyParams)
        {
            // 1. Params.
            var orderedSignParams = signatureParams.OrderBy(aa => aa.Key).ToList(); // Ascending Order.
            var orderedAuthKeyParams = authKeyParams.OrderBy(aa => aa.Key).ToList();

            var sb_urlParams = new StringBuilder();
            foreach (var param in orderedSignParams)
            {
                sb_urlParams.Append($"&{param.Key}={param.Value}");
            }

            var url = uri.BasicUri(); 
            var urlParamsStr = sb_urlParams.ToString().Trim(new[] { '&' });

            var oAuthRequest = $"{httpMethod.Method}&{CryptoUtil.UrlEncode(url)}&{CryptoUtil.UrlEncode(urlParamsStr)}";

            // 2. Auth key.
            var sb_authKeyParams = new StringBuilder();
            foreach (var key in orderedAuthKeyParams)
            {
                sb_authKeyParams.Append($"&{CryptoUtil.UrlEncode(key.Value)}");
            }
            var oAuthSecretkey = sb_authKeyParams.ToString().Trim(new[] { '&' });

            // 3. Signature.
            var signature = CryptoUtil.UrlEncode(CryptoUtil.Base64Encode(CryptoUtil.HMACSHA1(oAuthRequest, oAuthSecretkey, Encoding.UTF8)));

            return signature;
        }

        /// <summary>
        ///     Merges the additional signature parameters and the queries of the request, as a result, A sorted 
        ///     <see cref="IEnumerable{T}"/> that contains all signature parameters will be created.
        /// </summary>
        /// <param name="queries">
        ///     The queries of HTTP request.
        /// </param>
        /// <param name="additionals">
        ///     An <see cref="IEnumerable{T}"/> that contains ordered additional signature parameters for oauth.
        ///     If it is set to null or empty, the default additional signature parameters will be created automaticly.
        /// </param>
        /// <returns>
        ///     An <see cref="IEnumerable{T}"/> that contains ordered signature parameters for oauth.
        /// </returns>
        private IEnumerable<KeyValuePair<string, string>> MergeOrderedSignatureParams(IEnumerable<KeyValuePair<string, string>> queries, IEnumerable<KeyValuePair<string, string>> additionals = null)
        {
            List<KeyValuePair<string, string>> signatures;

            if (additionals == null || additionals.Count() == 0)
            {
                signatures = GenerateOrderedAdditionalSignatureParams().ToList();
            }
            else
            {
                signatures = additionals.OrderBy(aa => aa.Key).ToList();
            }

            foreach (var param in queries)
            {
                signatures.Add(param);
            }

            return signatures.OrderBy(aa => aa.Key);
        }

        /// <summary>
        ///     Generates additional signature parameters for oauth and sort the result by the key in ascending order.
        /// </summary>
        /// <returns>
        ///     An <see cref="IEnumerable{T}"/> that contains ordered additional signature parameters for oauth.
        /// </returns>
        private IEnumerable<KeyValuePair<string, string>> GenerateOrderedAdditionalSignatureParams()
        {
            var cultureInfo = CultureInfo.InvariantCulture;
            var oauth_nonce = new Random().Next(1_234_567, 9_000_000).ToString(cultureInfo);
            var oauth_timestamp = DateTimeUtil.CountTimeStamp(DateTime.Now).ToString(cultureInfo);

            var additional = new Dictionary<string, string>
            {
                ["oauth_nonce"] = oauth_nonce, // Random number.
                ["oauth_timestamp"] = oauth_timestamp, // Time stamp.
                ["oauth_version"] = "1.0", // OAuth version.
                ["oauth_signature_method"] = "HMAC-SHA1", // Signature method. ({ HMAC-SHA1, RSA-SHA1, PLAINTEXT })
                ["oauth_consumer_key"] = _consumerKey, // Consumer key.
                ["oauth_token"] = _accessToken // Token.
            };

            return additional.OrderBy(aa => aa.Key);
        }

        /// <summary>
        ///     Generates oauth keys.
        /// </summary>
        /// <returns>
        ///     An <see cref="IEnumerable{T}"/> that contains oauth keys.
        /// </returns>
        private IEnumerable<KeyValuePair<string, string>> GenerateAuthKeyParams()
        {
            var authKeys = new Dictionary<string, string>
            {
                ["oauth_consumer_secret"] = _consumerSecret,
                ["oauth_token_secret"] = _accessTokenSecret
            };

            return authKeys;
        }
    }
}
