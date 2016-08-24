namespace Smart.Converter
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public interface IConverterFactory
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="typePair"></param>
        /// <returns></returns>
        Func<TypePair, object, object> GetConverter(TypePair typePair);
    }
}
