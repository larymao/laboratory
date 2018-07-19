using Lary.Laboratory.Facebook.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Marketing
{
    /// <summary>
    ///     Facebook ad creative creating request.
    /// </summary>
    public partial class AdCreativeCreatingRequest
    {
        /// <summary>
        ///     Object of <see cref="Marketing.ObjectStorySpec"/>.
        /// </summary>
        [FacebookProperty("object_story_spec")]
        [JsonProperty("object_story_spec")]
        public ObjectStorySpec ObjectStorySpec { get; set; }
    }
}
