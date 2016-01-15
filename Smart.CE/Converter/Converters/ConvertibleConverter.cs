namespace Smart.Converter.Converters
{
    using System;
#if !WindowsCE
    using System.Globalization;
    using System.Threading;
#endif

    public class ConvertibleConverter : IObjectConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public bool IsMatch(object value, Type targetType)
        {
            return typeof(IConvertible).IsAssignableFrom(targetType);
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
            if ((value == null) || (value == DBNull.Value))
            {
                return DefaultValue.Of(targetType);
            }

#if WindowsCE
            return System.Convert.ChangeType(value, targetType, null);
#else
            return System.Convert.ChangeType(value, targetType);
#endif
        }
    }
}
