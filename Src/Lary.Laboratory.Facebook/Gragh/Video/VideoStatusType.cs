using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     Status of a video.
    /// </summary>
    public enum VideoStatusType
    {
        /// <summary>
        ///     Status of uploaded, encoded, thumbnails extracted.
        /// </summary>
        [Description("ready")]
        Ready,

        /// <summary>
        ///     Not ready yet.
        /// </summary>
        [Description("processing")]
        Processing,

        /// <summary>
        ///     Processing failed.
        /// </summary>
        [Description("error")]
        Error
    }
}
