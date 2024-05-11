using Lary.Laboratory.Core.IO;

namespace Lary.Laboratory.Core.Tests.IO;

public class ZipHelperTests : IDisposable
{
    private bool isDisposed;
    private readonly string _baseDirPath = Path.Combine(Path.GetTempPath(), nameof(ZipHelperTests));

    public ZipHelperTests()
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

    [Fact]
    public void ZipHelper_Compress_FileNotFound()
    {
        var fakeFilePath = GetRandomFilePath(createIfNotExists: false);

        FluentActions.Invoking(() => ZipHelper.Compress(fakeFilePath))
            .Should().Throw<FileNotFoundException>();
    }

    [Fact]
    public void ZipHelper_Compress_EmptyPack()
    {
        string[] srcPaths = [];
        var zipPath = GetRandomZipPath();

        ZipHelper.Compress(srcPaths, zipPath);

        File.Exists(zipPath).Should().BeTrue();
        new FileInfo(zipPath).Length.Should().BeLessThan(1024);
    }

    [Fact]
    public void ZipHelper_Compress_SingleFile()
    {
        var srcFilePath = GetRandomFilePath();
        var zipPath = Path.ChangeExtension(srcFilePath, ".zip");

        ZipHelper.Compress(srcFilePath);

        File.Exists(zipPath).Should().BeTrue();
    }

    [Fact]
    public void ZipHelper_Compress_ZipFileExists()
    {
        var srcFilePath = GetRandomFilePath();
        var zipPath = Path.ChangeExtension(srcFilePath, ".zip");
        File.Create(zipPath).Dispose();

        FluentActions.Invoking(() => ZipHelper.Compress(srcFilePath))
            .Should().Throw<IOException>();
    }

    [Fact]
    public void ZipHelper_Compress_OverwriteZipFile()
    {
        var srcFilePath = GetRandomFilePath();
        var zipPath = Path.ChangeExtension(srcFilePath, ".zip");
        File.Create(zipPath).Dispose();
        var createTime = new FileInfo(zipPath).LastWriteTime;

        ZipHelper.Compress(srcFilePath, true);

        new FileInfo(zipPath).LastWriteTime.Should().BeAfter(createTime);
    }

    [Fact]
    public void ZipHelper_Compress_SingleDirectory()
    {
        var srcDirPath = Path.Combine(_baseDirPath, Path.GetRandomFileName());
        _ = GetRandomFilePath(srcDirPath);
        var zipPath = $"{srcDirPath}.zip";

        ZipHelper.Compress(srcDirPath);

        File.Exists(zipPath).Should().BeTrue();
    }

    [Fact]
    public void ZipHelper_Compress_MixedPaths()
    {
        var subDirPath = Path.Combine(_baseDirPath, Path.GetRandomFileName());
        GetRandomFilePath(subDirPath);
        GetRandomFilePath(subDirPath);
        string[] srcPaths = [
            subDirPath,
            GetRandomFilePath(),
            GetRandomFilePath(),
            GetRandomFilePath(Path.Combine(_baseDirPath, Path.GetRandomFileName()))
        ];
        var zipPath = GetRandomZipPath();

        ZipHelper.Compress(srcPaths, zipPath);

        File.Exists(zipPath).Should().BeTrue();
    }

    private string GetRandomFilePath(string? baseDirPath = null, bool createIfNotExists = true)
    {
        baseDirPath = string.IsNullOrWhiteSpace(baseDirPath) ? _baseDirPath : baseDirPath;
        Directory.CreateDirectory(baseDirPath);
        var filePath = Path.Combine(baseDirPath, Path.GetRandomFileName());

        if (createIfNotExists && !File.Exists(filePath))
            File.Create(filePath).Dispose();

        return filePath;
    }

    private string GetRandomZipPath()
        => Path.Combine(_baseDirPath, Path.ChangeExtension(Path.GetRandomFileName(), ".zip"));
}
