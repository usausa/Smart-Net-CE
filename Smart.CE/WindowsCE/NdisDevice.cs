namespace Smart.WindowsCE
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class NdisDevice : IDisposable
    {
        private const uint IOCTL_NDISUIO_QUERY_BINDING = 0x12080C;
        private const uint IOCTL_NDISUIO_QUERY_OID_VALUE = 0x120804;
        private const uint IOCTL_NDISUIO_NIC_STATISTICS = 0x120824;

        private const int OID_802_11_RSSI = 0x0D010206;

        private const int MEDIA_STATE_CONNECTED = 0;

        private const int FILE_ATTRIBUTE_NORMAL = 0x00000080;
        private const int FILE_FLAG_OVERLAPPED = 0x40000000;

        private const int OPEN_EXISTING = 3;

        private byte[] deviceNameBytes;

        private IntPtr handle;

        public string DeviceName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ndisDeviceName"></param>
        public NdisDevice(string ndisDeviceName)
        {
            handle = NativeMethods.CreateFile(ndisDeviceName, 0xC0000000, (uint)FileShare.None, 0,
                                              OPEN_EXISTING, (uint)FILE_ATTRIBUTE_NORMAL | FILE_FLAG_OVERLAPPED, 0);
            UpdateDeviceName();
        }

        /// <summary>
        /// 
        /// </summary>
        ~NdisDevice()
        {
            Dispose(false);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (handle != IntPtr.Zero)
            {
                NativeMethods.CloseHandle(handle);
                handle = IntPtr.Zero;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public unsafe void UpdateDeviceName()
        {
            var buffer = new byte[1024];
            int bytesReturned;
            if (!NativeMethods.DeviceIoControl(handle, IOCTL_NDISUIO_QUERY_BINDING, buffer, buffer.Length, buffer, buffer.Length, out bytesReturned, IntPtr.Zero))
            {
                return;
            }

            fixed (byte* ptr = buffer)
            {
                var binding = (NDISUIO_QUERY_BINDING*)ptr;

                if (!NativeMethods.DeviceIoControl(handle, IOCTL_NDISUIO_QUERY_BINDING, buffer, buffer.Length, buffer, buffer.Length, out bytesReturned, IntPtr.Zero))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                deviceNameBytes = new byte[binding->DeviceNameLength];
                Buffer.BlockCopy(buffer, binding->DeviceNameOffset, deviceNameBytes, 0, deviceNameBytes.Length);

                DeviceName = Encoding.Unicode.GetString(deviceNameBytes, 0, deviceNameBytes.Length).TrimEnd(new char[1]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public unsafe bool MediaConnected
        {
            get
            {
                if (String.IsNullOrEmpty(DeviceName))
                {
                    return false;
                }

                var buffer = new byte[sizeof(NIC_STATISTICS)];
                fixed (byte* name = deviceNameBytes)
                fixed (byte* ptr = buffer)
                {
                    var statistics = (NIC_STATISTICS*)ptr;
                    statistics->Size = buffer.Length;
                    statistics->DeviceName = name;
                    int bytesReturned;

                    if (!NativeMethods.DeviceIoControl(handle, IOCTL_NDISUIO_NIC_STATISTICS, buffer, buffer.Length, buffer, buffer.Length, out bytesReturned, IntPtr.Zero))
                    {
                        return false;
                    }

                    return statistics->MediaState == MEDIA_STATE_CONNECTED;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public unsafe int TxPowerLevel
        {
            get
            {
                if (String.IsNullOrEmpty(DeviceName))
                {
                    return 0;
                }

                fixed (byte* name = deviceNameBytes)
                {
                    var queryOid = new NdisuioQueryOidBuffer(4, OID_802_11_RSSI, name);

                    var buffer = queryOid.Data;
                    int bytesReturned;
                    if (!NativeMethods.DeviceIoControl(handle, IOCTL_NDISUIO_QUERY_OID_VALUE, buffer, buffer.Length, buffer, buffer.Length, out bytesReturned, IntPtr.Zero))
                    {
                        return int.MinValue;
                    }

                    return queryOid.GetDataInt(0);
                }
            }
        }
    }
}
