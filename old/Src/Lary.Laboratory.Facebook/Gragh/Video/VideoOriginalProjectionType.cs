using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     Original Projection type of the video being uploaded.
    /// </summary>
    public enum VideoOriginalProjectionType
    {
        /// <summary>
        ///     Half equirectangular.
        /// </summary>
        [Description("half_equirectangular")]
        HalfEquirectangular,

        /// <summary>
        ///     Equiangular cubemap.
        /// </summary>
        [Description("equiangular_cubemap")]
        EquiangularCubemap,

        /// <summary>
        ///     Cubemap.
        /// </summary>
        [Description("cubemap")]
        Cubemap,

        /// <summary>
        ///     Equirectangular.
        /// </summary>
        [Description("equirectangular")]
        Equirectangular
    }
}
