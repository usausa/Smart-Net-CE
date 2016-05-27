namespace Smart.Converter.Converters
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public class StringConverter : IObjectConverter
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public bool IsMatch(object value, Type targetType)
        {
            return targetType == typeof(string);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, ObjectConverter converter)
        {
            return value == null ? null : value.ToString();
        }
    }
}
