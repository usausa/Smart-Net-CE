namespace Smart
{
    using System;
    using System.Collections.Generic;
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
        public static string Join(string separator, params string[] values)
        {
            return Join(separator, values as IEnumerable<string>);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string Join(string separator, params object[] values)
        {
            return Join(separator, values as IEnumerable<object>);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string Join<T>(string separator, IEnumerable<T> values)
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
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string Join(string separator, IEnumerable<string> values)
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
