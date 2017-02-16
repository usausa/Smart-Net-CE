namespace Smart
{
    using System;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Functions<T>
    {
        private static readonly Func<T, T> IdentifyInstance = x => x;

        private static readonly Func<T, string> StringInstance = x => x.ToString();

        /// <summary>
        ///
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Ignore")]
        public static Func<T, T> Identify
        {
            get { return IdentifyInstance; }
        }

        /// <summary>
        ///
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Ignore")]
        public static Func<T, string> String
        {
            get { return StringInstance; }
        }
    }
}
