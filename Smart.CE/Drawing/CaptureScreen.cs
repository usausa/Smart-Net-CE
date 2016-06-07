namespace Smart.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    /// <summary>
    ///
    /// </summary>
    public static class CaptureScreen
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="fileName"></param>
        public static void Snapshot(string fileName)
        {
            Snapshot(IntPtr.Zero, Screen.PrimaryScreen.Bounds, fileName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static Bitmap Snapshot()
        {
            return Snapshot(IntPtr.Zero, Screen.PrimaryScreen.Bounds);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="rect"></param>
        /// <param name="fileName"></param>
        public static void Snapshot(IntPtr hwnd, Rectangle rect, string fileName)
        {
            using (var bitmap = Snapshot(hwnd, rect))
            {
                bitmap.Save(fileName, ImageFormat.Bmp);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "Factory")]
        public static Bitmap Snapshot(IntPtr hwnd, Rectangle rect)
        {
            var bitmap = new Bitmap(rect.Width, rect.Height);

            var dc = NativeMethods.GetDC(hwnd);

            using (var deviceGraphics = Graphics.FromHdc(dc))
            using (var captureGraphics = Graphics.FromImage(bitmap))
            {
                NativeMethods.BitBlt(captureGraphics.GetHdc(), 0, 0, rect.Width, rect.Height,
                                      deviceGraphics.GetHdc(), rect.Left, rect.Top, RasterOp.SRCCOPY);
            }

            NativeMethods.ReleaseDC(hwnd, dc);

            return bitmap;
        }
    }
}
