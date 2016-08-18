namespace Smart.Converter.Converters
{
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    public static class DefaultObjectFactories
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static IList<IConverterFactory> Create()
        {
            return new List<IConverterFactory>
            {
                new ConvertConverterFactory(),              // System.Converter
                new EnumConverterFactory(),                 // Enum to Enum, String to Enum, Assignable to Enum, Enum to Assignable
                new ConversionOperatorConverterFactory(),   // Implicit/Explicit operator
                new AssignableConverterFactory(),           // IsAssignableFrom
                new ToStringConverterFactory(),             // ToString finally
            };
        }
    }
}
