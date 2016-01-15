namespace Smart.Diagnostics
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 
    /// </summary>
    public class FileVersionInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int FileMajorPart { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int FileMinorPart { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int FileBuildPart { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int FilePrivatePart { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProductMajorPart { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProductMinorPart { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProductBuildPart { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProductPrivatePart { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        private FileVersionInfo(string filename)
        {
            // ƒtƒ@ƒCƒ‹–¼
            FileName = Path.GetFileName(filename);

            var handle = IntPtr.Zero;
            var len = NativeMethods.GetFileVersionInfoSize(filename, ref handle);

            if (len > 0)
            {
                var buffer = NativeMethods.LocalAlloc(0x00 | 0x40, len);

                if (NativeMethods.GetFileVersionInfo(filename, (uint)handle, len, buffer))
                {
                    var fixedbuffer = IntPtr.Zero;
                    uint fixedlen = 0;
                    if (NativeMethods.VerQueryValue(buffer, "\\", ref fixedbuffer, ref fixedlen))
                    {
                        var fixedVersionInfo = new byte[fixedlen];
                        Marshal.Copy(fixedbuffer, fixedVersionInfo, 0, (Int32)fixedlen);

                        FileMajorPart = Convert.ToInt32(BitConverter.ToUInt16(fixedVersionInfo, 10));
                        FileMinorPart = Convert.ToInt32(BitConverter.ToUInt16(fixedVersionInfo, 8));
                        FileBuildPart = Convert.ToInt32(BitConverter.ToUInt16(fixedVersionInfo, 14));
                        FilePrivatePart = Convert.ToInt32(BitConverter.ToUInt16(fixedVersionInfo, 12));
                        ProductMajorPart = Convert.ToInt32(BitConverter.ToUInt16(fixedVersionInfo, 18));
                        ProductMinorPart = Convert.ToInt32(BitConverter.ToUInt16(fixedVersionInfo, 16));
                        ProductBuildPart = Convert.ToInt32(BitConverter.ToUInt16(fixedVersionInfo, 22));
                        ProductPrivatePart = Convert.ToInt32(BitConverter.ToUInt16(fixedVersionInfo, 20));
                    }
                }

                NativeMethods.LocalFree(buffer);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static FileVersionInfo GetVersionInfo(string fileName)
        {
            if (File.Exists(fileName))
            {
                return new FileVersionInfo(fileName);
            }

            throw new FileNotFoundException(fileName);
        }
    }
}