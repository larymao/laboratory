using Lary.Laboratory.Facebook.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     Facebook video creating response.
    /// </summary>
    public partial class VideoCreatingResponse
    {
        /// <summary>
        ///     ID.
        /// </summary>
        [FacebookProperty("id")]
        public string Id { get; set; }

        /// <summary>
        ///     Upload session id.
        /// </summary>
        [FacebookProperty("upload_session_id")]
        public string UploadSessionId { get; set; }

        /// <summary>
        ///     Video id.
        /// </summary>
        [FacebookProperty("video_id")]
        public string VideoId { get; set; }

        /// <summary>
        ///     Start offset.
        /// </summary>
        [FacebookProperty("start_offset")]
        public string StartOffset { get; set; }

        /// <summary>
        ///     End offset.
        /// </summary>
        [FacebookProperty("end_offset")]
        public string EndOffset { get; set; }

        /// <summary>
        ///     Success.
        /// </summary>
        [FacebookProperty("success")]
        public bool? Success { get; set; }

        /// <summary>
        ///     Skip upload.
        /// </summary>
        [FacebookProperty("skip_upload")]
        public bool? SkipUpload { get; set; }

        /// <summary>
        ///     Upload domain.
        /// </summary>
        [FacebookProperty("upload_domain")]
        public string UploadDomain { get; set; }

        /// <summary>
        ///     Region hint.
        /// </summary>
        [FacebookProperty("region_hint")]
        public string RegionHint { get; set; }

        /// <summary>
        ///     Transcode bit rate bps.
        /// </summary>
        [FacebookProperty("transcode_bit_rate_bps")]
        public string TranscodeBitRateBps { get; set; }

        /// <summary>
        ///     Transcode dimension.
        /// </summary>
        [FacebookProperty("transcode_dimension")]
        public string TranscodeDimension { get; set; }
    }
}
