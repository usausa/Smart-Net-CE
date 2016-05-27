namespace Smart
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public static class NumberExtension
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Compatibility")]
        public static ushort HiWord(this int src)
        {
            return (ushort)(src >> 16);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Compatibility")]
        public static ushort HiWord(this IntPtr src)
        {
            return src.ToInt32().HiWord();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Compatibility")]
        public static ushort HiWord(this uint src)
        {
            return (ushort)(src >> 16);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Compatibility")]
        public static ushort LoWord(this int src)
        {
            return (ushort)(src & 0xffff);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Compatibility")]
        public static ushort LoWord(this IntPtr src)
        {
            return src.ToInt32().LoWord();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Compatibility")]
        public static ushort LoWord(this uint src)
        {
            return (ushort)(src & 0xffff);
        }
    }
}
