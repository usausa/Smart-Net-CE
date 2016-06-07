namespace Smart.Drawing
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;

    /// <summary>
    ///
    /// </summary>
    public static class FontExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="font"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static Font ToClearType(this Font font)
        {
            var lf = new LOGFONT();

            var hFont = font.ToHfont();
            if (NativeMethods.GetObject(hFont, Marshal.SizeOf(lf), ref lf) == 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            NativeMethods.DeleteObject(hFont);

            lf.Quality = 6; // CLEARTYPE_COMPAT_QUALITY

            hFont = NativeMethods.CreateFontIndirect(ref lf);

            return Font.FromHfont(hFont);
        }
    }
}
