namespace Smart.Text
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>
    ///
    /// </summary>
    public static class HexEncoder
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

            return ToHexInternal(bytes, 0, bytes.Length, null, null, 0, Environment.NewLine);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="lineSize"></param>
        /// <returns></returns>
        public static string ToHex(byte[] bytes, int lineSize)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            return ToHexInternal(bytes, 0, bytes.Length, null, null, lineSize, Environment.NewLine);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="lineSize"></param>
        /// <param name="lineSeparator"></param>
        /// <returns></returns>
        public static string ToHex(byte[] bytes, int lineSize, string lineSeparator)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            return ToHexInternal(bytes, 0, bytes.Length, null, null, lineSize, lineSeparator ?? Environment.NewLine);
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

            return ToHexInternal(bytes, 0, bytes.Length, null, separator, 0, Environment.NewLine);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="separator"></param>
        /// <param name="lineSize"></param>
        /// <returns></returns>
        public static string ToHex(byte[] bytes, string separator, int lineSize)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            return ToHexInternal(bytes, 0, bytes.Length, null, separator, lineSize, Environment.NewLine);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="separator"></param>
        /// <param name="lineSize"></param>
        /// <param name="lineSeparator"></param>
        /// <returns></returns>
        public static string ToHex(byte[] bytes, string separator, int lineSize, string lineSeparator)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            return ToHexInternal(bytes, 0, bytes.Length, null, separator, lineSize, lineSeparator ?? Environment.NewLine);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="prefix"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToHex(byte[] bytes, string prefix, string separator)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            return ToHexInternal(bytes, 0, bytes.Length, prefix, separator, 0, Environment.NewLine);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="prefix"></param>
        /// <param name="separator"></param>
        /// <param name="lineSize"></param>
        /// <returns></returns>
        public static string ToHex(byte[] bytes, string prefix, string separator, int lineSize)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            return ToHexInternal(bytes, 0, bytes.Length, prefix, separator, lineSize, Environment.NewLine);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="prefix"></param>
        /// <param name="separator"></param>
        /// <param name="lineSize"></param>
        /// <param name="lineSeparator"></param>
        /// <returns></returns>
        public static string ToHex(byte[] bytes, string prefix, string separator, int lineSize, string lineSeparator)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            return ToHexInternal(bytes, 0, bytes.Length, prefix, separator, lineSize, lineSeparator ?? Environment.NewLine);
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

            return ToHexInternal(bytes, start, length, null, null, 0, Environment.NewLine);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="lineSize"></param>
        /// <returns></returns>
        public static string ToHex(byte[] bytes, int start, int length, int lineSize)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            return ToHexInternal(bytes, start, length, null, null, lineSize, Environment.NewLine);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="lineSize"></param>
        /// <param name="lineSeparator"></param>
        /// <returns></returns>
        public static string ToHex(byte[] bytes, int start, int length, int lineSize, string lineSeparator)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            return ToHexInternal(bytes, start, length, null, null, lineSize, lineSeparator ?? Environment.NewLine);
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

            return ToHexInternal(bytes, start, length, null, separator, 0, Environment.NewLine);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="separator"></param>
        /// <param name="lineSize"></param>
        /// <returns></returns>
        public static string ToHex(byte[] bytes, int start, int length, string separator, int lineSize)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            return ToHexInternal(bytes, start, length, null, separator, lineSize, Environment.NewLine);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="separator"></param>
        /// <param name="lineSize"></param>
        /// <param name="lineSeparator"></param>
        /// <returns></returns>
        public static string ToHex(byte[] bytes, int start, int length, string separator, int lineSize, string lineSeparator)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            return ToHexInternal(bytes, start, length, null, separator, lineSize, lineSeparator ?? Environment.NewLine);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="prefix"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToHex(byte[] bytes, int start, int length, string prefix, string separator)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            return ToHexInternal(bytes, start, length, prefix, separator, 0, Environment.NewLine);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="prefix"></param>
        /// <param name="separator"></param>
        /// <param name="lineSize"></param>
        /// <returns></returns>
        public static string ToHex(byte[] bytes, int start, int length, string prefix, string separator, int lineSize)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            return ToHexInternal(bytes, start, length, prefix, separator, lineSize, Environment.NewLine);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="prefix"></param>
        /// <param name="separator"></param>
        /// <param name="lineSize"></param>
        /// <param name="lineSeparator"></param>
        /// <returns></returns>
        public static string ToHex(byte[] bytes, int start, int length, string prefix, string separator, int lineSize, string lineSeparator)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            return ToHexInternal(bytes, start, length, prefix, separator, lineSize, lineSeparator ?? Environment.NewLine);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="prefix"></param>
        /// <param name="separator"></param>
        /// <param name="lineSize"></param>
        /// <param name="lineSeparator"></param>
        /// <returns></returns>
        private static string ToHexInternal(byte[] bytes, int start, int length, string prefix, string separator, int lineSize, string lineSeparator)
        {
            var addPrefix = !String.IsNullOrEmpty(prefix);
            var addSeparator = !String.IsNullOrEmpty(separator);

            var bufferSize = length * 2;
            var lines = lineSize > 0 ? (length - 1) / lineSize : 0;
            if (lineSize > 0)
            {
                bufferSize += lines * lineSeparator.Length;
            }

            if (addPrefix)
            {
                bufferSize += prefix.Length * length;
            }

            if (addSeparator)
            {
                bufferSize += (length - lines - 1) * separator.Length;
            }

            var sb = new StringBuilder(bufferSize);
            var count = 0;
            for (var i = start; i < start + length; i++)
            {
                if (count != 0)
                {
                    if (count == lineSize)
                    {
                        sb.Append(lineSeparator);
                        count = 0;
                    }
                    else if (addSeparator)
                    {
                        sb.Append(separator);
                    }
                }

                if (addPrefix)
                {
                    sb.Append(prefix);
                }
                sb.Append(bytes[i].ToString("X2", CultureInfo.InvariantCulture));
                count++;
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
