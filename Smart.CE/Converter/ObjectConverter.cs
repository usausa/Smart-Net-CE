namespace Smart.Converter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Smart.Collections.Generic;
    using Smart.Converter.Converters;

    /// <summary>
    ///
    /// </summary>
    public class ObjectConverter
    {
        private static readonly ObjectConverter Instance = new ObjectConverter();

        private readonly Dictionary<TypePair, Func<TypePair, object, object>> converterCache = new Dictionary<TypePair, Func<TypePair, object, object>>();

        private IList<IConverterFactory> factories;

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
        public ObjectConverter()
        {
            ResetFactories();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="list"></param>
        public void SetFactories(IList<IConverterFactory> list)
        {
            factories = list;
            lock (converterCache)
            {
                converterCache.Clear();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void ResetFactories()
        {
            factories = DefaultObjectFactories.Create();
            lock (converterCache)
            {
                converterCache.Clear();
            }
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
                if (value == null)
                {
                    return targetType.GetDefaultValue();
                }

                var sourceType = value.GetType();
                if (sourceType == (targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType))
                {
                    return value;
                }

                var typePair = new TypePair(sourceType, targetType);
                Func<TypePair, object, object> converter;
                lock (converterCache)
                {
                    if (!converterCache.TryGetValue(typePair, out converter))
                    {
                        converter = factories.Select(_ => _.GetConverter(typePair)).FirstOrDefault(_ => _ != null);
                        converterCache[typePair] = converter;
                    }
                }
                if (converter == null)
                {
                    throw new ObjectConverterException(String.Format(CultureInfo.InvariantCulture, "Type {0} can't convert to {1}", value.GetType().ToString(), targetType));
                }

                return converter(typePair, value);
            }
            catch (Exception ex)
            {
                throw new ObjectConverterException("Unknown exception.", ex);
            }
        }
    }
}
