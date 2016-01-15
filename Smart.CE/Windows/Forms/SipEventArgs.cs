namespace Smart.Windows.Forms
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class SipEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public int Key { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public SipEventArgs(int key)
        {
            Key = key;
        }
    }
}
