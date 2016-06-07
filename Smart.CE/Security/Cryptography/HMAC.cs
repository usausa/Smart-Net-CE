namespace Smart.Security.Cryptography
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    ///
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Compatibility")]
    public abstract class HMAC : KeyedHashAlgorithm
    {
        private readonly HashAlgorithm hash1;
        private readonly HashAlgorithm hash2;

        private bool hashed;
        private byte[] inner;
        private byte[] outer;

        /// <summary>
        ///
        /// </summary>
        public string HashName { get; private set; }

        /// <summary>
        ///
        /// </summary>
        protected int BlockSizeValue { get; set; }

        /// <summary>
        ///
        /// </summary>
        public override byte[] Key
        {
            get
            {
                return (byte[])KeyValue.Clone();
            }
            set
            {
                if (hashed)
                {
                    throw new CryptographicException("Hashed");
                }
                InitializeKey(value);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hashName"></param>
        /// <param name="hash1"></param>
        /// <param name="hash2"></param>
        protected HMAC(string hashName, HashAlgorithm hash1, HashAlgorithm hash2)
        {
            HashName = hashName;
            this.hash1 = hash1;
            this.hash2 = hash2;
            BlockSizeValue = 0x40;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ((IDisposable)hash1).Dispose();
                ((IDisposable)hash2).Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="ibStart"></param>
        /// <param name="cbSize"></param>
        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            if (!hashed)
            {
                hash1.TransformBlock(inner, 0, inner.Length, inner, 0);
                hashed = true;
            }
            hash1.TransformBlock(array, ibStart, cbSize, array, ibStart);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected override byte[] HashFinal()
        {
            if (!hashed)
            {
                hash1.TransformBlock(inner, 0, inner.Length, inner, 0);
                hashed = true;
            }

            hash1.TransformFinalBlock(new byte[0], 0, 0);
            var hashValue = hash1.Hash;
            hash2.TransformBlock(outer, 0, outer.Length, outer, 0);
            hash2.TransformBlock(hashValue, 0, hashValue.Length, hashValue, 0);
            hashed = false;
            hash2.TransformFinalBlock(new byte[0], 0, 0);

            return hash2.Hash;
        }

        /// <summary>
        ///
        /// </summary>
        public override void Initialize()
        {
            hash1.Initialize();
            hash2.Initialize();
            hashed = false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Protected")]
        protected void InitializeKey(byte[] key)
        {
            if (key.Length > BlockSizeValue)
            {
                KeyValue = hash1.ComputeHash(key);
            }
            else
            {
                KeyValue = (byte[])key.Clone();
            }

            inner = new byte[BlockSizeValue];
            outer = new byte[BlockSizeValue];

            for (var i = 0; i < BlockSizeValue; i++)
            {
                inner[i] = 0x36;
                outer[i] = 0x5c;
            }

            for (var i = 0; i < KeyValue.Length; i++)
            {
                inner[i] = (byte)(inner[i] ^ KeyValue[i]);
                outer[i] = (byte)(outer[i] ^ KeyValue[i]);
            }
        }
    }
}
