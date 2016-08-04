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
            if (borderTop || borderBottom || borderLeft || borderRight)
            {
                rect.Inflate(-1, -1);
            }

            return rect;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        protected override void DrawBorder(Graphics g, Rectangle rect)
        {
            g.DrawBorder(borderColor, rect, borderTop, borderBottom, borderLeft, borderRight);
        }
    }
}
