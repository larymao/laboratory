using Lary.Laboratory.Facebook.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Marketing
{
    /// <summary>
    ///     Ad creative link data.
    /// </summary>
    public class LinkData
    {
        /// <summary>
        ///     Call to action.
        /// </summary>
        [FacebookProperty("call_to_action")]
        [JsonProperty("call_to_action")]
        public Call2Action Call2Action { get; set; }

        /// <summary>
        ///     The caption of the link data.
        /// </summary>
        [FacebookProperty("caption")]
        [JsonProperty("caption")]
        public string Caption { get; set; }

        /// <summary>
        ///     The description of the link data.
        /// </summary>
        [FacebookProperty("description")]
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     The headline of the video data.
        /// </summary>
        [FacebookProperty("name")]
        [JsonProperty("name")]
        public string Headline { get; set; }

        /// <summary>
        ///     Link address.
        /// </summary>
        [FacebookProperty("link")]
        [JsonProperty("link")]
        public string Link { get; set; }

        /// <summary>
        ///     The message of the link data.
        /// </summary>
        [FacebookProperty("message")]
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        ///     A link to the picture on facebook.
        /// </summary>
        [FacebookProperty("picture")]
        [JsonProperty("picture")]
        public string Picture { get; set; }
    }
}
