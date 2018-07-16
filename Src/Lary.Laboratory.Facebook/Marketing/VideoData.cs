using Lary.Laboratory.Facebook.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Marketing
{
    /// <summary>
    ///     Ad creative video data.
    /// </summary>
    public class VideoData
    {
        /// <summary>
        ///     Call to action.
        /// </summary>
        [FacebookProperty("call_to_action")]
        [JsonProperty("call_to_action")]
        public Call2Action Call2Action { get; set; }

        /// <summary>
        ///     The caption of the video data.
        /// </summary>
        [FacebookProperty("caption")]
        [JsonProperty("caption")]
        public string Caption { get; set; }

        /// <summary>
        ///     The cover image url of video data.
        /// </summary>
        [FacebookProperty("image_url")]
        [JsonProperty("image_url")]
        public string Cover { get; set; }

        /// <summary>
        ///     The description of the video data.
        /// </summary>
        [FacebookProperty("description")]
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     The headline of the video data.
        /// </summary>
        [FacebookProperty("title")]
        [JsonProperty("title")]
        public string Headline { get; set; }

        /// <summary>
        ///     The message of the video data.
        /// </summary>
        [FacebookProperty("message")]
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        ///     The id of video on facebook.
        /// </summary>
        [FacebookProperty("video_id")]
        [JsonProperty("video_id")]
        public string VideoId { get; set; }
    }
}
