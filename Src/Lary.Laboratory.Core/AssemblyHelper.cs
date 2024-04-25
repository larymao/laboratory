using System;
using System.IO;
using System.Reflection;

namespace Lary.Laboratory.Core;

/// <summary>
/// Provides methods for assembly.
/// </summary>
public static class AssemblyHelper
{
    /// <summary>
    /// RReturns the parent directory information for the specified assembly.
    /// </summary>
    /// <param name="assembly">Assembly information.</param>
    /// <returns>The path of the parent directory of the given assembly.</returns>
    public static string GetDirectoryName(this Assembly assembly)
    {
        var codeBase = assembly.CodeBase;
        var uri = new UriBuilder(codeBase);
        var path = Uri.UnescapeDataString(uri.Path);
        return Path.GetDirectoryName(path);
    }
}
