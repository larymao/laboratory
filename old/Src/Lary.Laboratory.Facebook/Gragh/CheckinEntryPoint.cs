using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     The method that the user used to add a place tag to their story.
    /// </summary>
    public enum CheckinEntryPoint
    {
        /// <summary>
        ///     Branding other.
        /// </summary>
        [Description("BRANDING_OTHER")]
        BrandingOther,

        /// <summary>
        ///     Branding photo.
        /// </summary>
        [Description("BRANDING_PHOTO")]
        BrandingPhoto,

        /// <summary>
        ///     Branding status.
        /// </summary>
        [Description("BRANDING_STATUS")]
        BrandingStatus,

        /// <summary>
        ///     Branding checkin.
        /// </summary>
        [Description("BRANDING_CHECKIN")]
        BrandingCheckin
    }
}
