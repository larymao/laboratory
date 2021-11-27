using Lary.Laboratory.Facebook.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     <para/>Location node used with other objects in the Graph API. 
    ///     <para/>Get the city and state of a user, more information about a location, or location information about a page.
    /// </summary>
    public class Location
    {
        /// <summary>
        ///     City.
        /// </summary>
        [FacebookProperty("city")]
        public string City { get; set; }

        /// <summary>
        ///     City ID. Any city identified is also a city you can use for targeting ads.
        /// </summary>
        [FacebookProperty("city_id")]
        public uint? CityId { get; set; }

        /// <summary>
        ///     Country.
        /// </summary>
        [FacebookProperty("country")]
        public string Country { get; set; }
        
        /// <summary>
        ///     Country code.
        /// </summary>
        [FacebookProperty("country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        ///     Latitude.
        /// </summary>
        [FacebookProperty("latitude")]
        public double? Latitude { get; set; }

        /// <summary>
        ///     The parent location if this location is located within another location.
        /// </summary>
        [FacebookProperty("located_in")]
        public string LocatedIn { get; set; }

        /// <summary>
        ///     Longitude.
        /// </summary>
        [FacebookProperty("longitude")]
        public double? Longitude { get; set; }

        /// <summary>
        ///     Name.
        /// </summary>
        [FacebookProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Region.
        /// </summary>
        [FacebookProperty("region")]
        public string Region { get; set; }

        /// <summary>
        ///     Region ID. Specifies a geographic region, such as California. An identified region is the same as one 
        ///     you can use to target ads.
        /// </summary>
        [FacebookProperty("region_id")]
        public uint? RegionId { get; set; }

        /// <summary>
        ///     State.
        /// </summary>
        [FacebookProperty("state")]
        public string State { get; set; }

        /// <summary>
        ///     Street.
        /// </summary>
        [FacebookProperty("street")]
        public string Street { get; set; }

        /// <summary>
        ///     Zip code.
        /// </summary>
        [FacebookProperty("zip")]
        public string Zip { get; set; }
    }
}
