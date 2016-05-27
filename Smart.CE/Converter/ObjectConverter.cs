namespace Smart.Converter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    ///
    /// </summary>
    public class ObjectConverter
    {
        private static readonly ObjectConverter Instance = new ObjectConverter();

        /// <summary>
        ///
        /// </summary>
        public static ObjectConverter Default
        {
            get { return Instance; }
        }

        /// <summary>
        ///
        /// </summary>
        private IList<IObjectConverter> converters;

        /// <summary>
        ///
        /// </summary>
        public ObjectConverter()
        {
            ResetConverters();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="list"></param>
        public void SetConverters(IList<IObjectConverter> list)
        {
            converters = list;
        }

        /// <summary>
        ///
        /// </summary>
        public void ResetConverters()
        {
            converters = DefaultObjectConverters.Create();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public T Convert<T>(object value)
        {
            return (T)Convert(value, typeof(T));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType)
        {
            try
            {
                var converter = converters.FirstOrDefault(_ => _.IsMatch(value, targetType));
                if (converter == null)
                {
                    throw new ObjectConverterException(String.Format(CultureInfo.InvariantCulture, "Type {0} can't convert to {1}", value == null ? "null" : value.GetType().ToString(), targetType));
                }

                return converter.Convert(value, targetType, this);
            }
            catch (Exception ex)
            {
                throw new ObjectConverterException("Unknown exception.", ex);
            }
        }
    }
}
