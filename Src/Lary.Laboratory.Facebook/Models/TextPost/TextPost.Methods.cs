using Lary.Laboratory.Core.Models;
using Lary.Laboratory.Facebook.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lary.Laboratory.Facebook.Models
{
    /// <summary>
    ///     Facebook text post.
    /// </summary>
    public partial class TextPost : IPostable
    {
        /// <summary>
        ///     Publishes text post as an asynchronous operation.
        /// </summary>
        /// <param name="config">
        ///     <see cref="Config"/>
        /// </param>
        /// <returns>
        ///     <see cref="ResponseMessage{TResult}"/>
        /// </returns>
        public async Task<ResponseMessage<string>> PostAsync(Config config)
        {
            var post = new PagePost
            {
                Message = this.Message
            };

            if (this.ScheduledTime != default(DateTime))
            {
                post.Published = false;
                post.ScheduledTime = ScheduledTime;
            }

            return await post.PostAsync(config);
        }
    }
}
