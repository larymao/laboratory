using Lary.Laboratory.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lary.Laboratory.Core.Extensions
{
    /// <summary>
    ///     Provides extension methods for <see cref="HttpResponseMessage"/>.
    /// </summary>
    public static class HttpResponseMessageExtension
    {
        /// <summary>
        ///     Converts current instance to its equivalent <see cref="ResponseMessage{TResult}"/> 
        ///     representation as an asynchronous operation.
        ///     The <see cref="ResponseMessage{TResult}.Code"/> will be set to 0 if the 
        ///     <see cref="HttpResponseMessage.StatusCode"/> of httpResponse equals to 
        ///     <see cref="HttpStatusCode.OK"/>, otherwise, set to 1.
        /// </summary>
        /// <param name="httpResponse">
        ///     Current instance of <see cref="HttpResponseMessage"/>.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Throw if the parameter httpResponse is null.
        /// </exception>
        public static async Task<ResponseMessage<string>> ToResponseMessageAsync(this HttpResponseMessage httpResponse)
        {
            if (httpResponse == null)
            {
                throw new ArgumentNullException(nameof(httpResponse));
            }

            var result = new ResponseMessage<string>()
            {
                Code = httpResponse.StatusCode == HttpStatusCode.OK ? ResponseCode.SUCCESS : ResponseCode.UNKNOWN_ERROR,
                ReasonPhrase = $"{(int)httpResponse.StatusCode}, {httpResponse.ReasonPhrase}",
                Data = await httpResponse.Content.ReadAsStringAsync()
            };

            return result;
        }

        /// <summary>
        ///     Converts current instance to its equivalent <see cref="ResponseMessage{TResult}"/> 
        ///     representation as an asynchronous operation.
        ///     The <see cref="ResponseMessage{TResult}.Code"/> will be set to 0 if the 
        ///     <see cref="HttpResponseMessage.StatusCode"/> of httpResponse equals to 
        ///     <see cref="HttpStatusCode.OK"/>, otherwise, set to 1.
        /// </summary>
        /// <typeparam name="TResult">
        ///     The type of <see cref="ResponseMessage{TResult}.Data"/>.
        /// </typeparam>
        /// <param name="httpResponse">
        ///     Current instance of <see cref="HttpResponseMessage"/>.
        /// </param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Throw if the parameter httpResponse is null.
        /// </exception>
        public static async Task<ResponseMessage<TResult>> ToResponseMessageAsync<TResult>(this HttpResponseMessage httpResponse)
        {
            if (httpResponse == null)
            {
                throw new ArgumentNullException(nameof(httpResponse));
            }

            var result = new ResponseMessage<TResult>()
            {
                Code = httpResponse.StatusCode == HttpStatusCode.OK ? ResponseCode.SUCCESS : ResponseCode.UNKNOWN_ERROR,
                ReasonPhrase = $"{(int)httpResponse.StatusCode}, {httpResponse.ReasonPhrase}",
                Data = JsonConvert.DeserializeObject<TResult>(await httpResponse.Content.ReadAsStringAsync())
            };

            return result;
        }
    }
}
