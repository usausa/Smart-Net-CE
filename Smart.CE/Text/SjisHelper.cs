namespace Smart.Text
{
    using System;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public static class SjisHelper
    {
        private static readonly Encoding Encode = Encoding.GetEncoding("Shift_JIS");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetSjisByteCount(string str)
        {
            return Encode.GetByteCount(str);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] GetSjisBytes(string str)
        {
            return Encode.GetBytes(str);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string GetSjisString(byte[] bytes, int index, int count)
        {
            return Encode.GetString(bytes, index, count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <param name="alignment"></param>
        /// <param name="padding"></param>
        /// <returns></returns>
        public static byte[] GetSjisFixedBytes(string str, int length, StringAlignment alignment, byte padding)
        {
            var bytes = Encode.GetBytes(str);
            if (bytes.Length == length)
            {
                return bytes;
            }

            if (bytes.Length > length)
            {
                // 切り捨て
                var newBytes = new byte[length];
                if (((bytes[length - 1] >= 0x81) && (bytes[length - 1] <= 0x9f)) ||
                    ((bytes[length - 1] >= 0xe0) && (bytes[length - 1] <= 0xfc)))
                {
                    Buffer.BlockCopy(bytes, 0, newBytes, 0, length - 1);
                    newBytes[length - 1] = padding;
                }
                else
                {
                    Buffer.BlockCopy(bytes, 0, newBytes, 0, length);
                }
                return newBytes;
            }
            else
            {
                // パディング
                var newBytes = new byte[length];
                if (alignment == StringAlignment.Left)
                {
                    Buffer.BlockCopy(bytes, 0, newBytes, 0, bytes.Length);
                    for (var i = bytes.Length; i < length; i++)
                    {
                        newBytes[i] = padding;
                    }
                }
                else if (alignment == StringAlignment.Right)
                {
                    var i = 0;
                    for (; i < length - bytes.Length; i++)
                    {
                        newBytes[i] = padding;
                    }
                    Buffer.BlockCopy(bytes, 0, newBytes, i, bytes.Length);
                }
                else
                {
                    var i = 0;
                    for (; i < (length - bytes.Length) / 2; i++)
                    {
                        newBytes[i] = padding;
                    }
                    Buffer.BlockCopy(bytes, 0, newBytes, i, bytes.Length);
                    for (i = i + bytes.Length; i < length; i++)
                    {
                        newBytes[i] = padding;
                    }
                }
                return newBytes;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public static byte[] GetSjisFixedBytes(string str, int length, StringAlignment alignment)
        {
            return GetSjisFixedBytes(str, length, alignment, 0x20);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <param name="padding"></param>
        /// <returns></returns>
        public static byte[] GetSjisFixedBytes(string str, int length, byte padding)
        {
            return GetSjisFixedBytes(str, length, StringAlignment.Left, padding);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GetSjisFixedBytes(string str, int length)
        {
            return GetSjisFixedBytes(str, length, StringAlignment.Left, 0x20);
        }
    }
}
