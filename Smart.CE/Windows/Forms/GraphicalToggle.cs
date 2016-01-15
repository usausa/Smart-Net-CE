namespace Smart.Windows.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    public class GraphicalToggle : GraphicalButtonBase
    {
        public event EventHandler CheckStateChanged;

        private bool isChecked;
        private bool active;

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
                    Invalidate();
                    OnCheckStateChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override bool IsPressed
        {
            get { return active ? !isChecked : isChecked; }
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
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (active)
            {
                active = false;
                isChecked = !isChecked;
                Invalidate();
                OnCheckStateChanged(EventArgs.Empty);
            }

            base.OnMouseUp(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Protected")]
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if ((e.KeyChar == ' ') || (e.KeyChar == '\r'))
            {
                Checked = !Checked;
            }
        }
    }
}
