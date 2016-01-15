namespace Smart.Security.Cryptography
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public abstract class DeriveBytes : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cb"></param>
        /// <returns></returns>
        public abstract byte[] GetBytes(int cb);

        /// <summary>
        /// 
        /// </summary>
        public abstract void Reset();
    }
}
