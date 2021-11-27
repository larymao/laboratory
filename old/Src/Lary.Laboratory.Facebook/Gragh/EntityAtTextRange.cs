using Lary.Laboratory.Facebook.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     Location and identity of an object in some source text.
    /// </summary>
    public class EntityAtTextRange
    {
        /// <summary>
        /// ID of the profile.
        /// </summary>
        [FacebookProperty("id")]
        public string Id { get; set; }

        /// <summary>
        ///     Number of characters in the text indicating the object.
        /// </summary>
        [FacebookProperty("length")]
        public uint? Length { get; set; }

        /// <summary>
        ///     Name of the object.
        /// </summary>
        [FacebookProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     The object itself.
        /// </summary>
        [FacebookProperty("object")]
        public object Object { get; set; }

        /// <summary>
        ///     The character offset in the source text of the text indicating the object.
        /// </summary>
        [FacebookProperty("offset")]
        public uint? Offset { get; set; }

        /// <summary>
        ///     Type of the object.
        /// </summary>
        [FacebookProperty("type")]
        public EntityAtTextRangeType? Type { get; set; }
    }
}
