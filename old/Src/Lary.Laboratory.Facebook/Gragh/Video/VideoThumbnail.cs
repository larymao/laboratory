using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     Video thumbnail.
    /// </summary>
    public class VideoThumbnail
    {
        /// <summary>
        ///     Id of the thumbnail.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        ///     The height of this thumbnail in pixels.
        /// </summary>
        [JsonProperty("height")]
        public uint? Height { get; set; }

        /// <summary>
        ///     The scale of this thumbnail.
        /// </summary>
        [JsonProperty("scale")]
        public double? Scale { get; set; }

        /// <summary>
        ///     The uri of this thumbnail.
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; }

        /// <summary>
        ///     The width of this thumbnail in pixels.
        /// </summary>
        [JsonProperty("width")]
        public uint? Width { get; set; }

        /// <summary>
        ///     Indicates whether this thumbnail is preferred.
        /// </summary>
        [JsonProperty("is_preferred")]
        public bool? IsPreferred { get; set; }
    }
}
