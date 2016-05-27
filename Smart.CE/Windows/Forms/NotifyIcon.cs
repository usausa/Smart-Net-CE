namespace Smart.Windows.Forms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Microsoft.WindowsCE.Forms;

    /// <summary>
    ///
    /// </summary>
    public class NotifyIcon : Component
    {
        private const int NIF_MESSAGE = 0x00000001;
        private const int NIF_ICON = 0x00000002;
        private const int NIF_TIP = 0x00000004;

        private const int NIM_ADD = 0x00000000;
        private const int NIM_MODIFY = 0x00000001;
        private const int NIM_DELETE = 0x00000002;

        private const int WM_NOTIFY = 0x004E;

        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseUp;
        public event EventHandler DoubleClick;
        public event EventHandler Click;

        private NotifyIconMessageWindow messageWindow;
        private bool doubleclick;
        private NOTIFYICONDATA data;
        private Icon icon;
        private bool visible;

        /// <summary>
        ///
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                if (visible != value)
                {
                    if (value)
                    {
                        if (NativeMethods.Shell_NotifyIcon(NIM_ADD, ref data))
                        {
                            visible = true;
                        }
                    }
                    else
                    {
                        if (NativeMethods.Shell_NotifyIcon(NIM_DELETE, ref data))
                        {
                            visible = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Icon Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = value;
                if (value == null)
                {
                    data.Icon = IntPtr.Zero;
                    data.Flags &= ~NIF_ICON;
                }
                else
                {
                    data.Icon = icon.Handle;
                    data.Flags |= NIF_ICON;
                }

                if (visible)
                {
                    NativeMethods.Shell_NotifyIcon(NIM_MODIFY, ref data);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public string Text
        {
            get
            {
                return data.Tip;
            }
            set
            {
                if (value == null)
                {
                    data.Tip = null;
                    data.Flags &= ~NIF_TIP;
                }
                else
                {
                    if (value.Length > 64)
                    {
                        throw new ArgumentException("Text is too long.", "value");
                    }

                    data.Tip = value;
                    data.Flags |= NIF_TIP;
                }

                if (visible)
                {
                    NativeMethods.Shell_NotifyIcon(NIM_MODIFY, ref data);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public NotifyIcon()
        {
            messageWindow = new NotifyIconMessageWindow(this);
            data.Size = Marshal.SizeOf(data);
            data.CallbackMessage = WM_NOTIFY;
            data.Hwnd = messageWindow.Hwnd;
            data.Flags = NIF_MESSAGE;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            Visible = false;
            if (icon != null)
            {
                icon.Dispose();
                icon = null;
            }
            if (messageWindow != null)
            {
                messageWindow.Dispose();
                messageWindow = null;
            }
            base.Dispose(disposing);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        private void OnClick(EventArgs e)
        {
            if (Click != null)
            {
                Click(this, e);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        private void OnDoubleClick(EventArgs e)
        {
            if (DoubleClick != null)
            {
                DoubleClick(this, e);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        private void OnMouseDown(MouseEventArgs e)
        {
            if (MouseDown != null)
            {
                MouseDown(this, e);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        private void OnMouseUp(MouseEventArgs e)
        {
            if (MouseUp != null)
            {
                MouseUp(this, e);
            }
            if (!doubleclick)
            {
                OnClick(EventArgs.Empty);
            }
            doubleclick = false;
        }

        /// <summary>
        ///
        /// </summary>
        private class NotifyIconMessageWindow : MessageWindow
        {
            private readonly NotifyIcon parent;

            /// <summary>
            ///
            /// </summary>
            /// <param name="parent"></param>
            internal NotifyIconMessageWindow(NotifyIcon parent)
            {
                this.parent = parent;
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="m"></param>
            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WM_NOTIFY)
                {
                    switch ((int)m.LParam)
                    {
                        case 0x201: // LBUTTONDOWN
                            parent.OnMouseDown(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                            break;

                        case 0x202: // LBUTTONUP
                            parent.OnMouseUp(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                            break;

                        case 0x203: // LBUTTONDBLCLK
                            parent.OnMouseDown(new MouseEventArgs(MouseButtons.Left, 2, 0, 0, 0));
                            parent.doubleclick = true;
                            parent.OnDoubleClick(EventArgs.Empty);
                            break;

                        case 0x204: // RBUTTONDOWN
                            parent.OnMouseDown(new MouseEventArgs(MouseButtons.Right, 1, 0, 0, 0));
                            break;

                        case 0x205: // RBUTTONUP
                            parent.OnMouseUp(new MouseEventArgs(MouseButtons.Right, 1, 0, 0, 0));
                            break;
                    }
                }

                base.WndProc(ref m);
            }
        }
    }
}
