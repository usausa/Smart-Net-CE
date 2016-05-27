namespace Smart.Converter.Converters
{
    using System;
    using System.Data.SqlTypes;
#if !WindowsCE
    using System.Globalization;
#endif
    using System.Reflection;
#if !WindowsCE
    using System.Threading;
#endif

    /// <summary>
    ///
    /// </summary>
    public class SqlTypesNullableConverter : IObjectConverter
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public bool IsMatch(object value, Type targetType)
        {
            return typeof(INullable).IsAssignableFrom(targetType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Framework only")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", Justification = "Compatibility")]
        public object Convert(object value, Type targetType, ObjectConverter converter)
        {
            if ((value == null) || (value == DBNull.Value))
            {
                return targetType.InvokeMember("Null", BindingFlags.GetField, null, null, null);
            }

            var paramType = targetType.GetValueType();
#if WindowsCE
            return ActivatorEx.CreateInstance(targetType, new[] { System.Convert.ChangeType(value, paramType, null) });
#else
            return Activator.CreateInstance(targetType, new[] { System.Convert.ChangeType(value, paramType) });
#endif
        }
    }
}
