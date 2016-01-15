namespace Smart
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public static class Bcd
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ByteToInt(byte value)
        {
            return (((0xff & value) >> 4) * 10) + (0x0f & value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte IntToByte(int value)
        {
            var lower = value % 10;
            var upper = (value % 100) / 10;
            return (byte)((upper << 4) + lower);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static int BytesToInt(byte[] bytes, int offset, int length)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            var value = 0;
            for (var i = 0; i < length; i++)
            {
                value = value * 100;
                value += ((0xff & bytes[offset + i]) >> 4) * 10;
                value += 0x0f & bytes[offset + i];
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <param name="value"></param>
        public static void IntToBytes(byte[] bytes, int offset, int length, int value)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            var val = value;
            for (var i = 1; i <= length; i++)
            {
                var lower = val % 10;
                var upper = (val % 100) / 10;
                bytes[offset + length - i] = (byte)((upper << 4) + lower);
                val /= 100;
            }
        }
    }
}
