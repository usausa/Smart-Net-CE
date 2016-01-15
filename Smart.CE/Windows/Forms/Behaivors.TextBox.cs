namespace Smart.Windows.Forms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    using Microsoft.WindowsCE.Forms;

    using Smart.Win32;

    /// <summary>
    /// 
    /// </summary>
    public class FocusWithSelectTextBoxBehavior : IBehaivor<TextBox>
    {
        private static readonly FocusWithSelectTextBoxBehavior Singleton = new FocusWithSelectTextBoxBehavior();

        /// <summary>
        /// 
        /// </summary>
        public static FocusWithSelectTextBoxBehavior Default
        {
            get { return Singleton; }
        }

        /// <summary>
        /// 
        /// </summary>
        private FocusWithSelectTextBoxBehavior()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public void Attach(TextBox control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            control.GotFocus += ControlOnGotFocus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public void Detach(TextBox control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            control.GotFocus -= ControlOnGotFocus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ControlOnGotFocus(object sender, EventArgs args)
        {
            var control = (TextBox)sender;
            control.SelectAll();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class FocusColorTextBoxBehavior : IBehaivor<TextBox>
    {
        private readonly Color focusColor;
        
        private Color backColor;

        private bool attached;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="focusColor"></param>
        public FocusColorTextBoxBehavior(Color focusColor)
        {
            this.focusColor = focusColor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public void Attach(TextBox control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            if (attached)
            {
                throw new InvalidOperationException("behavior is attached");
            }

            control.GotFocus += ControlOnGotFocus;
            control.LostFocus += ControlOnLostFocus;

            attached = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public void Detach(TextBox control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            if (!attached)
            {
                throw new InvalidOperationException("behavior is not attached");
            }

            control.GotFocus -= ControlOnGotFocus;
            control.LostFocus -= ControlOnLostFocus;

            attached = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ControlOnGotFocus(object sender, EventArgs args)
        {
            var control = (TextBox)sender;
            backColor = control.BackColor;
            control.BackColor = focusColor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ControlOnLostFocus(object sender, EventArgs args)
        {
            var control = (TextBox)sender;
            control.BackColor = backColor;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class NumberInputBehavior : IBehaivor<TextBox>
    {
        private static readonly NumberInputBehavior DefaultSingleton = new NumberInputBehavior(false);

        private static readonly NumberInputBehavior NegativeSingleton = new NumberInputBehavior(false);

        /// <summary>
        /// 
        /// </summary>
        public static NumberInputBehavior Default
        {
            get { return DefaultSingleton; }
        }

        /// <summary>
        /// 
        /// </summary>
        public static NumberInputBehavior Negative
        {
            get { return NegativeSingleton; }
        }

        /// <summary>
        /// 
        /// </summary>
        private readonly bool allowNegative;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="allowNegative"></param>
        private NumberInputBehavior(bool allowNegative)
        {
            this.allowNegative = allowNegative;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public void Attach(TextBox control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            control.KeyPress += ControlOnKeyPress;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public void Detach(TextBox control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            control.KeyPress -= ControlOnKeyPress;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Ignore")]
        private void ControlOnKeyPress(object sender, KeyPressEventArgs args)
        {
            var textBox = (TextBox)sender;

            switch (args.KeyChar)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    if (textBox.Text.Length == 1)
                    {
                        if (textBox.Text[0] == '0')
                        {
                            textBox.Text = args.KeyChar.ToString(CultureInfo.InvariantCulture);
                            textBox.SelectionStart = textBox.Text.Length;
                            args.Handled = true;
                        }
                        else if (textBox.Text[0] == '-')
                        {
                            if (args.KeyChar == '0')
                            {
                                args.Handled = true;
                            }
                        }
                    }
                    else if (allowNegative)
                    {
                        if (textBox.Text.Length == textBox.MaxLength - 1)
                        {
                            if (textBox.Text[0] != '-')
                            {
                                if (textBox.SelectionLength == 0)
                                {
                                    args.Handled = true;
                                }
                            }
                        }
                        if (textBox.SelectionStart == 1)
                        {
                            if (textBox.Text[0] == '-')
                            {
                                if (textBox.SelectionLength == 0)
                                {
                                    args.Handled = true;
                                }
                                else
                                {
                                    textBox.Text = textBox.Text[0] + textBox.Text.Substring(textBox.SelectionStart + textBox.SelectionLength);
                                    args.Handled = true;
                                }
                            }
                        }
                    }
                    break;

                case '-':
                    args.Handled = true;
                    if (allowNegative)
                    {
                        if (textBox.Text.Length == 0)
                        {
                            textBox.Text = "-";
                        }
                        else if (textBox.Text[0] == '-')
                        {
                            textBox.Text = textBox.Text.Length == 1 ? string.Empty : textBox.Text.Substring(1);
                        }
                        else
                        {
                            textBox.Text = "-" + textBox.Text;
                        }
                        textBox.SelectionStart = textBox.Text.Length;
                    }
                    break;

                default:
                    args.Handled = true;
                    break;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ExtendTextBoxBehavior : IBehaivor<TextBox>
    {
        public event EventHandler<EventArgs> StartComposition;
        public event EventHandler<EventArgs> EndComposition;

        public event EventHandler<CancelEventArgs> Cutting;
        public event EventHandler<CancelEventArgs> Copying;
        public event EventHandler<CancelEventArgs> Pasting;
        public event EventHandler<CancelEventArgs> Clearing;

        public IntPtr Handle { get; private set; }

        public bool Compositioning { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public void Attach(TextBox control)
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
            MessageHook.Subclass(Handle, Callback);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public void Detach(TextBox control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            MessageHook.UnSubclass(Handle, Callback);
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
            switch (m.Msg)
            {
                case (int)WM.IME_STARTCOMPOSITION:
                    Compositioning = true;
                    OnStartComposition(EventArgs.Empty);
                    break;

                case (int)WM.IME_ENDCOMPOSITION:
                    Compositioning = false;
                    OnEndComposition(EventArgs.Empty);
                    break;

                case (int)WM.CUT:
                    {
                        var e = new CancelEventArgs(false);
                        OnCopy(e);
                        if (e.Cancel)
                        {
                            return true;
                        }
                    }
                    break;

                case (int)WM.COPY:
                    {
                        var e = new CancelEventArgs(false);
                        OnCopy(e);
                        if (e.Cancel)
                        {
                            return true;
                        }
                    }
                    break;

                case (int)WM.PASTE:
                    {
                        var e = new CancelEventArgs(false);
                        OnPaste(e);
                        if (e.Cancel)
                        {
                            return true;
                        }
                    }
                    break;

                case (int)WM.CLEAR:
                    {
                        var e = new CancelEventArgs(false);
                        OnClear(e);
                        if (e.Cancel)
                        {
                            return true;
                        }
                    }
                    break;

                case (int)WM.DESTROY:
                    Handle = IntPtr.Zero;
                    break;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnStartComposition(EventArgs e)
        {
            if (StartComposition != null)
            {
                StartComposition(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnEndComposition(EventArgs e)
        {
            if (EndComposition != null)
            {
                EndComposition(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCut(CancelEventArgs e)
        {
            if (Cutting != null)
            {
                Cutting(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCopy(CancelEventArgs e)
        {
            if (Copying != null)
            {
                Copying(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPaste(CancelEventArgs e)
        {
            if (Pasting != null)
            {
                Pasting(this, e);
            }
        }

        protected virtual void OnClear(CancelEventArgs e)
        {
            if (Clearing != null)
            {
                Clearing(this, e);
            }
        }
    }
}
