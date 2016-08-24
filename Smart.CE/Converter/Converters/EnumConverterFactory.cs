namespace Smart.Converter.Converters
{
    using System;
    using System.Globalization;

    /// <summary>
    ///
    /// </summary>
    public class EnumConverterFactory : IConverterFactory
    {
        private static readonly Type StringType = typeof(string);

        /// <summary>
        ///
        /// </summary>
        /// <param name="typePair"></param>
        /// <returns></returns>
        public Func<TypePair, object, object> GetConverter(TypePair typePair)
        {
            var sourceEnumType = typePair.SourceType.GetEnumType();
            var targetEnumType = typePair.TargetType.GetEnumType();

            if ((sourceEnumType != null) && (targetEnumType != null))
            {
                // Enum to Enum
                return (tp, s) => Enum.ToObject(targetEnumType, s);
            }

            if (targetEnumType != null)
            {
                // !Enum to Enum

                // String to Enum
                if (typePair.SourceType == StringType)
                {
                    return (tp, s) => Enum.Parse(targetEnumType, (string)s, true);
                }

                // Assignable
                if (typePair.SourceType.IsAssignableFrom(Enum.GetUnderlyingType(targetEnumType)))
                {
                    return (tp, s) => Enum.ToObject(targetEnumType, s);
                }

                return null;
            }

            if (sourceEnumType != null)
            {
                // Enum to !Enum

                // Aasignable
                if (typePair.TargetType.IsAssignableFrom(Enum.GetUnderlyingType(sourceEnumType)))
                {
                    var targetType = typePair.TargetType.IsNullableType() ? Nullable.GetUnderlyingType(typePair.TargetType) : typePair.TargetType;
                    return (tp, s) => Convert.ChangeType(s, targetType, CultureInfo.CurrentCulture);
                }

                return null;
            }

            return null;
        }
    }
}
