namespace Smart.Drawing
{
    using System;
    using System.Drawing;

    /// <summary>
    ///
    /// </summary>
    public class MemoryGraphics : IDisposable
    {
        private Bitmap memoryBitmap;

        private int width;

        private int height;

        /// <summary>
        ///
        /// </summary>
        public Graphics Graphics { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public Rectangle Bounds
        {
            get { return new Rectangle(0, 0, width, height); }
        }

        /// <summary>
        ///
        /// </summary>
        public Size Size
        {
            get { return new Size(width, height); }
        }

        /// <summary>
        ///
        /// </summary>
        ~MemoryGraphics()
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
            if (memoryBitmap != null)
            {
                memoryBitmap.Dispose();
                memoryBitmap = null;
            }

            if (Graphics != null)
            {
                Graphics.Dispose();
                Graphics = null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool CanBuffer()
        {
            return Graphics != null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="graphics"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Performance")]
        public void Render(Graphics graphics)
        {
            if (memoryBitmap != null)
            {
                graphics.DrawImage(memoryBitmap, new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <returns></returns>
        public bool CreateBuffer(int newWidth, int newHeight)
        {
            if (memoryBitmap != null)
            {
                memoryBitmap.Dispose();
                memoryBitmap = null;
            }

            if (Graphics != null)
            {
                Graphics.Dispose();
                Graphics = null;
            }

            if ((newWidth == 0) || (newHeight == 0))
            {
                return false;
            }

            if ((width != newWidth) || (height != newHeight))
            {
                width = newWidth;
                height = newHeight;

                memoryBitmap = new Bitmap(newWidth, newHeight);
                Graphics = Graphics.FromImage(memoryBitmap);
            }

            return true;
        }
    }
}