namespace Smart.WindowsCE
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 
    /// </summary>
    public static class DateTimeSetting
    {
        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "Ignore")]
        public static DateTime LocalTime
        {
            get
            {
                var systemTime = new SystemTime();
                if (!NativeMethods.GetLocalTime(ref systemTime))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                return systemTime.ToDateTime();
            }
            set
            {
                var systemTime = WindowsCE.SystemTime.FromDateTime(value);
                if (!NativeMethods.SetLocalTime(ref systemTime))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "Ignore")]
        public static DateTime SystemTime
        {
            get
            {
                var systemTime = new SystemTime();
                if (!NativeMethods.GetSystemTime(ref systemTime))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                return systemTime.ToDateTime();
            }
            set
            {
                var systemTime = WindowsCE.SystemTime.FromDateTime(value);
                if (!NativeMethods.SetSystemTime(ref systemTime))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }
    }
}
