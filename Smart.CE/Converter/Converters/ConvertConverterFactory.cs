namespace Smart.Converter.Converters
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public class ConvertConverterFactory : IConverterFactory
    {
        private static readonly Type ConvertibleType = typeof(IConvertible);

        /// <summary>
        ///
        /// </summary>
        /// <param name="typePair"></param>
        /// <returns></returns>
        public Func<TypePair, object, object> GetConverter(TypePair typePair)
        {
            var targetType = typePair.TargetType.IsNullableType() ? Nullable.GetUnderlyingType(typePair.TargetType) : typePair.TargetType;
            if (ConvertibleType.IsAssignableFrom(targetType))
            {
                return (tp, s) => System.Convert.ChangeType(s, targetType, null);
            }

            return null;
        }
    }
}
