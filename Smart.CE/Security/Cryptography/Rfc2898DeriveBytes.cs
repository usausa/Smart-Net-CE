namespace Smart.Security.Cryptography
{
    using System;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class Rfc2898DeriveBytes : DeriveBytes
    {
        private readonly HMACSHA1 hmacsha1;

        private uint block;
        private byte[] buffer;
        private int endIndex;
        private uint iterations;
        private byte[] salt;
        private int startIndex;

        /// <summary>
        /// 
        /// </summary>
        public int IterationCount
        {
            get
            {
                return (int)iterations;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                iterations = (uint)value;
                Initialize();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Compatibility")]
        public byte[] Salt
        {
            get
            {
                return (byte[])salt.Clone();
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (value.Length < 8)
                {
                    throw new ArgumentException("Salt bytes is short", "value");
                }

                salt = (byte[])value.Clone();
                Initialize();
            }
        }

        public Rfc2898DeriveBytes(string password, int saltSize)
            : this(password, saltSize, 0x3e8)
        {
        }

        public Rfc2898DeriveBytes(string password, byte[] salt)
            : this(password, salt, 0x3e8)
        {
        }

        public Rfc2898DeriveBytes(string password, int saltSize, int iterations)
        {
            if (saltSize < 0)
            {
                throw new ArgumentOutOfRangeException("saltSize");
            }

            var data = Helper.GenerateRandom(saltSize);
            Salt = data;
            IterationCount = iterations;
            hmacsha1 = new HMACSHA1(new UTF8Encoding(false).GetBytes(password));

            Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        public Rfc2898DeriveBytes(string password, byte[] salt, int iterations)
            : this(new UTF8Encoding(false).GetBytes(password), salt, iterations)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        public Rfc2898DeriveBytes(byte[] password, byte[] salt, int iterations)
        {
            Salt = salt;
            IterationCount = iterations;
            hmacsha1 = new HMACSHA1(password);

            Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ((IDisposable)hmacsha1).Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 
        /// </summary>
        private void Initialize()
        {
            buffer = new byte[20];
            block = 1;
            startIndex = 0;
            endIndex = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Reset()
        {
            Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cb"></param>
        /// <returns></returns>
        public override byte[] GetBytes(int cb)
        {
            if (cb <= 0)
            {
                throw new ArgumentOutOfRangeException("cb");
            }

            var dst = new byte[cb];
            var dstOffsetBytes = 0;
            var byteCount = endIndex - startIndex;
            if (byteCount > 0)
            {
                if (cb < byteCount)
                {
                    Array.Copy(buffer, startIndex, dst, 0, cb);
                    startIndex += cb;
                    return dst;
                }
                Array.Copy(buffer, startIndex, dst, 0, byteCount);
                startIndex = endIndex = 0;
                dstOffsetBytes += byteCount;
            }

            while (dstOffsetBytes < cb)
            {
                var src = GetSource();
                var length = cb - dstOffsetBytes;
                if (length > 20)
                {
                    Array.Copy(src, 0, dst, dstOffsetBytes, 20);
                    dstOffsetBytes += 20;
                }
                else
                {
                    Array.Copy(src, 0, dst, dstOffsetBytes, length);
                    Array.Copy(src, length, buffer, startIndex, 20 - length);
                    endIndex += 20 - length;
                    return dst;
                }
            }

            return dst;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private byte[] GetSource()
        {
            var intBuffer = Helper.GetInt(block);
            hmacsha1.TransformBlock(salt, 0, salt.Length, salt, 0);
            hmacsha1.TransformFinalBlock(intBuffer, 0, intBuffer.Length);

            var hash = hmacsha1.Hash;
            hmacsha1.Initialize();

            var source = hash;
            for (var i = 2; i <= iterations; i++)
            {
                hash = hmacsha1.ComputeHash(hash);
                for (var j = 0; j < 20; j++)
                {
                    source[j] = (byte)(source[j] ^ hash[j]);
                }
            }
            block++;

            return source;
        }
    }
}
