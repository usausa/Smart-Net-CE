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
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        T Convert<T>(object value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        object Convert(object value, Type targetType);
    }
}
