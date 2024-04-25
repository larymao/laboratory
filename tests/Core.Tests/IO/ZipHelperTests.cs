using Lary.Laboratory.Core.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Lary.Laboratory.Core.Tests.IO;

[TestClass]
public class ZipHelperTests
{
    [TestMethod]
    public void ZipTest()
    {
        var assembly = Assembly.GetAssembly(typeof(ZipHelperTests))!;
        var assemblyDir = assembly.GetDirectoryName();
        var assemblyFileName = $"{assembly.GetName().Name}.dll";
        var assemblyFilePath = Path.Combine(assemblyDir, assemblyFileName);
        var testDirPath = Path.Combine(assemblyDir, $"test_dir_{DateTime.Now:yyyyMMddHHmmss}");
        var testInnerDirPath = Path.Combine(testDirPath, $"test_inner_dir_{DateTime.Now:yyyyMMddHHmmss}");
        var testFilePath = Path.Combine(testDirPath, assemblyFileName);
        var fullPackZipPath = Path.Combine(assemblyDir, "full_pack.zip");

        Directory.CreateDirectory(testDirPath);
        File.Copy(assemblyFilePath, testFilePath);
        DirectoryHelper.CopyRecursively(testDirPath, testInnerDirPath);

        // file not exists
        var notExistsFilePath = Path.Combine(testDirPath, Guid.NewGuid().ToString("N"));
        Assert.ThrowsException<FileNotFoundException>(() => ZipHelper.Compress(notExistsFilePath));

        // empty pack
        ZipHelper.Compress(Array.Empty<string>(), Path.Combine(testDirPath, "empty_pack.zip"));

        // single file
        var zipAssemblyPath = ZipHelper.Compress(testFilePath);

        // target file exists
        Assert.ThrowsException<IOException>(() => ZipHelper.Compress(testFilePath, false));

        // target file exists and force replace
        ZipHelper.Compress(testFilePath, true);

        // single directory
        ZipHelper.Compress(testInnerDirPath);

        // multiple files
        var rootTestFiles = Directory.GetFiles(testDirPath);
        ZipHelper.Compress(rootTestFiles, Path.Combine(testDirPath, "files_only.zip"));

        // multiple files and directories
        var combinedPaths = Directory.GetFiles(testDirPath).Concat(Directory.GetDirectories(testDirPath));
        ZipHelper.Compress(combinedPaths, Path.Combine(testDirPath, "combined.zip"));

        // directory with subfiles and subdirectories
        ZipHelper.Compress(testDirPath, fullPackZipPath, true);

        // clean test directory
        DirectoryHelper.DeleteIfExists(testDirPath, true);
        FileHelper.DeleteIfExists(fullPackZipPath);
    }
}
