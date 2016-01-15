namespace Smart.Windows.Forms
{
    using System;
    using System.Collections.Generic;
    using Microsoft.WindowsCE.Forms;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable", Justification = "No problem")]
    public class MessageHook
    {
        private static readonly Dictionary<IntPtr, MessageHook> Hooks = new Dictionary<IntPtr, MessageHook>();

        private readonly IntPtr handle;
        private IntPtr orgWndProc;
        private WndProcDelegate hookedWndProc;
        private readonly List<WndProcCallback> callbacks = new List<WndProcCallback>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="callback"></param>
        public static void Subclass(IntPtr handle, WndProcCallback callback)
        {
            MessageHook messageHook;
            if (Hooks.TryGetValue(handle, out messageHook))
            {
                messageHook.AddHook(callback);
            }
            else
            {
                messageHook = new MessageHook(handle);
                Hooks[handle] = messageHook;
                messageHook.AddHook(callback);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="callback"></param>
        public static void UnSubclass(IntPtr handle, WndProcCallback callback)
        {
            MessageHook messageHook;
            if (Hooks.TryGetValue(handle, out messageHook))
            {
                messageHook.RemoveHook(callback);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", Justification = "Compatibility")]
        public static void DefaultWindowProc(ref Message message)
        {
            MessageHook messageHook;
            if (Hooks.TryGetValue(message.HWnd, out messageHook))
            {
                messageHook.CallWindowProc(ref message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        private MessageHook(IntPtr handle)
        {
            this.handle = handle;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        private void AddHook(WndProcCallback callback)
        {
            if (callbacks.Count == 0)
            {
                Subclass();
            }

            callbacks.Add(callback);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        private void RemoveHook(WndProcCallback callback)
        {
            callbacks.Remove(callback);

            if (callbacks.Count == 0)
            {
                Release();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Subclass()
        {
            hookedWndProc = Callback;
            orgWndProc = NativeMethods.SetWindowLong(handle, -4, hookedWndProc);
        }

        /// <summary>
        /// 
        /// </summary>
        private void UnSubclass()
        {
            NativeMethods.SetWindowLong(handle, -4, orgWndProc);
        }

        /// <summary>
        /// 
        /// </summary>
        private void Release()
        {
            UnSubclass();

            orgWndProc = IntPtr.Zero;
            hookedWndProc = null;

            Hooks.Remove(handle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private IntPtr Callback(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            var m = Message.Create(hWnd, msg, wParam, lParam);
            var handled = false;
            foreach (var callback in callbacks)
            {
                handled = callback(ref m);
                if (handled)
                {
                    break;
                }
            }

            // WM_NCDESTROY
            //if( msg == 130 ) 
            // WM_DESTROY
            if (msg == 2) 
            {
                Release();
            }

            if (handled)
            {
                return m.Result;
            }

            CallWindowProc(ref m);

            return m.Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        private void CallWindowProc(ref Message m)
        {
            m.Result = NativeMethods.CallWindowProc(orgWndProc, m.HWnd, m.Msg, m.WParam, m.LParam);
        }
    }
}
