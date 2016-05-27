namespace Smart.Diagnostics
{
    using System.Diagnostics;

    /// <summary>
    ///
    /// </summary>
    public static class DebugBreak
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="condition"></param>
        [DebuggerHidden]
        [Conditional("DEBUG")]
        public static void If(bool condition)
        {
            if (condition)
            {
                Debugger.Break();
            }
        }
    }
}
