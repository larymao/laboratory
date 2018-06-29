using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Lary.Laboratory.Core.Utils
{
    /// <summary>
    ///     Provides methods for facilitating the use of filepath.
    /// </summary>
    public static class FilePathUtil
    {
        /// <summary>
        ///     Converts the local file or directory path to its mapped network drive path equivalent. 
        ///     A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="localPath">
        ///     The local file or directory full path to convert.
        /// </param>
        /// <param name="server">
        ///     The IP address or domain name of the network drive that you mapped to.
        /// </param>
        /// <param name="uncPath">
        ///     When this method returns, contains the network drive path equivalent of the local
        ///     path, if the conversion succeed, or empty string if the conversion failed.
        ///     The conversion fails if the localPath or server is null, empty, or consists
        ///     only of white-space. This parameter is passed uninitialized, any value originally
        ///     supplied in result will be overwritten.
        /// </param>
        /// <returns>
        ///     True if uncPath was converted successfully; otherwise, false.
        /// </returns>
        public static bool TryConvertLocalPathToUnc(string localPath, string server, out string uncPath)
        {
            var success = false;
            uncPath = String.Empty;

            try
            {
                if (!String.IsNullOrWhiteSpace(localPath) && !String.IsNullOrWhiteSpace(server))
                {
                    var pattern = @"(^[a-zA-Z]:(?:\\?))";
                    var match = Regex.Match(localPath, pattern);
                    var oldValue = match.Groups[1].Value;
                    var newValue = String.Format(@"\\{0}\", server.Trim(new[] { '\\' }));

                    uncPath = localPath.Replace(oldValue, newValue);
                    success = true;
                }
            }
            catch
            {
            }

            return success;
        }

        /// <summary>
        ///     Converts the unc file or directory path to its mapped local disk path equivalent. 
        ///     A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="uncPath">
        ///     The unc file or directory full path to convert.
        /// </param>
        /// <param name="drive">
        ///     The name of the drive that the unc path mapped to.
        /// </param>
        /// <param name="localPath">
        ///     When this method returns, contains the local disk path equivalent of the unc path,
        ///     if the conversion succeed, or empty string if the conversion failed.
        ///     The conversion fails if the uncPath or drive is null, empty, or consists
        ///     only of white-space. This parameter is passed uninitialized, any value originally
        ///     supplied in result will be overwritten.
        /// </param>
        /// <returns>
        ///     True if uncPath was converted successfully; otherwise, false.
        /// </returns>
        public static bool TryConvertUncPathToLocal(string uncPath, string drive, out string localPath)
        {
            var success = false;
            localPath = String.Empty;

            try
            {
                if (!String.IsNullOrWhiteSpace(uncPath) && !String.IsNullOrWhiteSpace(drive))
                {
                    var pattern = @"(^\\\\.+?\\)";
                    var match = Regex.Match(uncPath, pattern);

                    if (match.Success)
                    {
                        var oldValue = match.Groups[1].Value;
                        var newValue = String.Format("{0}:", drive.Trim(new[] { ':' }));

                        localPath = uncPath.Replace(oldValue, newValue);
                        success = true;
                    }
                }
            }
            catch
            {
            }

            return success;
        }

        /// <summary>
        ///     Returns the case sensitive path of the given directory.
        /// </summary>
        /// <param name="dirInfo">
        ///     The directory to capitalization.
        /// </param>
        /// <returns>
        ///     The case sensitive path of the given directory.
        /// </returns>
        public static string GetProperDirectoryCapitalization(DirectoryInfo dirInfo)
        {
            DirectoryInfo parentDirInfo = dirInfo.Parent;

            if (parentDirInfo == null)
            {
                return dirInfo.Name;
            }

            return Path.Combine(GetProperDirectoryCapitalization(parentDirInfo),
                                parentDirInfo.GetDirectories(dirInfo.Name)[0].Name);
        }

        /// <summary>
        ///     Returns the case sensitive path of the given file.
        /// </summary>
        /// <param name="filename">
        ///     The file to capitalization.
        /// </param>
        /// <returns>
        ///     The case sensitive path of the given file.
        /// </returns>
        public static string GetProperFilePathCapitalization(string filename)
        {
            FileInfo fileInfo = new FileInfo(filename);
            DirectoryInfo dirInfo = fileInfo.Directory;
            return Path.Combine(GetProperDirectoryCapitalization(dirInfo),
                                dirInfo.GetFiles(fileInfo.Name)[0].Name);
        }
    }
}
