namespace Smart.Net
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using Smart.Runtime.InteropServices;

    /// <summary>
    ///
    /// </summary>
    internal enum NetworkErrors
    {
        NoError = 0,
        AccessDenied = 5,
        InvalidHandle = 6,
        NotEnoughMemory = 8,
        NotSupported = 50,
        UnexpectedNetError = 59,
        InvalidPassword = 86,
        InvalidParameter = 87,
        InvalidLevel = 124,
        Busy = 170,
        MoreData = 234,
        InvalidAddress = 487,
        DeviceAlreadyRemembered = 1202,
        ExtentedError = 1208,
        Cancelled = 1223,
        Retry = 1237,
        BadUsername = 2202,
        NoNetwork = 1222
    }

    /// <summary>
    ///
    /// </summary>
    public static class MapDrive
    {
        private const int RESOURCE_GLOBALNET = 0x00000002;
        private const int RESOURCE_REMEMBERED = 0x00000003;

        private const int RESOURCETYPE_DISK = 0x00000001;

        private const int RESOURCEDISPLAYTYPE_SHARE = 0x00000003;
        private const int RESOURCEUSAGE_CONNECTABLE = 0x00000001;

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="netRes"></param>
        /// <param name="shareName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public static void Add(IntPtr hwnd, string netRes, string shareName, string userName, string password)
        {
            var netresource = new NETRESOURCE();
            var ptr = IntPtr.Zero;
            try
            {
                netresource.Scope = RESOURCE_GLOBALNET | RESOURCE_REMEMBERED;
                netresource.Type = RESOURCETYPE_DISK;
                netresource.DisplayType = RESOURCEDISPLAYTYPE_SHARE;
                netresource.Usage = RESOURCEUSAGE_CONNECTABLE;
                netresource.RemoteName = MarshalEx.StringToHGlobalUni(netRes);
                netresource.LocalName = MarshalEx.StringToHGlobalUni(shareName);
                netresource.Comment = IntPtr.Zero;
                netresource.Provider = IntPtr.Zero;

                ptr = Marshal.AllocHGlobal(Marshal.SizeOf(netresource));
                Marshal.StructureToPtr(netresource, ptr, false);

                var ret = NativeMethods.WNetAddConnection3(hwnd, ptr, password, userName, 1);
                if (ret != 0)
                {
                    throw new Win32Exception(ret, ((NetworkErrors)ret).ToString());
                }
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
                Marshal.FreeHGlobal(netresource.RemoteName);
                Marshal.FreeHGlobal(netresource.LocalName);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="shareName"></param>
        /// <param name="force"></param>
        public static void Disconnect(string shareName, bool force)
        {
            if (!string.IsNullOrEmpty(shareName))
            {
                var ret = NativeMethods.WNetCancelConnection2(shareName, 1, force ? 1 : 0);
                if (ret != 0)
                {
                    throw new Win32Exception(ret, ((NetworkErrors)ret).ToString());
                }
            }
        }
    }
}
