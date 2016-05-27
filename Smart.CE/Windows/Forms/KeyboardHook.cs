namespace Smart.Windows.Forms
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using Smart.Win32;

    /// <summary>
    ///
    /// </summary>
    public class KeyboardHook : IDisposable
    {
        public event EventHandler<KeyDataEventArgs> KeyDetected;

        private readonly HookProc hookProc;
        private IntPtr hHook;

        /// <summary>
        ///
        /// </summary>
        public KeyboardHook()
        {
            hookProc = KeyboardHookProc;
        }

        /// <summary>
        ///
        /// </summary>
        ~KeyboardHook()
        {
            Dispose(false);
        }

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            Enabled = false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private int KeyboardHookProc(HookCode nCode, IntPtr wParam, IntPtr lParam)
        {
            if ((nCode == HookCode.Action) && (KeyDetected != null))
            {
                var khs = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                var args = new KeyDataEventArgs((WM)((int)wParam), khs.VkCode, khs.ScanCode, khs.Time);

                KeyDetected(this, args);

                if (args.Handled)
                {
                    return 1;
                }
            }

            return NativeMethods.CallNextHookEx(hHook, nCode, wParam, lParam);
        }

        /// <summary>
        ///
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1404:CallGetLastErrorImmediatelyAfterPInvoke", Justification = "No problem")]
        public bool Enabled
        {
            get
            {
                return hHook != IntPtr.Zero;
            }
            set
            {
                if (value)
                {
                    if (hHook == IntPtr.Zero)
                    {
                        hHook = NativeMethods.SetWindowsHookEx(HookType.KeyboardLowLevel, hookProc, NativeMethods.GetModuleHandle(null), 0);
                        if (hHook == IntPtr.Zero)
                        {
                            Debugger.Break();
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }
                    }
                }
                else
                {
                    if (hHook != IntPtr.Zero)
                    {
                        NativeMethods.UnhookWindowsHookEx(hHook);
                        hHook = IntPtr.Zero;
                    }
                }
            }
        }
    }
}