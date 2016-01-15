namespace Smart.Net.NetworkInformation
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class PingException : InvalidOperationException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public PingException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public PingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
