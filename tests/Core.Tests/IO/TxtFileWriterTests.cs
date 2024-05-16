using Lary.Laboratory.Core.IO;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Lary.Laboratory.Core.Tests.IO;

public class TxtFileWriterTests(
    IOFixture ioFixture)
    : IClassFixture<IOFixture>
{
    private readonly IOFixture _ioFixture = ioFixture;

    [Fact]
    public void TxtFileWriter_AppendTo_ShouldCreateFileIfNotExists()
    {
        var filePath = _ioFixture.GetRandomFilePath(createIfNotExists: false);
        var txt = Guid.NewGuid().ToString();
        var expectedLineCount = 1;
        var expectedTxt = txt;

        TxtFileWriter.AppendTo(filePath, txt);

        new FileInfo(filePath).Exists.Should().BeTrue();
        File.ReadAllLines(filePath).Length.Should().Be(expectedLineCount);
        File.ReadAllText(filePath).Should().Be(expectedTxt);
    }

    [Fact]
    public void TxtFileWriter_AppendTo_ShouldAppendText()
    {
        var filePath = _ioFixture.GetRandomFilePath();
        var txt = Guid.NewGuid().ToString();
        var loop = 10;
        var expectedLineCount = 1;
        var expectedTxt = new StringBuilder().Insert(0, txt, loop).ToString();

        for (var i = 0; i < loop; i++)
            TxtFileWriter.AppendTo(filePath, txt);

        File.ReadAllLines(filePath).Length.Should().Be(expectedLineCount);
        File.ReadAllText(filePath).Should().Be(expectedTxt);
    }

    [Fact]
    public void TxtFileWriter_AppendLineTo_ShouldCreateFileIfNotExists()
    {
        var filePath = _ioFixture.GetRandomFilePath(createIfNotExists: false);
        var line = Guid.NewGuid().ToString();
        var expectedLineCount = 1;
        var expectedTxt = $"{line}{Environment.NewLine}";

        TxtFileWriter.AppendLineTo(filePath, line);

        new FileInfo(filePath).Exists.Should().BeTrue();
        File.ReadAllLines(filePath).Length.Should().Be(expectedLineCount);
        File.ReadAllText(filePath).Should().Be(expectedTxt);
    }

    [Fact]
    public void TxtFileWriter_AppendLineTo_ShouldAppendLine()
    {
        var filePath = _ioFixture.GetRandomFilePath();
        var line = Guid.NewGuid().ToString();
        var loop = 10;
        var expectedLineCount = loop;
        var expectedTxt = new StringBuilder().Insert(0, $"{line}{Environment.NewLine}", loop).ToString();

        for (var i = 0; i < loop; i++)
            TxtFileWriter.AppendLineTo(filePath, line);

        File.ReadAllLines(filePath).Length.Should().Be(expectedLineCount);
        File.ReadAllText(filePath).Should().Be(expectedTxt);
    }

    [Fact]
    public void TxtFileWriter_AppendLinesTo_ShouldCreateFileIfNotExists()
    {
        var filePath = _ioFixture.GetRandomFilePath(createIfNotExists: false);
        string[] lines = [Guid.NewGuid().ToString()];
        var expectedLineCount = lines.Length;
        var expectedTxt = $"{string.Join(Environment.NewLine, lines)}{Environment.NewLine}";

        TxtFileWriter.AppendLinesTo(filePath, lines);

        new FileInfo(filePath).Exists.Should().BeTrue();
        File.ReadAllLines(filePath).Length.Should().Be(expectedLineCount);
        File.ReadAllText(filePath).Should().Be(expectedTxt);
    }

    [Fact]
    public void TxtFileWriter_AppendLinesTo_ShouldAppendLines()
    {
        var filePath = _ioFixture.GetRandomFilePath(createIfNotExists: false);
        string[] txtLines = [.. Enumerable.Range(1, 10).Select(i => Guid.NewGuid().ToString())];
        string[] emptyLines = [.. Enumerable.Range(1, 10).Select(i => string.Empty)];
        string[] lines = [.. txtLines.Concat(emptyLines).OrderBy(x => Guid.NewGuid().ToString())]; // shuffle
        var loop = 10;
        var expectedLineCount = loop * lines.Length;
        var expectedTxt = string.Join(
            string.Empty,
            Enumerable.Range(1, loop).SelectMany(x => lines.Select(l => $"{l}{Environment.NewLine}")));

        for (var i = 0; i < loop; i++)
            TxtFileWriter.AppendLinesTo(filePath, lines);

        File.ReadAllLines(filePath).Length.Should().Be(expectedLineCount);
        File.ReadAllText(filePath).Should().Be(expectedTxt);
    }
}
