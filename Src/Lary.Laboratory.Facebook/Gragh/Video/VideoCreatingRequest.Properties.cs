using Lary.Laboratory.Facebook.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     Facebook video creating request.
    /// </summary>
    public partial class VideoCreatingRequest
    {
        /// <summary>
        ///     Everstore handle of wave animation used to burn audio story video.
        /// </summary>
        [FacebookProperty("audio_story_wave_animation_handle")]
        public string AutoStoryWaveAnimationHandle { get; set; }

        /// <summary>
        ///     Composer session id.
        /// </summary>
        [FacebookProperty("composer_session_id")]
        public string ComposerSessionId { get; set; }

        /// <summary>
        ///     Description.
        /// </summary>
        [FacebookProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     End offset.
        /// </summary>
        [FacebookProperty("end_offset")]
        public long? EndOffset { get; set; }

        /// <summary>
        ///     The size of the video file in bytes. Using during chunked upload.
        /// </summary>
        [FacebookProperty("file_size")]
        public long? FileSize { get; set; }

        /// <summary>
        ///     File url.
        /// </summary>
        [FacebookProperty("file_url")]
        public string FileUrl { get; set; }

        /// <summary>
        ///     Whether the single fisheye video is cropped or not.
        /// </summary>
        [FacebookProperty("fisheye_video_cropped")]
        public bool? FisheyeVideoCropped { get; set; }

        /// <summary>
        ///     The front z rotation in degrees on the single fisheye video.
        /// </summary>
        [FacebookProperty("front_z_rotation")]
        public double? FrontZRotation { get; set; }

        /// <summary>
        ///     Is explicit share.
        /// </summary>
        [FacebookProperty("is_explicit_share")]
        public bool? IsExplicitShare { get; set; }

        /// <summary>
        ///     <para/>Default value: false
        ///     <para/>manual privacy
        /// </summary>
        [FacebookProperty("manual_privacy")]
        public bool ManualPrivacy { get; set; } = false;

        /// <summary>
        ///     The name of the video in the library.
        /// </summary>
        [FacebookProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     The Open Graph action type.
        /// </summary>
        [FacebookProperty("og_action_type_id")]
        public string OgActionTypeId { get; set; }

        /// <summary>
        ///     The Open Graph icon.
        /// </summary>
        [FacebookProperty("og_icon_id")]
        public string OgIconId { get; set; }

        /// <summary>
        ///     The Open Graph object ID.
        /// </summary>
        [FacebookProperty("og_object_id")]
        public string OgObjectId { get; set; }

        /// <summary>
        ///     The Open Graph phrase.
        /// </summary>
        [FacebookProperty("og_phrase")]
        public string OgPhrase { get; set; }

        /// <summary>
        ///     The Open Graph suggestion.
        /// </summary>
        [FacebookProperty("og_suggestion_mechanism")]
        public string OgSuggestionMechanism { get; set; }

        /// <summary>
        ///     Original field of view of the source camera.
        /// </summary>
        [FacebookProperty("original_fov")]
        public long? OriginalFov { get; set; }

        /// <summary>
        ///     Original Projection type of the video being uploaded.
        /// </summary>
        [FacebookProperty("original_projection_type")]
        public VideoOriginalProjectionType? OriginalProjectionType { get; set; }

        /// <summary>
        ///     This metadata is used in clip reaction feature.
        /// </summary>
        [FacebookProperty("react_mode_metadata")]
        public string ReactModeMetadata { get; set; }

        /// <summary>
        ///     Referenced sticker id.
        /// </summary>
        [FacebookProperty("referenced_sticker_id")]
        public string ReferencedStickerId { get; set; }

        /// <summary>
        ///     An object required for slideshow video.
        /// </summary>
        [FacebookProperty("slideshow_spec")]
        public string SlideshowSpec { get; set; }

        /// <summary>
        ///     The local video file path.
        /// </summary>
        [FacebookProperty("source")]
        public byte[] Source { get; set; }

        /// <summary>
        ///     The start position in byte of the chunk that is being sent, inclusive. Used during chunked upload.
        /// </summary>
        [FacebookProperty("start_offset")]
        public long? StartOffset { get; set; }

        /// <summary>
        ///     Time since original post.
        /// </summary>
        [FacebookProperty("time_since_original_post")]
        public long? TimeSiceOriginalPost { get; set; }

        /// <summary>
        ///     The name of the video being uploaded. Must be less than 255 characters. Special characters may 
        ///     count as more than 1 character.
        /// </summary>
        [FacebookProperty("title")]
        public string Title { get; set; }

        /// <summary>
        ///     Unpubliashed content type.
        /// </summary>
        [FacebookProperty("unpublished_content_type")]
        public UnpublishedContentType? UnpublishedContentType { get; set; }

        /// <summary>
        ///     The phase during chunked upload. Using during chunked upload.
        /// </summary>
        [FacebookProperty("upload_phase")]
        public UploadPhase? UploadPhase { get; set; }

        /// <summary>
        ///     The session ID of this chunked upload. Using during chunked upload.
        /// </summary>
        [FacebookProperty("upload_session_id")]
        public string UploadSessionId { get; set; }

        /// <summary>
        ///     The chunk of the video, between start_offset and end_offset. Using during chunked upload.
        /// </summary>
        [FacebookProperty("video_file_chunk")]
        public byte[] VideoFileChunk { get; set; }
    }
}
