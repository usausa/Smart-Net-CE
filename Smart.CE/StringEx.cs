namespace Smart
{
    using System;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public static class StringEx
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string Join(string separator, params object[] values)
        {
            var sb = new StringBuilder();

            foreach (var value in values)
            {
                sb.Append(value);
                sb.Append(separator);
            }

            if (sb.Length > 0)
            {
                sb.Length = sb.Length - separator.Length;
            }

            return sb.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(string value)
        {
            return (value == null) || value.All(Char.IsWhiteSpace);
        }
    }
}
