using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Models
{
    /// <summary>
    ///     The Ad attachments of facebook post.
    /// </summary>
    public class AdAttachments
    {
        /// <summary>
        ///     The Ad account id of user.
        /// </summary>
        [JsonProperty("adsaccount")]
        public string AdsAccount { get; set; }

        /// <summary>
        ///     The cover link address of video.
        /// </summary>
        [JsonProperty("videocover")]
        public string VideoCover { get; set; }

        /// <summary>
        ///     Headline.
        /// </summary>
        [JsonProperty("headline")]
        public string Headline { get; set; }

        /// <summary>
        ///     Description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     Caption.
        /// </summary>
        [JsonProperty("caption")]
        public string Caption { get; set; }

        /// <summary>
        ///     <see cref="Call2Action"/>
        /// </summary>
        [JsonProperty("call2action")]
        public Call2Action Action { get; set; }
    }
}
