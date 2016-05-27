namespace Smart.Windows.Forms
{
    using System;
    using System.Windows.Forms;

    using Microsoft.WindowsCE.Forms;

    using Smart.Win32;

    /// <summary>
    ///
    /// </summary>
    public class ExtendComboBoxBeaivors : IBehaivor<ComboBox>
    {
        public event EventHandler<EventArgs> DropDownOpend;
        public event EventHandler<EventArgs> DropDownClosed;

        /// <summary>
        ///
        /// </summary>
        public IntPtr Handle { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="control"></param>
        public void Attach(ComboBox control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            if (Handle != IntPtr.Zero)
            {
                throw new InvalidOperationException("behavior is attached");
            }

            Handle = control.Handle;
            MessageHook.Subclass(control.Parent.Handle, Callback);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="control"></param>
        public void Detach(ComboBox control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            MessageHook.UnSubclass(control.Parent.Handle, Callback);
            Handle = IntPtr.Zero;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private bool Callback(ref Message m)
        {
            return WndProc(ref m);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", Justification = "Compatibility")]
        protected virtual bool WndProc(ref Message m)
        {
            if ((m.Msg == (int)WM.COMMAND) && (m.LParam == Handle))
            {
                var hiword = m.WParam.ToInt32().HiWord();
                if (hiword == (int)CBN.DROPDOWN)
                {
                    if (DropDownOpend != null)
                    {
                        DropDownOpend(this, null);
                    }
                }
                else if (hiword == (int)CBN.CLOSEUP)
                {
                    if (DropDownClosed != null)
                    {
                        DropDownClosed(this, null);
                    }
                }
            }

            if (m.Msg == (int)WM.DESTROY)
            {
                Handle = IntPtr.Zero;
            }

            return false;
        }
    }
}
