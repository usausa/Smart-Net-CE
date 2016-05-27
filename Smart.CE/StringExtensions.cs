namespace Smart
{
    using System;
    using System.Text;

    /// <summary>
    ///
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Safe(this string value)
        {
            return String.IsNullOrEmpty(value) ? string.Empty : value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Ignore")]
        public static string Repeat(this string value, int count)
        {
            var sb = new StringBuilder(value.Length * count);
            for (var i = 0; i < count; i++)
            {
                sb.Append(value);
            }
            return sb.ToString();
        }
    }
}
