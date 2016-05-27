namespace Smart.Security.Cryptography
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    ///
    /// </summary>
    public abstract class KeyedHashAlgorithm : HashAlgorithm
    {
        /// <summary>
        ///
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Ignore")]
        protected byte[] KeyValue { get; set; }

        /// <summary>
        ///
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Compatibility")]
        public virtual byte[] Key
        {
            get
            {
                return (byte[])KeyValue.Clone();
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                KeyValue = (byte[])value.Clone();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (KeyValue != null)
                {
                    Array.Clear(KeyValue, 0, KeyValue.Length);
                }
                KeyValue = null;
            }
            base.Dispose(disposing);
        }
    }
}
