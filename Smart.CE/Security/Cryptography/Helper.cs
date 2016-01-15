namespace Smart.Security.Cryptography
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// 
    /// </summary>
    internal static class Helper
    {
        private static readonly RNGCryptoServiceProvider StaticRandomCryptoNumberGenerator = new RNGCryptoServiceProvider();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static byte[] GetInt(uint i)
        {
            var bytes = BitConverter.GetBytes(i);
            return !BitConverter.IsLittleEndian ? bytes : new[] { bytes[3], bytes[2], bytes[1], bytes[0] };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keySize"></param>
        /// <returns></returns>
        public static byte[] GenerateRandom(int keySize)
        {
            var data = new byte[keySize];
            StaticRandomCryptoNumberGenerator.GetBytes(data);
            return data;
        }
    }
}
