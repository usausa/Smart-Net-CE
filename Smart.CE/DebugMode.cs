namespace Smart
{
    /// <summary>
    ///
    /// </summary>
    public static class DebugMode
    {
        /// <summary>
        ///
        /// </summary>
        public static bool IsTrue
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }
    }
}
