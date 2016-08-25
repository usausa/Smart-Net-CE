namespace Smart.Windows.Forms
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using Microsoft.WindowsCE.Forms;

    /// <summary>
    ///
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable", Justification = "Compatibility")]
    public class NativeWindow
    {
        private bool ownHandle;

        private IntPtr defWindowProc;

        private WndProcDelegate windowProc;

        /// <summary>
        ///
        /// </summary>
        public IntPtr Handle { get; private set; }

        /// <summary>
        ///
        /// </summary>
        ~NativeWindow()
        {
            if (Handle != IntPtr.Zero)
            {
                ReleaseHandle();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public virtual void DestroyHandle()
        {
            ReleaseHandle();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static NativeWindow FromHandle(IntPtr handle)
        {
            var window = new NativeWindow();
            window.AssignHandle(handle);
            return window;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cp"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1404:CallGetLastErrorImmediatelyAfterPInvoke", Justification = "Ignore")]
        public virtual void CreateHandle(CreateParams cp)
        {
            if (cp != null)
            {
                if (Handle != IntPtr.Zero)
                {
                    ReleaseHandle();
                }

                var moduleHandle = NativeMethods.GetModuleHandle(null);
                Handle = NativeMethods.CreateWindowEx((uint)cp.ExStyle, cp.ClassName, cp.Caption, (uint)cp.Style, cp.X, cp.Y, cp.Width, cp.Height, cp.Parent, IntPtr.Zero, moduleHandle, cp.Param);
                if (Handle == IntPtr.Zero)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                ownHandle = true;
                Subclass();
                OnHandleChange();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="handle"></param>
        public void AssignHandle(IntPtr handle)
        {
            if (Handle != IntPtr.Zero)
            {
                ReleaseHandle();
            }

            ownHandle = false;
            Handle = handle;
            OnHandleChange();
        }

        /// <summary>
        ///
        /// </summary>
        protected virtual void OnHandleChange()
        {
        }

        /// <summary>
        ///
        /// </summary>
        public void ReleaseHandle()
        {
            if (Handle != IntPtr.Zero)
            {
                UnSubclass();

                if (ownHandle)
                {
                    NativeMethods.DestroyWindow(Handle);
                }
                Handle = IntPtr.Zero;
            }
        }

        /// <summary>
        ///
        /// </summary>
        private void Subclass()
        {
            if ((Handle != IntPtr.Zero) && (defWindowProc == IntPtr.Zero))
            {
                defWindowProc = NativeMethods.GetWindowLong(Handle, -4);
                windowProc = Callback;
                NativeMethods.SetWindowLong(Handle, -4, windowProc);
            }
        }

        /// <summary>
        ///
        /// </summary>
        private void UnSubclass()
        {
            if (defWindowProc != IntPtr.Zero)
            {
                NativeMethods.SetWindowLong(Handle, -4, defWindowProc);
                defWindowProc = IntPtr.Zero;
                windowProc = null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wparam"></param>
        /// <param name="lparam"></param>
        /// <returns></returns>
        private IntPtr Callback(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
        {
            var m = Message.Create(hWnd, msg, wparam, lparam);
            WndProc(ref m);

            // WM_NCDESTROY
            if (msg == 130)
            {
                ReleaseHandle();
            }

            return m.Result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="m"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", Justification = "Compatibility")]
        public void DefWndProc(ref Message m)
        {
            m.Result = NativeMethods.CallWindowProc(defWindowProc, m.HWnd, m.Msg, m.WParam, m.LParam);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="m"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", Justification = "Compatibility")]
        protected virtual void WndProc(ref Message m)
        {
            // 拡張ポイント
            DefWndProc(ref m);
        }
    }
}
