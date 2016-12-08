namespace Smart.Resolver.Metadatas
{
    using System;
    using System.Reflection;

    /// <summary>
    ///
    /// </summary>
    public class ParameterMetadata
    {
        /// <summary>
        ///
        /// </summary>
        public ParameterInfo Parameter { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Type ElementType { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="elementType"></param>
        public ParameterMetadata(ParameterInfo parameter, Type elementType)
        {
            Parameter = parameter;
            ElementType = elementType;
        }
    }
}
