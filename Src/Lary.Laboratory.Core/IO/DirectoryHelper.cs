using System.IO;

namespace Lary.Laboratory.Core.IO;

/// <summary>
/// Provides methods for directory operations.
/// </summary>
public static class DirectoryHelper
{
    /// <summary>
    /// Copies entire direcotry recursively.
    /// </summary>
    /// <param name="srcPath">Source directory path.</param>
    /// <param name="destPath">Destination directory path.</param>
    public static void CopyRecursively(string srcPath, string destPath)
    {
        var srcDirs = Directory.GetDirectories(srcPath, "*", SearchOption.AllDirectories);
        var srcFiles = Directory.GetFiles(srcPath, "*.*", SearchOption.AllDirectories);

        destPath = Path.GetFullPath(destPath);
        Directory.CreateDirectory(destPath);

        foreach (var dirPath in srcDirs)
        {
            Directory.CreateDirectory(dirPath.Replace(srcPath, destPath));
        }

        foreach (var filePath in srcFiles)
        {
            File.Copy(filePath, filePath.Replace(srcPath, destPath), true);
        }
    }

    /// <summary>
    /// Detects whether the specified directory exist, if true, deletes it and, if indicated, any subdirectories
    /// and files in the directory.
    /// </summary>
    /// <param name="dirPath">The name of the directory to remove.</param>
    /// <param name="recursive">True to remove directories, subdirectories, and files in path; otherwise, false.</param>
    public static void DeleteIfExists(string dirPath, bool recursive)
    {
        if (Directory.Exists(dirPath))
        {
            Directory.Delete(dirPath, recursive);
        }
    }
}
