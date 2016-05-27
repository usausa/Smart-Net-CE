namespace Smart.Text
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>
    ///
    /// </summary>
    public static class HexHelper
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHex(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            return ToHexInternal(bytes, 0, bytes.Length, null);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToHex(byte[] bytes, string separator)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            return ToHexInternal(bytes, 0, bytes.Length, separator);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToHex(byte[] bytes, int start, int length)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            return ToHexInternal(bytes, start, length, null);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToHex(byte[] bytes, int start, int length, string separator)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            return ToHexInternal(bytes, start, length, separator);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        private static string ToHexInternal(byte[] bytes, int start, int length, string separator)
        {
            var addSeparator = String.IsNullOrEmpty(separator);
            var sb = new StringBuilder((length * 2) + (addSeparator ? 0 : ((length - 1) * separator.Length)));
            for (var i = start; i < start + length; i++)
            {
                if (addSeparator && (i != start))
                {
                    sb.Append(separator);
                }

                sb.Append(bytes[i].ToString("X2", CultureInfo.InvariantCulture));
            }
            return sb.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static byte[] ToBytes(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            var bytes = new byte[text.Length / 2];
            for (var index = 0; index < bytes.Length; index++)
            {
                bytes[index] = byte.Parse(text.Substring(index * 2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
            return bytes;
        }
    }
}
