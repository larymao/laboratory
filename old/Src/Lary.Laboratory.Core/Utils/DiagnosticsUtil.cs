using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lary.Laboratory.Core.Utils
{
    /// <summary>
    ///     Provides methods for diagnosing.
    /// </summary>
    public static class DiagnosticsUtil
    {
        /// <summary>
        ///     Traces the calling stack of current method. It may not works well in async method.
        /// </summary>
        /// <param name="keyword">
        ///     The keyword to filter.
        /// </param>
        /// <returns>
        ///     The method infos found in calling stack.
        /// </returns>
        public static IEnumerable<MethodBase> TraceMethods(string keyword)
        {
            var stack = new StackTrace();
            var frames = new StackTrace().GetFrames();
            var currentMethod = new StackFrame(0).GetMethod();
            var methods = frames.Select(aa =>
                    aa.GetMethod()
                )
                .Where(bb => (
                    bb.DeclaringType.FullName.Contains(keyword)
                    && bb.Name != currentMethod.Name // Filter current method.
                    && !bb.DeclaringType.FullName.Contains("++++")) // Filter methods created by system.
                );

            return methods;
        }

        /// <summary>
        ///     Traces the calling method of current method. It may not works well in async method.
        /// </summary>
        /// <param name="keyword">
        ///     The keyword to filter.
        /// </param>
        /// <param name="skipFrame">
        ///     The number of frame to skip.
        /// </param>
        /// <returns>
        ///     The traced method info.
        /// </returns>
        public static MethodBase TraceMethod(string keyword, int skipFrame = 0)
        {
            if (skipFrame < 0)
            {
                return null;
            }

            var methods = TraceMethods(keyword).ToList();
            methods.RemoveAt(0); // Remove current method.

            if (methods.Count() > skipFrame)
            {
                return methods[skipFrame];
            }

            return null;
        }
    }
}
