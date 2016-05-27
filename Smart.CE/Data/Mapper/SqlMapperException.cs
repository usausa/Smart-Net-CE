namespace Smart.Data.Mapper
{
    using System;

    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public class SqlMapperException : Exception
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public SqlMapperException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public SqlMapperException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
