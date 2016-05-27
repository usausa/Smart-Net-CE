namespace Smart.Diagnostics
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

    /// <summary>
    ///
    /// </summary>
    public static class DebugMessage
    {
        private static string indent = string.Empty;

        /// <summary>
        ///
        /// </summary>
        [Conditional("DEBUG")]
        public static void Indent()
        {
            indent = indent + "  ";
        }

        /// <summary>
        ///
        /// </summary>
        [Conditional("DEBUG")]
        public static void Unindent()
        {
            if (indent.Length > 0)
            {
                indent = indent.Substring(0, indent.Length - 2);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        [Conditional("DEBUG")]
        public static void Write(object value)
        {
            NativeMethods.DebugMsg(String.Format(CultureInfo.InvariantCulture, "{0}{1}", indent, value));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        [Conditional("DEBUG")]
        public static void WriteLine(object value)
        {
            NativeMethods.DebugMsg(String.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", indent, value, Environment.NewLine));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="value"></param>
        [Conditional("DEBUG")]
        public static void Write(bool condition, object value)
        {
            if (condition)
            {
                NativeMethods.DebugMsg(String.Format(CultureInfo.InvariantCulture, "{0}{1}", indent, value));
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="value"></param>
        [Conditional("DEBUG")]
        public static void WriteLine(bool condition, object value)
        {
            if (condition)
            {
                NativeMethods.DebugMsg(String.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", indent, value, Environment.NewLine));
            }
        }
    }
}
