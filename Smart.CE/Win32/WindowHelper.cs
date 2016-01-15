namespace Smart.Win32
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    using Smart.WindowsCE;

    /// <summary>
    /// 
    /// </summary>
    public static class WindowHelper
    {
        private const uint SHDB_HIDE = 2;
        private const uint SHDB_SHOW = 1;

        private const int SHFS_SHOWTASKBAR = 0x1;
        private const int SHFS_HIDETASKBAR = 0x2;
        private const int SHFS_SHOWSIPBUTTON = 0x4;
        private const int SHFS_HIDESIPBUTTON = 0x8;
        private const int SHFS_SHOWSTARTICON = 0x10;
        private const int SHFS_HIDESTARTICON = 0x20;

        private const int ICON_SMALL = 0;
        private const int ICON_BIG = 1;

        private const int SHGFI_ICON = 0x000000100;
        private const int SHGFI_LARGEICON = 0x000000000;
        private const int SHGFI_SMALLICON = 0x000000001;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        public static void StartFullScreen(IntPtr handle)
        {
            var platform = DeviceManager.PlatformType;
            if ((platform == "PocketPC") || (platform == "SmartPhone"))
            {
                NativeMethods.SHFullScreen(handle, SHFS_HIDESTARTICON | SHFS_HIDESIPBUTTON | SHFS_HIDETASKBAR);

                var hTaskBar = Win32Window.FindWindow("HHTaskBar", null);
                Win32Window.ShowWindow(hTaskBar, SW.HIDE);

                Win32Window.MoveWindow(handle, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            }
            else
            {
                var hTaskBar = Win32Window.FindWindow("HHTaskBar", null);
                Win32Window.ShowWindow(hTaskBar, SW.HIDE);

                Win32Window.MoveWindow(handle, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        public static void EndFullScreen(IntPtr handle)
        {
            var platform = DeviceManager.PlatformType;
            if ((platform == "PocketPC") || (platform == "SmartPhone"))
            {
                NativeMethods.SHFullScreen(handle, SHFS_SHOWSTARTICON | SHFS_SHOWSIPBUTTON | SHFS_SHOWTASKBAR);

                var height = Screen.PrimaryScreen.Bounds.Height - Screen.PrimaryScreen.WorkingArea.Height;

                Win32Window.MoveWindow(handle, 0, height, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height - height);

                var hTaskBar = Win32Window.FindWindow("HHTaskBar", null);
                Win32Window.ShowWindow(hTaskBar, SW.SHOW);
            }
            else
            {
                var hTaskBar = Win32Window.FindWindow("HHTaskBar", null);
                Win32Window.ShowWindow(hTaskBar, SW.SHOW);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="value"></param>
        public static void SetDoneButton(IntPtr handle, bool value)
        {
            NativeMethods.SHDoneButton(handle, value ? SHDB_SHOW : SHDB_HIDE);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void DisableIME()
        {
            NativeMethods.ImmDisableIME(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="small"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", Justification = "P/Invoke")]
        public static Icon GetWindowIcon(IntPtr handle, bool small)
        {
            int processId;
            NativeMethods.GetWindowThreadProcessId(handle, out processId);
            var hInstance = NativeMethods.OpenProcess(0, false, processId);

            var fileName = new StringBuilder(256);
            var ret = NativeMethods.GetModuleFileName(IntPtr.Zero, fileName, fileName.Capacity);
            if (ret <= 0)
            {
                return null;
            }

            if (ret > fileName.Capacity)
            {
                throw new PathTooLongException("Assembly name is longer than MAX_PATH.");
            }

            var shinfo = new SHFILEINFO();
            NativeMethods.SHGetFileInfo(fileName.ToString(), 0, ref shinfo, Marshal.SizeOf(shinfo), SHGFI_ICON | (small ? SHGFI_SMALLICON : SHGFI_LARGEICON));

            NativeMethods.CloseHandle(hInstance);

            if (shinfo.Icon != IntPtr.Zero)
            {
                return Icon.FromHandle(shinfo.Icon);
            }

            var hParent = handle;
            while (hParent != IntPtr.Zero)
            {
                var hIcon = NativeMethods.SendMessage(hParent, (int)WM.GETICON, small ? ICON_SMALL : ICON_BIG, 0);
                if (hIcon != IntPtr.Zero)
                {
                    return Icon.FromHandle(hIcon);
                }

                hParent = NativeMethods.GetParent(hParent);
            }

            return null;
        }
    }
}
