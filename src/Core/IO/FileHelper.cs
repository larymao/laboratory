namespace Lary.Laboratory.Core.IO;

/// <summary>
/// Provides methods for file operations.
/// </summary>
public static class FileHelper
{
    private static readonly string[] _sizeSuffixes = ["bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"];
    private static readonly double[] _standardSizeInBytes = [
        1D,
        1_024D, // KB, Kilobyte	
        1_048_576D, // MB, Megabyte
        1_073_741_824D, // GB, Gigabyte
        1_099_511_627_776D, // TB, Terabyte
        1_125_899_906_842_624D, // PB, Petabyte
        1_152_921_504_606_846_976D, // EB, Exabyte
        1_180_591_620_717_411_303_424D, // ZB, Zettabyte
        1_208_925_819_614_629_174_706_176D, // YB, Yottabyte
    ];

    /// <summary>
    /// Detects whether the specified file exist, if true, deletes it.
    /// </summary>
    /// <param name="filePath">The name of the file to be deleted. Wildcard characters are not supported.</param>
    public static void DeleteIfExists(string filePath)
    {
        if (File.Exists(filePath))
            File.Delete(filePath);
    }

    /// <summary>
    /// <para>Detects whether the specified file is unavailable because it is:</para>
    /// <para>still being written to</para>
    /// <para>or being processed by another thread</para>
    /// <para>or does not exist (has already been processed)</para>
    /// </summary>
    /// <param name="file">The file to be checked.</param>
    /// <returns><see langword="true"/> if the specified file is unavailable for reasons above; otherwise, <see langword="false"/>.</returns>
    public static bool IsLocked(this FileInfo file)
    {
        try
        {
            using var stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            return false;
        }
        catch (IOException)
        {
            return true;
        }
    }

    /// <summary>
    /// Counts lines of the given text files.
    /// </summary>
    /// <param name="filePaths">A collection of file paths.</param>
    /// <returns>The summation of the file lines.</returns>
    public static int CountTxtLines(IEnumerable<string> filePaths)
    {
        var totalLines = 0;
        var locker = new object();

        Parallel.ForEach(filePaths, x =>
        {
            using var sr = new StreamReader(x);
            var counter = 0;

            while (sr.ReadLine() != null)
                counter++;

            lock (locker)
                totalLines += counter;
        });

        return totalLines;
    }

    /// <summary>
    /// Parses a human readable file size to the equivalent digital one.
    /// </summary>
    /// <param name="value">A human readable file size expression.</param>
    /// <param name="kbSize">The bytes of a kilobyte stand for.</param>
    /// <returns>File size in number.</returns>
    /// <exception cref="FormatException">Thrown if the file size expression is invalid.</exception>
    public static double ParseFileSize(string value, int kbSize = 1024)
    {
        value = value.Trim();

        try
        {
            var extStart = Enumerable.Range(0, value.Length)
                .Reverse()
                .FirstOrDefault(i => !char.IsLetter(value[i])) + 1;

            var number = double.Parse(value.Substring(0, extStart));
            var suffix = extStart == value.Length
                ? _sizeSuffixes[0]
                : value.Substring(extStart);
            var suffixIndex = Array.FindIndex(
                _sizeSuffixes,
                x => x.Equals(suffix, StringComparison.OrdinalIgnoreCase));

            if (suffixIndex == -1)
                throw new FormatException($"Unknown file size extension {suffix}.");

            var coefficient = GetFileSizeCoefficient(suffixIndex, kbSize);
            return System.Math.Round(number * coefficient);
        }
        catch (Exception ex)
        {
            throw new FormatException("Invalid file size format.", ex);
        }
    }

    /// <summary>
    /// Converts a numeric file size to the equivalent human readable one.
    /// </summary>
    /// <param name="value">File size in number.</param>
    /// <param name="kbSize">The bytes of a kilobyte stand for.</param>
    /// <param name="format">A numeric format string.</param>
    /// <returns>A human readable file size expression.</returns>
    public static string ConvertFileSize(double value, int kbSize = 1024, string format = "N2")
    {
        var suffixIndex = _sizeSuffixes.Length - 1;

        for (var i = 0; i < _sizeSuffixes.Length; i++)
        {
            var nextCoefficient = GetFileSizeCoefficient(i + 1, kbSize);

            if (value <= nextCoefficient)
            {
                suffixIndex = i;
                break;
            }
        }

        var coefficient = GetFileSizeCoefficient(suffixIndex, kbSize);
        return $"{(value / coefficient).ToString(format)} {_sizeSuffixes[suffixIndex]}";
    }

    private static double GetFileSizeCoefficient(int suffixIndex, int kbSize = 1024)
        => kbSize == 1024 ? _standardSizeInBytes[suffixIndex] : System.Math.Pow(kbSize, suffixIndex);
}
