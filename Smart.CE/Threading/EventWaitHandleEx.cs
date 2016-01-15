namespace Smart.Threading
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Threading;

    /// <summary>
    /// 
    /// </summary>
    public sealed class EventWaitHandleEx : IDisposable
    {
        private const int WAIT_TIMEOUT = 0x102;
        private const int EVENT_ALL_ACCESS = 0x3;
        private const int ERROR_ALREADY_EXISTS = 183;

        public static readonly IntPtr InvalidHandle = new IntPtr(-1);

        public IntPtr Handle { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1404:CallGetLastErrorImmediatelyAfterPInvoke", Justification = "No problem")]
        private EventWaitHandleEx(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            Handle = handle;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialState"></param>
        /// <param name="mode"></param>
        public EventWaitHandleEx(bool initialState, EventResetMode mode)
            : this(NativeMethods.CreateEvent(IntPtr.Zero, mode == EventResetMode.ManualReset, initialState, null))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialState"></param>
        /// <param name="mode"></param>
        /// <param name="name"></param>
        public EventWaitHandleEx(bool initialState, EventResetMode mode, string name)
            : this(NativeMethods.CreateEvent(IntPtr.Zero, mode == EventResetMode.ManualReset, initialState, name))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialState"></param>
        /// <param name="mode"></param>
        /// <param name="name"></param>
        /// <param name="createdNew"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", Justification = "Compatibility")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1404:CallGetLastErrorImmediatelyAfterPInvoke", Justification = "No problem")]
        public EventWaitHandleEx(bool initialState, EventResetMode mode, string name, out bool createdNew)
        {
            var handle = NativeMethods.CreateEvent(IntPtr.Zero, mode == EventResetMode.ManualReset, initialState, name);
            if (handle == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            createdNew = Marshal.GetLastWin32Error() != ERROR_ALREADY_EXISTS;

            Handle = handle;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
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
        public void Close()
        {
            Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static EventWaitHandleEx OpenExisting(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            var handle = NativeMethods.OpenEvent(EVENT_ALL_ACCESS, false, name);

            return new EventWaitHandleEx(handle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Reset()
        {
            return NativeMethods.EventModify(Handle, EVENT.RESET);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Set()
        {
            return NativeMethods.EventModify(Handle, EVENT.SET);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool WaitOne()
        {
            return NativeMethods.WaitForSingleObject(Handle, -1) != WAIT_TIMEOUT;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool WaitOne(int timeout)
        {
            return NativeMethods.WaitForSingleObject(Handle, timeout) != WAIT_TIMEOUT;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="waitHandles"></param>
        /// <returns></returns>
        public static int WaitAny(EventWaitHandleEx[] waitHandles)
        {
            return WaitAny(waitHandles, Timeout.Infinite);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="waitHandles"></param>
        /// <param name="millisecondsTimeout"></param>
        /// <returns></returns>
        public static int WaitAny(EventWaitHandleEx[] waitHandles, int millisecondsTimeout)
        {
            if (waitHandles == null)
            {
                throw new ArgumentNullException("waitHandles");
            }

            var handles = new IntPtr[waitHandles.Length];
            for (var i = 0; i < handles.Length; i++)
            {
                handles[i] = waitHandles[i].Handle;
            }

            return NativeMethods.WaitForMultipleObjects(handles.Length, handles, false, millisecondsTimeout);
        }
    }
}
