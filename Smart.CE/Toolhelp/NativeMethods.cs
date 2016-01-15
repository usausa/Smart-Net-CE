namespace Smart.ToolHelp
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 
    /// </summary>
    [Flags]
    internal enum CreateToolhelp32Flags
    {
        SNAPHEAPLIST = 1,
        SNAPPROCESS = 2,
        SNAPTHREAD = 4,
        SNAPMODULE = 8,
        SNAPNOHEAPS = 0x40000000,
    }

    /// <summary>
    /// 
    /// </summary>
    internal struct HEAPLIST32
    {
        internal uint Size;
        internal uint ProcessId;
        internal uint HeapId;
        internal uint Flags;
    }

    /// <summary>
    /// 
    /// </summary>
    internal struct HEAPENTRY32
    {
        internal uint Size;
        internal IntPtr Handle;
        internal uint Address;
        internal uint BlockSize;
        internal uint Flags;
        internal uint LockCount;
        internal uint Resvd;
        internal uint ProcessId;
        internal uint HeapId;
    }

    /// <summary>
    /// 
    /// </summary>
    internal static class NativeMethods
    {
        internal const int INVALID_HANDLE_VALUE = -1;

        internal const int PROCESS_TERMINATE = 1;

        // Toolhelp

        [DllImport("toolhelp.dll", SetLastError = true)]
        internal static extern IntPtr CreateToolhelp32Snapshot(CreateToolhelp32Flags flags, uint processid);
        
        [DllImport("toolhelp.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseToolhelp32Snapshot(IntPtr handle);

        // Process

        [DllImport("toolhelp.dll", SetLastError = true)]
        internal static extern int Process32First(IntPtr handle, byte[] pe);
        
        [DllImport("toolhelp.dll", SetLastError = true)]
        internal static extern int Process32Next(IntPtr handle, byte[] pe);

        // Thread

        [DllImport("toolhelp.dll", SetLastError = true)]
        internal static extern int Thread32First(IntPtr handle, byte[] te);
        
        [DllImport("toolhelp.dll", SetLastError = true)]
        internal static extern int Thread32Next(IntPtr handle, byte[] te);

        // Module

        [DllImport("toolhelp.dll", SetLastError = true)]
        internal static extern int Module32First(IntPtr handle, byte[] me);
        
        [DllImport("toolhelp.dll", SetLastError = true)]
        internal static extern int Module32Next(IntPtr handle, byte[] me);

        // Heap

        [DllImport("toolhelp.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Heap32ListFirst(IntPtr hSnapshot, ref HEAPLIST32 lphl);
        
        [DllImport("toolhelp.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Heap32ListNext(IntPtr hSnapshot, ref HEAPLIST32 lphl);
        
        [DllImport("toolhelp.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Heap32First(IntPtr hSnapshot, ref HEAPENTRY32 lphe, uint processId, uint heapId);
        
        [DllImport("toolhelp.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Heap32Next(IntPtr hSnapshot, ref HEAPENTRY32 lphe);

        // Etc

        [DllImport("coredll.dll", EntryPoint = "OpenProcess", SetLastError = true)]
        internal static extern IntPtr OpenProcess(uint fdwAccess, [MarshalAs(UnmanagedType.Bool)] bool fInherit, uint processId);

        [DllImport("coredll.dll", EntryPoint = "CloseHandle", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseHandle(IntPtr hObject);

        [DllImport("coredll.dll", EntryPoint = "TerminateProcess", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool TerminateProcess(IntPtr hProcess, int uExitCode);

        [DllImport("coredll.dll", EntryPoint = "CeGetThreadPriority", SetLastError = true)]
        internal static extern int GetThreadPriority(uint hThread);

        [DllImport("coredll.dll", EntryPoint = "CeSetThreadPriority", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetThreadPriority(uint hThread, int nPriority);

        [DllImport("coredll.dll", EntryPoint = "CeGetThreadQuantum", SetLastError = true)]
        internal static extern int GetThreadQuantum(uint hThread);

        [DllImport("coredll.dll", EntryPoint = "CeSetThreadQuantum", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetThreadQuantum(uint hThread, int dwTime);

        [DllImport("coredll.dll", EntryPoint = "SuspendThread", SetLastError = true)]
        internal static extern uint SuspendThread(uint hThread);

        [DllImport("coredll.dll", EntryPoint = "ResumeThread", SetLastError = true)]
        internal static extern uint ResumeThread(uint hThread);

        [DllImport("coredll.dll", EntryPoint = "TerminateThread", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool TerminateThread(uint hThread, int dwExitCode);
    }
}
