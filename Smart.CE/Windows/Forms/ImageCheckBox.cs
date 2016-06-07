namespace Smart.Windows.Forms
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;
    using Smart.Drawing;

    /// <summary>
    ///
    /// </summary>
    public class ImageCheckBox : ControlEx
    {
        public event EventHandler CheckStateChanged;

        private CheckState checkState = CheckState.Unchecked;
        private bool isChecked;
        private bool active;

        private Image imageChecked;
        private Image imageUnchecked;
        private Image imageIndeterminate;

        private Size imageSize = new Size(16, 16);

        private bool leftImage = true;

        private int textMargin = 4;

        private bool drawFocusRectangle;
        private Color focusRectangleColor = Color.Gray;

        private bool useTransparent;
        private Color transparentColor = Color.Magenta;

        private bool disposeResource;

        /// <summary>
        ///
        /// </summary>
        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public CheckState CheckState
        {
            get { return checkState; }
            set
            {
                if (checkState != value)
                {
                    checkState = value;
                    isChecked = checkState == CheckState.Checked;
                    Invalidate();
                    OnCheckStateChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public bool Checked
        {
            get { return isChecked; }
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    checkState = isChecked ? CheckState.Checked : CheckState.Unchecked;
                    Invalidate();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Image ImageChecked
        {
            get { return imageChecked; }
            set
            {
                if (DisposeResource && (imageChecked != null))
                {
                    imageChecked.Dispose();
                }

                imageChecked = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Image ImageUnchecked
        {
            get { return imageUnchecked; }
            set
            {
                if (DisposeResource && (imageUnchecked != null))
                {
                    imageUnchecked.Dispose();
                }

                imageUnchecked = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Image ImageIndeterminate
        {
            get { return imageIndeterminate; }
            set
            {
                if (DisposeResource && (imageIndeterminate != null))
                {
                    imageIndeterminate.Dispose();
                }

                imageIndeterminate = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Size ImageSize
        {
            get { return imageSize; }
            set
            {
                imageSize = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public int TextMargin
        {
            get { return textMargin; }
            set
            {
                textMargin = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public bool LeftImage
        {
            get { return leftImage; }
            set
            {
                leftImage = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public bool DrawFocusRectangle
        {
            get { return drawFocusRectangle; }
            set
            {
                drawFocusRectangle = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Color FocusRectangleColor
        {
            get { return focusRectangleColor; }
            set
            {
                focusRectangleColor = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public bool UseTransparent
        {
            get { return useTransparent; }
            set
            {
                useTransparent = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Color TransparentColor
        {
            get { return transparentColor; }
            set
            {
                transparentColor = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public bool DisposeResource
        {
            get { return disposeResource; }
            set
            {
                disposeResource = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public ImageCheckBox()
        {
            DoubleBuffered = true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (disposeResource)
                {
                    DisposeResources();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        ///
        /// </summary>
        protected virtual void DisposeResources()
        {
            if (imageChecked != null)
            {
                imageChecked.Dispose();
                imageChecked = null;
            }

            if (imageUnchecked != null)
            {
                imageUnchecked.Dispose();
                imageUnchecked = null;
            }

            if (imageIndeterminate != null)
            {
                imageIndeterminate.Dispose();
                imageIndeterminate = null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCheckStateChanged(EventArgs e)
        {
            if (CheckStateChanged != null)
            {
                CheckStateChanged(this, e);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            Invalidate();
            base.OnEnabledChanged(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e)
        {
            Invalidate();
            base.OnGotFocus(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostFocus(EventArgs e)
        {
            Invalidate();
            base.OnLostFocus(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Protected")]
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (!e.Handled)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                    case Keys.Up:
                        if (Parent != null)
                        {
                            Parent.SelectNextControl(this, false, true, true, true);
                        }
                        return;

                    case Keys.Right:
                    case Keys.Down:
                        if (Parent != null)
                        {
                            Parent.SelectNextControl(this, true, true, true, true);
                        }
                        return;

                    default:
                        return;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Protected")]
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if ((e.KeyChar == ' ') || (e.KeyChar == '\r'))
            {
                Checked = !Checked;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Protected")]
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                active = true;
                Focus();
                Invalidate();
            }

            base.OnMouseDown(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Protected")]
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var rect = new Rectangle(0, 0, Width, Height);
                if (active && !rect.Contains(e.X, e.Y))
                {
                    active = false;
                    Invalidate();
                }
                else if (!active && rect.Contains(e.X, e.Y))
                {
                    active = true;
                    Invalidate();
                }
            }

            base.OnMouseMove(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (active)
            {
                checkState = isChecked ? CheckState.Unchecked : CheckState.Checked;

                isChecked = !isChecked;

                active = false;
                Invalidate();

                OnCheckStateChanged(EventArgs.Empty);
            }

            base.OnMouseUp(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", Justification = "Compatibility")]
        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Protected")]
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = GetPresentationMedium(e.Graphics);

            g.Clear(BackColor);

            var color = Enabled ? ForeColor : SystemColors.InactiveBorder;

            // 画像選択
            Image image;
            if (active == false)
            {
                image = checkState == CheckState.Checked ? imageChecked : checkState == CheckState.Unchecked ? imageUnchecked : imageIndeterminate;
            }
            else
            {
                image = checkState == CheckState.Unchecked ? imageChecked : imageUnchecked;
            }

            var textSize = g.MeasureString(Text, Font);
            var textTop = (ClientRectangle.Height - (int)textSize.Height) / 2;
            var imageTop = (ClientRectangle.Height - imageSize.Height) / 2;

            Rectangle imageRect;
            Rectangle textRect;
            if (leftImage)
            {
                imageRect = new Rectangle(0, imageTop, imageSize.Width, imageSize.Height);
                textRect = new Rectangle(imageSize.Width + textMargin, textTop, ClientRectangle.Width - imageSize.Width - textMargin, imageSize.Height);
            }
            else
            {
                imageRect = new Rectangle(ClientRectangle.Width - imageSize.Width, imageTop, imageSize.Width, imageSize.Height);
                textRect = new Rectangle(0, textTop, ClientRectangle.Width - imageSize.Width - textMargin, imageSize.Height);
            }

            // 画像
            if (image != null)
            {
                if (useTransparent)
                {
                    using (var attributes = new ImageAttributes())
                    {
                        attributes.SetColorKey(transparentColor, transparentColor);
                        g.DrawImage(image, imageRect, 0, 0, imageSize.Width, imageSize.Height, GraphicsUnit.Pixel, attributes);
                    }
                }
                else
                {
                    g.DrawImage(image, imageRect, new Rectangle(0, 0, imageSize.Width, imageSize.Height), GraphicsUnit.Pixel);
                }
            }
            else if (DesignMode.IsTrue)
            {
                using (var pen = new Pen(Color.Gray))
                {
                    g.DrawRectangle(pen, imageRect.X, imageRect.Y, imageRect.Width - 1, imageRect.Height - 1);
                }
            }

            // 文字
            g.DrawText(Text, Font, color, textRect, leftImage ? ContentAlignmentEx.TopLeft : ContentAlignmentEx.TopRight);

            // フォーカス
            if (drawFocusRectangle && Focused)
            {
                using (var pen = new Pen(FocusRectangleColor))
                {
                    g.DrawRectangle(pen, textRect.X - 1, textRect.Y - 1, (int)textSize.Width + 1, (int)textSize.Height + 1);
                }
            }

            NotifyPaintingComplete(e);
        }
    }
}
