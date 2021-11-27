using Lary.Laboratory.Facebook.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     Video format.
    /// </summary>
    public class VideoFormat
    {
        /// <summary>
        ///     HTML to embed the video in this format.
        /// </summary>
        [FacebookProperty("embed_html")]
        public string EmbedHtml { get; set; }

        /// <summary>
        ///     The filter applied to this video format.
        /// </summary>
        [FacebookProperty("filter")]
        public string Filter { get; set; }

        /// <summary>
        ///     The height of the video in this format.
        /// </summary>
        [FacebookProperty("height")]
        public uint Height { get; set; }

        /// <summary>
        ///     The thumbnail for the video in this format.
        /// </summary>
        [FacebookProperty("picture")]
        public string Picture { get; set; }

        /// <summary>
        ///     The width of the video in this format.
        /// </summary>
        [FacebookProperty("width")]
        public uint Width { get; set; }
    }
}
