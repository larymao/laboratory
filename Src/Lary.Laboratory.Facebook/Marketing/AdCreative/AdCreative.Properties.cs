using Lary.Laboratory.Facebook.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Marketing
{
    /// <summary>
    ///     An ad creative is an object that contains all the data for visually rendering the ad itself. 
    /// </summary>
    public partial class AdCreative
    {
        /// <summary>
        ///     The id of ad creative.
        /// </summary>
        [JsonProperty("id")]
        [FacebookProperty("id")]
        public string Id { get; set; }
    }
}
