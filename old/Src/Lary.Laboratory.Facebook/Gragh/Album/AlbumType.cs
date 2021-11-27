using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     The type of the <see cref="Album"/>.
    /// </summary>
    public enum AlbumType
    {
        /// <summary>
        ///     App.
        /// </summary>
        [Description("app")]
        App,

        /// <summary>
        ///     Cover.
        /// </summary>
        [Description("cover")]
        Cover,

        /// <summary>
        ///  Profile.
        /// </summary>
        [Description("profile")]
        Profile,

        /// <summary>
        ///     Mobile.
        /// </summary>
        [Description("mobile")]
        Mobile,

        /// <summary>
        ///     Wall.
        /// </summary>
        [Description("wall")]
        Wall,

        /// <summary>
        ///     Normal.
        /// </summary>
        [Description("normal")]
        Normal,

        /// <summary>
        ///     Album.
        /// </summary>
        [Description("album")]
        Album
    }
}
