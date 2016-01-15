namespace Smart.Threading
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 
    /// </summary>
    internal enum EVENT
    {
        PULSE = 1,
        RESET = 2,
        SET = 3,
    }

    /// <summary>
    /// 
    /// </summary>
    internal static class NativeMethods
    {
        internal const int INFINITE = -1;
        internal const int WAIT_TIMEOUT = 258;
        internal const int ERROR_ALREADY_EXISTS = 183;

        // Mutex

        [DllImport("coredll.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr CreateMutex(IntPtr lpMutexAttributes, [MarshalAs(UnmanagedType.Bool)] bool bInitialOwner, string lpName);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ReleaseMutex(IntPtr hMutex);

        // Semaphore

        [DllImport("coredll.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr CreateSemaphore(IntPtr lpSemaphoreAttributes, int lInitialCount, int lMaximumCount, string lpName);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ReleaseSemaphore(IntPtr hSemaphore, int lReleaseCount, out int lpPreviousCount);

        // EventWaitHandle

        [DllImport("coredll.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr OpenEvent(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, string lpName);

        [DllImport("coredll.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr CreateEvent(IntPtr lpEventAttributes, [MarshalAs(UnmanagedType.Bool)] bool bManualReset, [MarshalAs(UnmanagedType.Bool)] bool bInitialState, string lpName);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EventModify(IntPtr hEvent, EVENT ef);

        // Wait

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern int WaitForSingleObject(IntPtr hHandle, int dwMilliseconds);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern int WaitForMultipleObjects(int nCount, IntPtr[] lpHandles, [MarshalAs(UnmanagedType.Bool)] bool fWaitAll, int dwMilliseconds);

        // Close

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseHandle(IntPtr hObject);
    }
}
