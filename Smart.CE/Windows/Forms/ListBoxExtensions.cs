namespace Smart.Windows.Forms
{
    using System;
    using System.Windows.Forms;

    using Smart.Win32;

    /// <summary>
    ///
    /// </summary>
    public static class ListBoxExtensions
    {
        private const int LVS_SINGLESEL = 0x4;

        private const int LVM_FIRST = 0x1000;
        private const int LVM_SCROLL = LVM_FIRST + 20;

        /// <summary>
        ///
        /// </summary>
        /// <param name="listView"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static IntPtr GetHeaderHandle(this ListView listView)
        {
            return Win32Window.SendMessage(listView.Handle, (int)LVM.GETHEADER, 0, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="listView"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static bool GetMultiline(this ListView listView)
        {
            var style = Win32Window.GetWindowLong(listView.Handle, GWL.STYLE);
            return (style & LVS_SINGLESEL) != 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="multiline"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static void SetMultiline(this ListView listView, bool multiline)
        {
            var style = Win32Window.GetWindowLong(listView.Handle, GWL.STYLE);
            if (multiline)
            {
                style &= ~LVS_SINGLESEL;
            }
            else
            {
                style |= LVS_SINGLESEL;
            }
            Win32Window.SetWindowLong(listView.Handle, GWL.STYLE, style);
        }

        // TODO HitTest

        /// <summary>
        ///
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static void Scroll(this ListView listView, int x, int y)
        {
            Win32Window.SendMessage(listView.Handle, LVM_SCROLL, x, y);
        }
    }
}
