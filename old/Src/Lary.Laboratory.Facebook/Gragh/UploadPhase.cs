using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     The phase during chunked upload. Using during chunked upload.
    /// </summary>
    public enum UploadPhase
    {
        /// <summary>
        ///     Start.
        /// </summary>
        [Description("start")]
        Start,

        /// <summary>
        ///     Transfer.
        /// </summary>
        [Description("transfer")]
        Transfer,

        /// <summary>
        ///     Finish.
        /// </summary>
        [Description("finish")]
        Finish,

        /// <summary>
        ///     Cancel.
        /// </summary>
        [Description("cancel")]
        Cancel
    }
}
