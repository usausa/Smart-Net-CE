namespace Smart
{
    using System;

    using Smart.ComponentModel.DataAnnotations;

    /// <summary>
    /// 
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Ignore")]
        public static string ToDisplayName(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);
            return attributes.Length >= 1 ? attributes[0].Name : value.ToString();
        }
    }
}
