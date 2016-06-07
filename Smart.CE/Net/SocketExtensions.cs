namespace Smart.Net
{
    using System.Net.Sockets;

    /// <summary>
    ///
    /// </summary>
    public static class SocketExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static bool IsConnecting(this Socket socket)
        {
            if (!socket.Connected)
            {
                return false;
            }

            if (socket.Poll(0, SelectMode.SelectError))
            {
                return false;
            }

            if (socket.Poll(0, SelectMode.SelectRead) && (socket.Available == 0))
            {
                return false;
            }

            return true;
        }
    }
}
