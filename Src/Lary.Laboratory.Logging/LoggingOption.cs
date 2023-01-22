using System.Collections.Generic;

namespace Lary.Laboratory.Logging
{
    /// <summary>
    /// Logging option.
    /// </summary>
    public class LoggingOption
    {
        /// <summary>
        /// Custom enriches.
        /// </summary>
        public Dictionary<string, string> Enriches { get; set; } = new Dictionary<string, string>();
    }
}
