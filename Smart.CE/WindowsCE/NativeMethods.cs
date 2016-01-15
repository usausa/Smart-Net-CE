namespace Smart.WindowsCE
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct SystemTime
    {
        internal short Year;
        internal short Month;
        internal short DayOfWeek;
        internal short Day;
        internal short Hour;
        internal short Minute;
        internal short Second;
        internal short Millisecond;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        internal static SystemTime FromDateTime(DateTime dt)
        {
            return new SystemTime
            {
                Year = (short)dt.Year,
                Month = (short)dt.Month,
                DayOfWeek = (short)dt.DayOfWeek,
                Day = (short)dt.Day,
                Hour = (short)dt.Hour,
                Minute = (short)dt.Minute,
                Second = (short)dt.Second,
                Millisecond = (short)dt.Millisecond
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal DateTime ToDateTime()
        {
            if ((Year == 0) && (Month == 0) && (Day == 0) && (Hour == 0) && (Minute == 0) && (Second == 0))
            {
                return DateTime.MinValue;
            }
            return new DateTime(Year, Month, Day, Hour, Minute, Second, Millisecond);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct NLED_COUNT_INFO
    {
        internal uint Leds;
    }

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct NLED_SETTINGS_INFO
    {
        internal int LedNum;
        internal int OffOnBlink;
        internal int TotalCycleTime;
        internal int OnTime;
        internal int OffTime;
        internal int MetaCycleOn;
        internal int MetaCycleOff;
    }

    /// <summary>
    /// 
    /// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal struct NLED_SUPPORTS_INFO
    //{
    //    internal uint LedNum;
    //    internal int CycleAdjust;
    //    internal bool AdjustTotalCycleTime;
    //    internal bool AdjustOnTime;
    //    internal bool AdjustOffTime;
    //    internal bool MetaCycleOn;
    //    internal bool MetaCycleOff;
    //}

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct NDISUIO_QUERY_BINDING
    {
        public int BindingIndex;
        public int DeviceNameOffset;
        public int DeviceNameLength;
        public int DeviceDescrOffset;
        public int DeviceDescrLength;
    }

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct NIC_STATISTICS
    {
        internal int Size;                      // Of this structure.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2151:FieldsWithCriticalTypesShouldBeCriticalFxCopRule", Justification = "P/Invoke")]
        internal unsafe byte* DeviceName;       // The device name to be queried..
        internal int DeviceState;               // DEVICE_STATE_XXX above
        internal int MediaType;                 // NdisMediumXXX
        internal int MediaState;                // MEDIA_STATE_XXX above
        internal int PhysicalMediaType;
        internal int LinkSpeed;                 // In 100bits/s. 10Mb/s = 100000
        internal long PacketsSent;
        internal long PacketsReceived;
        internal int InitTime;                  // In milliseconds
        internal int ConnectTime;               // In seconds
        internal long BytesSent;                // 0 - Unknown (or not supported)
        internal long BytesReceived;            // 0 - Unknown (or not supported)
        internal long DirectedBytesReceived;
        internal long DirectedPacketsReceived;  
        internal int PacketsReceiveErrors;
        internal int PacketsSendErrors;
        internal int ResetCount;
        internal int MediaSenseConnectCount;
        internal int MediaSenseDisconnectCount;
    }

    /// <summary>
    /// 
    /// </summary>
    internal class NdisuioQueryOidBuffer
    {
        internal int Size { get; private set; }

        internal byte[] Data { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="oid"></param>
        /// <param name="deviceName"></param>
        internal unsafe NdisuioQueryOidBuffer(int size, uint oid, byte* deviceName)
        {
            Size = size + 8;
            Data = new byte[Size];
            var oidBytes = BitConverter.GetBytes(oid);
            Buffer.BlockCopy(oidBytes, 0, Data, 0, 4);
            var deviceNameBytes = BitConverter.GetBytes((uint)deviceName);
            Buffer.BlockCopy(deviceNameBytes, 0, Data, 4, 4);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        internal int GetDataInt(int offset)
        {
            return BitConverter.ToInt32(Data, offset + 8);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal static class NativeMethods
    {
        // Cpu Usage

        [DllImport("coredll.dll")]
        internal static extern uint GetIdleTime();

        [DllImport("coredll.dll")]
        internal static extern uint GetTickCount();

        // DateTime

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetLocalTime(ref SystemTime lpSystemTime);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetLocalTime(ref SystemTime lpSystemTime);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetSystemTime(ref SystemTime lpSystemTime);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetSystemTime(ref SystemTime lpSystemTime);

        // ActiveSync

        [DllImport("aygshell.dll")]
        internal static extern int ActiveSyncStart();

        [DllImport("aygshell.dll")]
        internal static extern int ActiveSyncStop();

        // Device

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern void TouchCalibrate();

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SystemParametersInfo(int uiAction, int uiParam, byte[] pvParam, int fWinIni);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern void GetSystemInfo(out SystemInfo psi);

        // LED

        [DllImport("coredll.dll", EntryPoint = "NLedGetDeviceInfo", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool NLedGetDeviceCount(short nId, ref NLED_COUNT_INFO pOutput);

        //[DllImport("coredll.dll", EntryPoint = "NLedGetDeviceInfo", SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //internal static extern bool NLedGetDeviceSupports(short nId, ref NLED_SUPPORTS_INFO pOutput);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool NLedSetDevice(short nId, ref NLED_SETTINGS_INFO pOutput);

        // NDIS

        [DllImport("coredll.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, int lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, int hTemplateFile);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseHandle(IntPtr hObject);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeviceIoControl(IntPtr hDevice, uint dwIoControlCode, byte[] lpInBuffer, int nInBufferSize, byte[] lpOutBuffer, int nOutBufferSize, out int lpBytesReturned, IntPtr lpOverlapped);

        // Memory

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetSystemMemoryDivision(ref int lpdwStorePages, ref int lpdwRamPages, ref int lpdwPageSize);
    
        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetSystemMemoryDivision(int dwStorePages);

        [DllImport("coredll.dll")]
        internal static extern void GlobalMemoryStatus(out MemoryStatus msce);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetStoreInformation(out StoreInformation lpsi);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetSystemPowerStatusEx2(ref PowerStatus pStatus, int dwLen, [MarshalAs(UnmanagedType.Bool)] bool fUpdate);

        // Power

        [DllImport("aygshell.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ExitWindowsEx(int uFlags, int dwReserved);

        [DllImport("coredll.dll", SetLastError = true)]
        public static extern void GwesPowerOff();

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern int SetSystemPowerState(IntPtr psState, int stateFlags, int options);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern void SystemIdleTimerReset();

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool KernelIoControl(int dwIoControlCode, IntPtr lpInBuf, int nInBufSize, byte[] lpOutBuf, int nOutBufSize, ref int lpBytesReturned);
    }
}
