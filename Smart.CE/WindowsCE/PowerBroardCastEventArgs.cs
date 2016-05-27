namespace Smart.WindowsCE
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public class PowerBroardCastEventArgs : EventArgs
    {
        /// <summary>
        ///
        /// </summary>
        public PowerBroadcastStatus Status
        {
            get;
            private set;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="status"></param>
        public PowerBroardCastEventArgs(PowerBroadcastStatus status)
        {
            Status = status;
        }
    }
}
