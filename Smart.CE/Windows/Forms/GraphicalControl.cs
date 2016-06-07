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
    public class GraphicalControl : ControlEx
    {
        private Size textPadding = new Size(0, 0);
        private ContentAlignmentEx textAlign = ContentAlignmentEx.MiddleCenter;
        private bool multiLine;

        private GradationStyle gradationStyle = GradationStyle.None;
        private Color backColor2 = SystemColors.Control;

        private Color shadowColor = Color.Gray;
        private ShadowMask shadowMask = ShadowMask.None;

        private bool useTransparent;
        private Color transparentColor = Color.Magenta;

        private Image image;

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
        public Size TextPadding
        {
            get { return textPadding; }
            set
            {
                textPadding = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public ContentAlignmentEx TextAlign
        {
            get { return textAlign; }
            set
            {
                textAlign = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public bool MultiLine
        {
            get { return multiLine; }
            set
            {
                multiLine = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public GradationStyle GradationStyle
        {
            get { return gradationStyle; }
            set
            {
                gradationStyle = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                base.BackColor = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Color BackColor2
        {
            get { return backColor2; }
            set
            {
                backColor2 = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Color ShadowColor
        {
            get { return shadowColor; }
            set
            {
                shadowColor = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public ShadowMask ShadowMask
        {
            get { return shadowMask; }
            set
            {
                shadowMask = value;
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
        public Image Image
        {
            get { return image; }
            set
            {
                if (DisposeResource && (image != null))
                {
                    image.Dispose();
                }

                image = value;
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
            if (image != null)
            {
                image.Dispose();
                image = null;
            }
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
            OnDraw(g);
            NotifyPaintingComplete(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        protected virtual void OnDraw(Graphics g)
        {
            // 背景描画
            DrawBackground(g, CalcBackgroundRect());

            // 文字描画
            DrawText(g, CalcTextRect());

            // ボーダー描画
            DrawBorder(g, CalcBorderRect());
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected virtual Rectangle CalcBackgroundRect()
        {
            return ClientRectangle;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected virtual Rectangle CalcTextRect()
        {
            var rect = CalcBackgroundRect();
            rect.X += textPadding.Width;
            rect.Y += textPadding.Height;
            rect.Width -= textPadding.Width * 2;
            rect.Height -= textPadding.Height * 2;
            return rect;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected virtual Rectangle CalcBorderRect()
        {
            return ClientRectangle;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Protected")]
        protected virtual void DrawBackground(Graphics g, Rectangle rect)
        {
            if (image != null)
            {
                DrawImage(g, rect, image, useTransparent, BackColor, transparentColor);
            }
            else
            {
                if (gradationStyle == GradationStyle.None)
                {
                    using (var brush = new SolidBrush(BackColor))
                    {
                        g.FillRectangle(brush, rect);
                    }
                }
                else if (gradationStyle == GradationStyle.Horz)
                {
                    g.GradientFill(rect, BackColor, backColor2, FillDirection.LeftToRight);
                }
                else if (gradationStyle == GradationStyle.Vert)
                {
                    g.GradientFill(rect, BackColor, backColor2, FillDirection.TopToBottom);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="target"></param>
        /// <param name="isTransparent"></param>
        /// <param name="back"></param>
        /// <param name="transparent"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Protected")]
        protected static void DrawImage(Graphics g, Rectangle rect, Image target, bool isTransparent, Color back, Color transparent)
        {
            if (isTransparent)
            {
                using (Brush brush = new SolidBrush(back))
                {
                    g.FillRectangle(brush, rect);
                }

                using (var attributes = new ImageAttributes())
                {
                    attributes.SetColorKey(transparent, transparent);
                    g.DrawImage(target, rect, 0, 0, target.Width, target.Height, GraphicsUnit.Pixel, attributes);
                }
            }
            else
            {
                g.DrawImage(target, rect, new Rectangle(0, 0, target.Width, target.Height), GraphicsUnit.Pixel);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        protected virtual void DrawText(Graphics g, Rectangle rect)
        {
            DrawText(g, rect, Text, textAlign, multiLine, Font, ForeColor, shadowColor, shadowMask);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="text"></param>
        /// <param name="alignment"></param>
        /// <param name="multiline"></param>
        /// <param name="font"></param>
        /// <param name="color"></param>
        /// <param name="shadow"></param>
        /// <param name="mask"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Protected")]
        protected static void DrawText(Graphics g, Rectangle rect, string text, ContentAlignmentEx alignment, bool multiline, Font font, Color color, Color shadow, ShadowMask mask)
        {
            if (multiline == false)
            {
                var rc = alignment.CalcTextRect(g.MeasureString(text, font), rect);

                if (mask != ShadowMask.None)
                {
                    g.DrawShadow(text, font, shadow, (int)rc.X, (int)rc.Y, mask);
                }

                using (var brush = new SolidBrush(color))
                {
                    g.DrawString(text, font, brush, rc.X, rc.Y);
                }
            }
            else
            {
                var texts = g.GetMultilineText(text, font, rect.Width);

                var textSize = g.CalcMultilineTextSize(texts, font);
                if (mask.IsTop() || mask.IsBottom())
                {
                    textSize.Height += texts.Length;
                }

                var rc = alignment.CalcTextRect(textSize, rect);
                using (var brush = new SolidBrush(color))
                {
                    var top = (int)rc.Top;
                    foreach (var line in texts)
                    {
                        var size = g.MeasureString(String.IsNullOrEmpty(line) ? " " : line, font);
                        var rcLine = alignment.CalcTextRect(size, rc.Left, top, rc.Width, size.Height);

                        if (mask != ShadowMask.None)
                        {
                            g.DrawShadow(text, font, shadow, (int)rcLine.X, (int)rcLine.Y, mask);
                        }

                        g.DrawString(line, font, brush, rcLine.X, rcLine.Y);

                        top += (int)rcLine.Height;
                        if (mask.IsTop() || mask.IsBottom())
                        {
                            top++;
                        }
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        protected virtual void DrawBorder(Graphics g, Rectangle rect)
        {
        }
    }
}
