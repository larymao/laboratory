using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lary.Laboratory.Core.Helpers
{
    /// <summary>
    ///     Helper for method.
    /// </summary>
    public static class MethodHelper
    {
        /// <summary>
        ///     Get the name of current method.
        /// </summary>
        /// <returns>
        ///     The name of current method.
        /// </returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            StackTrace trace = new StackTrace();
            StackFrame frame = trace.GetFrame(1);

            return frame.GetMethod().Name;
        }
    }
}
