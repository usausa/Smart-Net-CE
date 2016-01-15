namespace Smart.Converter.Converters
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class DBNullConverter : IObjectConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public bool IsMatch(object value, Type targetType)
        {
            return value == DBNull.Value;
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
            return DefaultValue.Of(targetType);
        }
    }
}
