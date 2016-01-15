namespace Smart.Windows.Forms
{
    using System;
    using System.Windows.Forms;

    using Smart.Win32;

    /// <summary>
    /// 
    /// </summary>
    public static class ButtonExtensions
    {
        private const int BM_CLOCK = 0x00f5;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="button"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static void PerformClick(this Button button)
        {
            if (button.Enabled && button.Visible)
            {
                Win32Window.PostMessage(button.Handle, BM_CLOCK, IntPtr.Zero, IntPtr.Zero);
            }
        }
    }
}
