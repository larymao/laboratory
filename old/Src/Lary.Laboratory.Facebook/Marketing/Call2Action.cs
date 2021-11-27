using Lary.Laboratory.Facebook.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Marketing
{
    /// <summary>
    ///     Call to action of facebook ad creative.
    /// </summary>
    public class Call2Action
    {
        /// <summary>
        ///     The type of <see cref="Call2Action"/>.
        /// </summary>
        [FacebookProperty("type")]
        [JsonProperty("type")]
        public Call2ActionType Type { get; set; }

        /// <summary>
        ///     The value of <see cref="Call2Action"/>.
        /// </summary>
        [FacebookProperty("value")]
        [JsonProperty("value")]
        public Call2ActionValue Value { get; set; }
    }

    /// <summary>
    ///     The value of <see cref="Call2Action"/>.
    /// </summary>
    public class Call2ActionValue
    {
        /// <summary>
        ///     Link.
        /// </summary>
        [FacebookProperty("link")]
        [JsonProperty("link")]
        public string Link { get; set; }
    }
}
