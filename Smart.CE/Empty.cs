namespace Smart
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Empty<T>
    {
        private static readonly T[] ArrayInstance = new T[0];

        /// <summary>
        ///
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Ignore")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Ignore")]
        public static T[] Array
        {
            get { return ArrayInstance; }
        }
    }
}
