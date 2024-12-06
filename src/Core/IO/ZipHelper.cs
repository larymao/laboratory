using System.IO.Compression;

namespace Lary.Laboratory.Core.IO;

/// <summary>
/// Provides methods for file compressing.
/// </summary>
public static class ZipHelper
{
    /// <summary>
    /// Compresses file or directory.
    /// </summary>
    /// <param name="srcPath">The file/directory to be compressed.</param>
    /// <param name="force">
    /// Sets to <see langword="true"/> to replace the file with the same name with the output zip file; otherwise,
    /// <see langword="false"/>.
    /// </param>
    /// <returns>The path of the output zip file.</returns>
    public static string Compress(string srcPath, bool force = false)
    {
        var zipPath = PathHelper.IsFile(srcPath)
            ? Path.ChangeExtension(srcPath, ".zip")
            : $"{srcPath}.zip";

        Compress(srcPath, zipPath, force);

        return zipPath;
    }

    /// <summary>
    /// Compresses file or directory.
    /// </summary>
    /// <param name="srcPath">The file/directory to be compressed.</param>
    /// <param name="zipPath">The path of the output zip file.</param>
    /// <param name="force">
    /// Sets to <see langword="true"/> to replace the file with the same name with the output zip file; otherwise,
    /// <see langword="false"/>.
    /// </param>
    public static void Compress(string srcPath, string zipPath, bool force = false)
        => Compress([srcPath], zipPath, force);

    /// <summary>
    /// Compresses multiple files or directories.
    /// </summary>
    /// <param name="srcPaths">The files and directories to be compressed.</param>
    /// <param name="zipPath">The path of the output zip file.</param>
    /// <param name="force">
    /// Sets to <see langword="true"/> to replace the file with the same name with the output zip file; otherwise,
    /// <see langword="false"/>.
    /// </param>
    public static void Compress(IEnumerable<string> srcPaths, string zipPath, bool force = false)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(zipPath));
        if (force && File.Exists(zipPath))
            File.Delete(zipPath);

        var count = srcPaths?.Count() ?? 0;
        Action action = (count, srcPaths) switch
        {
            (0, _) => () => CreateEmptyPack(zipPath),
            (1, _) => () => CompressSingleSource(srcPaths.First(), zipPath, true),
            (_, var p) when p.All(PathHelper.IsFile) => () => CompressFiles(p!, zipPath),
            _ => () => CompressMixtures(srcPaths!, zipPath)
        };

        action.Invoke();
    }

    private static void CreateEmptyPack(string zipPath)
    {
        using var _ = ZipFile.Open(zipPath, ZipArchiveMode.Create);
    }

    private static void CompressSingleSource(string srcPath, string zipPath, bool includeBaseDir)
    {
        if (PathHelper.IsDirectory(srcPath))
        {
            ZipFile.CreateFromDirectory(srcPath, zipPath, CompressionLevel.Optimal, includeBaseDir);
            return;
        }

        using var zip = ZipFile.Open(zipPath, ZipArchiveMode.Create);
        zip.CreateEntryFromFile(srcPath, Path.GetFileName(srcPath));
    }

    private static void CompressFiles(IEnumerable<string> filePaths, string zipPath)
    {
        using var zip = ZipFile.Open(zipPath, ZipArchiveMode.Create);

        foreach (var filePath in filePaths)
            zip.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
    }

    private static void CompressMixtures(IEnumerable<string> srcPaths, string zipPath)
    {
        // 1. copies the selected files and directories to a new temp directory
        // 2. compresses the temp directory
        var tempDirName = $"{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid():N}";
        Directory.CreateDirectory(tempDirName);

        foreach (var path in srcPaths)
        {
            var destPath = Path.Combine(tempDirName, Path.GetFileName(path));

            if (PathHelper.IsFile(path))
            {
                File.Copy(path, destPath);
            }
            else
            {
                DirectoryHelper.CopyAll(path, destPath);
            }
        }

        CompressSingleSource(tempDirName, zipPath, false);
        Directory.Delete(tempDirName, true);
    }
}
