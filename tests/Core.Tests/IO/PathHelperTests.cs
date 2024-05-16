using Lary.Laboratory.Core.IO;

namespace Lary.Laboratory.Core.Tests.IO;

public class PathHelperTests(
    IOFixture ioFixture)
    : IClassFixture<IOFixture>
{
    private readonly IOFixture _ioFixture = ioFixture;

    [Fact]
    public void PathHelper_IsFile_ShouldWork()
    {
        var filePath = _ioFixture.GetRandomFilePath();

        var isFile = PathHelper.IsFile(filePath);

        isFile.Should().BeTrue();
    }

    [Fact]
    public void PathHelper_IsFile_ReturnFalseIfFileNotExists()
    {
        var filePath = _ioFixture.GetRandomFilePath(createIfNotExists: false);

        var isFile = PathHelper.IsFile(filePath);

        isFile.Should().BeFalse();
    }

    [Fact]
    public void PathHelper_IsDirectory_ShouldWork()
    {
        var dirPath = _ioFixture.GetRandomDirectoryPath();

        var isDir = PathHelper.IsDirectory(dirPath);

        isDir.Should().BeTrue();
    }

    [Fact]
    public void PathHelper_IsDirectory_ReturnFalseIfDirectoryNotExists()
    {
        var dirPath = _ioFixture.GetRandomDirectoryPath(createIfNotExists: false);

        var isDir = PathHelper.IsDirectory(dirPath);

        isDir.Should().BeFalse();
    }
}
