namespace Smart.Net.NetworkInformation
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct IP_OPTION_INFORMATION
    {
        internal byte Ttl;
        internal byte Tos;
        internal byte Flags;
        internal byte OptionsSize;
        internal IntPtr OptionsData;

        internal IP_OPTION_INFORMATION(PingOptions options)
        {
            Ttl = 0x80;
            Tos = 0;
            Flags = 0;
            OptionsSize = 0;
            OptionsData = IntPtr.Zero;

            if (options != null)
            {
                Ttl = (byte)options.Ttl;
                if (options.DontFragment)
                {
                    Flags = 2;
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct ICMP_ECHO_REPLY
    {
        internal uint Address;
        internal uint Status;
        internal uint RoundTripTime;
        internal ushort DataSize;
        internal ushort Reserved;
        internal IntPtr Data;
        internal byte Ttl;
        internal byte Tos;
        internal byte Flags;
        internal byte OptionsSize;
        internal IntPtr OptionsData;
    }

    /// <summary>
    /// 
    /// </summary>
    internal static class NativeMethods
    {
        [DllImport("iphlpapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IcmpCloseHandle(IntPtr handle);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        internal static extern IntPtr IcmpCreateFile();

        [DllImport("iphlpapi.dll", SetLastError = true)]
        internal static extern uint IcmpSendEcho2(IntPtr icmpHandle, IntPtr hEvent, IntPtr apcRoutine, IntPtr apcContext, uint ipAddress, IntPtr data, ushort dataSize, ref IP_OPTION_INFORMATION options, IntPtr replyBuffer, uint replySize, uint timeout);
    }
}
