namespace Smart.Navigation
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class ViewConfirmEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsClosing { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isClosing"></param>
        public ViewConfirmEventArgs(bool isClosing)
        {
            IsClosing = isClosing;
        }
    }
}
