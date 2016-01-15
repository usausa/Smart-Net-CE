namespace Smart.Windows.Forms
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    internal enum HookType
    {
        JournalPlayback = 1,
        JournalRecord = 0,
        KeyboardLowLevel = 20
    }

    /// <summary>
    /// 
    /// </summary>
    internal enum HookCode
    {
        Action,
        GetNext,
        Skip,
        NoRemove,
        SystemModalOn,
        SystemModalOff
    }

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct KBDLLHOOKSTRUCT
    {
        internal int VkCode;
        internal int ScanCode;
        internal int Flags;
        internal int Time;
        internal int ExtraInfo;
    }

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct MSG
    {
        internal IntPtr Hwnd;
        internal int Message;
        internal IntPtr WParam;
        internal IntPtr LParam;
        internal int Time;
        internal int X;
        internal int Y;
    }

    ///// <summary>
    ///// 
    ///// </summary>
    //[StructLayout( LayoutKind.Sequential )]
    //internal struct PAINTSTRUCT
    //{
    //    private IntPtr hdc;
    //    public bool fErase;
    //    public RECT rcPaint;
    //    public bool fRestore;
    //    public bool fIncUpdate;
    //    [MarshalAs( UnmanagedType.ByValArray, SizeConst = 32 )]
    //    public byte[] rgbReserved;
    //}

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct NOTIFYICONDATA
    {
        public int Size;
        public IntPtr Hwnd;
        public uint Id;
        public int Flags;
        public int CallbackMessage;
        public IntPtr Icon;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string Tip;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hWnd"></param>
    /// <param name="msg"></param>
    /// <param name="wParam"></param>
    /// <param name="lParam"></param>
    /// <returns></returns>
    internal delegate IntPtr WndProcDelegate(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="code"></param>
    /// <param name="wParam"></param>
    /// <param name="lParam"></param>
    /// <returns></returns>
    internal delegate int HookProc(HookCode code, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// 
    /// </summary>
    internal static class NativeMethods
    {
        // Module

        [DllImport("coredll.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int GetModuleFileName(IntPtr hModule, StringBuilder lpFilename, int nSize);

        // Message

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PeekMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

        [DllImport("coredll.dll", EntryPoint = "GetMessageW", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool TranslateMessage(out MSG lpMsg);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DispatchMessage(ref MSG lpMsg);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern void PostQuitMessage(int nExitCode);

        //[DllImport("coredll.dll", SetLastError = true)]
        //internal static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref RECT rect);

        //[DllImport("coredll.dll", SetLastError = true)]
        //internal static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref ListViewEx.ListViewHitTestInfo rect);

        // Window Long

        [DllImport("coredll.dll", SetLastError = true)]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, WndProcDelegate newProc);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr nValue);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr CallWindowProc(IntPtr pfn, IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        // System metrics

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern int GetSystemMetrics(int nIndex);

        // Module

        [DllImport("coredll.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr GetModuleHandle(string modName);

        // NativeWindow

        [DllImport("coredll.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr CreateWindowEx(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hwndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyWindow(IntPtr hwnd);

        // NotifyIcon

        [DllImport("coredll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean Shell_NotifyIcon(int dwMessage, ref NOTIFYICONDATA lpData);

        // Windows Hook

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr SetWindowsHookEx(HookType idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern int CallNextHookEx(IntPtr hhk, HookCode nCode, IntPtr wParam, IntPtr lParam);

        // Keyboard

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        // Draw

        //[DllImport( "coredll.dll", EntryPoint = "SetTextColor", SetLastError = true )]
        //internal static extern uint SetTextColor(IntPtr hdc, int crColor);

        //[DllImport( "coredll.dll", EntryPoint = "SetBkColor", SetLastError = true )]
        //internal static extern uint SetBkColor(IntPtr hdc, int crColor);

        //[DllImport( "coredll.dll" )]
        //internal extern static void InvalidateRect(IntPtr handle, IntPtr rect, bool erase);

        //[DllImport( "coredll.dll" )]
        //internal extern static void InvalidateRect(IntPtr handle, ref RECT rect, bool erase);

        //[DllImport( "coredll.dll" )]
        //internal extern static IntPtr BeginPaint(IntPtr hWnd, ref PAINTSTRUCT ps);

        //[DllImport( "coredll.dll" )]
        //internal extern static bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT ps);

        //[DllImport( "coredll.dll" )]
        //internal static extern int GetClipBox(IntPtr hdc, ref RECT lprc);
    }
}
