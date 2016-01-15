namespace Smart.Windows.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    public class GraphicalButton : GraphicalButtonBase, IButtonControl
    {
        private bool active;

        /// <summary>
        /// 
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DialogResult DialogResult { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override bool IsPressed
        {
            get { return active; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override bool IsFocused
        {
            get { return Focused || IsDefault; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostFocus(EventArgs e)
        {
            active = false;
            base.OnLostFocus(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            if (active)
            {
                active = false;
                Invalidate();
                Update();
            }

            if ((DialogResult != DialogResult.None) && (TopLevelControl is Form))
            {
                ((Form)TopLevelControl).DialogResult = DialogResult;
            }

            base.OnClick(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDoubleClick(EventArgs e)
        {
            if (active)
            {
                active = false;
                Invalidate();
                Update();
            }

            base.OnDoubleClick(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Protected")]
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (!e.Handled && !active && ((e.KeyChar == ' ') || (e.KeyChar == '\r')))
            {
                active = true;
                Invalidate();
                Refresh();
                PerformClick();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Protected")]
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Protected")]
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
        /// <param name="value"></param>
        public void NotifyDefault(bool value)
        {
            IsDefault = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public void PerformClick()
        {
            if (Enabled && Visible)
            {
                OnClick(EventArgs.Empty);
            }
        }
    }
}
