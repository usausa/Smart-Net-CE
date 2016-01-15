namespace Smart.Net.NetworkInformation
{
    using System;
    using System.ComponentModel;
    using System.Net;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 
    /// </summary>
    public class Ping : IDisposable
    {
        private IntPtr handle;
        private byte[] defaultSendBuffer;
        private bool disposed;

        /// <summary>
        /// 
        /// </summary>
        private byte[] SendBuffer
        {
            get
            {
                if (defaultSendBuffer == null)
                {
                    defaultSendBuffer = new byte[64];
                    for (var i = 1; i < 64; i++)
                    {
                        defaultSendBuffer[i] = (byte)i;
                    }
                }
                return defaultSendBuffer;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        ~Ping()
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
            if (!disposed)
            {
                if (handle != IntPtr.Zero)
                {
                    NativeMethods.IcmpCloseHandle(handle);
                    handle = IntPtr.Zero;
                }
                disposed = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostNameOrAddress"></param>
        /// <returns></returns>
        public PingReply Send(string hostNameOrAddress)
        {
            return Send(hostNameOrAddress, 5000, null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public PingReply Send(IPAddress address)
        {
            return Send(address, 5000, null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostNameOrAddress"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public PingReply Send(string hostNameOrAddress, int timeout)
        {
            return Send(hostNameOrAddress, timeout, null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public PingReply Send(IPAddress address, int timeout)
        {
            return Send(address, timeout, null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostNameOrAddress"></param>
        /// <param name="timeout"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public PingReply Send(string hostNameOrAddress, int timeout, byte[] buffer)
        {
            return Send(hostNameOrAddress, timeout, buffer, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="timeout"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public PingReply Send(IPAddress address, int timeout, byte[] buffer)
        {
            return Send(address, timeout, buffer, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostNameOrAddress"></param>
        /// <param name="timeout"></param>
        /// <param name="buffer"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public PingReply Send(string hostNameOrAddress, int timeout, byte[] buffer, PingOptions options)
        {
            if (String.IsNullOrEmpty(hostNameOrAddress))
            {
                throw new ArgumentNullException("hostNameOrAddress");
            }

            IPAddress address;
            try
            {
                address = IPAddress.Parse(hostNameOrAddress);
            }
            catch (FormatException)
            {
                try
                {
                    address = Dns.GetHostEntry(hostNameOrAddress).AddressList[0];
                }
                catch (Exception ex)
                {
                    throw new PingException("Impossible to resolve hostname.", ex);
                }
            }

            return Send(address, timeout, buffer, options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="timeout"></param>
        /// <param name="buffer"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public PingReply Send(IPAddress address, int timeout, byte[] buffer, PingOptions options)
        {
            if (buffer == null)
            {
                buffer = SendBuffer;
            }

            if (buffer.Length <= 0)
            {
                throw new ArgumentOutOfRangeException("buffer");
            }

            if (timeout < 0)
            {
                throw new ArgumentOutOfRangeException("timeout");
            }

            if (address == null)
            {
                throw new ArgumentNullException("address");
            }

            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            PingReply reply;
            try
            {
                reply = InternalSend(address, buffer, timeout, options);
            }
            catch (PingException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PingException("Impossible to send packet.", ex);
            }

            return reply;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="buffer"></param>
        /// <param name="timeout"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1404:CallGetLastErrorImmediatelyAfterPInvoke", Justification = "No problem")]
        private PingReply InternalSend(IPAddress address, byte[] buffer, int timeout, PingOptions options)
        {
            if (handle == IntPtr.Zero)
            {
                handle = NativeMethods.IcmpCreateFile();
                if (handle.ToInt32() == -1)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }

            ICMP_ECHO_REPLY reply;
            var replyBuffer = new byte[65536];
            var bufferHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            var replyHandle = GCHandle.Alloc(replyBuffer, GCHandleType.Pinned);
            try
            {
                var ipoption = new IP_OPTION_INFORMATION(options);
                if (NativeMethods.IcmpSendEcho2(handle, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero,
                                                BitConverter.ToUInt32(address.GetAddressBytes(), 0),
                                                bufferHandle.AddrOfPinnedObject(), (ushort)buffer.Length, ref ipoption,
                                                replyHandle.AddrOfPinnedObject(), (uint)replyBuffer.Length, (uint)timeout) == 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            finally
            {
                reply = (ICMP_ECHO_REPLY)Marshal.PtrToStructure(replyHandle.AddrOfPinnedObject(), typeof(ICMP_ECHO_REPLY));
                replyHandle.Free();
                bufferHandle.Free();
            }

            return new PingReply(reply);
        }
    }
}
