using Lary.Laboratory.Core.IO;

namespace Lary.Laboratory.Core.Tests.IO;

public class ZipHelperTests(
    IOFixture ioFixture)
    : IClassFixture<IOFixture>
{
    private readonly IOFixture _ioFixture = ioFixture;

    [Fact]
    public void ZipHelper_Compress_FileNotFound()
    {
        var fakeFilePath = _ioFixture.GetRandomFilePath(createIfNotExists: false);

        FluentActions.Invoking(() => ZipHelper.Compress(fakeFilePath))
            .Should().ThrowExactly<FileNotFoundException>();
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
        var srcFilePath = _ioFixture.GetRandomFilePath();
        var zipPath = Path.ChangeExtension(srcFilePath, ".zip");

        ZipHelper.Compress(srcFilePath);

        File.Exists(zipPath).Should().BeTrue();
    }

    [Fact]
    public void ZipHelper_Compress_ZipFileExists()
    {
        var srcFilePath = _ioFixture.GetRandomFilePath();
        var zipPath = Path.ChangeExtension(srcFilePath, ".zip");
        File.Create(zipPath).Dispose();

        FluentActions.Invoking(() => ZipHelper.Compress(srcFilePath))
            .Should().Throw<IOException>();
    }

    [Fact]
    public void ZipHelper_Compress_OverwriteZipFile()
    {
        var srcFilePath = _ioFixture.GetRandomFilePath();
        var zipPath = Path.ChangeExtension(srcFilePath, ".zip");
        File.Create(zipPath).Dispose();
        var createTime = new FileInfo(zipPath).LastWriteTime;

        ZipHelper.Compress(srcFilePath, true);

        new FileInfo(zipPath).LastWriteTime.Should().BeAfter(createTime);
    }

    [Fact]
    public void ZipHelper_Compress_SingleDirectory()
    {
        var srcDirPath = Path.Combine(_ioFixture.BaseDirPath, Path.GetRandomFileName());
        _ = _ioFixture.GetRandomFilePath(srcDirPath);
        var zipPath = $"{srcDirPath}.zip";

        ZipHelper.Compress(srcDirPath);

        File.Exists(zipPath).Should().BeTrue();
    }

    [Fact]
    public void ZipHelper_Compress_MixedPaths()
    {
        var subDirPath = _ioFixture.GetRandomDirectoryPath();
        _ioFixture.GetRandomFilePath(subDirPath);
        _ioFixture.GetRandomFilePath(subDirPath);
        string[] srcPaths = [
            subDirPath,
            _ioFixture.GetRandomFilePath(),
            _ioFixture.GetRandomFilePath(),
            _ioFixture.GetRandomFilePath(_ioFixture.GetRandomDirectoryPath())
        ];
        var zipPath = GetRandomZipPath();

        ZipHelper.Compress(srcPaths, zipPath);

        File.Exists(zipPath).Should().BeTrue();
    }

    private string GetRandomZipPath()
        => Path.ChangeExtension(_ioFixture.GetRandomFilePath(createIfNotExists: false), ".zip");
}
