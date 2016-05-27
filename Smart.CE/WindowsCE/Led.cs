namespace Smart.WindowsCE
{
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    /// <summary>
    ///
    /// </summary>
    public static class Led
    {
        private const int NLED_COUNT_INFO_ID = 0;
        //private const int NLED_SUPPORTS_INFO_ID = 1;
        private const int NLED_SETTINGS_INFO_ID = 2;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "Ignore")]
        public static int LedCount
        {
            get
            {
                var output = new NLED_COUNT_INFO();
                if (NativeMethods.NLedGetDeviceCount(NLED_COUNT_INFO_ID, ref output))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                return (int)output.Leds;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="led"></param>
        /// <param name="status"></param>
        public static void SetLedStatus(int led, LedStatus status)
        {
            var output = new NLED_SETTINGS_INFO
            {
                LedNum = led,
                OffOnBlink = (int)status
            };
            NativeMethods.NLedSetDevice(NLED_SETTINGS_INFO_ID, ref output);
        }
    }
}
