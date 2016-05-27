namespace Smart.Windows.Forms
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    using Smart.Runtime.InteropServices;
    using Smart.Win32;

    /// <summary>
    ///
    /// </summary>
    public static class ComboBoxExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="combo"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static bool GetDroppedDown(this ComboBox combo)
        {
            return Win32Window.SendMessage(combo.Handle, (int)CB.GETDROPPEDSTATE, 0, 0) != IntPtr.Zero;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="value"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static void SetDroppedDown(this ComboBox combo, bool value)
        {
            Win32Window.SendMessage(combo.Handle, (int)CB.SHOWDROPDOWN, value ? 1 : 0, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="combo"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static int GetDropDownWidth(this ComboBox combo)
        {
            return (int)Win32Window.SendMessage(combo.Handle, (int)CB.GETDROPPEDWIDTH, 0, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="value"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static void SetDropDownWidth(this ComboBox combo, int value)
        {
            Win32Window.SendMessage(combo.Handle, (int)CB.SETDROPPEDWIDTH, value, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static int FindString(this ComboBox combo, string s)
        {
            return FindString(combo, s, -1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="s"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static int FindString(this ComboBox combo, string s, int startIndex)
        {
            var lParam = MarshalEx.StringToHGlobalUni(s);
            var find = (int)Win32Window.SendMessage(combo.Handle, (int)CB.FINDSTRING, startIndex, lParam);
            Marshal.FreeHGlobal(lParam);

            return find;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static int FindStringExact(this ComboBox combo, string s)
        {
            return FindStringExact(combo, s, -1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="s"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static int FindStringExact(this ComboBox combo, string s, int startIndex)
        {
            var lParam = MarshalEx.StringToHGlobalUni(s);
            var find = (int)Win32Window.SendMessage(combo.Handle, (int)CB.FINDSTRINGEXACT, startIndex, lParam);
            Marshal.FreeHGlobal(lParam);

            return find;
        }
    }
}
