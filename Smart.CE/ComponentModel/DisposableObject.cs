namespace Smart.ComponentModel
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public abstract class DisposableObject : IDisposable
    {
        private readonly object sync = new object();

        /// <summary>
        ///
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        ///
        /// </summary>
        ~DisposableObject()
        {
            Dispose(false);
        }

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            lock (sync)
            {
                if (disposing && !IsDisposed)
                {
                    IsDisposed = true;
                }
            }
        }
    }
}
