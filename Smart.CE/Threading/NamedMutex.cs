namespace Smart.Threading
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Threading;

    /// <summary>
    /// 
    /// </summary>
    public sealed class NamedMutex : WaitHandle
    {
        /// <summary>
        /// 
        /// </summary>
        public NamedMutex()
            : this(false, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialOwner"></param>
        public NamedMutex(bool initialOwner)
            : this(initialOwner, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialOwner"></param>
        /// <param name="name"></param>
        public NamedMutex(bool initialOwner, string name)
        {
            var handle = NativeMethods.CreateMutex(IntPtr.Zero, initialOwner, name);
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
        /// <param name="initialOwner"></param>
        /// <param name="name"></param>
        /// <param name="createdNew"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", Justification = "Compatibility")]
        public NamedMutex(bool initialOwner, string name, out bool createdNew)
        {
            var handle = NativeMethods.CreateMutex(IntPtr.Zero, initialOwner, name);
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
        public void ReleaseMutex()
        {
            if (!NativeMethods.ReleaseMutex(Handle))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
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
