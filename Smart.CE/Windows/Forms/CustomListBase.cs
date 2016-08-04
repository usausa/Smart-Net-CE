namespace Smart.Windows.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    ///
    /// </summary>
    public class CustomListBase : ControlEx
    {
        public event EventHandler<EventArgs> SelectedIndexChanged;

        private bool updating;

        private bool showScrollBar = true;

        private readonly VScrollBar scroll = new VScrollBar();

        //--------------------------------------------------------------------------------
        // Property
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        public bool ShowScrollBar
        {
            get { return showScrollBar; }
            set
            {
                showScrollBar = value;
                UpdateLayout();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ScrollBarWidth
        {
            get { return scroll.Width; }
            set
            {
                scroll.Width = value;
                OnResize(EventArgs.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ScrollBarVisible
        {
            get { return scroll.Visible; }
        }

        /// <summary>
        ///
        /// </summary>
        public int TopIndex
        {
            get { return scroll.Value; }
            set { scroll.Value = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected int ScrollBarMaximum
        {
            get { return scroll.Maximum; }
        }

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        public CustomListBase()
        {
            DoubleBuffered = true;

            scroll.LargeChange = 1;
            scroll.Maximum = 0;
            scroll.ValueChanged += (sender, args) => Refresh();
            scroll.Visible = false;
            Controls.Add(scroll);
        }

        //--------------------------------------------------------------------------------
        // Event
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "Ignore")]
        protected void FireSelectedIndexChanged()
        {
            var handler = SelectedIndexChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        //--------------------------------------------------------------------------------
        // Update
        //--------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        public void BeginUpdate()
        {
            updating = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void EndUpdate()
        {
            updating = false;
            UpdateLayout();
        }

        //--------------------------------------------------------------------------------
        // Layout
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            var border = CalcBorderWidth();
            scroll.Top = border;
            scroll.Left = Width - scroll.Width - border;
            scroll.Height = Height - (border * 2);
            UpdateLayout();
            base.OnResize(e);
        }

        /// <summary>
        /// 
        /// </summary>
        protected void UpdateLayout()
        {
            if ((Width == 0) || (Height == 0))
            {
                return;
            }

            if (updating)
            {
                return;
            }

            var count = CalcItemCount();
            scroll.Maximum = count == 0 ? 0 : CalcFirstInRectFrom(count - 1);
            scroll.Visible = ShowScrollBar || (scroll.Maximum > 0);

            Invalidate();
        }

        //--------------------------------------------------------------------------------
        // Calculate
        //--------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual int CalcItemCount()
        {
            // Must override
            return 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected virtual int CalcBorderWidth()
        {
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual Rectangle CalcListRect()
        {
            var rect = ClientRectangle;

            var border = CalcBorderWidth();
            rect.Inflate(-border, -border);

            if (scroll.Visible)
            {
                rect.Width -= scroll.Width;
            }

            return rect;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected virtual int CalcFirstInRectFrom(int index)
        {
            // Must override
            return index;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected virtual int CalcLastInRectFrom(int index)
        {
            // Must override
            return index;
        }

        //--------------------------------------------------------------------------------
        // Move
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        public void EnsureVisible(int index)
        {
            if (index < scroll.Value)
            {
                scroll.Value = Math.Max(index, 0);
            }
            else if (index > scroll.Value)
            {
                scroll.Value = CalcFirstInRectFrom(index);
            }
        }

        //--------------------------------------------------------------------------------
        // Focus
        //--------------------------------------------------------------------------------

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
            if (updating)
            {
                return;
            }

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
            // Must override
            using (var br = new SolidBrush(BackColor))
            {
                g.FillRectangle(br, ClientRectangle);
            }
        }
    }
}
