using System.IO;

namespace Lary.Laboratory.Core.IO;

/// <summary>
/// Provides methods for file/directory path operations.
/// </summary>
public static class PathHelper
{
    /// <summary>
    /// Checks whether the file of the given path is a file.
    /// </summary>
    /// <param name="path">The path to be checked.</param>
    /// <returns><see langword="true"/> if the file is file; otherwise, false.</returns>
    public static bool IsFile(string path)
    {
        return !IsDirectory(path);
    }

    /// <summary>
    /// Checks whether the file of the given path is a directory.
    /// </summary>
    /// <param name="path">The path to be checked.</param>
    /// <returns><see langword="true"/> if the file is directory; otherwise, false.</returns>
    public static bool IsDirectory(string path)
    {
        return FileAttributes.Directory.CanMatchWith(path);
    }

    /// <summary>
    /// Checks whether the given file attribute can be matched with the file/directory of the given path.
    /// </summary>
    /// <param name="attr">File/Directory attribute.</param>
    /// <param name="path">The path to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if the attribute can be matched with the file/directory; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool CanMatchWith(this FileAttributes attr, string path)
    {
        var fileAttr = File.GetAttributes(path);

        return (fileAttr & attr) == attr;
    }
}
