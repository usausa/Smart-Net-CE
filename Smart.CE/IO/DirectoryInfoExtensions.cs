namespace Smart.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    ///
    /// </summary>
    public static class DirectoryInfoExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="directory"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static void DeleteIfExists(this DirectoryInfo directory)
        {
            if (directory.Exists)
            {
                directory.Delete();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="recursive"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static void DeleteIfExists(this DirectoryInfo directory, bool recursive)
        {
            if (directory.Exists)
            {
                directory.Delete(recursive);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static long GetFolderSize(this DirectoryInfo directory)
        {
            return directory.GetFiles().Sum(fi => fi.Length) + directory.GetDirectories().Sum(subFolder => GetFolderSize(subFolder));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static IEnumerable<FileInfo> FindFileRecursive(this DirectoryInfo directory, string pattern)
        {
            return directory.GetFiles(pattern)
                .Concat(directory.GetDirectories()
                    .SelectMany(subDirectory => subDirectory.FindFileRecursive(pattern)));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static IEnumerable<FileInfo> FindFileRecursive(this DirectoryInfo directory, Func<FileInfo, bool> predicate)
        {
            return directory.GetFiles()
                .Where(predicate)
                .Concat(directory.GetDirectories()
                    .SelectMany(subDirectory => subDirectory.FindFileRecursive(predicate)));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static DirectoryInfo CopyTo(this DirectoryInfo source, string destination)
        {
            var targetDirectory = new DirectoryInfo(destination);
            CopyTo(source, targetDirectory);
            return targetDirectory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static void CopyTo(this DirectoryInfo source, DirectoryInfo destination)
        {
            if (destination.Exists == false)
            {
                destination.Create();
            }

            foreach (var child in source.GetDirectories())
            {
                CopyTo(child, Path.Combine(destination.FullName, child.Name));
            }

            foreach (var file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(destination.FullName, file.Name));
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static DirectoryInfo CopyTo(this DirectoryInfo source, string destination, bool overwrite)
        {
            var destinationDirectory = new DirectoryInfo(destination);
            CopyTo(source, destinationDirectory, overwrite);
            return destinationDirectory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="overwrite"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static void CopyTo(this DirectoryInfo source, DirectoryInfo destination, bool overwrite)
        {
            if (destination.Exists == false)
            {
                destination.Create();
            }

            foreach (var child in source.GetDirectories())
            {
                CopyTo(child, Path.Combine(destination.FullName, child.Name), overwrite);
            }

            foreach (var file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(destination.FullName, file.Name), overwrite);
            }
        }
    }
}
