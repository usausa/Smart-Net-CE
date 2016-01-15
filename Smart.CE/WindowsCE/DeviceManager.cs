namespace Smart.WindowsCE
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public static class DeviceManager
    {
        private const int METHOD_BUFFERED = 0;
        private const int FILE_ANY_ACCESS = 0;
        private const int FILE_DEVICE_HAL = 0x00000101;

        private const int ERROR_NOT_SUPPORTED = 0x32;
        private const int ERROR_INSUFFICIENT_BUFFER = 0x7A;

        private const int IOCTL_HAL_GET_DEVICEID = (FILE_DEVICE_HAL << 16) | (FILE_ANY_ACCESS << 14) | (21 << 2) | METHOD_BUFFERED;

        private const int SPI_GETPLATFORMVERSION = 0x00e0;
        private const int SPI_GETPLATFORMTYPE = 0x0101;
        private const int SPI_GETOEMINFO = 0x0102;

        /// <summary>
        /// 
        /// </summary>
        public static SystemInfo SystemInformation
        {
            get
            {
                SystemInfo systemInfo;
                NativeMethods.GetSystemInfo(out systemInfo);
                return systemInfo;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "Ignore")]
        public static string OemInfo
        {
            get
            {
                var buffer = new byte[260];
                if (!NativeMethods.SystemParametersInfo(SPI_GETOEMINFO, buffer.Length, buffer, 0))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                var str = Encoding.Unicode.GetString(buffer, 0, buffer.Length);
                var index = str.IndexOf('\0');
                if (index > -1)
                {
                    str = str.Substring(0, index);
                }

                return str;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "Ignore")]
        public static string PlatformType
        {
            get
            {
                var buffer = new byte[260];
                if (!NativeMethods.SystemParametersInfo(SPI_GETPLATFORMTYPE, buffer.Length, buffer, 0))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                var str = Encoding.Unicode.GetString(buffer, 0, buffer.Length);
                var index = str.IndexOf('\0');
                if (index > -1)
                {
                    str = str.Substring(0, index);
                }

                return str;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Version PlatformVersion
        {
            get
            {
                Version platformVersion;

                var buffer = new byte[16];
                if (NativeMethods.SystemParametersInfo(SPI_GETPLATFORMVERSION, buffer.Length, buffer, 0))
                {
                    var major = BitConverter.ToInt32(buffer, 0);
                    var minor = BitConverter.ToInt32(buffer, 4);
                    platformVersion = new Version(major, minor);
                }
                else if (Environment.OSVersion.Version.Major < 4)
                {
                    platformVersion = new Version(0, 0);
                }
                else
                {
                    platformVersion = new Version(Environment.OSVersion.Version.Major - 1, 0);
                }

                return platformVersion;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Guid DeviceGuid
        {
            get
            {
                var rawDeviceId = GetRawDeviceId();
                var num = BitConverter.ToInt32(rawDeviceId, 4);
                var num2 = BitConverter.ToInt32(rawDeviceId, 8);
                var num3 = BitConverter.ToInt32(rawDeviceId, 12);
                var num4 = BitConverter.ToInt32(rawDeviceId, 16);

                var dst = new byte[16];
                Buffer.BlockCopy(rawDeviceId, (num + num2) - 10, dst, 0, 10);
                Buffer.BlockCopy(rawDeviceId, (num3 + num4) - 6, dst, 10, 6);

                return new Guid(dst);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string DeviceId
        {
            get
            {
                var rawDeviceId = GetRawDeviceId();
                var num = BitConverter.ToInt32(rawDeviceId, 4);
                var num2 = BitConverter.ToInt32(rawDeviceId, 8);
                var num3 = BitConverter.ToInt32(rawDeviceId, 12);
                var num4 = BitConverter.ToInt32(rawDeviceId, 16);

                var sb = new StringBuilder();
                for (var i = num; i < (num + num2); i++)
                {
                    sb.Append(string.Format(CultureInfo.InvariantCulture, "{0:X2}", rawDeviceId[i]));
                }
                sb.Append("-");
                for (var j = num3; j < (num3 + num4); j++)
                {
                    sb.Append(string.Format(CultureInfo.InvariantCulture, "{0:X2}", rawDeviceId[j]));
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static byte[] GetRawDeviceId()
        {
            var bufferSize = 64;
            var outbuff = new byte[bufferSize];
            BitConverter.GetBytes(bufferSize).CopyTo(outbuff, 0);

            var done = false;
            while (!done)
            {
                var bytesReturned = 0;
                if (!NativeMethods.KernelIoControl(IOCTL_HAL_GET_DEVICEID, IntPtr.Zero, 0, outbuff, bufferSize, ref bytesReturned))
                {
                    var error = Marshal.GetLastWin32Error();
                    switch (error)
                    {
                        case ERROR_NOT_SUPPORTED:
                            throw new NotSupportedException("IOCTL_HAL_GET_DEVICEID is not supported");

                        case ERROR_INSUFFICIENT_BUFFER:
                            {
                                bufferSize = BitConverter.ToInt32(outbuff, 0);
                                outbuff = new byte[bufferSize];
                                BitConverter.GetBytes(bufferSize).CopyTo(outbuff, 0);
                                continue;
                            }
                    }
                    throw new Win32Exception(error);
                }
                done = true;
            }

            return outbuff;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ShowCalibrationScreen()
        {
            NativeMethods.TouchCalibrate();
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ActiveSyncStart()
        {
            Marshal.ThrowExceptionForHR(NativeMethods.ActiveSyncStart());
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ActiveSyncStop()
        {
            Marshal.ThrowExceptionForHR(NativeMethods.ActiveSyncStop());
        }
    }
}
