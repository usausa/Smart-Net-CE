namespace Smart
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void InvokeOrNop(this EventHandler handler, object sender, EventArgs e)
        {
            if (handler == null)
            {
                return;
            }

            handler(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEventArgs"></typeparam>
        /// <param name="handler"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void InvokeOrNop<TEventArgs>(this EventHandler<TEventArgs> handler, object sender, TEventArgs e) where TEventArgs : EventArgs
        {
            if (handler == null)
            {
                return;
            }

            handler(sender, e);
        }
    }
}
