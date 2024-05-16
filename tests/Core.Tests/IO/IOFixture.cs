namespace Lary.Laboratory.Core.Tests.IO;

public class IOFixture : IDisposable
{
    private bool isDisposed;
    private readonly string _baseDirPath = Directory.CreateTempSubdirectory(nameof(ZipHelperTests)).FullName;

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
}
