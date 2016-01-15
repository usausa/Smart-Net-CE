namespace Smart.Threading
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Threading;

    /// <summary>
    /// 
    /// </summary>
    public sealed class Semaphore : WaitHandle
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialCount"></param>
        /// <param name="maximumCount"></param>
        public Semaphore(int initialCount, int maximumCount)
            : this(initialCount, maximumCount, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialCount"></param>
        /// <param name="maximumCount"></param>
        /// <param name="name"></param>
        public Semaphore(int initialCount, int maximumCount, string name)
        {
            if (initialCount < 0)
            {
                throw new ArgumentOutOfRangeException("initialCount");
            }

            if (maximumCount < 1)
            {
                throw new ArgumentOutOfRangeException("maximumCount");
            }

            if (initialCount > maximumCount)
            {
                throw new ArgumentException("initialCount greater than maximumCount.");
            }

            var handle = NativeMethods.CreateSemaphore(IntPtr.Zero, initialCount, maximumCount, name);
            var error = Marshal.GetLastWin32Error();

            if (handle == IntPtr.Zero)
            {
                throw new Win32Exception(error);
            }

            Handle = handle;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialCount"></param>
        /// <param name="maximumCount"></param>
        /// <param name="name"></param>
        /// <param name="createdNew"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", Justification = "Compatibility")]
        public Semaphore(int initialCount, int maximumCount, string name, out bool createdNew)
        {
            if (initialCount < 0)
            {
                throw new ArgumentOutOfRangeException("initialCount");
            }

            if (maximumCount < 1)
            {
                throw new ArgumentOutOfRangeException("maximumCount");
            }

            if (initialCount > maximumCount)
            {
                throw new ArgumentException("initialCount greater than maximumCount.");
            }

            var handle = NativeMethods.CreateSemaphore(IntPtr.Zero, initialCount, maximumCount, name);
            var error = Marshal.GetLastWin32Error();

            if (handle == IntPtr.Zero)
            {
                throw new Win32Exception(error);
            }

            createdNew = error != NativeMethods.ERROR_ALREADY_EXISTS;

            Handle = handle;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="explicitDisposing"></param>
        protected override void Dispose(bool explicitDisposing)
        {
            Close();
            base.Dispose(explicitDisposing);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Close()
        {
            if (Handle != InvalidHandle)
            {
                NativeMethods.CloseHandle(Handle);
                Handle = InvalidHandle;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Release()
        {
            return Release(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="releaseCount"></param>
        /// <returns></returns>
        public int Release(int releaseCount)
        {
            if (releaseCount < 1)
            {
                throw new ArgumentOutOfRangeException("releaseCount");
            }

            int previousCount;
            if (!NativeMethods.ReleaseSemaphore(Handle, releaseCount, out previousCount))
            {
                throw new SemaphoreFullException();
            }

            return previousCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool WaitOne()
        {
            return WaitOne(NativeMethods.INFINITE, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="millisecondsTimeout"></param>
        /// <param name="exitContext"></param>
        /// <returns></returns>
        public override bool WaitOne(int millisecondsTimeout, bool exitContext)
        {
            return NativeMethods.WaitForSingleObject(Handle, millisecondsTimeout) != NativeMethods.WAIT_TIMEOUT;
        }
    }
}
