using Lary.Laboratory.Core.IO;

namespace Lary.Laboratory.Core.Tests.IO;

public class DirectoryHelperTests(
    IOFixture ioFixture)
    : IClassFixture<IOFixture>
{
    private readonly IOFixture _ioFixture = ioFixture;

    [Fact]
    public void DirectoryHelper_CopyRecursively_ShouldWork()
    {
        var destDirPath = _ioFixture.GetRandomDirectoryPath(createIfNotExists: false);
        var srcDirPath = _ioFixture.GetRandomDirectoryPath();
        _ioFixture.GetRandomFilePath(srcDirPath);
        _ioFixture.GetRandomFilePath(srcDirPath);
        var srcSubDirPath = _ioFixture.GetRandomDirectoryPath(srcDirPath);
        _ioFixture.GetRandomFilePath(srcSubDirPath);
        _ioFixture.GetRandomFilePath(srcSubDirPath);
        _ioFixture.GetRandomDirectoryPath(srcDirPath);
        _ioFixture.GetRandomDirectoryPath(srcSubDirPath);

        DirectoryHelper.CopyAll(srcDirPath, destDirPath);
        var dirInfo = new DirectoryInfo(destDirPath);

        dirInfo.EnumerateDirectories("*", SearchOption.TopDirectoryOnly).Count().Should().Be(2);
        dirInfo.EnumerateDirectories("*", SearchOption.AllDirectories).Count().Should().Be(3);
        dirInfo.EnumerateFiles("*", SearchOption.TopDirectoryOnly).Count().Should().Be(2);
        dirInfo.EnumerateFiles("*", SearchOption.AllDirectories).Count().Should().Be(4);
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, false)]
    public void DirectoryHelper_DeleteIfExists_DirectoryNeverExists(bool recursive, bool expected)
    {
        var dirPath = _ioFixture.GetRandomDirectoryPath(createIfNotExists: false);

        DirectoryHelper.DeleteIfExists(dirPath, recursive);

        Directory.Exists(dirPath).Should().Be(expected);
    }

    [Fact]
    public void DirectoryHelper_DeleteIfExists_DirectoryShouldNotBeDeleted()
    {
        var dirPath = _ioFixture.GetRandomDirectoryPath();
        _ioFixture.GetRandomFilePath(dirPath);

        FluentActions.Invoking(() => DirectoryHelper.DeleteIfExists(dirPath, false))
            .Should().ThrowExactly<IOException>();
    }

    [Fact]
    public void DirectoryHelper_DeleteIfExists_DirectoryShouldBeDeleted()
    {
        var dirPath = GetRandomDirectory();

        DirectoryHelper.DeleteIfExists(dirPath, true);

        Directory.Exists(dirPath).Should().BeFalse();
    }

    [Fact]
    public void DirectoryHelper_ForceInit_ShouldWork()
    {
        var dirPath = GetRandomDirectory();

        DirectoryHelper.ForceInit(dirPath);
        var dirInfo = new DirectoryInfo(dirPath);

        dirInfo.Exists.Should().BeTrue();
        dirInfo.EnumerateDirectories("*", SearchOption.AllDirectories).Should().BeEmpty();
        dirInfo.EnumerateFiles("*", SearchOption.AllDirectories).Should().BeEmpty();
    }

    private string GetRandomDirectory()
    {
        var dirPath = _ioFixture.GetRandomDirectoryPath();
        _ioFixture.GetRandomFilePath(dirPath);
        _ioFixture.GetRandomDirectoryPath(dirPath);
        var subDirPath = _ioFixture.GetRandomDirectoryPath(dirPath);
        _ioFixture.GetRandomFilePath(subDirPath);

        return dirPath;
    }
}
