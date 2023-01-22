using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Logging
{
    /// <summary>
    /// Logging option.
    /// </summary>
    public class LoggingOption
    {
        /// <summary>
        /// custom enriches 
        /// </summary>
        public Dictionary<string, string> Enriches { get; set; } = new Dictionary<string, string>();
    }
}
