namespace Smart.Win32
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct SHFILEINFO
    {
        internal IntPtr Icon;
        internal int Index;
        internal uint Attributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        internal string DisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        internal string TypeName;
    }

    /// <summary>
    /// 
    /// </summary>
    internal static class NativeMethods
    {
        // Handle

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr GetActiveWindow();

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr GetFocus();

        [DllImport("coredll.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr FindWindow(string className, string windowName);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr GetWindow(IntPtr hwnd, GW nCmd);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr GetParent(IntPtr hwnd);

        // Get/Set

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("coredll.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int GetWindowText(IntPtr hwnd, StringBuilder sb, int maxCount);

        [DllImport("coredll.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowText(IntPtr hwnd, string lpString);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern int GetWindowLong(IntPtr hwnd, GWL index);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern void SetWindowLong(IntPtr hwnd, GWL index, int nValue);

        // Operation

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnableWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool bEnable);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetFocus(IntPtr hwnd);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetForegroundWindow(IntPtr hwnd);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowPos(IntPtr hwnd, HWND pos, int x, int y, int cx, int cy, SWP uFlags);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ShowWindow(IntPtr hwnd, SW nCmdShow);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, int repaint);

        // Post/Send

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr PostMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr SendMessage(IntPtr hwnd, int msg, int wParam, int lParam);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr SendMessage(IntPtr hwnd, int msg, int wParam, IntPtr lParam);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr SendMessage(IntPtr hwnd, int msg, int wParam, byte[] lParam);

        [DllImport("coredll.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr SendMessage(IntPtr hwnd, int msg, int wParam, string lParam);

        [DllImport("coredll.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wParam, string lParam);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wParam, byte[] lParam);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wParam, int lParam);

        // WindowHelper

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr OpenProcess(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

        [DllImport("coredll.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int GetModuleFileName(IntPtr hModule, StringBuilder lpFilename, int nSize);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseHandle(IntPtr hObject);

        [DllImport("coredll.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, int cbSizeFileInfo, int uFlags);

        // Form

        [DllImport("aygshell.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SHFullScreen(IntPtr hwnd, int state);

        [DllImport("aygshell.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SHDoneButton(IntPtr hwndRequester, uint dwState);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ImmDisableIME(int idThread);
    }
}
