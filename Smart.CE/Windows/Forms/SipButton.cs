namespace Smart.Windows.Forms
{
    using System.ComponentModel;
    using System.Drawing;

    using Smart.Drawing;

    /// <summary>
    /// 
    /// </summary>
    [DesignTimeVisible(false)]
    public class SipButton : Component
    {
        private Rectangle rectangle;
        private string text = string.Empty;
        private bool enabled = true;
        private Font font;
        private Color backColor = SystemColors.Window;
        private Color backColor2 = SystemColors.Window;
        private Color foreColor = SystemColors.WindowText;
        private Color borderColor = SystemColors.WindowText;
        private Color selectedBackColor = SystemColors.WindowText;
        private Color selectedBackColor2 = SystemColors.WindowText;
        private Color selectedForeColor = SystemColors.Window;
        private Color selectedBorderColor = SystemColors.Window;
        private Image image;
        private Image selectedImage;

        /// <summary>
        /// 
        /// </summary>
        internal SipControl Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set
            {
                rectangle = value;
                UpdateParent();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                UpdateParent(rectangle);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            set
            {
                enabled = value;
                UpdateParent(rectangle);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Font Font
        {
            get { return font; }
            set
            {
                font = value;
                UpdateParent(rectangle);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color BackColor
        {
            get { return backColor; }
            set
            {
                backColor = value;
                UpdateParent(rectangle);
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
                UpdateParent(rectangle);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color ForeColor
        {
            get { return foreColor; }
            set
            {
                foreColor = value;
                UpdateParent(rectangle);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                UpdateParent(rectangle);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color SelectedBackColor
        {
            get { return selectedBackColor; }
            set
            {
                selectedBackColor = value;
                UpdateParent(rectangle);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color SelectedBackColor2
        {
            get { return selectedBackColor2; }
            set
            {
                selectedBackColor2 = value;
                UpdateParent(rectangle);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color SelectedForeColor
        {
            get { return selectedForeColor; }
            set
            {
                selectedForeColor = value;
                UpdateParent(rectangle);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color SelectedBorderColor
        {
            get { return selectedBorderColor; }
            set
            {
                selectedBorderColor = value;
                UpdateParent(rectangle);
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
                image = value;
                UpdateParent(rectangle);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Image SelectedImage
        {
            get { return selectedImage; }
            set
            {
                selectedImage = value;
                UpdateParent(rectangle);
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
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 
        /// </summary>
        protected void UpdateParent()
        {
            if (Parent != null)
            {
                Parent.Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        protected void UpdateParent(Rectangle rect)
        {
            if (Parent != null)
            {
                Parent.Invalidate(rect);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal virtual bool HitTest(int x, int y)
        {
            return enabled && rectangle.Contains(x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="selected"></param>
        internal virtual void Draw(Graphics g, bool selected)
        {
            if (selected)
            {
                if (selectedImage != null)
                {
                    g.DrawImage(selectedImage, rectangle, rectangle, GraphicsUnit.Pixel);
                }
                else
                {
                    if (selectedBackColor == selectedBackColor2)
                    {
                        using (var brush = new SolidBrush(selectedBackColor))
                        {
                            g.FillRectangle(brush, rectangle);
                        }
                    }
                    else
                    {
                        g.GradientFill(rectangle, selectedBackColor, selectedBackColor2, FillDirection.TopToBottom);
                    }

                    using (var pen = new Pen(selectedBorderColor))
                    {
                        g.DrawRectangle(pen, rectangle.Left, rectangle.Top, rectangle.Width - 1, rectangle.Height - 1);
                    }
                }
            }
            else
            {
                if (image != null)
                {
                    g.DrawImage(selectedImage, rectangle, rectangle, GraphicsUnit.Pixel);
                }
                else
                {
                    if (backColor == backColor2)
                    {
                        using (var brush = new SolidBrush(backColor))
                        {
                            g.FillRectangle(brush, rectangle);
                        }
                    }
                    else
                    {
                        g.GradientFill(rectangle, backColor, backColor2, FillDirection.TopToBottom);
                    }

                    using (var pen = new Pen(borderColor))
                    {
                        g.DrawRectangle(pen, rectangle.Left, rectangle.Top, rectangle.Width - 1, rectangle.Height - 1);
                    }
                }
            }

            g.DrawText(text, this.font ?? this.Parent.Font, selected ? selectedForeColor : foreColor, rectangle, ContentAlignmentEx.MiddleCenter);
        }
    }
}
