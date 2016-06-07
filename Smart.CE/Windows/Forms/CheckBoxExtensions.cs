namespace Smart.Windows.Forms
{
    using System.Windows.Forms;

    using Smart.Win32;

    /// <summary>
    ///
    /// </summary>
    public static class CheckBoxExtensions
    {
        private const int BS_RIGHTBUTTON = 0x00000020;

        /// <summary>
        ///
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static bool GetRightToLeft(this CheckBox check)
        {
            var style = Win32Window.GetWindowLong(check.Handle, GWL.STYLE);
            return (style & BS_RIGHTBUTTON) != 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="check"></param>
        /// <param name="value"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static void SetRightToLeft(this CheckBox check, bool value)
        {
            var style = Win32Window.GetWindowLong(check.Handle, GWL.STYLE);
            if (value)
            {
                style |= BS_RIGHTBUTTON;
            }
            else
            {
                style &= ~BS_RIGHTBUTTON;
            }
            Win32Window.SetWindowLong(check.Handle, GWL.STYLE, style);
        }
    }
}
