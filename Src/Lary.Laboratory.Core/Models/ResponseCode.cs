using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Core.Models
{
    /// <summary>
    ///     Contains the values of status codes defined for <see cref="ResponseMessage{TResult}"/>
    /// </summary>
    public static class ResponseCode
    {
        /// <summary>
        ///     Success.
        /// </summary>
        public const int SUCCESS = 0;

        /// <summary>
        ///     Unknown error.
        /// </summary>
        public const int UNKNOWN_ERROR = 1;
    }
}
