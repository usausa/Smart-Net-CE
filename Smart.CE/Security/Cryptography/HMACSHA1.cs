namespace Smart.Security.Cryptography
{
    using System.Security.Cryptography;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Ignore")]
    public class HMACSHA1 : HMAC
    {
        /// <summary>
        /// 
        /// </summary>
        public HMACSHA1()
            : this(Helper.GenerateRandom(0x40), false)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public HMACSHA1(byte[] key)
            : this(key, false)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="useManagedSha1"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:スコープを失う前にオブジェクトを破棄", Justification = "Ignore")]
        public HMACSHA1(byte[] key, bool useManagedSha1)
            : base("SHA1", useManagedSha1 ? (HashAlgorithm)new SHA1Managed() : new SHA1CryptoServiceProvider(), useManagedSha1 ? (HashAlgorithm)new SHA1Managed() : new SHA1CryptoServiceProvider())
        {
            HashSizeValue = 160;
            InitializeKey(key);
        }
    }
}
