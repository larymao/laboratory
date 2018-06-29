using Lary.Laboratory.Core.Models;
using Lary.Laboratory.Facebook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lary.Laboratory.Facebook.Interfaces
{
    /// <summary>
    ///     Provides a mechanism for publishing facebook post.
    /// </summary>
    public interface IPostable
    {
        /// <summary>
        ///     Publishes post as an asynchronous operation.
        /// </summary>
        /// <param name="config">
        ///     <see cref="Config"/>
        /// </param>
        /// <returns>
        ///     <see cref="ResponseMessage{TResult}"/>
        /// </returns>
        Task<ResponseMessage<string>> PostAsync(Config config);
    }
}
