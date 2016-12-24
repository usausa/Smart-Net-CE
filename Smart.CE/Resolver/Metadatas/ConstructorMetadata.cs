namespace Smart.Resolver.Metadatas
{
    using System.Reflection;

    using Smart.Reflection;
    using Smart.Resolver.Constraints;

    /// <summary>
    ///
    /// </summary>
    public class ConstructorMetadata
    {
        private IActivator activator;

        public ConstructorInfo Constructor { get; private set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Performance")]
        public ParameterMetadata[] Parameters { get; private set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Performance")]
        public IConstraint[] Constraints { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public IActivator Activator
        {
            get
            {
                return activator ?? (activator = Constructor.ToActivator());
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="constructor"></param>
        /// <param name="parameters"></param>
        /// <param name="constraints"></param>
        public ConstructorMetadata(ConstructorInfo constructor, ParameterMetadata[] parameters, IConstraint[] constraints)
        {
            Constructor = constructor;
            Parameters = parameters;
            Constraints = constraints;
        }
    }
}
