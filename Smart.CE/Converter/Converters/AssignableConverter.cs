namespace Smart.Converter.Converters
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public class AssignableConverter : IObjectConverter
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public bool IsMatch(object value, Type targetType)
        {
            return (value != null) && targetType.IsInstanceOfType(value);
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
            return value;
        }
    }
}
