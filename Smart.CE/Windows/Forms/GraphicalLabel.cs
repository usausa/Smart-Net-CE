namespace Smart.Windows.Forms
{
    using System.Drawing;

    using Smart.Drawing;

    /// <summary>
    ///
    /// </summary>
    public class GraphicalLabel : GraphicalControl
    {
        private bool borderTop;

        private bool borderBottom;

        private bool borderLeft;

        private bool borderRight;

        private Color borderColor = SystemColors.ControlText;

        /// <summary>
        ///
        /// </summary>
        public bool BorderTop
        {
            get { return borderTop; }
            set
            {
                borderTop = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool BorderBottom
        {
            get { return borderBottom; }
            set
            {
                borderBottom = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool BorderLeft
        {
            get { return borderLeft; }
            set
            {
                borderLeft = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool BorderRight
        {
            get { return borderRight; }
            set
            {
                borderRight = value;
                Invalidate();
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
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public GraphicalLabel()
        {
            DoubleBuffered = true;
            TabStop = false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected override Rectangle CalcTextRect()
        {
            var rect = base.CalcTextRect();
            rect.Inflate(-1, -1);
            return rect;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        protected override void DrawBorder(Graphics g, Rectangle rect)
        {
            using (var pen = new Pen(borderColor))
            {
                if (borderTop)
                {
                    g.DrawLine(pen, rect.Left, rect.Top, rect.Right - 1, rect.Top);
                }
                if (borderBottom)
                {
                    g.DrawLine(pen, rect.Left, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
                }
                if (borderLeft)
                {
                    g.DrawLine(pen, rect.Left, rect.Top, rect.Left, rect.Bottom - 1);
                }
                if (borderRight)
                {
                    g.DrawLine(pen, rect.Right - 1, rect.Top, rect.Right - 1, rect.Bottom - 1);
                }
            }
        }
    }
}
