namespace Smart.WindowsCE
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    using Microsoft.WindowsCE.Forms;

    /// <summary>
    /// 
    /// </summary>
    public static class PowerManager
    {
        private const int METHOD_BUFFERED = 0;
        private const int FILE_ANY_ACCESS = 0;
        private const int FILE_DEVICE_HAL = 0x00000101;
        private const int IOCTL_HAL_REBOOT = (FILE_DEVICE_HAL << 16) | (FILE_ANY_ACCESS << 14) | (15 << 2) | METHOD_BUFFERED;

        //private const int EWX_LOGOFF = 0;
        //private const int EWX_SHUTDOWN = 1;
        private const int EWX_REBOOT = 2;
        //private const int EWX_FORCE = 4;
        private const int EWX_POWEROFF = 8;

        private const int POWER_STATE_IDLE = 0x00100000;
        private const int POWER_STATE_SUSPEND = 0x00200000;
        private const int POWER_STATE_RESET = 0x00800000;

        private const int POWER_FORCE = 4096;

        private static readonly object Sync = new object();
        private static PowerMessageWindow powerMessageWindow;
        private static EventHandler<PowerBroardCastEventArgs> powerBroadCast;

        /// <summary>
        /// 
        /// </summary>
        public static event EventHandler<PowerBroardCastEventArgs> PowerBroadCast
        {
            add
            {
                lock (Sync)
                {
                    if (powerMessageWindow == null)
                    {
                        powerMessageWindow = new PowerMessageWindow();
                    }
                    powerBroadCast = (EventHandler<PowerBroardCastEventArgs>)Delegate.Combine(powerBroadCast, value);
                }
            }
            remove
            {
                lock (Sync)
                {
                    powerBroadCast = (EventHandler<PowerBroardCastEventArgs>)Delegate.Remove(powerBroadCast, value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private class PowerMessageWindow : MessageWindow
        {
            private const int WM_POWERBROADCAST = 0x0218;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="m"></param>
            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WM_POWERBROADCAST)
                {
                    var handler = powerBroadCast;
                    if (handler != null)
                    {
                        var args = new PowerBroardCastEventArgs((PowerBroadcastStatus)m.WParam.ToInt32());
                        handler(null, args);
                    }
                }
                base.WndProc(ref m);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static PowerStatus PowerStatus
        {
            get
            {
                var powerStatus = new PowerStatus();
                NativeMethods.GetSystemPowerStatusEx2(ref powerStatus, Marshal.SizeOf(powerStatus), false);
                return powerStatus;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ResetSystemIdleTimer()
        {
            NativeMethods.SystemIdleTimerReset();
        }

        /// <summary>
        /// 
        /// </summary>
        public static void SetIdleState()
        {
            var error = NativeMethods.SetSystemPowerState(IntPtr.Zero, POWER_STATE_IDLE, POWER_FORCE);
            if (error != 0)
            {
                throw new Win32Exception(error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void PowerOff()
        {
            if (Environment.OSVersion.Version.Major > 4)
            {
                if (!NativeMethods.ExitWindowsEx(EWX_POWEROFF, 0))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            else
            {
                if (NativeMethods.SetSystemPowerState(IntPtr.Zero, POWER_STATE_SUSPEND, POWER_FORCE) != 0)
                {
                    NativeMethods.GwesPowerOff();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", Justification = "Ignore")]
        public static void SoftReset()
        {
            if (Environment.OSVersion.Version.Major > 4)
            {
                if (!NativeMethods.ExitWindowsEx(EWX_REBOOT, 0))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            else if (Environment.OSVersion.Version.Major > 3)
            {
                NativeMethods.SetSystemPowerState(IntPtr.Zero, POWER_STATE_RESET, POWER_FORCE);
            }
            else
            {
                var lpBytesReturned = 0;
                var outBuf = new byte[2];
                NativeMethods.KernelIoControl(IOCTL_HAL_REBOOT, IntPtr.Zero, 0, outBuf, 0, ref lpBytesReturned);
            }
        }
    }
}
