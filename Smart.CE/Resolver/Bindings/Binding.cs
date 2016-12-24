namespace Smart.Resolver.Bindings
{
    using System;

    using Smart.Resolver.Parameters;
    using Smart.Resolver.Providers;
    using Smart.Resolver.Scopes;

    /// <summary>
    ///
    /// </summary>
    public class Binding : IBinding
    {
        private static readonly IBindingMetadata EmptyBindingMetadata = new BindingMetadata();

        /// <summary>
        ///
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public IProvider Provider { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public IScope Scope { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public IBindingMetadata Metadata { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public ParameterMap ConstructorArguments { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public ParameterMap PropertyValues { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        public Binding(Type type)
            : this(type, null, null, null, null, null)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <param name="provider"></param>
        public Binding(Type type, IProvider provider)
            : this(type, provider, null, null, null, null)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <param name="provider"></param>
        /// <param name="scope"></param>
        /// <param name="metadata"></param>
        /// <param name="constructorArguments"></param>
        /// <param name="propertyValues"></param>
        public Binding(Type type, IProvider provider, IScope scope, IBindingMetadata metadata, ParameterMap constructorArguments, ParameterMap propertyValues)
        {
            Type = type;
            Provider = provider;
            Scope = scope;
            Metadata = metadata ?? EmptyBindingMetadata;
            ConstructorArguments = constructorArguments ?? new ParameterMap(null);
            PropertyValues = propertyValues ?? new ParameterMap(null);
        }
    }
}
