namespace Smart.Windows.Forms
{
    using System.Windows.Forms;

    using Smart.Win32;

    /// <summary>
    /// 
    /// </summary>
    public static class TextBoxExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static void Cut(this TextBox text)
        {
            Win32Window.SendMessage(text.Handle, (int)WM.CUT, 0, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static void Copy(this TextBox text)
        {
            Win32Window.SendMessage(text.Handle, (int)WM.COPY, 0, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static void Paste(this TextBox text)
        {
            Win32Window.SendMessage(text.Handle, (int)WM.PASTE, 0, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static void Clear(this TextBox text)
        {
            Win32Window.SendMessage(text.Handle, (int)WM.CLEAR, 0, 0);
        }
    }
}
