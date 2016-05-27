namespace Smart.Windows.Forms
{
    using System;

    using Smart.Win32;

    /// <summary>
    ///
    /// </summary>
    public class KeyDataEventArgs : EventArgs
    {
        /// <summary>
        ///
        /// </summary>
        public bool Handled { get; set; }

        /// <summary>
        ///
        /// </summary>
        public WM KeyMessage { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public int KeyCode { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public int ScanCode { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public int TimeStamp { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="keyMessage"></param>
        /// <param name="keyCode"></param>
        /// <param name="scanCode"></param>
        /// <param name="timeStamp"></param>
        public KeyDataEventArgs(WM keyMessage, int keyCode, int scanCode, int timeStamp)
        {
            KeyMessage = keyMessage;
            KeyCode = keyCode;
            ScanCode = scanCode;
            TimeStamp = timeStamp;
        }
    }
}
