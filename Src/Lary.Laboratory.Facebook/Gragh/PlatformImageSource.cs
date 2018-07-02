using Lary.Laboratory.Facebook.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     Image source and dimensions.
    /// </summary>
    public class PlatformImageSource
    {
        /// <summary>
        ///     Height of the image.
        /// </summary>
        [FacebookProperty("height")]
        public uint? Height { get; set; }

        /// <summary>
        ///     URI of the image.
        /// </summary>
        [FacebookProperty("source")]
        public string Source { get; set; }

        /// <summary>
        ///     Width of the image.
        /// </summary>
        [FacebookProperty("width")]
        public uint? Width { get; set; }
    }
}
