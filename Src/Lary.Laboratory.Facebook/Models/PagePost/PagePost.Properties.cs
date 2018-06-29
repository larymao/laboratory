using Lary.Laboratory.Core.Utils;
using Lary.Laboratory.Facebook.Attributes;
using Lary.Laboratory.Facebook.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace Lary.Laboratory.Facebook.Models
{
    /// <summary>
    ///     Facebook normal page post.
    /// </summary>
    public partial class PagePost
    {
        /// <summary>
        ///     The text message of normal page post.
        /// </summary>
        [FacebookProperty("message")]
        public string Message { get; set; }

        /// <summary>
        ///     The link address the page post links to.
        /// </summary>
        [FacebookProperty("link")]
        public string Link { get; set; }

        /// <summary>
        ///     The picture link address of normal page post.
        /// </summary>
        [FacebookProperty("picture")]
        public string Picture { get; set; }

        /// <summary>
        ///     The thumbnail of normal page post.
        /// </summary>
        [FacebookProperty("thumbnail")]
        public string Thumbnail { get; set; }

        /// <summary>
        ///     The video id of normal page post.
        /// </summary>
        [FacebookProperty("source")]
        public string Video { get; set; }

        /// <summary>
        ///     The Headline of normal page post.
        /// </summary>
        [FacebookProperty("name")]
        public string Headline { get; set; }

        /// <summary>
        ///     The description of normal page post.
        /// </summary>
        [FacebookProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     The caption of normal page post.
        /// </summary>
        [FacebookProperty("caption")]
        public string Caption { get; set; }

        /// <summary>
        ///     The call to action of normal page post.
        /// </summary>
        [FacebookProperty("call_to_action")]
        public Call2Action Action { get; set; }

        /// <summary>
        ///     Indicate whether post should be published.
        /// </summary>
        [FacebookProperty("published")]
        public bool? Published { get; set; }

        /// <summary>
        ///     The schedule publish time of post. Time zone should be considered.
        /// </summary>
        [FacebookProperty("scheduled_publish_time")]
        public DateTime ScheduledTime { get; set; }
    }
}
