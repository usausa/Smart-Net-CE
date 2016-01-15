namespace Smart.Net.Sntp
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    /// 
    /// </summary>
    public class SntpClient
    {
        private const int NTP_PACKET_LENGTH = 48;
        private const int TRANSMIT_OFFSET = 40;

        private readonly byte[] data = new byte[NTP_PACKET_LENGTH];

        /// <summary>
        /// 
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Transmit
        {
            get
            {
                return ComputeDate(GetMilliSeconds(TRANSMIT_OFFSET));
            }
            set
            {
                SetDate(TRANSMIT_OFFSET, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SntpClient()
        {
            Port = 123;
            Timeout = 5000;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        private static DateTime ComputeDate(ulong milliseconds)
        {
            var time = new DateTime(1900, 1, 1);
            var span = TimeSpan.FromMilliseconds(milliseconds);
            return time + span;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        private ulong GetMilliSeconds(int offset)
        {
            ulong second = 0;
            ulong fraction = 0;

            for (var i = 0; i <= 3; i++)
            {
                second = (256 * second) + data[offset + i];
            }

            for (var i = 4; i <= 7; i++)
            {
                fraction = (256 * fraction) + data[offset + i];
            }

            return (second * 1000) + ((fraction * 1000) / 0x100000000L);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="date"></param>
        private void SetDate(int offset, DateTime date)
        {
            var startOfCentury = new DateTime(1900, 1, 1, 0, 0, 0);

            var milliseconds = (ulong)(date - startOfCentury).TotalMilliseconds;

            var second = milliseconds / 1000;
            var fraction = ((milliseconds % 1000) * 0x100000000L) / 1000;

            var temp = second;
            for (var i = 3; i >= 0; i--)
            {
                data[offset + i] = (byte)(temp % 256);
                temp = temp / 256;
            }

            temp = fraction;
            for (var i = 7; i >= 4; i--)
            {
                data[offset + i] = (byte)(temp % 256);
                temp = temp / 256;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Initialize()
        {
            data[0] = 27;    // LI=0, VN=3, Mode=3
            for (var i = 1; i < 48; i++)
            {
                data[i] = 0;
            }

            Transmit = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Synchronize()
        {
            Initialize();

            IPAddress address;
            try
            {
                address = IPAddress.Parse(Server);
            }
            catch (FormatException)
            {
                address = Dns.GetHostEntry(Server).AddressList[0];
            }
            EndPoint ep = new IPEndPoint(address, Port);

            using (var socket = new Socket(ep.AddressFamily, SocketType.Dgram, ProtocolType.Udp))
            {
                socket.SendTo(data, ep);

                while (socket.Available < NTP_PACKET_LENGTH)
                {
                    if (socket.Poll(Timeout * 1000, SelectMode.SelectRead) == false)
                    {
                        return false;
                    }
                }

                socket.ReceiveFrom(data, data.Length, SocketFlags.None, ref ep);
            }

            return true;
        }
    }
}
