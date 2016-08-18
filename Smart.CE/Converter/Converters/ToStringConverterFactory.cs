namespace Smart.Converter.Converters
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public class ToStringConverterFactory : IConverterFactory
    {
        private static readonly Type StringType = typeof(string);

        private static readonly Func<TypePair, object, object> Converter = (typePair, source) => source.ToString();

        /// <summary>
        ///
        /// </summary>
        /// <param name="typePair"></param>
        /// <returns></returns>
        public Func<TypePair, object, object> GetConverter(TypePair typePair)
        {
            return typePair.TargetType == StringType ? Converter : null;
        }
    }
}
