namespace Smart.Converter
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public interface IObjectConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        bool IsMatch(object value, Type targetType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        object Convert(object value, Type targetType, ObjectConverter converter);
    }
}
