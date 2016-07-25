namespace Smart.Windows.Forms
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    using Smart.Drawing;

    /// <summary>
    ///
    /// </summary>
    public class TemplateLabel : ControlEx
    {
        private object item;

        private readonly TemplateElementCollection elements;

        private bool borderTop;

        private bool borderBottom;

        private bool borderLeft;

        private bool borderRight;

        private Color borderColor = SystemColors.ControlText;

        private Color elementBorderColor = SystemColors.ControlText;

        private Component designData;

        //--------------------------------------------------------------------------------
        // Property
        //--------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        public object Item
        {
            get { return item; }
            set
            {
                item = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public TemplateElementCollection Elements
        {
            get { return elements; }
        }

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
        public Color ElementBorderColor
        {
            get { return elementBorderColor; }
            set
            {
                elementBorderColor = value;
                Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Component DesignData
        {
            get { return designData; }
            set
            {
                designData = value;
                Invalidate();
            }
        }

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        public TemplateLabel()
        {
            DoubleBuffered = true;
            TabStop = false;

            elements = new TemplateElementCollection(this);
        }

        //--------------------------------------------------------------------------------
        // Draw
        //--------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
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
        private void OnDraw(Graphics g)
        {
            // Background
            using (var brush = new SolidBrush(BackColor))
            {
                g.FillRectangle(brush, ClientRectangle);
            }

            // Element
            var rect = ClientRectangle;
            if (borderTop || borderBottom || borderLeft || borderRight)
            {
                rect.Inflate(-1, -1);
            }

            var context = new TemplateDrawContext
            {
                PreferBaseForeColor = false,
                PreferBaseBackColor = false,
                PreferBaseBorderColor = false,
                BaseForeColor = ForeColor,
                BaseBackColor = BackColor,
                BaseBorderColor = ElementBorderColor,
                Font = Font
            };

            var designMode = (Site != null) && Site.DesignMode;
            var source = designData as ITemplateDesignData;
            var target = designMode & source != null ? source.Create() : item;
            TemplateDrawHelper.DrawElements(g, rect, target, elements, context);

            // Border
            g.DrawBorder(borderColor, ClientRectangle, borderTop, borderBottom, borderLeft, borderRight);
        }
    }
}
