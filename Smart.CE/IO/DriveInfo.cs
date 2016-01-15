namespace Smart.IO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 
    /// </summary>
    public class DriveInfo
    {
        private readonly string driveName;

        /// <summary>
        /// 
        /// </summary>
        public DirectoryInfo RootDirectory
        {
            get
            {
                return new DirectoryInfo(driveName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long AvailableFree { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public long Total { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public long TotalFree { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driveName"></param>
        public DriveInfo(string driveName)
        {
            if (string.IsNullOrEmpty(driveName))
            {
                throw new ArgumentNullException("driveName");
            }

            if (!Directory.Exists(driveName))
            {
                throw new ArgumentException("driveName does not exist");
            }

            long availableFree = 0;
            long total = 0;
            long totalFree = 0;
            if (!NativeMethods.GetDiskFreeSpaceEx(driveName, ref availableFree, ref total, ref totalFree))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            this.driveName = driveName;
            AvailableFree = availableFree;
            Total = total;
            TotalFree = totalFree;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return driveName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DriveInfo[] GetDrives()
        {
            const FileAttributes Attributes = FileAttributes.Temporary | FileAttributes.Directory;

            var list = new List<DriveInfo> { new DriveInfo(@"\") };
            list.AddRange(new DirectoryInfo(@"\").GetDirectories()
                .Where(di => (di.Attributes & Attributes) == Attributes)
                .Select(di => new DriveInfo(di.Name)));

            return list.ToArray();
        }
    }
}
