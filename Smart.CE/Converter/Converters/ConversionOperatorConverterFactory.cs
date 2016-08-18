namespace Smart.Converter.Converters
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    ///
    /// </summary>
    public class ConversionOperatorConverterFactory : IConverterFactory
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="typePair"></param>
        /// <returns></returns>
        public Func<TypePair, object, object> GetConverter(TypePair typePair)
        {
            var methodInfo = GetImplicitConversionOperator(typePair);
            if (methodInfo != null)
            {
                return (tp, s) => methodInfo.Invoke(null, new[] { s });
            }

            methodInfo = GetExplicitConversionOperator(typePair);
            if (methodInfo != null)
            {
                return (tp, s) => methodInfo.Invoke(null, new[] { s });
            }

            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="typePair"></param>
        /// <returns></returns>
        private static MethodInfo GetImplicitConversionOperator(TypePair typePair)
        {
            var targetType = typePair.TargetType.IsNullableType() ? Nullable.GetUnderlyingType(typePair.TargetType) : typePair.TargetType;

            var sourceTypeMethod = typePair.SourceType
                .GetMethods(BindingFlags.Static)
                .FirstOrDefault(mi => mi.Name == "op_Implicit" && mi.ReturnType == targetType);
            return sourceTypeMethod ?? targetType
                .GetMethods(BindingFlags.Static)
                .FirstOrDefault(mi => mi.Name == "op_Implicit" && mi.GetParameters().Length == 1 && mi.GetParameters()[0].ParameterType == typePair.SourceType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="typePair"></param>
        /// <returns></returns>
        private static MethodInfo GetExplicitConversionOperator(TypePair typePair)
        {
            var targetType = typePair.TargetType.IsNullableType() ? Nullable.GetUnderlyingType(typePair.TargetType) : typePair.TargetType;

            var sourceTypeMethod = typePair.SourceType
                .GetMethods(BindingFlags.Static)
                .FirstOrDefault(mi => mi.Name == "op_Explicit" && mi.ReturnType == targetType);
            return sourceTypeMethod ?? targetType
                .GetMethods(BindingFlags.Static)
                .FirstOrDefault(mi => mi.Name == "op_Explicit" && mi.GetParameters().Length == 1 && mi.GetParameters()[0].ParameterType == typePair.SourceType);
        }
    }
}
