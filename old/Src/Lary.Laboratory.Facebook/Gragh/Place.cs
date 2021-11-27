using Lary.Laboratory.Facebook.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     Indicates a place.
    /// </summary>
    public class Place
    {
        /// <summary>
        ///     ID.
        /// </summary>
        [FacebookProperty("id")]
        public string Id { get; set; }

        /// <summary>
        ///     Location of Place.
        /// </summary>
        [FacebookProperty("location")]
        public Location Location { get; set; }

        /// <summary>
        ///     Name.
        /// </summary>
        [FacebookProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Overall Rating of Place, on a 5-star scale. 0 means not enough data to get a combined rating.
        /// </summary>
        [FacebookProperty("overall_rating")]
        public double? OverallRating { get; set; }
    }
}
