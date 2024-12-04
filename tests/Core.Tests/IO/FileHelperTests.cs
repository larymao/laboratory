using Lary.Laboratory.Core.IO;

namespace Lary.Laboratory.Core.Tests.IO;

public class FileHelperTests(
    IOFixture ioFixture)
    : IClassFixture<IOFixture>
{
    private readonly IOFixture _ioFixture = ioFixture;

    [Fact]
    public void FileHelper_DeleteIfExists_FileShouldBeDeleted()
    {
        var filePath = _ioFixture.GetRandomFilePath();

        FileHelper.DeleteIfExists(filePath);

        File.Exists(filePath).Should().BeFalse();
    }

    [Fact]
    public void FileHelper_IsLocked_ShouldWork()
    {
        var filePath = _ioFixture.GetRandomFilePath();

        var fs = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite);
        var fileInfo = new FileInfo(filePath);

        fileInfo.IsLocked().Should().BeTrue();

        fs.Dispose();

        fileInfo.IsLocked().Should().BeFalse();
    }

    [Fact]
    public void FileHelper_CountTxtLines_ShouldWork()
    {
        var lines = Enumerable.Range(1, 10000).Select(i => Guid.NewGuid().ToString()).ToArray();
        var txtFilePaths = Enumerable.Range(1, 100).Select(i => _ioFixture.CreateRandomTxtFile(lines));

        var lineCount = FileHelper.CountTxtLines(txtFilePaths);

        lineCount.Should().Be(txtFilePaths.Count() * lines.Length);
    }

    [Fact]
    public void FileHelper_CountTxtLines_ShouldErrorIfFileNotExists()
    {
        var lines = Enumerable.Range(1, 10000).Select(i => Guid.NewGuid().ToString()).ToArray();
        var txtFilePaths = Enumerable.Range(1, 100).Select(i => _ioFixture.CreateRandomTxtFile(lines)).ToArray();
        var deleteAt = new Random().Next(50, 99);
        FileHelper.DeleteIfExists(txtFilePaths[deleteAt]);

        FluentActions.Invoking(() => FileHelper.CountTxtLines(txtFilePaths))
            .Should().ThrowExactly<FileNotFoundException>();
    }

    [Fact]
    public void FileHelper_CountTxtLines_ShouldCountEmptyLines()
    {
        string[] txtLines = [.. Enumerable.Range(1, 10).Select(i => Guid.NewGuid().ToString())];
        string[] emptyLines = [.. Enumerable.Range(1, 10).Select(i => string.Empty)];
        string[] lines = [.. txtLines.Concat(emptyLines).OrderBy(x => Guid.NewGuid().ToString())]; // shuffle
        var shuffledFiles = Enumerable.Range(1, 10).Select(i => _ioFixture.CreateRandomTxtFile(lines));
        var emptyLinesFile = _ioFixture.CreateRandomTxtFile(emptyLines);
        var emptyFile = _ioFixture.CreateRandomTxtFile([]);
        string[] txtFilePaths = [
            .. shuffledFiles,
            emptyLinesFile,
            emptyFile
        ];
        var expectedLineCount = (txtLines.Length + emptyLines.Length) * shuffledFiles.Count() + emptyLines.Length;

        var lineCount = FileHelper.CountTxtLines(txtFilePaths);

        lineCount.Should().Be(expectedLineCount);
    }

    [Theory]
    [InlineData("1024", 1024, 1024D)]
    [InlineData("1024bytes", 1024, 1024D)]
    [InlineData("2KB", 1024, 2 * 1024D)]
    [InlineData("2GB", 996, 2 * 996 * 996 * 996)]
    [InlineData("2GB", 1000, 2 * 1000 * 1000 * 1000D)]
    [InlineData("2GB", 1024, 2 * 1024 * 1024 * 1024D)]
    [InlineData("2Gb", 1024, 2 * 1024 * 1024 * 1024D)]
    [InlineData("2gb", 1024, 2 * 1024 * 1024 * 1024D)]
    public void FileHelper_ParseFileSize_ReturnDigitalFileSize(string fileSize, int kbSize, double expected)
    {
        var result = FileHelper.ParseFileSize(fileSize, kbSize);

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("1024 bytes", 1024, 1024D)]
    [InlineData(" 1024  bytes", 1024, 1024D)]
    [InlineData(" 1024   bytes  ", 1024, 1024D)]
    [InlineData(" 5   KB  ", 1000, 5 * 1000D)]
    public void FileHelper_ParseFileSize_ShouldIgnoreLegalSpace(string fileSize, int kbSize, double expected)
    {
        var result = FileHelper.ParseFileSize(fileSize, kbSize);

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("1024 B")]
    [InlineData("1024 kilobyte")]
    [InlineData("1024 GiB")]
    [InlineData("1024 G")]
    public void FileHelper_ParseFileSize_ThrowFormatException(string fileSize)
    {
        FluentActions.Invoking(() => FileHelper.ParseFileSize(fileSize, 1024))
            .Should().ThrowExactly<FormatException>();
    }

    [Theory]
    [InlineData(2.4 * 1024, 1024, "N2", "2.40 KB")]
    [InlineData(2.48 * 1000, 1000, "N1", "2.5 KB")]
    [InlineData(24.842 * 1000 * 1000 * 1000, 1000, "N0", "25 GB")]
    [InlineData((0.86 * (1024 * 1024 * 1024)), 1024, "N1", "880.6 MB")]
    public void FileHelper_ConvertFileSize_ReturnFileSizeDescription(
        double value, int kbSize, string format, string expected)
    {
        var result = FileHelper.ConvertFileSize(value, kbSize, format);

        result.Should().Be(expected);
    }
}
