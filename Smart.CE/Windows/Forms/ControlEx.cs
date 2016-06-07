namespace Smart.Windows.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    ///
    /// </summary>
    public class ControlEx : Control
    {
        private Bitmap bufferBitmap;

        private Graphics bufferGraphics;

        private bool doubleBuffered;

        /// <summary>
        ///
        /// </summary>
        protected Graphics DoubleBuffer
        {
            get
            {
                if (bufferGraphics == null)
                {
                    UpdateDoubleBuffer();
                }
                return bufferGraphics;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public bool DoubleBuffered
        {
            get
            {
                return doubleBuffered;
            }
            set
            {
                if (doubleBuffered == value)
                {
                    return;
                }

                doubleBuffered = value;
                if (doubleBuffered == false)
                {
                    DisposeDoubleBuffer();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public SizeF ScaleFactor { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DisposeDoubleBuffer();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        ///
        /// </summary>
        private void DisposeDoubleBuffer()
        {
            if (bufferGraphics != null)
            {
                bufferGraphics.Dispose();
                bufferGraphics = null;
            }

            if (bufferBitmap != null)
            {
                bufferBitmap.Dispose();
                bufferBitmap = null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        private void UpdateDoubleBuffer()
        {
            if ((ClientRectangle.Width > 0) && (ClientRectangle.Height > 0))
            {
                bufferBitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                bufferGraphics = Graphics.FromImage(bufferBitmap);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="graphics"></param>
        /// <returns></returns>
        protected Graphics GetPresentationMedium(Graphics graphics)
        {
            if (DoubleBuffered)
            {
                return DoubleBuffer;
            }

            return graphics;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="args"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Protected")]
        protected void NotifyPaintingComplete(PaintEventArgs args)
        {
            if (DoubleBuffered)
            {
                args.Graphics.DrawImage(bufferBitmap, 0, 0);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            if (DoubleBuffered)
            {
                DisposeDoubleBuffer();
            }
            Invalidate();
            base.OnResize(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="specified"></param>
        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            ScaleFactor = factor;
            base.ScaleControl(factor, specified);
        }
    }
}
