namespace Smart
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public static class Bytes
    {
        private static readonly byte[] EmptyInstance = new byte[0];

        /// <summary>
        ///
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Ignore")]
        public static byte[] Empty
        {
            get { return EmptyInstance; }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] FromHex(string hex)
        {
            if (hex == null)
            {
                throw new ArgumentNullException("hex");
            }

            var bytes = new byte[hex.Length / 2];

            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }

            return bytes;
        }
    }
}
