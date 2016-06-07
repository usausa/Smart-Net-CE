namespace Smart.Converter.Converters
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public class NullableConverter : IObjectConverter
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
            return targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public object Convert(object value, Type targetType, ObjectConverter converter)
        {
            if ((value == null) || (value == DBNull.Value))
            {
                return null;
            }

            var valueType = targetType.GetValueType();
            var convertedValue = converter.Convert(value, valueType);
#if WindowsCE
            return convertedValue != null ? ActivatorEx.CreateInstance(targetType, new[] { convertedValue }) : null;
#else
            return convertedValue != null ? Activator.CreateInstance(targetType, new[] { convertedValue }) : null;
#endif
        }
    }
}
