namespace Smart.Net.NetworkInformation
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class PingOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public bool DontFragment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Ttl { get; set; }

        public PingOptions()
        {
            Ttl = 128;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ttl"></param>
        /// <param name="dontFragment"></param>
        public PingOptions(int ttl, bool dontFragment)
        {
            if (ttl <= 0)
            {
                throw new ArgumentOutOfRangeException("ttl");
            }

            Ttl = ttl;
            DontFragment = dontFragment;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reply"></param>
        internal PingOptions(ICMP_ECHO_REPLY reply)
        {
            Ttl = reply.Ttl;
            DontFragment = (reply.Flags & 2) > 0;
        }
    }
}
