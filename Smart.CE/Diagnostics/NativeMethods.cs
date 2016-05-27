namespace Smart.Diagnostics
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///
    /// </summary>
    internal static class NativeMethods
    {
        // Debug

        [DllImport("coredll.dll", CharSet = CharSet.Unicode, EntryPoint = "NKDbgPrintfW")]
        internal static extern void DebugMsg(string message);

        // DebugMessage

        [DllImport("coredll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetFileVersionInfo(string filename, uint handle, uint len, IntPtr data);

        [DllImport("coredll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern uint GetFileVersionInfoSize(string filename, ref IntPtr handle);

        [DllImport("coredll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool VerQueryValue(IntPtr block, string subblock, ref IntPtr buffer, ref uint len);

        [DllImport("coredll", SetLastError = true)]
        internal static extern IntPtr LocalAlloc(uint uFlags, uint uBytes);

        [DllImport("coredll", SetLastError = true)]
        internal static extern IntPtr LocalFree(IntPtr hMem);
    }
}
