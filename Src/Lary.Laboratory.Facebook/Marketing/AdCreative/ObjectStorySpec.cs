using Lary.Laboratory.Facebook.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Marketing
{
    /// <summary>
    ///     Object story spec.
    /// </summary>
    public class ObjectStorySpec
    {
        /// <summary>
        ///     Page id.
        /// </summary>
        [FacebookProperty("page_id")]
        [JsonProperty("page_id")]
        public string PageId { get; set; }

        /// <summary>
        ///     Video data.
        /// </summary>
        [FacebookProperty("video_data")]
        [JsonProperty("video_data")]
        public VideoData VideoData { get; set; }

        /// <summary>
        ///     Link data.
        /// </summary>
        [FacebookProperty("link_data")]
        [JsonProperty("link_data")]
        public LinkData LinkData { get; set; }
    }
}
