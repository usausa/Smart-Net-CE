namespace Smart.Drawing
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    public class Shape : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public IntPtr Region { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public Shape(Stream stream)
        {
            RebuildRegion(stream, Color.Transparent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="color"></param>
        public Shape(Stream stream, Color color)
        {
            RebuildRegion(stream, color);
        }

        /// <summary>
        /// 
        /// </summary>
        ~Shape()
        {
            Dispose(false);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (Region != IntPtr.Zero)
            {
                NativeMethods.DeleteObject(Region);
                Region = IntPtr.Zero;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="color"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", Justification = "Ignore")]
        private void RebuildRegion(Stream stream, Color color)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            if (Region != IntPtr.Zero)
            {
                NativeMethods.DeleteObject(Region);
            }

            var data = DibSection.GetDibData(stream);

            if (data.BitCount != 24)
            {
                throw new NotSupportedException("Supported only 24 bpp");
            }

            int mask;
            if (color == Color.Transparent)
            {
                mask = data.GetPixel(0, 0);
            }
            else
            {
                mask = (color.B << 16) | (color.G << 8) | color.R;
            }

            Region = NativeMethods.CreateRectRgn(0, 0, 0, 0);

            for (var y = 0; y < data.Height; y++)
            {
                for (var x = 0; x < data.Width; x++)
                {
                    while ((x < data.Width) && (data.GetPixel(x, y) == mask))
                    {
                        x++;
                    }

                    var left = x;

                    while ((x < data.Width) && (data.GetPixel(x, y) != mask))
                    {
                        x++;
                    }

                    if ((x - left) > 0)
                    {
                        var rectRegion = NativeMethods.CreateRectRgn(left, y, x, y + 1);
                        NativeMethods.CombineRgn(Region, Region, rectRegion, RgnCombineMode.RGN_OR);
                        NativeMethods.DeleteObject(rectRegion);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        public void Apply(Control color)
        {
            if (color == null)
            {
                throw new ArgumentNullException("color");
            }

            if (NativeMethods.SetWindowRgn(color.Handle, Region, 1) != 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
    }
}
