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
        var fileAttr = new FileInfo(path).Attributes;

        return (int)fileAttr > 0 && !IsDirectory(fileAttr);
    }

    /// <summary>
    /// Checks whether the file of the given path is a directory.
    /// </summary>
    /// <param name="path">The path to be checked.</param>
    /// <returns><see langword="true"/> if the file is directory; otherwise, false.</returns>
    public static bool IsDirectory(string path)
    {
        var dirAttr = new DirectoryInfo(path).Attributes;

        return (int)dirAttr > 0 && IsDirectory(dirAttr);
    }

    private static bool IsDirectory(FileAttributes attr)
    {
        return (attr & FileAttributes.Directory) == FileAttributes.Directory;
    }
}
