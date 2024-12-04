namespace Lary.Laboratory.Core.IO;

public static class TxtFileWriter
{
    private static readonly ReaderWriterLock _fileLocker = new();

    /// <summary>
    /// Appends a string to the end of the specified file. If the file not exists,
    /// create it automatically.
    /// </summary>
    /// <param name="filePath">The path of file to append text to.</param>
    /// <param name="txt">The text to append.</param>
    public static void AppendTo(string filePath, string txt)
    {
        InvokeWritter(filePath, sw => sw.Write(txt));
    }

    /// <summary>
    /// Appends a string followed by the default line terminator to the end of the specified file.
    /// If the file not exists, create it automatically.
    /// </summary>
    /// <param name="filePath">The path of file to append text to.</param>
    /// <param name="txt">The text to append.</param>
    public static void AppendLineTo(string filePath, string txt)
    {
        InvokeWritter(filePath, sw => sw.WriteLine(txt));
    }

    /// <summary>
    /// Appends multiple strings followed by the default line terminator to the end of the specified file.
    /// If the file not exists, create it automatically.
    /// </summary>
    /// <param name="filePath">The path of file to append text to.</param>
    /// <param name="lines">The texts to append.</param>
    public static void AppendLinesTo(string filePath, IEnumerable<string> lines)
    {
        foreach (var line in lines)
            AppendLineTo(filePath, line);
    }

    private static void InvokeWritter(string filePath, Action<StreamWriter> writter)
    {
        var dirPath = Path.GetDirectoryName(filePath);
        Directory.CreateDirectory(dirPath);

        try
        {
            _fileLocker.AcquireWriterLock(int.MaxValue);

            using var sw = new StreamWriter(filePath, true);
            writter.Invoke(sw);
        }
        catch
        {
            throw;
        }
        finally
        {
            _fileLocker.ReleaseWriterLock();
        }
    }
}
