using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Cron.Exceptions
{
    /// <summary>
    ///     Represents errors that occur during parsing cron expression.
    /// </summary>
    public class CronParserException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CronParserException"/> class.
        /// </summary>
        public CronParserException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CronParserException"/> class with a specified error 
        ///     message.
        /// </summary>
        /// <param name="message">
        ///     The message that describes the error.
        /// </param>
        public CronParserException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CronParserException"/> class with a specified error
        ///     message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">
        ///     The message that describes the error.
        /// </param>
        /// <param name="exception">
        ///     The exception that is the cause of the current exception, or a null reference
        ///     (Nothing in Visual Basic) if no inner exception is specified.
        /// </param>
        public CronParserException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
