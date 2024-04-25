using System.IO;

namespace Lary.Laboratory.Core.IO;

/// <summary>
/// Provides methods for file operations.
/// </summary>
public static class FileHelper
{
    /// <summary>
    /// Detects whether the specified file exist, if true, deletes it.
    /// </summary>
    /// <param name="filePath">The name of the file to be deleted. Wildcard characters are not supported.</param>
    public static void DeleteIfExists(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    /// <summary>
    /// <para>Detects whether the specified file is unavailable because it is:</para>
    /// <para>still being written to</para>
    /// <para>or being processed by another thread</para>
    /// <para>or does not exist (has already been processed)</para>
    /// </summary>
    /// <param name="file">The file to be checked.</param>
    /// <returns><see langword="true"/> if the specified file is unavailable for reasons above; otherwise, <see langword="false"/>.</returns>
    public static bool IsLocked(this FileInfo file)
    {
        try
        {
            using var stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            stream.Close();
        }
        catch (IOException)
        {
            return true;
        }

        return false;
    }
}
