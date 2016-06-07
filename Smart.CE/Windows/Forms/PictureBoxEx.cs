namespace Smart.Windows.Forms
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    /// <summary>
    ///
    /// </summary>
    public class PictureBoxEx : PictureBox
    {
        /// <summary>
        ///
        /// </summary>
        public Color TransparentColor { get; set; }

        /// <summary>
        ///
        /// </summary>
        public PictureBoxEx()
        {
            TransparentColor = Color.Magenta;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Protected")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", Justification = "Compatibility")]
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (var br = new SolidBrush(Parent.BackColor))
            {
                e.Graphics.FillRectangle(br, ClientRectangle);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Protected")]
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Image != null)
            {
                var destRect = new Rectangle(0, 0, Width, Height);
                var rect = new Rectangle(0, 0, Image.Width, Image.Height);

                if (SizeMode == PictureBoxSizeMode.CenterImage)
                {
                    destRect = new Rectangle((Width - Image.Width) / 2, (Height - Image.Height) / 2, Image.Width, Image.Height);
                }
                else if (SizeMode == PictureBoxSizeMode.Normal)
                {
                    destRect = new Rectangle(0, 0, Image.Width, Image.Height);
                }

                using (var imageAttr = new ImageAttributes())
                {
                    imageAttr.SetColorKey(TransparentColor, TransparentColor);
                    e.Graphics.DrawImage(Image, destRect, rect.X, rect.Y, rect.Width, rect.Height, GraphicsUnit.Pixel, imageAttr);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
    }
}
