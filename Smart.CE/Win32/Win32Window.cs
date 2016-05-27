namespace Smart.Win32
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    ///
    /// </summary>
    public static class Win32Window
    {
        //--------------------------------------------------------------------------------
        // Handle
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "P/Invoke")]
        public static IntPtr GetActiveWindow()
        {
            return NativeMethods.GetActiveWindow();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "P/Invoke")]
        public static IntPtr GetFocus()
        {
            return NativeMethods.GetFocus();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="className"></param>
        /// <param name="wndName"></param>
        /// <returns></returns>
        public static IntPtr FindWindow(string className, string wndName)
        {
            return NativeMethods.FindWindow(className, wndName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="nCmd"></param>
        /// <returns></returns>
        public static IntPtr GetWindow(IntPtr hwnd, GW nCmd)
        {
            return NativeMethods.GetWindow(hwnd, nCmd);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static IntPtr GetParent(IntPtr hwnd)
        {
            return NativeMethods.GetParent(hwnd);
        }

        //--------------------------------------------------------------------------------
        // Get/Set
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static Rectangle GetWindowRect(IntPtr hwnd)
        {
            RECT rect;
            if (!NativeMethods.GetWindowRect(hwnd, out rect))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static string GetWindowText(IntPtr hwnd)
        {
            var sb = new StringBuilder(256);
            var length = NativeMethods.GetWindowText(hwnd, sb, sb.Capacity);
            if (length > 0)
            {
                sb.Length = length;
            }
            return sb.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool SetWindowText(IntPtr hwnd, string text)
        {
            return NativeMethods.SetWindowText(hwnd, text);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static WS GetWindowLongStyle(IntPtr hwnd)
        {
            return (WS)NativeMethods.GetWindowLong(hwnd, GWL.STYLE);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="value"></param>
        public static void SetWindowStyle(IntPtr hwnd, WS value)
        {
            NativeMethods.SetWindowLong(hwnd, GWL.STYLE, (int)value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="style"></param>
        public static void SetWindowStyle(IntPtr hwnd, int style)
        {
            NativeMethods.SetWindowLong(hwnd, GWL.STYLE, style);
            NativeMethods.SetWindowPos(hwnd, HWND.TOP, 0, 0, 0, 0, SWP.DRAWFRAME | SWP.NOACTIVATE | SWP.NOZORDER | SWP.NOMOVE | SWP.NOSIZE);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="removeStyle"></param>
        /// <param name="addStyle"></param>
        public static void UpdateWindowStyle(IntPtr hwnd, int removeStyle, int addStyle)
        {
            var style = NativeMethods.GetWindowLong(hwnd, GWL.STYLE);
            style &= ~removeStyle;
            style |= addStyle;
            NativeMethods.SetWindowLong(hwnd, GWL.STYLE, style);
            NativeMethods.SetWindowPos(hwnd, HWND.TOP, 0, 0, 0, 0, SWP.DRAWFRAME | SWP.NOACTIVATE | SWP.NOZORDER | SWP.NOMOVE | SWP.NOSIZE);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static WS_EX GetWindowLongExtendedStyle(IntPtr hwnd)
        {
            return (WS_EX)NativeMethods.GetWindowLong(hwnd, GWL.EXSTYLE);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="value"></param>
        public static void SetWindowExtendedStyle(IntPtr hwnd, WS_EX value)
        {
            NativeMethods.SetWindowLong(hwnd, GWL.EXSTYLE, (int)value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int GetWindowLong(IntPtr hwnd, GWL index)
        {
            return NativeMethods.GetWindowLong(hwnd, index);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="index"></param>
        /// <param name="nValue"></param>
        public static void SetWindowLong(IntPtr hwnd, GWL index, int nValue)
        {
            NativeMethods.SetWindowLong(hwnd, index, nValue);
        }

        //--------------------------------------------------------------------------------
        // Operation
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="bEnable"></param>
        /// <returns></returns>
        public static bool EnableWindow(IntPtr hwnd, bool bEnable)
        {
            return NativeMethods.EnableWindow(hwnd, bEnable);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static bool SetFocus(IntPtr hwnd)
        {
            return NativeMethods.SetFocus(hwnd);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static bool SetForegroundWindow(IntPtr hwnd)
        {
            return NativeMethods.SetForegroundWindow(hwnd);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="pos"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        public static bool SetWindowPos(IntPtr hwnd, HWND pos, int x, int y, int cx, int cy, SWP uFlags)
        {
            return NativeMethods.SetWindowPos(hwnd, pos, x, y, cx, cy, uFlags);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        public static bool ShowWindow(IntPtr hwnd, SW nCmdShow)
        {
            return NativeMethods.ShowWindow(hwnd, nCmdShow);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <returns></returns>
        public static IntPtr MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy)
        {
            return NativeMethods.MoveWindow(hwnd, x, y, cx, cy, 1);
        }

        //--------------------------------------------------------------------------------
        // Post/Send
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public static IntPtr PostMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            return NativeMethods.PostMessage(hwnd, msg, wParam, lParam);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public static IntPtr SendMessage(IntPtr hwnd, int msg, int wParam, int lParam)
        {
            return NativeMethods.SendMessage(hwnd, msg, wParam, lParam);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public static IntPtr SendMessage(IntPtr hwnd, int msg, int wParam, IntPtr lParam)
        {
            return NativeMethods.SendMessage(hwnd, msg, wParam, lParam);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public static IntPtr SendMessage(IntPtr hwnd, int msg, int wParam, byte[] lParam)
        {
            return NativeMethods.SendMessage(hwnd, msg, wParam, lParam);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public static IntPtr SendMessage(IntPtr hwnd, int msg, int wParam, string lParam)
        {
            return NativeMethods.SendMessage(hwnd, msg, wParam, lParam);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public static IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wParam, string lParam)
        {
            return NativeMethods.SendMessage(hwnd, msg, wParam, lParam);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public static IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wParam, byte[] lParam)
        {
            return NativeMethods.SendMessage(hwnd, msg, wParam, lParam);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public static IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wParam, int lParam)
        {
            return NativeMethods.SendMessage(hwnd, msg, wParam, lParam);
        }
    }
}
