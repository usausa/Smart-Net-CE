namespace Smart.Net.NetworkInformation
{
    using System.Net;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 
    /// </summary>
    public class PingReply
    {
        /// <summary>
        /// 
        /// </summary>
        public IPAddress Address { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IPStatus Status { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public long RoundTripTime { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Compatibility")]
        public byte[] Buffer { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public PingOptions Options { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reply"></param>
        internal PingReply(ICMP_ECHO_REPLY reply)
        {
            Address = new IPAddress(reply.Address);
            Status = (IPStatus)reply.Status;
            Options = new PingOptions(reply);
            if (Status == IPStatus.Success)
            {
                RoundTripTime = reply.RoundTripTime;
                Buffer = new byte[reply.DataSize];
                Marshal.Copy(reply.Data, Buffer, 0, reply.DataSize);
            }
            else
            {
                Buffer = new byte[0];
            }
        }
    }
}
