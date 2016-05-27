namespace Smart.Threading
{
    using System;

    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public class SemaphoreFullException : SystemException
    {
        /// <summary>
        ///
        /// </summary>
        public SemaphoreFullException()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public SemaphoreFullException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public SemaphoreFullException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
