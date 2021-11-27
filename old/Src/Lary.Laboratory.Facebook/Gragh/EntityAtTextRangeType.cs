using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     Type of the <see cref="EntityAtTextRange"/> object.
    /// </summary>
    public enum EntityAtTextRangeType
    {
        /// <summary>
        ///     User.
        /// </summary>
        [Description("user")]
        User,

        /// <summary>
        ///     Page.
        /// </summary>
        [Description("page")]
        Page,

        /// <summary>
        ///     Event.
        /// </summary>
        [Description("event")]
        Event,

        /// <summary>
        ///     Group.
        /// </summary>
        [Description("group")]
        Group,

        /// <summary>
        ///     Application.
        /// </summary>
        [Description("application")]
        Application
    }
}
