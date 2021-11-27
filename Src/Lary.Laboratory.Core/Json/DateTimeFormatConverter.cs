using Newtonsoft.Json.Converters;

namespace Lary.Laboratory.Core.Json
{
    /// <inheritdoc/>
    public class DateTimeFormatConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeFormatConverter"/> class.
        /// </summary>
        /// <param name="format">The date time format used when converting a date to and from JSON.</param>
        public DateTimeFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
