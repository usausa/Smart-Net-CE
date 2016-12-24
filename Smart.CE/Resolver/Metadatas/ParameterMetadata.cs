namespace Smart.Resolver.Metadatas
{
    using System;
    using System.Reflection;

    /// <summary>
    ///
    /// </summary>
    public class ParameterMetadata
    {
        public ParameterInfo Parameter { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public Type ElementType { get; private set; }

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
