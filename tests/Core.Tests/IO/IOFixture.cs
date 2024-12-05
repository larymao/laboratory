namespace Lary.Laboratory.Core.Tests.IO;

public class IOFixture : IDisposable
{
    private bool isDisposed;
#if NET7_0_OR_GREATER
    private readonly string _baseDirPath = Directory.CreateTempSubdirectory(nameof(ZipHelperTests)).FullName;
#else
    private readonly string _baseDirPath = CreateTemporaryDirectory(nameof(ZipHelperTests)).FullName;
#endif

    public string BaseDirPath => _baseDirPath;

    public IOFixture()
    {
        Directory.CreateDirectory(_baseDirPath);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (isDisposed)
            return;

        if (disposing)
            if (Directory.Exists(_baseDirPath))
                Directory.Delete(_baseDirPath, true);

        isDisposed = true;
    }

    public string GetRandomDirectoryPath(string? baseDirPath = null, bool createIfNotExists = true)
    {
        baseDirPath = string.IsNullOrWhiteSpace(baseDirPath) ? _baseDirPath : baseDirPath;
        var dirPath = Path.Combine(baseDirPath, Path.GetRandomFileName());

        if (createIfNotExists && !Directory.Exists(dirPath))
            Directory.CreateDirectory(dirPath);

        return dirPath;
    }

    public string GetRandomFilePath(string? baseDirPath = null, bool createIfNotExists = true)
    {
        baseDirPath = string.IsNullOrWhiteSpace(baseDirPath) ? _baseDirPath : baseDirPath;
        var filePath = Path.Combine(baseDirPath, Path.GetRandomFileName());

        if (createIfNotExists && !File.Exists(filePath))
        {
            Directory.CreateDirectory(baseDirPath);
            File.Create(filePath).Dispose();
        }

        return filePath;
    }

    public string CreateRandomTxtFile(string[] contents, string? baseDirPath = null)
    {
        var filePath = Path.ChangeExtension(GetRandomFilePath(baseDirPath), ".txt");

        File.WriteAllLines(filePath, contents);

        return filePath;
    }

#if !NET7_0_OR_GREATER
    public static DirectoryInfo CreateTemporaryDirectory(string? prefix = null)
    {
        var tempDirectory = Path.Combine(Path.GetTempPath(), $"{prefix}{Path.GetRandomFileName()}");

        if (File.Exists(tempDirectory))
            return CreateTemporaryDirectory();

        Directory.CreateDirectory(tempDirectory);
        return new DirectoryInfo(tempDirectory);
    }
#endif
}
