using Lary.Laboratory.Facebook.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     Status attributes of video.
    /// </summary>
    public class VideoStatus
    {
        /// <summary>
        ///     Video processing progress in percent [int 0 to 100].
        /// </summary>
        [FacebookProperty("processing_progress")]
        public uint ProcessingProgress { get; set; }

        /// <summary>
        ///     Status of a video, either "ready" (uploaded, encoded, thumbnails extracted), 
        ///     "processing" (not ready yet) or "error" (processing failed).
        /// </summary>
        [FacebookProperty("video_status")]
        public VideoStatus Status { get; set; }
    }
}
